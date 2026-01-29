import Post from "../Post/Post";
import "./PostList.css";

interface PostData {
    Id: number;
    MovieTitle: string;
    Username: string;
    Content: string;
}

interface PostListProps {
    posts: PostData[];
}

//could call for username here too if we still just have the id

export default function PostList({ posts }: PostListProps) {
    return (
        <div className="PostList">
            {posts.length > 0 ? (
                posts.map(post => (
                    <Post
                        key={post.Id}
                        Id={post.Id}
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