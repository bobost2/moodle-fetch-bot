import { Fab, Slider } from '@mui/material';
import React, { FC } from 'react';
import styles from './RefreshRateBotSettings.module.scss';
import DoneIcon from '@mui/icons-material/Done';

interface RefreshRateBotSettingsProps {}

const RefreshRateBotSettings: FC<RefreshRateBotSettingsProps> = () => (
  <div className={styles.SettingBox}>
      <div className={styles.SettingBoxTitle} style={{fontSize: '25px'}}>🕑 Adjust bot refresh rate 🕑</div>
      <div className={styles.InsideBox}>
        <div>
          <div className={styles.SliderText}>Enter refresh rate in minutes:</div>
          <Slider
            aria-label="RefreshRateInMinutes"
            defaultValue={30}
            valueLabelDisplay="auto"
            step={10}
            marks
            min={10}
            max={120}
          />
        </div>
        <Fab size="medium" color="success" aria-label="enter">
          <DoneIcon/>
        </Fab>
      </div>
  </div>
);

export default RefreshRateBotSettings;
