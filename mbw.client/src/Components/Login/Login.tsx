import { useState, type ChangeEvent, type FormEvent } from 'react';
import '../Login/Login.css';
import type { AppMessage } from '../../Pages/LoginAndRegPage/LoginAndRegPage';
type SubmitMode = 'login' | 'register';

interface AuthFormProps {
    setMessage: (message: AppMessage) => void;
}

interface AuthFormData {
    email: string;
    password: string;
}

interface AuthErrors {
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

function AuthForm({ setMessage }: AuthFormProps) {
    const [formData, setFormData] = useState<AuthFormData>({ email: '', password: '' });
    const [errors, setErrors] = useState<AuthErrors>({});
    const [isSubmitting, setIsSubmitting] = useState(false);
    const [submitMode, setSubmitMode] = useState<SubmitMode>('login');

    const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setFormData(prev => ({ ...prev, [name]: value }));

        if (errors[name as keyof AuthErrors]) {
            setErrors(prev => {
                const next = { ...prev };
                delete next[name as keyof AuthErrors];
                return next;
            });
        }
    };

    const validate = (): AuthErrors => {
        const newErrors: AuthErrors = {};
        if (!formData.email.trim()) newErrors.email = 'Email is mandatory';
        if (!formData.password) newErrors.password = 'Password is mandatory';
        return newErrors;
    };

    const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        setMessage(null);

        const newErrors = validate();
        if (Object.keys(newErrors).length) {
            setErrors(newErrors);
            return;
        }

        const endpoint =
            submitMode === 'login'
                ? 'http://localhost:3000/api/login'
                : 'http://localhost:3000/api/register';

        setIsSubmitting(true);
        try {
            const response = await fetch(endpoint, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(formData),
            });

            const result = await safeJson(response);

            if (response.ok) {
                setMessage({
                    type: 'success',
                    text:
                        submitMode === 'login'
                            ? `${result?.name ?? 'User'}, you are logged in successfully!`
                            : `${formData.email} has been successfully registered!`,
                });

                setFormData({ email: '', password: '' });
                setErrors({});
            } else {
                setMessage({
                    type: 'error',
                    text:
                        result?.message ||
                        (submitMode === 'login'
                            ? 'Email or password is not correct'
                            : 'Registration failed'),
                });
            }
        } catch (err) {
            console.error('Auth error:', err);
            setMessage({ type: 'error', text: 'Error connecting to server. Please try again.' });
        } finally {
            setIsSubmitting(false);
        }
    };

    return (
        <>
            <h2 className="auth-title">Login / Register</h2>

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

                {/* Two submit buttons */}
                <div className="auth-toggle" style={{ marginBottom: 0 }}>
                    <button
                        type="submit"
                        className="auth-submit-btn"
                        disabled={isSubmitting}
                        onClick={() => setSubmitMode('login')}
                    >
                        {isSubmitting && submitMode === 'login' ? 'Logging in…' : 'Login'}
                    </button>

                    <button
                        type="submit"
                        className="auth-submit-btn"
                        disabled={isSubmitting}
                        onClick={() => setSubmitMode('register')}
                    >
                        {isSubmitting && submitMode === 'register' ? 'Registering…' : 'Register'}
                    </button>
                </div>
            </form>
        </>
    );
}

export default AuthForm;
