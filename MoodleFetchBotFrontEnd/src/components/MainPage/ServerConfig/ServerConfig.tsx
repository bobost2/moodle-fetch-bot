import { FormControl, MenuItem, Select, SelectChangeEvent } from '@mui/material';
import React, { FC, useEffect, useState } from 'react';
import { MoodleCourse } from '../../../interfaces/MoodleCourse';
import styles from './ServerConfig.module.scss';
import ServerConfigMenuButton from './ServerConfigMenuButton/ServerConfigMenuButton';

interface ServerConfigProps {
  ServerID: string;
  updateState: Function;
}

const ServerConfig: FC<ServerConfigProps> = (props) => {

  const [courses, setCourses] = useState<MoodleCourse[]>([]);
  const [selectedCourse, setSelectedCourse] = useState('');
  const [activeButtonIndex, setActiveButtonIndex] = useState(4);

  const handleChangeSelectedCourse = (event: SelectChangeEvent) => {
    setSelectedCourse(event.target.value as string);
  };

  function SwitchActiveButton(newActive: number){
    setActiveButtonIndex(newActive);
  }

  function GoBack(){
    props.updateState(0);
  }

  function FetchCourses(){
    let userToken = localStorage.getItem('userToken');
    const requestOptions = {
      method: 'POST',
      headers: { 
        'Content-Type': 'application/json', 
        'Accept': 'application/json',
      },
      body: JSON.stringify({ userToken: userToken, guildId: props.ServerID })
    };
    fetch(`${process.env.REACT_APP_APIAdress}FetchCoursesByGuild`, requestOptions)
      .then(response => {
        if(response.ok) {
          return response.json()
        } else {
          throw new Error('Something is wrong in our end. We are sorry!');
        }
      })
      .then(data => {
        setCourses(data.courses);
        setSelectedCourse(data.courses[0].id);
      })
      .catch((error) => {
        console.error(error);
      });
  }

  useEffect(() => {
    FetchCourses();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [])

  return (
    <div className={styles.ServerConfig}>
      <div className={styles.LeftMenu}>
        <div className={styles.SelectCourse}>
          <div className={styles.SelectCourseText}>Select course:</div>
          <div>
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
                value={selectedCourse}
                onChange={handleChangeSelectedCourse}
              >
                {courses.map((course, i) => (
                  <MenuItem value={course.id} key={i}>
                    {course.fullnamedisplay}
                  </MenuItem>
                ))}
              </Select>
            </FormControl>
          </div>
        </div>
        <div className={styles.ButtonGroup}>
          <div style={{borderBottom: "1px solid black"}}>
            <ServerConfigMenuButton id={1} active={activeButtonIndex === 1} switchActive={SwitchActiveButton} text='Course data' icon={1}/>
            <ServerConfigMenuButton id={2} active={activeButtonIndex === 2} switchActive={SwitchActiveButton} text='Course forums' icon={2}/>
            <ServerConfigMenuButton id={3} active={activeButtonIndex === 3} switchActive={SwitchActiveButton} text='Course assignments' icon={3}/>
          </div>
          <div>
            <ServerConfigMenuButton active={activeButtonIndex === 4} switchActive={SwitchActiveButton} id={4} text='Bot settings' icon={4}/>
            <ServerConfigMenuButton onClick={GoBack} text='Go back' icon={5} borderBottomLeftRound/>
          </div>         
        </div>
      </div>

      <div className={styles.RightMenu}>
        <div style={{fontSize: '50px', marginBottom: '5px'}}>🔒</div>
        <div>Settings are currently disabled.</div>
      </div>
    </div>
  );
}
export default ServerConfig;
