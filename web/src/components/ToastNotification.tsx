import React, { useEffect } from 'react';
import '../css/components/toast_notification.css';

interface ToastNotificationProps {
    message: string;
    type?: 'error' | 'success' | 'info' | 'warning';
    onClose: () => void;
    duration?: number; // milliseconds
}

const ToastNotification: React.FC<ToastNotificationProps> = ({
    message,
    type = 'info',
    onClose,
    duration = 4000
}) => {
    useEffect(() => {
        const timer = setTimeout(onClose, duration);
        return () => clearTimeout(timer);
    }, [onClose, duration]);

    const getIcon = () => {
        switch (type) {
            case 'success':
                return '✔️';
            case 'error':
                return '❌';
            case 'info':
                return 'ℹ️';
            case 'warning':
                return '⚠️';
            default:
                return '';
        }
    };

    return (
        <div className={`toast-notification toast-${type}`}>
            <div className="toast-pipe" />
            <div className="toast-message">{message}</div>
            <div className="toast-icon">{getIcon()}</div>
        </div>
    );
};

export default ToastNotification;
