import "./Reply.css"
import {Link} from "react-router-dom";

interface ReplyProps {
    Username: string;
    Content: string;
}

const Reply = ({Username, Content}: ReplyProps) => {
  return (
      <div className="reply">
          <div className="comment-user">
              <Link className="unstyled-link" to={`/profile/${Username}`}>{Username}</Link>
          </div>
          <p className="comment-text">{Content}</p>
      </div>
  )
}

export default Reply;