import { AuthProvider } from "./Authenticate/AuthContext";
import {Outlet} from "react-router";


export default function App() {
    return (
        <AuthProvider>
            <Outlet/>
        </AuthProvider>
    );
}


