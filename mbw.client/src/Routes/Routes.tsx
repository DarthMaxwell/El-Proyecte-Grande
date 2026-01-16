import {createBrowserRouter} from "react-router";
import App from "../App.tsx";
import CreatePostPage from "../Pages/CreatePostPage/CreatePostPage.tsx";
import ProfilePage from "../Pages/ProfilePage/ProfilePage.tsx";
import MoviePage from "../Pages/MoviePage/MoviePage.tsx";
import MainPage from "../Pages/MainPage/MainPage.tsx";
import LoginAndRegPage from "../Pages/LoginAndRegPage/LoginAndRegPage.tsx";

export const router = createBrowserRouter([
    {
        path: "/",
        element: <App/>,
        children: [
            {path: "", element: <MainPage />},
            {path: "login", element: <LoginAndRegPage />},
            {path: "create", element: <CreatePostPage />},
            {path: "movie", element: <MoviePage />},
            {path: "profile", element: <ProfilePage />},
        ]
    }
])
