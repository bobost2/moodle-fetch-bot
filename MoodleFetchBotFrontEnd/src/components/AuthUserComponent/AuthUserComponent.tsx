import { Button, CircularProgress, TextField } from '@mui/material';
import React, { FC, useEffect, useState } from 'react';
import styles from './AuthUserComponent.module.scss';

interface AuthUserComponentProps {}

const AuthUserComponent: FC<AuthUserComponentProps> = () => {

  let userToken:string = "";
  const [enableAccountWin, setEnableAccountWin] = useState(false);
  const [verificationFailed, setVerificationFailed] = useState(false);
 
  const [website, setWebsite] = React.useState("");
  const handleWebsiteChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setWebsite(event.target.value);
  };

  const [username, setUsername] = React.useState("");
  const handleUsernameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setUsername(event.target.value);
  };
  
  const [password, setPassword] = React.useState("");
  const handlePasswordChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setPassword(event.target.value);
  };

  const AuthenticateUser = () => {
    var url_string = window.location.href; 
    var url = new URL(url_string);
    var code = url.searchParams.get("code");
    if(code != null)
    {
      const requestOptions = {
        method: 'POST',
        headers: { 
          'Content-Type': 'application/json', 
          'Accept': 'application/json',
        },
        body: JSON.stringify({ code: code })
      };
      fetch(`${process.env.REACT_APP_APIAdress}AuthenticateDiscord`, requestOptions)
          .then(response => {
            if(response.ok) {
              return response.json()
            } else {
              throw new Error('Something is wrong in our end. We are sorry!');
            }
          })
          .then(data => {
            userToken = data.userToken;
            //console.log(userToken)
            localStorage.setItem('userToken', userToken);
            
            const requestOptions = {
              method: 'POST',
              headers: { 
                'Content-Type': 'application/json', 
                'Accept': 'application/json',
              },
              body: JSON.stringify({ userToken: userToken })
            };
            fetch(`${process.env.REACT_APP_APIAdress}CheckUserMoodleToken`, requestOptions)
                .then(response => {
                  if(response.ok) {
                    return response.json()
                  } else {
                    throw new Error('Something is wrong in our end. We are sorry!');
                  }
                })
                .then(data => {
                  if(data.hasBeenRegistered){
                    console.log("Account exists.");
                  } else {
                    setEnableAccountWin(true);
                  }
                })
                .catch((error) => {
                  console.error(error);
                });
          })
          .catch((error) => {
            if(userToken === "") {
              console.error(error);
              window.location.href = "/";
            }
          });
    }
    else
    {
      if(url.searchParams.get("error") === "access_denied")
      {
        // var errorDescription = url.searchParams.get("error_description") || "";
        // console.error(errorDescription);
        window.location.href = "/";
      }
      else
      {
        window.location.href = "/";
      }  
    }
  }

  function TryLinkAccount(){
    setEnableAccountWin(false);

    const requestOptions = {
      method: 'POST',
      headers: { 
        'Content-Type': 'application/json', 
        'Accept': 'application/json',
      },
      body: JSON.stringify({ userToken: localStorage.getItem("userToken"), username: username, password: password, website: website }),
    };
    fetch(`${process.env.REACT_APP_APIAdress}LinkMoodleAccount`, requestOptions)
        .then(response => {
          if(response.ok) {
            return response.json()
          } else {
            throw new Error('Something is wrong in our end. We are sorry!');
          }
        })
        .then(data => {
          if(data.verificationPassed){
            console.log("Account created!");
          } else {
            setEnableAccountWin(true);
            setVerificationFailed(true);
          }
        })
        .catch((error) => {
          console.error(error);
        });
  }

  useEffect(() => {
    AuthenticateUser();
  }, [])

  return (
    <div className={styles.AuthUserComponent}>
      <div className={styles.LoginBox}>
        <div style={{fontSize: '25px', fontWeight: '600', margin: '10px', color: "white"}}>Moodle notifier Discord bot</div>
        <div style={{height: "3px", background: "black" }}/>
        {
          enableAccountWin ? 
          <div style={{display: 'flex', flexDirection: 'column'}}>
            <div style={{color: 'white', fontWeight: '500', fontSize: '16px', margin: '2px 0px 15px'}}>Please enter Moodle credentials:</div>
            <TextField value={website} onChange={handleWebsiteChange} label="Website" variant="outlined" style={{marginBottom: '15px', marginLeft: '4px', marginRight: '4px'}} placeholder="https://example.com/"/>
            <TextField value={username} onChange={handleUsernameChange} label="Username" variant="outlined" style={{marginBottom: '15px', marginLeft: '4px', marginRight: '4px'}}/>
            <TextField value={password} onChange={handlePasswordChange} label="Password" type="password" variant="outlined" style={{marginBottom: '15px', marginLeft: '4px', marginRight: '4px'}} />
            <div style={{color: '#ff6f6f', fontWeight: '500', fontSize: '16px', margin: '-10px 0px 5px', display: verificationFailed ? 'block' : 'none'}}>Account verification failed.</div>
            <Button onClick={TryLinkAccount} variant="contained" style={{margin: '10px', background: '#dd7800', color: 'white'}}>
              Authenticate Moodle account
            </Button>
          </div>
          :
          <div style={{padding: '20px'}}>
            <CircularProgress style={{color: "#dd7800"}}/>
            <div style={{color: 'white', fontWeight: '500', fontSize: '20px'}}>Authenticating user...</div>
          </div>
        }     
      </div>
    </div>
  );
}

export default AuthUserComponent;
