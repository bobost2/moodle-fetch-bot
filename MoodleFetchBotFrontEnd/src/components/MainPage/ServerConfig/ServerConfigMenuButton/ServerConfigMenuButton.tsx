import React, { FC, MouseEventHandler } from 'react';
import styles from './ServerConfigMenuButton.module.scss';
import DataThresholdingIcon from '@mui/icons-material/DataThresholding';
import School from '@mui/icons-material/School';
import ForumIcon from '@mui/icons-material/Forum';
import SettingsSuggestIcon from '@mui/icons-material/SettingsSuggest';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';

interface ServerConfigMenuButtonProps {
  id?: number;
  active?: boolean;
  switchActive?: Function;
  text: string;
  icon: number;
  borderBottomLeftRound?: boolean;
  onClick? : MouseEventHandler<HTMLDivElement>;
}

const ServerConfigMenuButton: FC<ServerConfigMenuButtonProps> = (props) => {

  function IconSwitch() {
    switch(props.icon){
      case 1:
        return <DataThresholdingIcon />;
      case 2:
        return <ForumIcon />;
      case 3:
        return <School />;
      case 4:
        return <SettingsSuggestIcon />;
      case 5:
        return <ArrowBackIcon />;
      default:
        return <></>;
    }
  }

  function ClickButton(){
    props.switchActive?.(props.id);
  }

  return (
    <div onClick={props.onClick ?? ClickButton} className={`${styles.ServerConfigMenuButton} ${props.borderBottomLeftRound ? styles.LeftBorderFix : '' } ${props.active ? styles.SelectedBG : ''}`}>
      <IconSwitch/>
      <div>{props.text}</div>
    </div>
  );

}
export default ServerConfigMenuButton;
