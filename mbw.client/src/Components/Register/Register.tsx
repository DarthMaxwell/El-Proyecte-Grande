import { useState, type ChangeEvent, type FormEvent } from 'react';
import '../../Components/Login/Login.css';

interface RegisterProps {
    setMessage: (message: { type: 'success' | 'error'; text: string } | null) => void;
}

interface RegisterFormData {
    email: string;
    password: string;
}

interface RegisterErrors {
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

function Register({ setMessage }: RegisterProps) {
    const [formData, setFormData] = useState<RegisterFormData>({ email: '', password: '' });
    const [errors, setErrors] = useState<RegisterErrors>({});
    const [isSubmitting, setIsSubmitting] = useState(false);

    const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setFormData(prev => ({ ...prev, [name]: value }));

        if (errors[name as keyof RegisterErrors]) {
            setErrors(prev => {
                const next = { ...prev };
                delete next[name as keyof RegisterErrors];
                return next;
            });
        }
    };

    const validate = (): RegisterErrors => {
        const newErrors: RegisterErrors = {};
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
            const response = await fetch('http://localhost:3000/api/register', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(formData),
            });

            const result = await safeJson(response);

            if (response.ok) {
                setMessage({
                    type: 'success',
                    text: `${formData.email} has been successfully registered!`,
                });
                setFormData({ email: '', password: '' });
                setErrors({});
            } else {
                setMessage({
                    type: 'error',
                    text: result?.message || 'Registration failed',
                });
            }
        } catch (error) {
            setMessage({ type: 'error', text: 'Error connecting to server. Please try again.' });
            console.error('Registration error:', error);
        } finally {
            setIsSubmitting(false);
        }
    };

    return (
        <>
            <h2 className="auth-title">Registration Form</h2>

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
                        autoComplete="new-password"
                    />
                    {errors.password && <span className="auth-error">{errors.password}</span>}
                </div>

                <button type="submit" className="auth-submit-btn" disabled={isSubmitting}>
                    {isSubmitting ? 'Registering…' : 'Register'}
                </button>
            </form>
        </>
    );
}

export default Register;
