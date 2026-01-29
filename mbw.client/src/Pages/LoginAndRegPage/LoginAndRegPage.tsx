import { useState } from 'react';
import Login from '../../Components/Login/Login';
import '../../Components/Login/Login.css';

export type AppMessage = { type: 'success' | 'error'; text: string } | null;

function LoginAndRegPage() {
    const [message, setMessage] = useState<AppMessage>(null);

    return (
        <div className="auth-container">
            {message && <div className={`auth-message ${message.type}`}>{message.text}</div>}

            <Login setMessage={setMessage} />
        </div>
    );
}

export default LoginAndRegPage;




    
