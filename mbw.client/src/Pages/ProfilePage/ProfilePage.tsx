import { useEffect, useState} from "react";
import {useParams} from "react-router-dom";
import "./ProfilePage.css";
import PostList from "../../Components/PostList/PostList.tsx";
import Spinner from "../../Components/Spinner/Spinner.tsx";
import type {MovieData, Post, PostData} from "../../Types/Types.tsx";
import { BASE_URL } from "../../config.ts";

export default function ProfilePage() {
    const { username } = useParams<{ username: string }>();
    const [posts, setPosts] = useState<PostData[]>([]);
    const [loading, setLoading] = useState(true);
    const [userExists, setUserExists] = useState(true);
    
       const refetch = async () => {
           if(!username) return;
           setLoading(true);

            try {
                const response = await fetch(`${BASE_URL}/api/posts/user/${username}`);

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
                const movies: { [key: number]: MovieData } = {};
                await Promise.all(
                    uniqueMovieIds.map(async (movieId) => {
                        try {
                            const movieResponse = await fetch(`${BASE_URL}/api/movie/${movieId}`);
                            if (movieResponse.ok) {
                                movies[movieId] = await movieResponse.json();
                            }
                        } catch (err) {
                            console.error(`Failed to fetch movie ${movieId}`, err);
                        }
                    })
                );

                // Transform posts with actual movie titles
                const transformedPosts: PostData[] = postsData.map(post => ({
                    Id: post.id,
                    Username: username,
                    Content: post.content,
                    Title: post.title,
                    MovieTitle: movies[post.movieId].title,
                    MovieId: post.movieId,
                    MovieGenre: movies[post.movieId].genre,
                    MovieLength: movies[post.movieId].length,
                    MovieReleaseDate: movies[post.movieId].releaseDate,
                }));

                setPosts(transformedPosts);
            } catch (err) {
                console.error("Failed to load user posts", err);
                setUserExists(false);
            } finally {
                setLoading(false);
            }
        }
        useEffect(() => {
        refetch();
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
                <PostList posts={posts} loading={false} refetch={refetch} />
            )}
        </div>
    );
}