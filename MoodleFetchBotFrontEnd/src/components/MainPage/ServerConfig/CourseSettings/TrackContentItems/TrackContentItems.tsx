import { Checkbox, FormControlLabel, FormGroup } from '@mui/material';
import React, { FC, useEffect, useState } from 'react';
import { MoodleModule } from '../../../../../interfaces/MoodleModule';
import styles from './TrackContentItems.module.scss';

interface TrackContentItemsProps {}

const TrackContentItems: FC<TrackContentItemsProps> = () => {

  const [modules, setModules] = useState<MoodleModule[]>([]);

  const handleModuleListChange = (event: React.ChangeEvent<HTMLInputElement>, id:number) => {
    let moodleModule = [...modules];
    moodleModule[id].selected = event.target.checked;
    setModules(moodleModule);
  };

  function fetchModules(){
    let userToken = localStorage.getItem('userToken');
    const requestOptions = {
      method: 'POST',
      headers: { 
        'Content-Type': 'application/json', 
        'Accept': 'application/json',
      },
      body: JSON.stringify({ userToken: userToken, guildId: 'none', courseId: 36786 })
    };
    fetch(`${process.env.REACT_APP_APIAdress}FetchMoodleCourseModules`, requestOptions)
      .then(response => {
        if(response.ok) {
          return response.json()
        } else {
          throw new Error('Something is wrong in our end. We are sorry!');
        }
      })
      .then(data => {
        setModules(data.modules);
      })
      .catch((error) => {
        console.error(error);
      });
  }

  useEffect(() => {
    fetchModules();
  }, []);
  
  return (
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
            {modules.map((module, index) =>
              <FormControlLabel key={index} control={<Checkbox checked={modules[module.id].selected} onChange={x => handleModuleListChange(x, module.id)}/>} label={module.modplural}/>
            )}
            <FormControlLabel control={<Checkbox defaultChecked />} label="Other" />
          </FormGroup>
        </div>   
    </div>
  );
}

export default TrackContentItems;
