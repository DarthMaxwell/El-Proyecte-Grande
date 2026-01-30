import Movie from "../../Components/Movie/Movie";
import PostList from "../../Components/PostList/PostList";
import {useParams} from "react-router-dom";
import {useEffect, useState} from "react";
import Spinner from "../../Components/Spinner/Spinner.tsx";

interface MovieData {
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

export default function MoviePage() {
    const { movieId } = useParams<{ movieId: string }>();
    const [movie, setMovie] = useState<MovieData | null>(null);
    const [posts, setPosts] = useState<PostData[]>([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        async function loadMovieAndPosts() {
            if (!movieId) return;
            setLoading(true);

            try {
                // Fetch movie data
                const movieResponse = await fetch(`/api/movie/${movieId}`);
                if (!movieResponse.ok) throw new Error(`HTTP ${movieResponse.status}`);
                const movieData: MovieData = await movieResponse.json();
                setMovie(movieData);

                // Fetch posts for this movie
                const postsResponse = await fetch(`/api/posts/${movieId}`);
                if (postsResponse.ok) {
                    const postsData: Post[] = await postsResponse.json();

                    // Transform posts to include movie title
                    const transformedPosts: PostData[] = postsData.map(post => ({
                        Id: post.id,
                        MovieTitle: movieData.title,
                        Username: post.username,
                        Content: post.content,
                        Title: post.title,
                    }));

                    setPosts(transformedPosts);
                } else {
                    setPosts([]);
                }
            } catch (err) {
                console.error("Failed to load movie or posts", err);
            } finally {
                setLoading(false);
            }
        }

        loadMovieAndPosts();
    }, [movieId]);

    if (!movie) {
        return <div className="MoviePage">Movie not found</div>;
    }

    return (
        (loading) ? (<Spinner/>) : (
            <div className="MoviePage">
                <Movie
                    ReleaseDate={movie.releaseDate}
                    Length={movie.length}
                    Title={movie.title}
                    Director={movie.director}
                    Description={movie.description}
                    Genre={movie.genre}
                />
                <h2>Discussion</h2>
                <p>Create a post</p>
                <PostList posts={posts} loading={false} />
            </div>
        )
    );
}