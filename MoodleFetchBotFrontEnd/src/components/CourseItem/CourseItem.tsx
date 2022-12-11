import { Avatar } from '@mui/material';
import React, { FC } from 'react';
import { MoodleCourse } from '../../interfaces/MoodleCourse';
import styles from './CourseItem.module.scss';

interface CourseItemProps {
  MoodleCourse: MoodleCourse;
  UpdateState: Function;
}

const CourseItem: FC<CourseItemProps> = (props) => {
  function HandleClickServer(){
    
  }

  return (
    <div onClick={HandleClickServer} className={styles.CourseItem}>
      <div className={styles.CourseInnerBox}>
        <div className={styles.CourseTitle}>
          {props.MoodleCourse.fullname}  
        </div>
        <div>
          <a className={styles.ViewCourse} href={props.MoodleCourse.viewurl}>View course in Moodle</a>
        </div>  
      </div>
    </div>
  );
}

export default CourseItem;
