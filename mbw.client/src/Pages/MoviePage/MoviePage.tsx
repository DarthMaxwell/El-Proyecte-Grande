import { useState, useEffect } from "react";

// This will be in the tpye fiels so we dont have to reuse it
interface Movie {
    id: number;
    title: string;
    length: number;
}

function MoviePage() {
    const [movies, setMovies] = useState<Movie[]>([]);


    async function populateData() {
        try {
            const response = await fetch('/api/movie'); // <-- note leading /
            if (!response.ok) throw new Error(`HTTP ${response.status}`);
            const data = await response.json();
            setMovies(data);
        } catch (err) {
            console.error('Failed to load movies:', err);
            setMovies([]); // optional fallback
        }
    }


    useEffect(() => {
        populateData();
    }, []);
    
    return (
        <>
            <h1>MoviePage</h1>
            <ul>
                {movies.map((movie) => (
                    <li key={movie.id}>
                        <strong>{movie.title}</strong> — {movie.length} min
                    </li>
                ))}
            </ul>
        </>
    );
    
}
export default MoviePage;