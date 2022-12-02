import { Button } from '@mui/material';
import React, { FC } from 'react';
import LoginIcon from '@mui/icons-material/Login';
import styles from './LoginComponent.module.scss';

interface LoginComponentProps {}

const LoginComponent: FC<LoginComponentProps> = () => {
  
  function SendUserToAuth(){
    const redirect = encodeURIComponent(`${process.env.REACT_APP_RedirectURL + "auth"}`);
    window.location.href = `https://discord.com/api/oauth2/authorize?client_id=${process.env.REACT_APP_DiscordAppID}&redirect_uri=${redirect}&response_type=code&scope=identify`;
  }

  return (
    <div className={styles.LoginComponent}>
      <div className={styles.LoginBox}>
        <div style={{fontSize: '25px', fontWeight: '600', margin: '10px', color: "white"}}>Moodle notifier Discord bot</div>
        <div style={{height: "3px", background: "black" }}/>
        <Button onClick={SendUserToAuth} variant="contained" startIcon={<LoginIcon />} style={{margin: '10px', background: '#005ebf', color: 'white'}}>
          Login with Discord
        </Button>
      </div>
    </div>
  );
}

export default LoginComponent;
