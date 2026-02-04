import {useEffect, useState} from "react";
import Reply from "../Reply/Reply";
import "./ReplyList.css"
import ReplyForm from "../ReplyForm/ReplyForm.tsx";
import type {Reply as r} from "../../Types/Types.tsx";

interface ParentPost {
    ParentId: number,
}

function ReplyList({ ParentId }: ParentPost) {
    const [replies, setReplies] = useState<r[]>([]);
    const [showForm, setShowForm] = useState(false);

    useEffect(() => {
        populateReplyData();
    }, []);

    function insertData() {
        if (replies) {
            if (replies.length > 0) {
                return (replies.map(r => 
                    <Reply Username={r.username} Content={r.content}/>
                ));
            } else {
                return (<p>No comments, click the plus icon to add a comment</p>);
            }
        }
    }
    
    function toggleForm() {
        setShowForm(!showForm);
    }

    return (
        <section className="replies">
            <div className="comments-header-row">
                <h2 className="comments-header">Comments ({replies.length})</h2>
                <button className="add-comment-btn" onClick={toggleForm}>+</button>
            </div>

            {showForm && (<ReplyForm closeForm={() => setShowForm(false)} postId={ParentId}/>)}

            {insertData()}
        </section>
    );

    async function populateReplyData() {
        const response = await fetch('api/reply/' + ParentId)
        if (response.ok) {
            const data = await response.json();
            setReplies(data);
        } //Need error handling
    }
}

export default ReplyList;