import { Link } from "react-router-dom";

type Post = {
    id: string;
    title: string;
    body: string;
    movie: { id: number; title: string };
    author: { id: string; username: string };
};

export default function PostCard({ post }: { post: Post }) {
    return (
        <article style={{ border: "1px solid #ddd", borderRadius: 12, padding: 12 }}>
            <h2 style={{ margin: 0 }}>
                <Link to={`/posts/${post.id}`}>{post.title}</Link>
            </h2>

            <div style={{ marginTop: 6, color: "#666", display: "flex", gap: 8, flexWrap: "wrap" }}>
        <span>
          By <Link to={`/users/${post.author.id}`}>{post.author.username}</Link>
        </span>
                <span>•</span>
                <span>
          Movie: <Link to={`/movies/${post.movie.id}`}>{post.movie.title}</Link>
        </span>
            </div>

            <p style={{ marginTop: 10 }}>{post.body}</p>
        </article>
    );
}


