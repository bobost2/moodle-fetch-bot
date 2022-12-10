import { Button } from '@mui/material';
import React, { FC, useEffect, useState } from 'react';
import { DiscordServer } from '../../interfaces/DiscordServer';
import ServerItem from '../ServerItem/ServerItem';
import TopBar from '../TopBar/TopBar';
import styles from './MainPage.module.scss';

interface MainPageProps {}

const MainPage: FC<MainPageProps> = () => {
  const redirect = encodeURIComponent(`${process.env.REACT_APP_RedirectURL}`);
  const [serverList, setServerList] = useState<DiscordServer[]>([]);

  var generateBotLink = `https://discord.com/api/oauth2/authorize?client_id=${process.env.REACT_APP_DiscordAppID}&permissions=8&redirect_uri=${redirect}&response_type=code&scope=bot%20identify`;
  
  function fetchServers(){

    let userToken = localStorage.getItem('userToken');
    const requestOptions = {
      method: 'POST',
      headers: { 
        'Content-Type': 'application/json', 
        'Accept': 'application/json',
      },
      body: JSON.stringify({ userToken: userToken })
    };
    fetch(`${process.env.REACT_APP_APIAdress}FetchBotServersForUser`, requestOptions)
      .then(response => {
        if(response.ok) {
          return response.json()
        } else {
          throw new Error('Something is wrong in our end. We are sorry!');
        }
      })
      .then(data => {
        setServerList(data.guilds);
      })
      .catch((error) => {
        console.error(error);
      });
  }

  useEffect(() => {
    fetchServers();
  },[]);

  return (  
    <div>
      <TopBar/>
      <div className={styles.MainPage}>
        <div className={styles.ContentBox}>
          <div className={styles.ContentBox_title}>
            Your servers:
          </div>
          <div className={`${styles.ServerList} ${styles.ServerListRes1}`}>
            {serverList.map((server, i) => {
                return (<ServerItem key={i} DiscordServer={server}/>);
            })}
          </div>
          <div className={styles.ContentBox_bottom}>
            <div style={{marginBottom: '8px'}}>Not seeing your server?</div>
            <Button onClick={() => window.location.href = generateBotLink} variant="contained" style={{background: '#005ebf', color: 'white'}}>
              Invite bot
            </Button>
          </div>
        </div>
      </div>
    </div>
  );
}

export default MainPage;
