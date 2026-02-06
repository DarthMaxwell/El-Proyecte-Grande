import Post from "../Post/Post";
import "./PostList.css";
import Spinner from "../Spinner/Spinner.tsx";
import type {PostData} from "../../Types/Types.tsx";
import {useEffect, useState} from "react";

interface PostListProps {
    posts: PostData[],
    loading: boolean
}

export default function PostList({posts, loading}: PostListProps) {
    const [visiblePosts, setVisiblePosts] = useState<PostData[]>(posts);
    
    useEffect(() => {
        setVisiblePosts(posts);
    }, [posts]);
    
    if(loading) {
        return <Spinner/>;
    }
    
    return (
        <div className="PostList">
            {visiblePosts.length > 0 ? (
                visiblePosts.map(post => (
                    <Post key={post.Id} post={post} onDeleted={(id) => setVisiblePosts((prev) => prev.filter((p) => p.Id !== id))}/>
                ))
            ) : (
                <p className="no-posts">No posts yet. Be the first to share your thoughts!</p>
            )}
        </div>
    );
}