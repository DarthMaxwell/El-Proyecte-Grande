import { useState, type ChangeEvent, type FormEvent } from 'react';
import './Login.css';

interface LoginProps {
    setMessage: (message: { type: 'success' | 'error'; text: string } | null) => void;
}

interface LoginFormData {
    email: string;
    password: string;
}

interface LoginErrors {
    email?: string;
    password?: string;
}

async function safeJson(response: Response) {
    const contentType = response.headers.get('content-type') || '';
    if (contentType.includes('application/json')) return response.json();

    const text = await response.text();
    try {
        return text ? JSON.parse(text) : {};
    } catch {
        return { message: text };
    }
}

function Login({ setMessage }: LoginProps) {
    const [formData, setFormData] = useState<LoginFormData>({ email: '', password: '' });
    const [errors, setErrors] = useState<LoginErrors>({});
    const [isSubmitting, setIsSubmitting] = useState(false);

    const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;

        setFormData(prev => ({ ...prev, [name]: value }));

        // Clear field error as user types
        if (errors[name as keyof LoginErrors]) {
            setErrors(prev => {
                const next = { ...prev };
                delete next[name as keyof LoginErrors];
                return next;
            });
        }
    };

    const validate = (): LoginErrors => {
        const newErrors: LoginErrors = {};

        if (!formData.email.trim()) newErrors.email = 'Email is mandatory';
        if (!formData.password) newErrors.password = 'Password is mandatory';

        return newErrors;
    };

    const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        setMessage(null);

        const newErrors = validate();
        if (Object.keys(newErrors).length > 0) {
            setErrors(newErrors);
            return;
        }

        setIsSubmitting(true);
        try {
            const response = await fetch('http://localhost:3000/api/login', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(formData),
            });

            const result = await safeJson(response);

            if (response.ok) {
                setMessage({
                    type: 'success',
                    text: `${result?.name ?? 'User'}, you are logged in successfully!`,
                });
                setFormData({ email: '', password: '' });
                setErrors({});
            } else {
                setMessage({
                    type: 'error',
                    text: result?.message || 'Email or password is not correct',
                });
            }
        } catch (error) {
            setMessage({ type: 'error', text: 'Error connecting to server. Please try again.' });
            console.error('Login error:', error);
        } finally {
            setIsSubmitting(false);
        }
    };

    return (
        <>
            <h2 className="auth-title">Login</h2>

            <form className="auth-form" onSubmit={handleSubmit} noValidate>
                <div className="auth-form-group">
                    <input
                        type="email"
                        name="email"
                        value={formData.email}
                        onChange={handleChange}
                        placeholder="Email"
                        className="auth-input"
                        autoComplete="email"
                    />
                    {errors.email && <span className="auth-error">{errors.email}</span>}
                </div>

                <div className="auth-form-group">
                    <input
                        type="password"
                        name="password"
                        value={formData.password}
                        onChange={handleChange}
                        placeholder="Password"
                        className="auth-input"
                        autoComplete="current-password"
                    />
                    {errors.password && <span className="auth-error">{errors.password}</span>}
                </div>

                <button type="submit" className="auth-submit-btn" disabled={isSubmitting}>
                    {isSubmitting ? 'Logging in…' : 'Login'}
                </button>
            </form>
        </>
    );
}

export default Login;
