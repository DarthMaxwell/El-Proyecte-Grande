import SearchBar from "../../Components/SearchBar/SearchBar";
import { useEffect, useState } from "react";
import "./MainPage.css";
import Post from "../../Components/Post/Post";
import PostList from "../../Components/PostList/PostList.tsx";

// top 5 movies
// get there posts for the post list


interface Movie {
    id: number;
    releaseDate: string;
    length: number;
    title: string;
    director: string;
    description: string;
    genre: string;
}

interface Post {
    id: number;
    userId: number;
    movieId: number;
    content: string;
}

interface PostWithMovie {
    post: Post;
    movieTitle: string;
}

function MainPage() {
    const [postsWithMovies, setPostsWithMovies] = useState<PostWithMovie[]>([]);

    useEffect(() => {
        async function loadMoviesAndPosts() {
            try {
                // Get top 5 movies
                const moviesResponse = await fetch("/api/movie/topfive");
                if (!moviesResponse.ok) throw new Error(`HTTP ${moviesResponse.status}`);
                const movies: Movie[] = await moviesResponse.json();

                // For each movie, fetch its posts
                const allPostsWithMovies: PostWithMovie[] = [];

                for (const movie of movies) {
                    try {
                        const postsResponse = await fetch(`/api/posts/${movie.id}`);
                        if (postsResponse.ok) {
                            const posts: Post[] = await postsResponse.json();

                            // Add movie info to each post
                            posts.forEach(post => {
                                allPostsWithMovies.push({
                                    post: post,
                                    movieTitle: movie.title,
                                });
                            });
                        }
                    } catch (err) {
                        console.error(`Failed to load posts for movie ${movie.id}`, err);
                    }
                }

                setPostsWithMovies(allPostsWithMovies);
            } catch (err) {
                console.error("Failed to load movies and posts", err);
                setPostsWithMovies([]);
            }
        }

        loadMoviesAndPosts();
    }, []);
    

    return (
        <div className="mainPage">
            <div className="searchRow">
                <div className="searchWrap">
                    <SearchBar/>
                </div>
            </div>

            <PostList posts={postsWithMovies.map(item => ({
                Id: item.post.id,
                MovieTitle: item.movieTitle,
                Username: item.post.userId.toString(), //NEED TO BE USERNAME LATER
                Content: item.post.content
            }))} />
        </div>
    );
}

export default MainPage;
