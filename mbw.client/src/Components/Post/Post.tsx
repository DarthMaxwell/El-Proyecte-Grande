import "./Post.css";
import ReplyList from "../ReplyList/ReplyList.tsx";
import {Link} from "react-router-dom";
import type {PostData} from "../../Types/Types.tsx";

interface PostProps {
    post: PostData
}

export default function Post({post}: PostProps) {
    return (
        <div className="post">
            <h1 className="post-title">{post.Title}</h1>

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

