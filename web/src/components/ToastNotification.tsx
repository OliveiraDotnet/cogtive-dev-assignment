import React, { useEffect } from 'react';
import '../css/components/toast_notification.css';

interface ToastNotificationProps {
    message: string;
    type?: 'error' | 'success' | 'info' | 'warning';
    onClose: () => void;
    duration?: number; //milliseconds
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

    return (
        <div className={`toast-notification toast-${type}`}>
            {message}
        </div>
    );
};

export default ToastNotification;
