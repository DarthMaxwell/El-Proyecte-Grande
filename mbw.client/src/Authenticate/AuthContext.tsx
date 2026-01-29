import { createContext, useContext, useEffect, useMemo, useState } from "react";

export type AuthUser = { username: string; role?: string; id?: number };

type AuthState = {
    user: AuthUser | null;
    token: string | null;
    loading: boolean;
    login: (payload: { username: string; token: string; role?: string; id?: number }) => void;
    logout: () => void;
};

const AuthContext = createContext<AuthState | undefined>(undefined);

export function AuthProvider({ children }: { children: React.ReactNode }) {
    const [user, setUser] = useState<AuthUser | null>(null);
    const [token, setToken] = useState<string | null>(null);
    const [loading, setLoading] = useState(true);

    // Restore from localStorage on first load
    useEffect(() => {
        const storedToken = localStorage.getItem("token");
        const storedUsername = localStorage.getItem("username");
        const storedRole = localStorage.getItem("role");

        if (storedToken && storedUsername) {
            setToken(storedToken);
            setUser({ username: storedUsername, role: storedRole ?? undefined });
        }

        setLoading(false);
    }, []);

    const login: AuthState["login"] = ({ username, token, role, id }) => {
        setToken(token);
        setUser({ username, role, id });

        localStorage.setItem("token", token);
        localStorage.setItem("username", username);
        if (role) localStorage.setItem("role", role);
        if (id != null) localStorage.setItem("userId", String(id));
    };

    const logout = () => {
        setToken(null);
        setUser(null);

        localStorage.removeItem("token");
        localStorage.removeItem("username");
        localStorage.removeItem("role");
        localStorage.removeItem("userId");
    };

    const value = useMemo(
        () => ({ user, token, loading, login, logout }),
        [user, token, loading]
    );

    return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}

export function useAuth() {
    const ctx = useContext(AuthContext);
    if (!ctx) throw new Error("useAuth must be used inside AuthProvider");
    return ctx;
}
