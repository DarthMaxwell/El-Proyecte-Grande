import "./Movie.css"
import type {MovieData} from "../../Types/Types.tsx";

interface MovieProps {
    movie: MovieData
}

export default function Movie({movie}: MovieProps) {
    return (
        <div className="Movie">
            <h1>{movie.title} ({movie.genre})</h1>
            <p>Description: {movie.description}</p>
            <p>Director: {movie.director}</p>
            <p>Length: {movie.length} min</p>
            <p>Release date: {movie.releaseDate}</p>
        </div>
    );
}