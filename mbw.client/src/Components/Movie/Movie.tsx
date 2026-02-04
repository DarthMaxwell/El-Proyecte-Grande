import "./Movie.css"
import type {MovieData} from "../../Types/Types.tsx";

interface MovieProps {
    movie: MovieData
}

export default function Movie({movie}: MovieProps) {
    return (
        <div className="Movie">
            <h1>{movie.title} ({movie.genre})</h1>
            <p>{movie.description}</p>
            <p>{movie.director}</p>
            <p>{movie.length}</p>
            <p>{movie.releaseDate}</p>
        </div>
    );
}