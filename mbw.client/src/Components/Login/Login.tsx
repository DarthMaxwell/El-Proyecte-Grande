import { useState, type ChangeEvent, type FormEvent } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import "../Login/Login.css";
import type { AppMessage } from "../../Pages/LoginAndRegPage/LoginAndRegPage";
import { useAuth } from "../../Authenticate/AuthContext";

type SubmitMode = "login" | "register";

interface AuthFormProps {
    setMessage: (message: AppMessage) => void;
}

interface AuthFormData {
    username: string;
    password: string;
}

interface AuthErrors {
    username?: string;
    password?: string;
}

async function safeJson(response: Response) {
    const contentType = response.headers.get("content-type") || "";
    if (contentType.includes("application/json")) return response.json();

    const text = await response.text();
    try {
        return text ? JSON.parse(text) : {};
    } catch {
        return { message: text };
    }
}

function AuthForm({ setMessage }: AuthFormProps) {
    const { login } = useAuth();
    const navigate = useNavigate();
    const location = useLocation();

    const from = (location.state as any)?.from?.pathname || "/";

    const [formData, setFormData] = useState<AuthFormData>({ username: "", password: "" });
    const [errors, setErrors] = useState<AuthErrors>({});
    const [isSubmitting, setIsSubmitting] = useState(false);
    const [submitMode, setSubmitMode] = useState<SubmitMode>("login");

    const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setFormData((prev) => ({ ...prev, [name]: value }));

        if (errors[name as keyof AuthErrors]) {
            setErrors((prev) => {
                const next = { ...prev };
                delete next[name as keyof AuthErrors];
                return next;
            });
        }
    };

    const validate = (): AuthErrors => {
        const newErrors: AuthErrors = {};
        if (!formData.username.trim()) newErrors.username = "Username is mandatory";
        if (!formData.password) newErrors.password = "Password is mandatory";
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
            submitMode === "login"
                ? "http://localhost:5132/api/Auth/login"
                : "http://localhost:5132/api/Auth/register";

        setIsSubmitting(true);
        try {
            const response = await fetch(endpoint, {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    username: formData.username,
                    password: formData.password,
                }),
            });

            const result = await safeJson(response);

            if (response.ok) {
                if (submitMode === "login") {
                    const username = result.username ?? result.Username ?? formData.username;
                    const token = result.token ?? result.Token;
                    const role = result.role ?? result.Role;
                    const id = result.id ?? result.Id ?? result.userId ?? result.UserId;

                    if (token) {
                        login({ username, token, role, id: typeof id === "number" ? id : undefined });
                    }

                    setMessage({ type: "success", text: `${username}, you are logged in successfully!` });
                    navigate(from, { replace: true });
                } else {
                    setMessage({ type: "success", text: `${formData.username} has been successfully registered!` });
                }

                setFormData({ username: "", password: "" });
                setErrors({});
            } else {
                setMessage({
                    type: "error",
                    text:
                        result?.message ||
                        (submitMode === "login"
                            ? "Username or password is not correct"
                            : "Registration failed"),
                });
            }
        } catch (err) {
            console.error("Auth error:", err);
            setMessage({ type: "error", text: "Error connecting to server. Please try again." });
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
                        type="text"
                        name="username"
                        value={formData.username}
                        onChange={handleChange}
                        placeholder="Username"
                        className="auth-input"
                        autoComplete="username"
                    />
                    {errors.username && <span className="auth-error">{errors.username}</span>}
                </div>

                <div className="auth-form-group">
                    <input
                        type="password"
                        name="password"
                        value={formData.password}
                        onChange={handleChange}
                        placeholder="Password"
                        className="auth-input"
                        autoComplete={submitMode === "login" ? "current-password" : "new-password"}
                    />
                    {errors.password && <span className="auth-error">{errors.password}</span>}
                </div>

                <div className="auth-toggle" style={{ marginBottom: 0 }}>
                    <button
                        type="submit"
                        className="auth-submit-btn"
                        disabled={isSubmitting}
                        onClick={() => setSubmitMode("login")}
                    >
                        {isSubmitting && submitMode === "login" ? "Logging in…" : "Login"}
                    </button>

                    <button
                        type="submit"
                        className="auth-submit-btn"
                        disabled={isSubmitting}
                        onClick={() => setSubmitMode("register")}
                    >
                        {isSubmitting && submitMode === "register" ? "Registering…" : "Register"}
                    </button>
                </div>
            </form>
        </>
    );
}

export default AuthForm;

