import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import "./ProfilePage.css";
import PostList from "../../Components/PostList/PostList.tsx";
import Spinner from "../../Components/Spinner/Spinner.tsx";

interface Post {
    id: number;
    username: string;
    movieId: number;
    content: string;
    title: string;
}

interface PostData {
    Id: number;
    MovieTitle: string;
    Username: string;
    Content: string;
    Title: string;
}

export default function ProfilePage() {
    const { username } = useParams<{ username: string }>();
    const [posts, setPosts] = useState<PostData[]>([]);
    const [loading, setLoading] = useState(true);
    const [userExists, setUserExists] = useState(true);

    useEffect(() => {
        async function loadUserPosts() {
            if (!username) return;
            setLoading(true);

            try {
                const response = await fetch(`/api/posts/user/${username}`);

                if (!response.ok) {
                    if (response.status === 404) {
                        setUserExists(false);
                    }
                    setPosts([]);
                    return;
                }

                const postsData: Post[] = await response.json();

                if (postsData.length === 0) {
                    setPosts([]);
                    return;
                }

                // Get unique movie IDs
                const uniqueMovieIds = [...new Set(postsData.map(post => post.movieId))];

                // Fetch all movie titles
                const movieTitles: { [key: number]: string } = {};
                await Promise.all(
                    uniqueMovieIds.map(async (movieId) => {
                        try {
                            const movieResponse = await fetch(`/api/movie/${movieId}`);
                            if (movieResponse.ok) {
                                const movieData = await movieResponse.json();
                                movieTitles[movieId] = movieData.title;
                            }
                        } catch (err) {
                            console.error(`Failed to fetch movie ${movieId}`, err);
                            movieTitles[movieId] = `Movie ${movieId}`;
                        }
                    })
                );

                // Transform posts with actual movie titles
                const transformedPosts: PostData[] = postsData.map(post => ({
                    Id: post.id,
                    MovieTitle: movieTitles[post.movieId] || `Movie ${post.movieId}`,
                    Username: username,
                    Content: post.content,
                    Title: post.title,
                }));

                setPosts(transformedPosts);
            } catch (err) {
                console.error("Failed to load user posts", err);
                setUserExists(false);
            } finally {
                setLoading(false);
            }
        }

        loadUserPosts();
    }, [username]);

    if (!userExists) {
        return (
            <div className="ProfilePage">
                <h1>{username}</h1>
                <p className="no-user">This user doesn't exist.</p>
            </div>
        );
    }

    return (
        <div className="ProfilePage">
            <h1>{username}</h1>
            {loading ? (
                <Spinner />
            ) : posts.length === 0 ? (
                <p className="no-posts">This user hasn't made any posts yet.</p>
            ) : (
                <PostList posts={posts} loading={false} />
            )}
        </div>
    );
}