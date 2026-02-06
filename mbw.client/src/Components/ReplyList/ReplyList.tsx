import { useEffect, useState } from "react";
import Reply from "../Reply/Reply";
import "./ReplyList.css";
import ReplyForm from "../ReplyForm/ReplyForm.tsx";
import type { Reply as R } from "../../Types/Types.tsx";

interface ParentPost {
    ParentId: number;
}

function ReplyList({ ParentId }: ParentPost) {
    const [replies, setReplies] = useState<R[]>([]);
    const [showForm, setShowForm] = useState(false);

    useEffect(() => {
        populateReplyData();
    }, [ParentId]);

    function toggleForm() {
        setShowForm((prev) => !prev);
    }

    function handleReplyDeleted(replyId: number) {
        setReplies((prev) => prev.filter((r) => r.id !== replyId));
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
                />
            ));
        }
        return <p>No comments, click the plus icon to add a comment</p>;
    }

    return (
        <section className="replies">
            <div className="comments-header-row">
                <h2 className="comments-header">Comments ({replies.length})</h2>
                <button className="add-comment-btn" onClick={toggleForm}>
                    +
                </button>
            </div>

            {showForm && (
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
