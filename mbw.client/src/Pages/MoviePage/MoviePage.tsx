import Movie from "../../Components/Movie/Movie";
import PostList from "../../Components/PostList/PostList";

export default function MoviePage() {
    return (
        <div className="MoviePage">
            <Movie Length={10} Title={"MovieTitle"} Director={"Director"} Description={"Here ther eis gonad sdf sf dfsfds fsdfds fsfsd f"} Genre={"Genre"} />
            <p>Create a post</p>
            <PostList/>
        </div>
    );
}
