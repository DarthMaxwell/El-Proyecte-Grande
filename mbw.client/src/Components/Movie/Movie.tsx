import "./Movie.css"

interface MovieProps {
    Length: number;
    Title: string;
    Director: string;
    Description: string;
    Genre: string;
}

export default function Movie({Length, Title, Director, Description, Genre}: MovieProps) {
    return (
        <div className="Movie">
            <h1>{Title} ({Genre})</h1>
            <p>{Description}</p>
            <p>{Director}</p>
            <p>{Length}</p>
        </div>
    );
}