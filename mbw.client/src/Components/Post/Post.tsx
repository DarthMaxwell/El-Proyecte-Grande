import "./Post.css";
import ReplyList from "../ReplyList/ReplyList.tsx";
import ReplyForm from "../ReplyForm/ReplyForm.tsx";
import {Link} from "react-router-dom";


interface PostProps {
    Id: number;
    MovieTitle: string;
    Username: string;
    Content: string;
};

export default function Post({ Id, MovieTitle, Username, Content }: PostProps) {
    return (
        <div className="Post">
            <p>{MovieTitle}</p>
            <Link to={`/profile/${Username}`} className="username-link">
                <p>{Username}</p>
            </Link>
            <p>{Content}</p>
            
            <ReplyForm/>
            <ReplyList ParentId={Id}/>
        </div>
    );
}

