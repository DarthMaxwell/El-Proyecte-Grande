import "./ReplyForm.css";
import { useState } from "react";
import { useAuth } from "../../Authenticate/AuthContext"; 
import { BASE_URL } from "../../config";

interface ReplyFormProps {
    closeForm: () => void;
    postId: number;
    onCreated?: () => void;
}

export default function ReplyForm({ closeForm, postId, onCreated }: ReplyFormProps) {
    const { token } = useAuth();
    const [content, setContent] = useState("");

    const submitReply = async (e: React.FormEvent) => {
        e.preventDefault();
        if (!token) return;

        const trimmed = content.trim();
        if (!trimmed) {
            alert("Cannot be empty");
            return;
        }

        try {

            const res = await fetch(BASE_URL + "/api/reply", {
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
                alert(res.text());
                return;
            }

            setContent("");
            onCreated?.(); 
            closeForm();     
        } catch (error) {
            console.log(error);
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
                    <button type="submit" className="submit-btn">
                        Post
                    </button>

                    <button type="button" className="cancel-btn" onClick={closeForm}>
                        Cancel
                    </button>
                </div>
            </form>
        </div>
    );
}
