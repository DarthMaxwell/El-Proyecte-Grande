import { useEffect, useState, type FormEvent } from "react";
import { useNavigate } from "react-router-dom";
import type { MovieRef, Post } from "../../Types/Types";

export default function CreatePostPage() {
    const navigate = useNavigate();

    const [movies, setMovies] = useState<MovieRef[]>([]);
    const [movieId, setMovieId] = useState<number | null>(null);
    const [title, setTitle] = useState("");
    const [body, setBody] = useState("");

    useEffect(() => {
        async function loadMovies() {
            try {
                const res = await fetch("/api/movie");
                if (!res.ok) throw new Error(`HTTP ${res.status}`);
                const data = await res.json();
                setMovies(data.map((m: any) => ({ id: m.id, title: m.title })));
            } catch {
                setMovies([{ id: 7, title: "Inception" }]);
            }
        }
        loadMovies();
    }, []);

    async function submit(e: FormEvent) {
        e.preventDefault();
        if (movieId === null || !title.trim() || !body.trim()) return;

        const res = await fetch("/api/posts", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ title, body, movieId }),
        });

        if (res.ok) {
            const created: Post = await res.json();
            navigate(`/posts/${created.id}`);
            return;
        }

        navigate("/");
    }

    return (
        <div style={{ padding: 20, maxWidth: 700, margin: "0 auto" }}>
            <h1>Create post</h1>

            <form onSubmit={submit} style={{ display: "grid", gap: 12 }}>
                <label>
                    Movie
                    <select
                        value={movieId ?? ""}
                        onChange={(e) => setMovieId(e.target.value ? Number(e.target.value) : null)}
                    >
                        <option value="">Select a movie…</option>
                        {movies.map((m) => (
                            <option key={m.id} value={m.id}>
                                {m.title}
                            </option>
                        ))}
                    </select>
                </label>

                <label>
                    Title
                    <input value={title} onChange={(e) => setTitle(e.target.value)} />
                </label>

                <label>
                    Body
                    <textarea rows={6} value={body} onChange={(e) => setBody(e.target.value)} />
                </label>

                <button type="submit">Create</button>
            </form>
        </div>
    );
}

