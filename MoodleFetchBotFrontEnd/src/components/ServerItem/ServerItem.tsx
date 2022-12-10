import { Avatar } from '@mui/material';
import React, { FC } from 'react';
import { DiscordServer } from '../../interfaces/DiscordServer';
import styles from './ServerItem.module.scss';

interface ServerItemProps {
  DiscordServer: DiscordServer;
}

const ServerItem: FC<ServerItemProps> = (props) => (
  <div className={styles.ServerItem}>
    <Avatar
      alt={props.DiscordServer.name + " server icon"}
      src={props.DiscordServer.iconUrl}
      style={{margin: '10px', border: '3px solid #919191'}}
      sx={{ width: 56, height: 56 }}
    >{props.DiscordServer.name.split(' ')[0][0]}</Avatar>
    <div className={styles.ServerInnerBox}>
      <div className={styles.ServerTitle}>
        {props.DiscordServer.name}  
      </div>
      <div>
        {props.DiscordServer.configured ? 
          <div className={styles.ConfigText} style={{color: '#25bb3e'}}>Active</div> 
        : 
          <div className={styles.ConfigText} style={{color: '#ff4747'}}>Not configured!</div>
        }
      </div>  
    </div>
    
  </div>
);

export default ServerItem;
