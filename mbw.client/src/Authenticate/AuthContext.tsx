import React, { createContext, useContext, useMemo, useState } from "react";

type User = { id: string; username: string };
type AuthValue = {
    user: User | null;
    login: (u: User) => void;
    logout: () => void;
};

const AuthContext = createContext<AuthValue | null>(null);

export function AuthProvider({ children }: { children: React.ReactNode }) {
    const [user, setUser] = useState<User | null>(() => {
        const raw = localStorage.getItem("user");
        return raw ? (JSON.parse(raw) as User) : null;
    });

    const value = useMemo<AuthValue>(
        () => ({
            user,
            login: (u) => {
                setUser(u);
                localStorage.setItem("user", JSON.stringify(u));
            },
            logout: () => {
                setUser(null);
                localStorage.removeItem("user");
            },
        }),
        [user]
    );

    return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}

export function useAuth() {
    const ctx = useContext(AuthContext);
    if (!ctx) throw new Error("useAuth must be used inside AuthProvider");
    return ctx;
}
