import "./Reply.css";
import { Link } from "react-router-dom";
import { useAuth } from "../../Authenticate/AuthContext.tsx";
import { useState } from "react";

interface ReplyProps {
    id: number;
    Username: string;
    Content: string;
    onDeleted?: (replyId: number) => void;
    onUpdated?: (replyId: number, newContent: string) => void;
    onChanged?: () => void
}

const Reply = ({ id, Username, Content, onDeleted, onUpdated }: ReplyProps) => {
    const { user, token } = useAuth();

    const canEdit = !!user && (user.username === Username);
    const canDelete = canEdit || user?.role === "1";
    

    const [editing, setEditing] = useState(false);
    const [text, setText] = useState(Content);

    const deleteReply = async () => {
        if (!token || !canDelete) return;
        if (!confirm("Delete this comment?")) return;

        try {

            const res = await fetch(`/api/reply/${id}`, {
                method: "DELETE",
                headers: { Authorization: `Bearer ${token}` },
            });

            if (!res.ok) {
                alert(res.text());
                return;
            }

            onDeleted?.(id);
        } catch (err) {
            console.error(err);
        }
    };

    const saveReply = async () => {
        if (!token || !canEdit) return;

        const trimmed = text.trim();
        if (!trimmed) {
            alert("The comment cannot be empty");
            return;
        }

        try {

            const res = await fetch("/api/reply", {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: `Bearer ${token}`,
                },
                body: JSON.stringify({ id, content: trimmed }),
            });

            if (!res.ok) {
                alert(res.text());
                return;
            }

            onUpdated?.(id, trimmed);
            setEditing(false);
        } catch (err) {
            console.error(err);
        }
    };

    return (
        <div className="reply">
            <div className="reply-header">
                <div className="comment-user">
                    <Link className="unstyled-link" to={`/profile/${Username}`}>
                        {Username}
                    </Link>
                </div>

                {canDelete && (
                    <div className="reply-actions">
                        {!editing ? (
                            <>
                                {canEdit && (
                                <button className="edit-reply-btn" onClick={() => setEditing(true)}>
                                    Edit
                                </button>
                                    )}
                                <button className="delete-reply-btn" onClick={deleteReply}>
                                    Delete
                                </button>
                            </>
                        ) : (
                            <>
                                <button className="save-reply-btn" onClick={saveReply}>
                                    Save
                                </button>
                                <button
                                    className="cancel-reply-btn"
                                    onClick={() => {
                                        setText(Content);
                                        setEditing(false);
                                    }}
                                >
                                    Cancel
                                </button>
                            </>
                        )}
                    </div>
                )}
            </div>

            {!editing ? (
                <p className="comment-text">{Content}</p>
            ) : (
                <textarea
                    className="reply-edit-textarea"
                    value={text}
                    onChange={(e) => setText(e.target.value)}
                />
            )}
        </div>
    );
};

export default Reply;

