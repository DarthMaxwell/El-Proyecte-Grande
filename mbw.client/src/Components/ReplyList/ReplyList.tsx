import { useEffect, useState } from "react";
import Reply from "../Reply/Reply";
import "./ReplyList.css";
import ReplyForm from "../ReplyForm/ReplyForm.tsx";
import type { Reply as R } from "../../Types/Types.tsx";
import { useAuth } from "../../Authenticate/AuthContext.tsx"; 

interface ParentPost {
    ParentId: number;
}

function ReplyList({ ParentId }: ParentPost) {
    const { user, loading } = useAuth();
    const [replies, setReplies] = useState<R[]>([]);
    const [showForm, setShowForm] = useState(false);

    useEffect(() => {
        populateReplyData();
    }, [ParentId]);


    useEffect(() => {
        if (!loading && !user) setShowForm(false);
    }, [user, loading]);

    function toggleForm() {
        setShowForm((prev) => !prev);
    }

    function handleReplyDeleted(replyId: number) {
        setReplies((prev) => prev.filter((r) => r.id !== replyId));
    }
    function handleReplyUpdated(replyId: number, newContent: string) {
        setReplies((prev) =>
            prev.map((r) => (r.id === replyId ? { ...r, content: newContent } : r))
        );
    }


    function insertData() {
        if (replies.length > 0) {
            return replies.map((r) => (
                <Reply
                    key={r.id}
                    id={r.id}
                    Username={r.username}
                    Content={r.content}
                    onDeleted={handleReplyDeleted}
                    onUpdated={handleReplyUpdated}
                />
            ));
        }
        
        return user ? (
            <p>No comments, click the plus icon to add a comment</p>
        ) : (
            <p>No comments. Log in to add a comment.</p>
        );
    }

    return (
        <section className="replies">
            <div className="comments-header-row">
                <h2 className="comments-header">Comments ({replies.length})</h2>
                {user && (
                    <button className="add-comment-btn" onClick={toggleForm}>
                        +
                    </button>
                )}
            </div>

            {user && showForm && (
                <ReplyForm
                    closeForm={() => setShowForm(false)}
                    postId={ParentId}
                    onCreated={populateReplyData}
                />
            )}

            {insertData()}
        </section>
    );

    async function populateReplyData() {
        try {
            const response = await fetch(`/api/reply/${ParentId}`);

            if (response.ok) {
                const data = await response.json();
                setReplies(data);
            } else {
                console.error("Failed to fetch replies:", response.status);
                setReplies([]);
            }
        } catch (error) {
            console.error("Error fetching replies:", error);
            setReplies([]);
        }
    }
}

export default ReplyList;
