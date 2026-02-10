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
}

const Reply = ({ id, Username, Content, onDeleted, onUpdated }: ReplyProps) => {
    const { user, token } = useAuth();

    const canEdit = !!user && (user.username === Username || user.role === "ADMIN");
    const canDelete = canEdit;

    const [deleting, setDeleting] = useState(false);

    const [editing, setEditing] = useState(false);
    const [text, setText] = useState(Content);
    const [saving, setSaving] = useState(false);

    const deleteReply = async () => {
        if (!token || !canDelete || deleting) return;
        if (!confirm("Delete this comment?")) return;

        try {
            setDeleting(true);

            const res = await fetch(`/api/reply/${id}`, {
                method: "DELETE",
                headers: { Authorization: `Bearer ${token}` },
            });

            if (!res.ok) {
                alert(await res.text());
                return;
            }

            onDeleted?.(id);
        } finally {
            setDeleting(false);
        }
    };

    const saveReply = async () => {
        if (!token || !canEdit || saving) return;

        const trimmed = text.trim();
        if (!trimmed) return;

        try {
            setSaving(true);

            const res = await fetch("/api/reply", {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: `Bearer ${token}`,
                },
                body: JSON.stringify({ id, content: trimmed }),
            });

            if (!res.ok) {
                alert(await res.text());
                return;
            }

            onUpdated?.(id, trimmed);
            setEditing(false);
        } finally {
            setSaving(false);
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

                {canEdit && (
                    <div className="reply-actions">
                        {!editing ? (
                            <>
                                <button className="edit-reply-btn" onClick={() => setEditing(true)}>
                                    Edit
                                </button>
                                <button className="delete-reply-btn" onClick={deleteReply} disabled={deleting}>
                                    {deleting ? "Deleting..." : "Delete"}
                                </button>
                            </>
                        ) : (
                            <>
                                <button className="save-reply-btn" onClick={saveReply} disabled={saving}>
                                    {saving ? "Saving..." : "Save"}
                                </button>
                                <button
                                    className="cancel-reply-btn"
                                    onClick={() => {
                                        setText(Content);
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

