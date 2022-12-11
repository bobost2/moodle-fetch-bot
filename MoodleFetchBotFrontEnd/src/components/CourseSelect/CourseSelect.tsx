import { Button, CircularProgress } from '@mui/material';
import React, { FC, useEffect, useState } from 'react';
import { MoodleCourse } from '../../interfaces/MoodleCourse';
import CourseItem from '../CourseItem/CourseItem';
import styles from './CourseSelect.module.scss';
import styles2 from '../MainPage/MainPage.module.scss';

interface CourseSelectProps {
  ServerID: string;
  updateState: Function;
}

const CourseSelect: FC<CourseSelectProps> = (props) => {

  const [courses, setCourses] = useState<MoodleCourse[]>([]);
  const [loaded, setLoaded] = useState(false);

  function FetchCourses(){
    let userToken = localStorage.getItem('userToken');
    const requestOptions = {
      method: 'POST',
      headers: { 
        'Content-Type': 'application/json', 
        'Accept': 'application/json',
      },
      body: JSON.stringify({ userToken: userToken })
    };
    fetch(`${process.env.REACT_APP_APIAdress}FetchMoodleCourses`, requestOptions)
      .then(response => {
        if(response.ok) {
          return response.json()
        } else {
          throw new Error('Something is wrong in our end. We are sorry!');
        }
      })
      .then(data => {
        setCourses(data.courses);
        setLoaded(true);
      })
      .catch((error) => {
        console.error(error);
      });
  }

  function GoBack(){
    props.updateState(0);
  }

  useEffect(() => {
    FetchCourses();
  },[]);

  return (
    <div className={styles.CourseSelect}>
      {
        loaded ?
          <div className={`${styles.CourseList} ${styles.CourseListRes1}`}>
            {courses.map((course, i) => {
                return (<CourseItem key={i} MoodleCourse={course} UpdateState={props.updateState}/>);
            })}
          </div>
        :
          <div className={styles.CourseLoading}>
            <CircularProgress style={{width: '50px', height: '50px', color: '#d5d5d5'}}/>
            <div className={styles.CourseLoading_text}>Loading...</div>
          </div>
      }
      <div className={`${styles2.ContentBox_bottom} ${styles.CourseBottomMenu}`} style={{height: '48px'}}>
        <Button variant="contained" color="error" onClick={GoBack}>Go back</Button>
        <div>0 selected</div>
        <Button variant="contained" color="success">Save settings</Button>
      </div>
    </div>
  );
}
export default CourseSelect;
