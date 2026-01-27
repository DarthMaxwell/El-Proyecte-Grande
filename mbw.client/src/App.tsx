import { Routes, Route } from "react-router-dom";
import { AuthProvider } from "./Authenticate/AuthContext";
import RequireAuth from "./Authenticate/RequireAuth";

import MainPage from "./Pages/MainPage/MainPage";
import MoviePage from "./Pages/MoviePage/MoviePage";
import ProfilePage from "./Pages/ProfilePage/ProfilePage";
import LoginAndRegPage from "./Pages/LoginAndRegPage/LoginAndRegPage";
import PostPage from "./Pages/PostPage/PostPage";
import CreatePostPage from "./Pages/CreatePostPage/CreatePostPage";

export default function App() {
    return (
        <AuthProvider>
            <Routes>
                <Route path="/" element={<MainPage />} />
                <Route path="/posts/:postId" element={<PostPage />} />
                <Route path="/movies/:movieId" element={<MoviePage />} />
                <Route path="/users/:userId" element={<ProfilePage />} />
                <Route path="/login" element={<LoginAndRegPage />} />

                <Route
                    path="/posts/new"
                    element={
                        <RequireAuth>
                            <CreatePostPage />
                        </RequireAuth>
                    }
                />
            </Routes>
        </AuthProvider>
    );
}


