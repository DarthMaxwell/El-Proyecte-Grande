import { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";

type Post = {
    id: string;
    title: string;
    body: string;
    movie: { id: number; title: string };
    author: { id: string; username: string };
};

export default function PostPage() {
    const { movieId } = useParams();
    const [post, setPost] = useState<Post | null>(null);

    useEffect(() => {
        async function load() {
            const res = await fetch(`/api/Posts/${movieId}`);
            if (res.ok) setPost(await res.json());
            else setPost(null);
        }
        if (movieId) load();
    }, [movieId]);

    if (!post) return <div style={{ padding: 20 }}>Post not found.</div>;

    return (
        <div style={{ padding: 20, maxWidth: 850, margin: "0 auto" }}>
            <h1>{post.title}</h1>

            <div style={{ color: "#666", display: "flex", gap: 8, flexWrap: "wrap" }}>
        <span>
          By <Link to={`/users/${post.author.id}`}>{post.author.username}</Link>
        </span>
                <span>•</span>
                <span>
          Movie: <Link to={`/movies/${post.movie.id}`}>{post.movie.title}</Link>
        </span>
            </div>

            <p style={{ marginTop: 12 }}>{post.body}</p>

            {/* Comments section comes next (with login-only form) */}
        </div>
    );
}
