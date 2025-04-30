import React from 'react';
import MachineList from './MachineList';
import './styles.css';

const App: React.FC = () => (
  <div className="app-container">
    <header className="app-header">
      <h1>Cogtive Industrial IoT Platform</h1>
      <p>Monitor your factory equipment in real-time</p>
    </header>
    
    <main className="app-content">
      <MachineList />
    </main>
    
    <footer className="app-footer">
      <p>Cogtive Technical Challenge - Industrial IoT Platform</p>
    </footer>
  </div>
);

export default App;