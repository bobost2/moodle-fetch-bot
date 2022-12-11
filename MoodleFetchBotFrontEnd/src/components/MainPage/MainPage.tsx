import React, { FC, useEffect, useState } from 'react';
import ServerPicker from '../ServerPicker/ServerPicker';
import TopBar from '../TopBar/TopBar';
import styles from './MainPage.module.scss';

interface MainPageProps {}

const MainPage: FC<MainPageProps> = () => {
  const [title, setTitle] = useState("Your servers:");

  return (  
    <div>
      <TopBar/>
      <div className={styles.MainPage}>
        <div className={styles.ContentBox}>
          <div className={styles.ContentBox_title}>
            {title}
          </div>
          <ServerPicker/>
        </div>
      </div>
    </div>
  );
}

export default MainPage;
