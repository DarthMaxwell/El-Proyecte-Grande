import Post from "../../Components/Post/Post";

type Post = {
    id: string;
    title: string;
    body: string;
    movie: { id: number; title: string };
    author: { id: string; username: string };
};

export default function PostPage() {
    

    return (
        <Post Id={1} MovieTitle={"Absolute Cinema"} Username={"testing"} Content={"This is not from the database just a test"}/>
    );
}
