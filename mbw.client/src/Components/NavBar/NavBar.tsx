import { NavLink } from "react-router-dom";
import "./NavBar.css";
import { useAuth } from "../../Authenticate/AuthContext";
import { useState } from "react";

const NavBar = () => {
    const { user, loading, logout } = useAuth();
    const [open, setOpen] = useState(false);

    const closeMenu = () => setOpen(false);

    return (
        <header className="nav-wrap">
            <nav className="navbar">
                <div className="navbar-left">
                    <NavLink to="/" className="brand" onClick={closeMenu}>
                        <span className="brand-mark" aria-hidden="true">🎬</span>
                        <span className="brand-text">Absolute Cinema</span>
                    </NavLink>
                </div>

                <button
                    className="nav-toggle"
                    type="button"
                    aria-label="Toggle navigation"
                    aria-expanded={open}
                    onClick={() => setOpen((v) => !v)}
                >
                    <span className="burger" />
                </button>

                <div className={`navbar-center ${open ? "is-open" : ""}`}>
                    <ul className="navbar-nav">
                        {!loading && user && (
                            <>
                                <li className="nav-user">
                                    <NavLink
                                        to={`/profile/${user.username}`}
                                        onClick={closeMenu}
                                        className={({ isActive }) => `chip chip-link ${isActive ? "chip-active" : ""}`}
                                        title={user.username}
                                    >
                                        <span className="chip-dot" aria-hidden="true" />
                                        {user.username}
                                    </NavLink>
                                </li>
                                <li>
                                    <NavLink
                                        to="/posts/new"
                                        onClick={closeMenu}
                                        className={({ isActive }) => `nav-link ${isActive ? "active" : ""}`}
                                    >
                                        Create+
                                    </NavLink>
                                </li>
                                <li className="navbar-spacer"/>
                                <li>
                                    <button
                                        className="btn btn-ghost"
                                        type="button"
                                        onClick={() => {
                                            logout();
                                            closeMenu();
                                        }}
                                    >
                                        Logout
                                    </button>
                                </li>
                            </>
                        )}
                    </ul>
                </div>
x
                <div className="navbar-right">
                    {!loading && !user && (
                        <NavLink to="/login" className="btn btn-primary" onClick={closeMenu}>
                            Login / Register
                        </NavLink>
                    )}
                </div>
            </nav>
        </header>
    );
};

export default NavBar;
