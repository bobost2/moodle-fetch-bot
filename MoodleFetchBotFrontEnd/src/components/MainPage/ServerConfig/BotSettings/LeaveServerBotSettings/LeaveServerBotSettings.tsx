import { Button } from '@mui/material';
import React, { FC } from 'react';
import styles from './LeaveServerBotSettings.module.scss';

interface LeaveServerBotSettingsProps {}

const LeaveServerBotSettings: FC<LeaveServerBotSettingsProps> = () => (
  <div className={styles.SettingBox}>
      <div className={styles.SettingBoxTitle} style={{fontSize: '25px'}}>🚫 Leave server 🚫</div>
      <Button style={{margin: '20px 0px 20px 0px'}} variant="contained" color="error">Are you sure?</Button>
  </div>
);

export default LeaveServerBotSettings;
