import "./Reply.css"
import {Link} from "react-router-dom";

interface ReplyProps {
    Username: string;
    Content: string;
}

const Reply = ({Username, Content}: ReplyProps) => {
  return (
      <div className="reply">
          <p>{Content} - <Link to={`/profile/${Username}`} className="username-link">
              <p>{Username}</p>
          </Link></p>
      </div>
  )
}

export default Reply;