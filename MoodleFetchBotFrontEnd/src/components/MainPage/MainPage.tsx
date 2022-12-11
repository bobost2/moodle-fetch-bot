import React, { FC, useState } from 'react';
import CourseSelect from '../CourseSelect/CourseSelect';
import ServerPicker from '../ServerPicker/ServerPicker';
import TopBar from '../TopBar/TopBar';
import styles from './MainPage.module.scss';

interface MainPageProps {}

const MainPage: FC<MainPageProps> = () => {
  const [title, setTitle] = useState("Your servers:");
  const [mainWindow, setMainWindow] = useState<JSX.Element>(<ServerPicker updateState={UpdateAppState}/>);

  function UpdateAppState(state:number, serverId?:string) {
    switch (state){
      case 0:
        setTitle("Your servers:");
        setMainWindow(<ServerPicker updateState={UpdateAppState}/>);
        break;
      case 1:
        setTitle("Select courses:");
        setMainWindow(<CourseSelect updateState={UpdateAppState} ServerID={serverId ?? ''}/>);
        break;
    }
  }

  return (  
    <div>
      <TopBar/>
      <div className={styles.MainPage}>
        <div className={styles.ContentBox}>
          <div className={styles.ContentBox_title}>
            {title}
          </div>
          {mainWindow}
        </div>
      </div>
    </div>
  );
}

export default MainPage;
