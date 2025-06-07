import React from 'react';
import '../css/components/screen_loader.css';

interface FullScreenLoaderProps {
  show: boolean;
}

const FullScreenLoader: React.FC<FullScreenLoaderProps> = ({ show }) => {
  if (!show) return null;

  return (
    <div className="fullscreen-loader-overlay">
      <div className="fullscreen-loader-spinner" />
    </div>
  );
};

export default FullScreenLoader;