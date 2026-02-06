import "./ReplyForm.css";
import { useState } from "react";
import { useAuth } from "../../Authenticate/AuthContext"; 

interface ReplyFormProps {
    closeForm: () => void;
    postId: number;
    onCreated?: () => void;
}

export default function ReplyForm({ closeForm, postId, onCreated }: ReplyFormProps) {
    const { token } = useAuth();
    const [content, setContent] = useState("");
    const [posting, setPosting] = useState(false);

    const submitReply = async (e: React.FormEvent) => {
        e.preventDefault();
        if (!token || posting) return;

        const trimmed = content.trim();
        if (!trimmed) return;

        try {
            setPosting(true);

            const res = await fetch("/api/reply", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: `Bearer ${token}`,
                },
                body: JSON.stringify({
                    parentPostId: postId,
                    content: trimmed,
                }),
            });

            if (!res.ok) {
                alert(await res.text());
                return;
            }

            setContent("");
            onCreated?.(); 
            closeForm();     
        } finally {
            setPosting(false);
        }
    };

    return (
        <div className="comment-form" id="commentForm">
            <form onSubmit={submitReply}>
                <div className="form-group">
                    <label className="form-label" htmlFor="comment-text">
                        Comment
                    </label>

                    <textarea
                        id="comment-text"
                        className="form-textarea"
                        placeholder="Share your thoughts about this post..."
                        value={content}
                        onChange={(e) => setContent(e.target.value)}
                    />
                </div>

                <div className="form-actions">
                    <button type="submit" className="submit-btn" disabled={posting}>
                        {posting ? "Posting..." : "Post Comment"}
                    </button>

                    <button type="button" className="cancel-btn" onClick={closeForm} disabled={posting}>
                        Cancel
                    </button>
                </div>
            </form>
        </div>
    );
}
