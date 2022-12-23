import { Button, FormControl, MenuItem, Select, SelectChangeEvent } from '@mui/material';
import React, { FC, useState } from 'react';
import styles from './NotificationBotSettings.module.scss';

interface NotificationBotSettingsProps {}

const NotificationBotSettings: FC<NotificationBotSettingsProps> = () => {

  const [selectedServer, setSelectedServer] = useState<string>("");

  const handleChangeSelectedServer = (event: SelectChangeEvent) => {
    setSelectedServer(event.target.value as string);
  };

  return (
    <div className={styles.SettingBox}>
      <div className={styles.SettingBoxTitle} style={{fontSize: '25px'}}>Notification settings channel</div>
      <Button style={{margin: '20px 0px 15px 0px'}} variant="contained" color="secondary">Create channel automatically</Button>
      <div className={styles.SettingBoxLine}>
        <div className={styles.SettingBoxSingleLine}/>
        <div className={styles.SettingBoxLineText}>OR</div>
        <div className={styles.SettingBoxSingleLine}/>
      </div>
      <div style={{fontSize: '20px', marginTop: '5px'}}>Select existing channel:</div>
      <FormControl sx={{width: '90%', background: '#00000059'}} style={{marginTop: '10px'}}>
        <Select
          sx={{
            color: "#b5b5b5",
            '.MuiOutlinedInput-notchedOutline': {
              borderColor: 'rgba(255, 255, 255, 0.3)',
            },
            '&.Mui-focused .MuiOutlinedInput-notchedOutline': {
              borderColor: 'rgba(255, 255, 255, 0.5)',
            },
            '&:hover .MuiOutlinedInput-notchedOutline': {
              borderColor: 'rgba(255, 255, 255, 0.4)',
            },
          }}
          displayEmpty
          value={selectedServer}
          onChange={handleChangeSelectedServer}
        >
          {/* {courses.map((course, i) => (
            <MenuItem value={course.id} key={i}>
              {course.fullnamedisplay}
            </MenuItem>
          ))} */}
          <MenuItem value={"TestValue"}>TestValue</MenuItem>
        </Select>
      </FormControl>
    </div>
  );
}

export default NotificationBotSettings;
