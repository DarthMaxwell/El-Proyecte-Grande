import { useState } from 'react';
import Login from '../../Components/Login/Login';
import Register from '../../Components/Register/Register';
import '../../Components/Login/Login.css';

type AppMessage = { type: 'success' | 'error'; text: string } | null;

function LoginAndRegPage() {
    const [isLogin, setIsLogin] = useState(true);
    const [message, setMessage] = useState<AppMessage>(null);

    const switchMode = (loginMode: boolean) => {
        setIsLogin(loginMode);
        setMessage(null);
    };

    return (
        <div className="auth-container">
            <div className="auth-toggle">
                <button
                    onClick={() => switchMode(true)}
                    className={`auth-toggle-btn ${isLogin ? 'active' : 'inactive'}`}
                    type="button"
                >
                    Login
                </button>

                <button
                    onClick={() => switchMode(false)}
                    className={`auth-toggle-btn ${!isLogin ? 'active' : 'inactive'}`}
                    type="button"
                >
                    Register
                </button>
            </div>

            {message && (
                <div className={`auth-message ${message.type}`}>
                    {message.text}
                </div>
            )}

            {isLogin ? <Login setMessage={setMessage} /> : <Register setMessage={setMessage} />}
        </div>
    );
}

export default LoginAndRegPage;


    
