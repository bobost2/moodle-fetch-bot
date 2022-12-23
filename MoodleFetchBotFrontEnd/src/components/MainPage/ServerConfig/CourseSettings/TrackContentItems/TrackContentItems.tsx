import { Checkbox, FormControlLabel, FormGroup } from '@mui/material';
import React, { FC } from 'react';
import styles from './TrackContentItems.module.scss';

interface TrackContentItemsProps {}

const TrackContentItems: FC<TrackContentItemsProps> = () => (
  <div className={styles.SettingBox}>
      <div className={styles.SettingBoxTitle} style={{fontSize: '25px'}}>🎯 Track contents 🎯</div>
      <div className={styles.CenterContents}>
        <FormGroup>
          <FormControlLabel control={<Checkbox defaultChecked />} label="Track course contents" />
        </FormGroup>
      </div>   
      <div className={styles.SettingBoxLine}>
        <div className={styles.SettingBoxSingleLine}/>
        <div className={styles.SettingBoxLineText}>SELECT MODULES TO TRACK:</div>
        <div className={styles.SettingBoxSingleLine}/>
      </div>
      <div className={styles.CenterAndFitCheckboxes}>
        <FormGroup>
          <FormControlLabel control={<Checkbox defaultChecked />} label="URLs" />
          <FormControlLabel control={<Checkbox defaultChecked />} label="Resource" />
          <FormControlLabel control={<Checkbox defaultChecked />} label="Folder" />
          <FormControlLabel control={<Checkbox defaultChecked />} label="Label" />
          <FormControlLabel control={<Checkbox defaultChecked />} label="Page" />

          <FormControlLabel control={<Checkbox defaultChecked />} label="Other" />
        </FormGroup>
      </div>   
  </div>
);

export default TrackContentItems;
