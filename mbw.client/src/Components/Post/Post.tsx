import "./Post.css";
import ReplyList from "../ReplyList/ReplyList.tsx";
import {Link} from "react-router-dom";
import type {PostData} from "../../Types/Types.tsx";
import {useAuth} from "../../Authenticate/AuthContext.tsx";
import {useState} from "react";

interface PostProps {
    post: PostData
    onDeleted?: (postId: number) => void;
}

export default function Post({post, onDeleted}: PostProps) {
    const { user, token } = useAuth();
    const [deleted, setDeleted] = useState(false);
    
    const canDelete = !!user && (user.username === post.Username || user.role === "ADMIN");
    
    const deletePost = async() => {
        if(!token || !canDelete || deleted) return;
        if(!confirm("Are you sure you want to delete?")) return;
        
        try {
            setDeleted(true);
            const response = await fetch(`/api/posts/${post.Id}`, {
                method: "DELETE",
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            });
            if (!response.ok) {
                alert(await response.text());
                return;
            }
            onDeleted?.(post.Id);
        }finally {
            setDeleted(false);
        }
    };
    return (
        <div className="post">
            <div className="post-header">
            <h1 className="post-title">{post.Title}</h1>

            {canDelete && (
                <button className="delete-post-btn" onClick={deletePost} disabled={deleted}>
                    {deleted ? "Deleting..." : "Delete"}
                </button>
            )}
            </div>
            <div className="author-info">
                By <Link to={`/profile/${post.Username}`} className="username-link">
                <span className="author-name">{post.Username}</span>
            </Link>
            </div>

            <Link to={`/movie/${post.MovieId}`} className="movie-link">
                <div className="movie-info">
                    <div className="movie-title">{post.MovieTitle}</div>
                    <div className="movie-meta">{post.MovieReleaseDate} • {post.MovieGenre} • {post.MovieLength.toString()} min</div>
                </div>
            </Link>

            <div className="post-text">
                <p>{post.Content}</p>
            </div>

            <ReplyList ParentId={post.Id}/>
        </div>
    );
}

