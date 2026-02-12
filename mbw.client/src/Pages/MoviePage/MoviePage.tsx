import Movie from "../../Components/Movie/Movie";
import PostList from "../../Components/PostList/PostList";
import {useParams} from "react-router-dom";
import {useEffect, useState} from "react";
import Spinner from "../../Components/Spinner/Spinner.tsx";
import type {Post, PostData, MovieData} from "../../Types/Types.tsx";

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
                        MovieId: movieData.id,
                        MovieGenre: movieData.genre,
                        MovieLength: movieData.length,
                        MovieReleaseDate: movieData.releaseDate,
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


    if (loading) return <Spinner/>;

    if (!movie) return <div className="MoviePage">Movie not found</div>;

    return (
        <div className="MoviePage">
            <Movie movie={movie}/>
            <h2>Discussion</h2>
            <p>Create a post</p>
            <PostList posts={posts} loading={false} />
        </div>
    );
}