import { Avatar } from '@mui/material';
import React, { FC, useState } from 'react';
import { MoodleCourse } from '../../interfaces/MoodleCourse';
import styles from './CourseItem.module.scss';

interface CourseItemProps {
  ElementId: number;
  MoodleCourse: MoodleCourse;
  UpdateState: Function;
  SelectCourse: Function;
}

const CourseItem: FC<CourseItemProps> = (props) => {
  
  const [selected, setSelected] = useState(false);
  
  function HandleClickServer(){
    if(!selected){
      setSelected(true);
      props.SelectCourse(props.ElementId, props.MoodleCourse, true);
    } else {
      setSelected(false);
      props.SelectCourse(props.ElementId, props.MoodleCourse, false)
    }
  }

  return (
    <div onClick={HandleClickServer} className={`${styles.CourseItem} ${selected ? styles.CourseItemEnabled : styles.CourseItemDisabled}`}>
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
