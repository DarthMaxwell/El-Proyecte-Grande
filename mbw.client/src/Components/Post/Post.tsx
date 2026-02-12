import "./Post.css";
import ReplyList from "../ReplyList/ReplyList";
import { Link } from "react-router-dom";
import type { PostData } from "../../Types/Types";
import { useAuth } from "../../Authenticate/AuthContext";
import { useState } from "react";

interface PostProps {
    post: PostData;
    onDeleted?: (postId: number) => void;
    onUpdated?: (updated: PostData) => void;
}

export default function Post({ post, onDeleted, onUpdated }: PostProps) {
    const { user, token } = useAuth();

    const canEdit = !!user && (user.username === post.Username || user.role === "ADMIN");
    const canDelete = canEdit;

    const [deleting, setDeleting] = useState(false);

    const [editing, setEditing] = useState(false);
    const [title, setTitle] = useState(post.Title);
    const [content, setContent] = useState(post.Content);
    const [saving, setSaving] = useState(false);

    const deletePost = async () => {
        if (!token || !canDelete || deleting) return;
        if (!confirm("Are you sure you want to delete?")) return;

        try {
            setDeleting(true);

            const response = await fetch(`/api/posts/${post.Id}`, {
                method: "DELETE",
                headers: { Authorization: `Bearer ${token}` },
            });

            if (!response.ok) {
                alert(await response.text());
                return;
            }

            onDeleted?.(post.Id);
        } finally {
            setDeleting(false);
        }
    };

    const savePost = async () => {
        if (!token || !canEdit || saving) return;

        const t = title.trim();
        const c = content.trim();
        if (!t || !c) return;

        try {
            setSaving(true);

            const res = await fetch("/api/posts", {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: `Bearer ${token}`,
                },
                body: JSON.stringify({ id: post.Id, title: t, content: c }),
            });

            if (!res.ok) {
                alert(await res.text());
                return;
            }

            const updated: PostData = { ...post, Title: t, Content: c };
            onUpdated?.(updated);

            setEditing(false);
        } finally {
            setSaving(false);
        }
    };

    return (
        <div className="post">
            <div className="post-header">
                {!editing ? (
                            <Link to={`/post/${post.Id}`} className="username-link">
                                <h1 className="post-title">{post.Title}</h1>
                            </Link>
                ) : (
                    <input
                        className="post-edit-title"
                        value={title}
                        onChange={(e) => setTitle(e.target.value)}
                        disabled={saving}
                    />
                )}

                {canEdit && (
                    <div className="post-actions">
                        {!editing ? (
                            <>
                                <button className="edit-post-btn" onClick={() => setEditing(true)}>
                                    Edit
                                </button>

                                <button className="delete-post-btn" onClick={deletePost} disabled={deleting}>
                                    {deleting ? "Deleting..." : "Delete"}
                                </button>
                            </>
                        ) : (
                            <>
                                <button className="save-post-btn" onClick={savePost} disabled={saving}>
                                    {saving ? "Saving..." : "Save"}
                                </button>

                                <button
                                    className="cancel-post-btn"
                                    onClick={() => {
                                        setTitle(post.Title);
                                        setContent(post.Content);
                                        setEditing(false);
                                    }}
                                    disabled={saving}
                                >
                                    Cancel
                                </button>
                            </>
                        )}
                    </div>
                )}
            </div>

            <div className="author-info">
                By{" "}
                <Link to={`/profile/${post.Username}`} className="username-link">
                    <span className="author-name">{post.Username}</span>
                </Link>
            </div>

            <Link to={`/movie/${post.MovieId}`} className="movie-link">
                <div className="movie-info">
                    <div className="movie-title">{post.MovieTitle}</div>
                    <div className="movie-meta">
                        {post.MovieReleaseDate} • {post.MovieGenre} • {post.MovieLength} min
                    </div>
                </div>
            </Link>

            <div className="post-text">
                {!editing ? (
                    <p>{post.Content}</p>
                ) : (
                    <div className="post-edit-field">
                        <label className="post-edit-label" htmlFor={`post-content-${post.Id}`}>
                            Content
                        </label>
                        <textarea
                            id={`post-content-${post.Id}`}
                            className="post-edit-content"
                            value={content}
                            onChange={(e) => setContent(e.target.value)}
                            disabled={saving}
                        />
                    </div>
                )}
            </div>



            <ReplyList ParentId={post.Id} />
        </div>
    );
}