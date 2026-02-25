import { useEffect, useState } from "react";
import Reply from "../Reply/Reply";
import "./ReplyList.css";
import ReplyForm from "../ReplyForm/ReplyForm";
import type { Reply as R } from "../../Types/Types";
import { useAuth } from "../../Authenticate/AuthContext";
import { BASE_URL } from "../../config";

interface ParentPost {
    ParentId: number;
}

export default function ReplyList({ ParentId }: ParentPost) {
    const { user, loading } = useAuth();
    const [replies, setReplies] = useState<R[]>([]);
    const [showForm, setShowForm] = useState(false);

    const refetch = async () => {
        try {
            const res = await fetch(`${BASE_URL}/api/reply/${ParentId}`);
            if (!res.ok) {
                console.error("Failed to fetch replies:", res.status);
                setReplies([]);
                return;
            }
            setReplies(await res.json());
        } catch (err) {
            console.error("Error fetching replies:", err);
            setReplies([]);
        }
    };

    useEffect(() => {
        refetch();
    }, [ParentId]);

    useEffect(() => {
        if (!loading && !user) setShowForm(false);
    }, [user, loading]);

    return (
        <section className="replies">
            <div className="comments-header-row">
                <h2 className="comments-header">Comments ({replies.length})</h2>
                {user && (
                    <button className="add-comment-btn" onClick={() => setShowForm((p) => !p)}>
                        +
                    </button>
                )}
            </div>

            {user && showForm && (
                <ReplyForm
                    closeForm={() => setShowForm(false)}
                    postId={ParentId}
                    onCreated={refetch}
                />
            )}

            {replies.length > 0 ? (
                replies.map((r) => (
                    <Reply
                        key={r.id}
                        id={r.id}
                        Username={r.username}
                        Content={r.content}
                        onChanged={refetch} 
                    />
                ))
            ) : user ? (
                <p>No comments, click the plus icon to add a comment</p>
            ) : (
                <p>No comments. Log in to add a comment.</p>
            )}
        </section>
    );
}
