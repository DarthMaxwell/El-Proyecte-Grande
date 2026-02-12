import Post from "../Post/Post";
import "./PostList.css";
import Spinner from "../Spinner/Spinner";
import type { PostData } from "../../Types/Types";

interface PostListProps {
    posts: PostData[];
    loading: boolean;
    refetch: () => Promise<void>;
}

export default function PostList({ posts, loading, refetch }: PostListProps) {
    if (loading) return <Spinner />;

    return (
        <div className="PostList">
            {posts.length > 0 ? (
                posts.map((post) => (
                    <Post
                        key={post.Id}
                        post={post}
                        onDeleted={refetch}
                        onUpdated={refetch}
                    />
                ))
            ) : (
                <p className="no-posts">No posts yet. Be the first to share your thoughts!</p>
            )}
        </div>
    );
}
