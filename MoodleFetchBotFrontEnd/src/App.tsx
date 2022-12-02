import { createTheme, ThemeProvider } from '@mui/material';
import { Route, Routes, BrowserRouter } from 'react-router-dom';
import React from 'react';
import './App.css';
import LoginComponent from './components/LoginComponent/LoginComponent';
import AuthUserComponent from './components/AuthUserComponent/AuthUserComponent';

function App() {

  const darkTheme = createTheme({
    palette: {
      mode: 'dark',
    },
  });

  return (
    <div className="App">
      <ThemeProvider theme={darkTheme}>
        <BrowserRouter>
          <Routes>
            <Route path="/" element={<LoginComponent />} />
            <Route path="/auth" element={<AuthUserComponent />} />
          </Routes>
        </BrowserRouter>
      </ThemeProvider>
    </div>
  );
}

export default App;
