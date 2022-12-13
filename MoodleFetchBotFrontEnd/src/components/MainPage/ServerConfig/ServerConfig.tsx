import React, { FC } from 'react';
import styles from './ServerConfig.module.scss';

interface ServerConfigProps {
  ServerID: string;
  updateState: Function;
}

const ServerConfig: FC<ServerConfigProps> = (props) => {
  return (
    <div className={styles.ServerConfig}>
      ServerConfig Component
    </div>
  );
}
export default ServerConfig;
