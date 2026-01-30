import Post from "../Post/Post";
import "./PostList.css";
import Spinner from "../Spinner/Spinner.tsx";

interface PostData {
    Id: number;
    MovieTitle: string;
    Username: string;
    Title: string;
    Content: string;
}

interface PostListProps {
    posts: PostData[],
    loading: boolean
}

//could call for username here too if we still just have the id

export default function PostList({posts, loading}: PostListProps) {
    
    if(loading) {
        return <Spinner/>;
    }
    
    return (
        <div className="PostList">
            {posts.length > 0 ? (
                posts.map(post => (
                    <Post
                        key={post.Id}
                        Id={post.Id}
                        Tittle={post.Title}
                        MovieTitle={post.MovieTitle}
                        Username={post.Username}
                        Content={post.Content}
                    />
                ))
            ) : (
                <p className="no-posts">No posts yet. Be the first to share your thoughts!</p>
            )}
        </div>
    );
}