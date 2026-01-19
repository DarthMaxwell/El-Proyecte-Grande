import SearchBar from "../../Components/SearchBar/SearchBar.tsx";
import {useEffect, useMemo, useState} from "react";

interface Movie {
    title: string;
    id : number;
    length: number;
}

function MainPage() {
        const [movies, setMovies] = useState<Movie[]>([]);
        
        useEffect(() => {
            async function loadMovies() {
                try {
                    const response = await fetch("/api/movie");
                    if(!response.ok) throw new Error(`HTTP ${response.status}`);
                    const data: Movie[] = await response.json();
                    setMovies(data);
                } catch (err) {
                    console.error("Failed to load movies", err);
                    setMovies([]);
                }
            }
            loadMovies();
        }, []);
        
        const sampleSuggestions = useMemo(() => movies.map(m => m.title), [movies]);

        const handleSearch = (query: string) => {
            console.log("Searching for:", query);
            
            const matches = movies.filter((m => m.title.toLowerCase().includes(query.toLowerCase()))
            );
            console.log("mathces",matches);
        };
        return (
            <>
                <h1>MainPage</h1>
                <div style={{padding: "20px"}}>
                    <SearchBar placeholder="Type to search..."
                               onSearch={handleSearch}
                               suggestions={sampleSuggestions}
                    />
                </div>
            </>
        );
    }
export default MainPage;