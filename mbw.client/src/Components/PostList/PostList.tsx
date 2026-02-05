import Post from "../Post/Post";
import "./PostList.css";
import Spinner from "../Spinner/Spinner.tsx";
import type {PostData} from "../../Types/Types.tsx";

interface PostListProps {
    posts: PostData[],
    loading: boolean
}

export default function PostList({posts, loading}: PostListProps) {
    
    if(loading) {
        return <Spinner/>;
    }
    
    return (
        <div className="PostList">
            {posts.length > 0 ? (
                posts.map(post => (
                    <Post key={post.Id} post={post}/>
                ))
            ) : (
                <p className="no-posts">No posts yet. Be the first to share your thoughts!</p>
            )}
        </div>
    );
}