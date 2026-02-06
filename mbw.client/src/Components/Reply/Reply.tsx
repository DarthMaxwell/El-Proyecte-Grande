import "./Reply.css";
import { Link } from "react-router-dom";
import { useAuth } from "../../Authenticate/AuthContext.tsx";
import { useState } from "react";

interface ReplyProps {
    id: number;
    Username: string;
    Content: string;
    onDeleted?: (replyId: number) => void;
}

const Reply = ({ id, Username, Content, onDeleted }: ReplyProps) => {
    const { user, token } = useAuth();
    const [deleting, setDeleting] = useState(false);

    const canDelete = !!user && (user.username === Username || user.role === "ADMIN");

    const deleteReply = async () => {
        if (!token || !canDelete || deleting) return;
        if (!confirm("Are you sure you want to delete this comment?")) return;

        try {
            setDeleting(true);

            const res = await fetch(`/api/reply/${id}`, {
                method: "DELETE",
                headers: {
                    Authorization: `Bearer ${token}`,
                },
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

    return (
        <div className="reply">
            <div className="reply-header">
                <div className="comment-user">
                    <Link className="unstyled-link" to={`/profile/${Username}`}>
                        {Username}
                    </Link>
                </div>

                {canDelete && (
                    <button className="delete-reply-btn" onClick={deleteReply} disabled={deleting}>
                        {deleting ? "Deleting..." : "Delete"}
                    </button>
                )}
            </div>

            <p className="comment-text">{Content}</p>
        </div>
    );
};

export default Reply;
