import { Checkbox, FormControlLabel, FormGroup } from '@mui/material';
import React, { FC } from 'react';
import styles from './ForumSelect.module.scss';

interface ForumSelectProps {}

const ForumSelect: FC<ForumSelectProps> = () => (
  <div className={styles.SettingBox}>
      <div className={styles.SettingBoxTitle} style={{fontSize: '25px'}}>🎯 Select forums to track 🎯</div>
      <div className={styles.CenterAndFitCheckboxes}>
        <FormGroup>
          <FormControlLabel control={<Checkbox defaultChecked />} label="Forum" />

          <FormControlLabel control={<Checkbox defaultChecked />} label="New forums" />
        </FormGroup>
      </div>   
  </div>
);

export default ForumSelect;
