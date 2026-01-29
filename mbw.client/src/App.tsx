import { AuthProvider } from "./Authenticate/AuthContext";
import {Outlet} from "react-router";
import NavBar from "./Components/NavBar/NavBar.tsx";


export default function App() {
    return (
        <div>
            <AuthProvider>
            <NavBar/>
            <Outlet/>
        </AuthProvider>
        </div>
    );
}


