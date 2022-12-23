import { Button } from '@mui/material';
import React, { FC } from 'react';
import styles from './AutomateBotSettings.module.scss';

interface AutomateBotSettingsProps {}

const AutomateBotSettings: FC<AutomateBotSettingsProps> = () => (
  <div className={styles.SettingBox}>
      <div className={styles.SettingBoxTitle} style={{fontSize: '25px'}}>⚡ Automatic setup ⚡</div>
      <Button style={{margin: '20px 0px 20px 0px'}} variant="contained" color="error">Click here to proceed</Button>
  </div>
);

export default AutomateBotSettings;
