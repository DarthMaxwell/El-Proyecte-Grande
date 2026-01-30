import {useEffect, useState} from "react";
import Reply from "../Reply/Reply";
import "./ReplyList.css"

interface Reply {
    id: number,
    parentPostId: number,
    content: string;
    username: string;
}

interface ParentPost {
    ParentId: number,
}

function ReplyList({ ParentId }: ParentPost) {
    const [replies, setReplies] = useState<Reply[]>([]);

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
                return (<p>No comments</p>);
            }
        }
        
        // return Spinner HERE
    }

    return (
      <div className="replylist">
          {insertData()}
      </div>  
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