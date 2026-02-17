import { useEffect, useState, type FormEvent } from "react";
import { Link, useNavigate } from "react-router-dom";
import "./CreatePostPage.css";

type MovieRef = { id: number; title: string };
type Post = { id: number; movieId: number; title: string; body: string };

const API_BASE = "http://localhost:5132";

export default function CreatePostPage() {
    const navigate = useNavigate();

    const [movies, setMovies] = useState<MovieRef[]>([]);
    const [movieId, setMovieId] = useState<number | "">("");
    const [title, setTitle] = useState("");
    const [body, setBody] = useState("");

    const [loadingMovies, setLoadingMovies] = useState(true);
    const [submitting, setSubmitting] = useState(false);
    const [error, setError] = useState<string>("");
    
        const refetch = async () => {
            setLoadingMovies(true);
            setError("");

            try {
                const res = await fetch(`${API_BASE}/api/movie`);
                if (!res.ok) throw new Error(`HTTP ${res.status}`);

                const data = await res.json();
                setMovies(data.map((m: any) => ({id: m.id, title: m.title})));
            } catch (err) {
                console.error("loadMovies failed:", err);
                // fallback so you still have something to select
                setMovies([{id: 7, title: "Inception"}]);
            } finally {
                setLoadingMovies(false);
            }
        }
        useEffect(() => {
        refetch();
    }, []);

    async function submit(e: FormEvent) {
        e.preventDefault();
        setError("");

        if (movieId === "" || !title.trim() || !body.trim()) {
            setError("Please select a movie, enter a title, and write a body.");
            return;
        }

        const token = localStorage.getItem("token");
        if (!token) {
            setError("You must be logged in to create a post.");
            navigate("/login");
            return;
        }

        setSubmitting(true);
        try {
            const res = await fetch(`${API_BASE}/api/posts`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: `Bearer ${token}`,
                },
                body: JSON.stringify({
                    movieId: Number(movieId),
                    title: title.trim(),
                    content: `${body.trim()}`,
                }),
            });

            if (res.ok) {
                const created: Post = await res.json();
                navigate(`/post/${created.id}`);
                return;
            }

            const text = await res.text();
            setError(text || `Failed to create post (HTTP ${res.status}).`);
        } catch (err) {
            console.error("create post failed:", err);
            setError("Could not reach the server. Please try again.");
        } finally {
            setSubmitting(false);
        }
    }

    return (
        <div className="cp-page">
            <div className="cp-card">
                <header className="cp-header">
                    <h1 className="cp-title">Create post</h1>
                    <p className="cp-subtitle">
                        Choose a movie and write your post. You must be logged in.
                    </p>
                </header>

                {error ? <div className="cp-alert cp-alert--error">{error}</div> : null}

                <form className="cp-form" onSubmit={submit} noValidate>
                    <label className="cp-field">
                        <span className="cp-label">Movie</span>
                        <select
                            className="cp-input"
                            value={movieId}
                            onChange={(e) => setMovieId(e.target.value ? Number(e.target.value) : "")}
                            disabled={loadingMovies || submitting}
                        >
                            <option value="">
                                {loadingMovies ? "Loading movies…" : "Select a movie…"}
                            </option>
                            {movies.map((m) => (
                                <option key={m.id} value={m.id}>
                                    {m.title}
                                </option>
                            ))}
                        </select>
                    </label>

                    <label className="cp-field">
                        <span className="cp-label">Title</span>
                        <input
                            className="cp-input"
                            value={title}
                            onChange={(e) => setTitle(e.target.value)}
                            placeholder="A short, clear title…"
                            disabled={submitting}
                        />
                    </label>

                    <label className="cp-field">
                        <span className="cp-label">Content</span>
                        <textarea
                            className="cp-input cp-textarea"
                            rows={7}
                            value={body}
                            onChange={(e) => setBody(e.target.value)}
                            placeholder="Write your post…"
                            disabled={submitting}
                        />
                    </label>

                    <div className="cp-actions">
                        <Link className="cp-link" to="/">
                            Cancel
                        </Link>

                        <button className="cp-button" type="submit" disabled={submitting}>
                            {submitting ? "Creating…" : "Create"}
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
}

