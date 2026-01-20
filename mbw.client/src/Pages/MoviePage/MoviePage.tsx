import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";

interface Movie {
    id: number;
    title: string;
    length: number;
}

export default function MoviePage() {
    const { movieId } = useParams();
    const [movie, setMovie] = useState<Movie | null>(null);

    useEffect(() => {
        async function load() {
            try {
                const response = await fetch(`/api/movie/${movieId}`);
                if (!response.ok) throw new Error(`HTTP ${response.status}`);
                const data: Movie = await response.json();
                setMovie(data);
            } catch (err) {
                console.error("Failed to load movie:", err);
                setMovie(null);
            }
        }
        if (movieId) load();
    }, [movieId]);

    if (!movie) return <h1 style={{ padding: 20 }}>Movie not found.</h1>;

    return (
        <div style={{ padding: 20 }}>
            <h1>{movie.title}</h1>
            <p>Length: {movie.length} min</p>
        </div>
    );
}
