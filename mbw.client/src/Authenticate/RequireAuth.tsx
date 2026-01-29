import { Navigate, useLocation } from "react-router-dom";
import { useAuth } from "./AuthContext";
import type {JSX} from "react";

export default function RequireAuth({ children }: { children: JSX.Element }) {
    const { user, loading } = useAuth();
    const location = useLocation();

    if (loading) {
        return <div style={{ padding: 20 }}>Loading…</div>;
    }

    if (!user) {
        return <Navigate to="/login" replace state={{ from: location }} />;
    }

    return children;
}
