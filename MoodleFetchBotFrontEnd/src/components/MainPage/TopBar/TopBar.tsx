import { Avatar, IconButton, Tooltip } from '@mui/material';
import React, { FC, useEffect, useState } from 'react';
import styles from './TopBar.module.scss';
import LogoutIcon from '@mui/icons-material/Logout';
import SettingsIcon from '@mui/icons-material/Settings';

interface TopBarProps {}

const TopBar: FC<TopBarProps> = () => {

  const [username, setUsername] = useState("");
  const [avatar, setAvatar] = useState("");

  function downloadDiscordData(){
    const requestOptions = {
      method: 'GET',
      headers: { 
        'Content-Type': 'application/json', 
        'Accept': 'application/json',
        'Authorization': `Bearer ${localStorage.getItem('userToken')}`
      },
    };
    fetch(`https://discord.com/api/users/@me`, requestOptions)
        .then(response => {
          if(response.ok) {
            return response.json()
          } else if (response.status === 401){
            window.location.href = "/login";
          } else {
            throw new Error('Something is wrong in our end. We are sorry!');
          }
        })
        .then(data => {
          setUsername(data.username + "#" + data.discriminator);
          setAvatar(`https://cdn.discordapp.com/avatars/${data.id}/${data.avatar}`);
        })
        .catch((error) => {
          console.error(error);
        });
  }

  function Logout(){
    localStorage.removeItem('userToken');
    window.location.href = "/login";
  }

  useEffect(() => {
    downloadDiscordData();
  },[])
  
  return (    
    <div className={styles.NavBar}>
      <div style={{display: "flex", alignItems: 'center'}}>
        <Avatar
          alt={username + " avatar"}
          src={avatar}
          style={{margin: '10px', border: '3px solid #ff5e00'}}
          sx={{ width: 56, height: 56 }}
        />
        <div style={{margin: '10px', fontWeight: '700', fontSize: '23px', color: 'white'}}>{username}</div>
      </div>
      
      <div style={{display: "flex", alignItems: 'center'}}>
        {/* <Tooltip title="Settings"> */}
          <IconButton disabled aria-label="settings-button" size="large">
            <SettingsIcon />
          </IconButton>
        {/* </Tooltip> */}
        <Tooltip title="Logout">       
          <IconButton onClick={Logout} aria-label="logout-button" size="large">
            <LogoutIcon />
          </IconButton>
        </Tooltip>
      </div>
    </div>
  )
}

export default TopBar;
