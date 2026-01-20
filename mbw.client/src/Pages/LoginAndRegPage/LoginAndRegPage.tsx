
import { useNavigate, useLocation } from "react-router-dom";
import { useAuth } from "../../Authenticate/AuthContext";

export default function LoginPage() {
    const { login } = useAuth();
    const navigate = useNavigate();
    const location = useLocation();

    const from = (location.state as any)?.from?.pathname || "/";

    function doLogin() {
        // Fake user. Replace with backend auth later.
        login({ id: "20", username: "You" });
        navigate(from, { replace: true });
    }

    return (
        <div style={{ padding: 20 }}>
            <h1>Login</h1>
            <p>This is a fake login for now.</p>
            <button onClick={doLogin}>Log in as demo user</button>
        </div>
    );
}
