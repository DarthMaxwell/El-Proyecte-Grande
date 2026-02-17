import Post from "../../Components/Post/Post";
import {useParams} from "react-router-dom";
import {useEffect, useState} from "react";
import Spinner from "../../Components/Spinner/Spinner.tsx";
import type {PostData, MovieData} from "../../Types/Types.tsx";

export default function PostPage() {
    const { postId } = useParams<{ postId: string }>();
    const [postData, setPostData] = useState<PostData | null>(null);
    const [loading, setLoading] = useState(true);
    
        const refetch = async () => {
            setLoading(true);
            try {
                const response = await fetch(`/api/posts/post/${postId}`);
                if(response.status === 404) {
                    setPostData(null);
                    return;
                }
                if (!response.ok) throw new Error(`HTTP ${response.status}`);
                const post = await response.json();
                

                // Fetch movie data for the post
                const movieResponse = await fetch(`/api/movie/${post.movieId}`);
                if (!movieResponse.ok) throw new Error(`HTTP ${movieResponse.status}`);
                const movieData: MovieData = await movieResponse.json();

                // Transform post to include movie title
                const transformedPost: PostData = {
                    Id: post.id,
                    MovieTitle: movieData.title,
                    Username: post.username,
                    Content: post.content,
                    Title: post.title,
                    MovieId: movieData.id,
                    MovieGenre: movieData.genre,
                    MovieLength: movieData.length,
                    MovieReleaseDate: movieData.releaseDate,
                };
                setPostData(transformedPost);
            } catch (err) {
                console.error("Failed to load post", err);
                setPostData(null);
            } finally {
                setLoading(false);
            }
        };
        useEffect(() => {
        refetch();
    }, [postId]);

    if (loading) return <Spinner/>;

    if (!postData) return <div className="PostPage">Post not found</div>;

    return (
        <div className="PostPage">
            <Post post={postData}
            onChanged={refetch}/>
        </div>
    );
}
