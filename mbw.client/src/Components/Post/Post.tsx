import "./Post.css";
import ReplyList from "../ReplyList/ReplyList.tsx";
import ReplyForm from "../ReplyForm/ReplyForm.tsx";


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
            <p>{Username}</p>
            <p>{Content}</p>
            
            <ReplyForm/>
            <ReplyList ParentId={Id}/>
        </div>
    );
}

