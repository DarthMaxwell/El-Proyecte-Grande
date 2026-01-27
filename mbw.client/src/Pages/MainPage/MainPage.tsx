import SearchBar from "../../Components/SearchBar/SearchBar";
import { useEffect, useMemo, useState } from "react";
import "./MainPage.css";
import Post from "../../Components/Post/Post";
import { Link } from "react-router-dom";
import { useAuth } from "../../Authenticate/AuthContext";

interface Movie {
    title: string;
    id: number;
    length: number;
}

type PostType = {
    id: string;
    title: string;
    body: string;
    movie: { id: number; title: string };
    author: { id: string; username: string };
};

function MainPage() {
    const [movies, setMovies] = useState<Movie[]>([]);
    const [posts, setPosts] = useState<PostType[]>([]);
    const { user, logout } = useAuth();

    const isLoggedIn = !!user;

    useEffect(() => {
        async function loadMovies() {
            try {
                const response = await fetch("/api/movie");
                if (!response.ok) throw new Error(`HTTP ${response.status}`);
                const data: Movie[] = await response.json();
                setMovies(data);
            } catch (err) {
                console.error("Failed to load movies", err);
                setMovies([]);
            }
        }

        async function loadPosts() {
            try {
                const response = await fetch("/api/posts");
                if (!response.ok) throw new Error(`HTTP ${response.status}`);
                const data: PostType[] = await response.json();
                setPosts(data);
            } catch (err) {
                console.error("Failed to load posts", err);
                setPosts([
                    {
                        id: "post-1",
                        title: "Inception is still amazing",
                        body: "Watched it again — the pacing is perfect.",
                        movie: { id: 7, title: "Inception" },
                        author: { id: "20", username: "You" },
                    },
                ]);
            }
        }

        loadMovies();
        loadPosts();
    }, []);

    const sampleSuggestions = useMemo(() => movies.map((m) => m.title), [movies]);

    const handleSearch = (query: string) => {
        const matches = movies.filter((m) =>
            m.title.toLowerCase().includes(query.toLowerCase())
        );
        console.log("matches", matches);
    };

    return (
        <div className="mainPage">
            <h1 className="mainTitle">MainPage</h1>

            <div className="topActions">
                {!isLoggedIn ? (
                    <Link to="/login" className="loginLink">
                        Log in / Register
                    </Link>
                ) : (
                    <>
                        <span className="welcomeText">Hi, {user.username}</span>

                        <button className="logoutBtn" onClick={logout} type="button">
                            Logout
                        </button>

                        <Link to="/posts/new" className="createPostLink">
                            + Create post
                        </Link>
                    </>
                )}
            </div>

            <div className="searchRow">
                <div className="searchWrap">
                    <SearchBar
                        placeholder="Type to search..."
                        onSearch={handleSearch}
                        suggestions={sampleSuggestions}
                    />
                </div>
            </div>

            <div className="postRow">
                <div className="postWrap">
                    <Post />
                </div>
            </div>
        </div>
    );
}

export default MainPage;
