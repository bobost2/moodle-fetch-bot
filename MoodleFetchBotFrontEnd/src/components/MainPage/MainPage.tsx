import { Button } from '@mui/material';
import React, { FC, useEffect, useState } from 'react';
import TopBar from '../TopBar/TopBar';
import styles from './MainPage.module.scss';

interface MainPageProps {}

const MainPage: FC<MainPageProps> = () => {
  const redirect = encodeURIComponent(`${process.env.REACT_APP_RedirectURL}`);
  var generateBotLink = `https://discord.com/api/oauth2/authorize?client_id=${process.env.REACT_APP_DiscordAppID}&permissions=8&redirect_uri=${redirect}&response_type=code&scope=bot%20identify`;
    
  return (  
    <div>
      <TopBar/>
      <div className={styles.MainPage}>
        <div className={styles.ContentBox}>
          <div className={styles.ContentBox_title}>
            Your servers:
          </div>
          <div>

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
