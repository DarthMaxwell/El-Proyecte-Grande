import React, {type FormEvent, useEffect, useState} from 'react';
import "./SearchBar.css"
import {useNavigate} from "react-router-dom";

interface MovieOptions {
    id: number;
    title: string;
}

const SearchBar = () => {
    const [searchQuery, setSearchQuery] = useState('');
    const [filteredSuggestions, setFilteredSuggestions] = useState<MovieOptions[]>([]);
    const [allMovieTitles, setAllMovieTitles] = useState<MovieOptions[]>([]);
    const navigate = useNavigate();

    useEffect(() => {
        async function loadAllMovieTitles() {
            try {
                const response = await fetch("/api/movie");
                if (!response.ok) throw new Error(`HTTP ${response.status}`);
                const movies: MovieOptions[] = await response.json();

                setAllMovieTitles(movies.map(m => ({ id: m.id, title: m.title })));
            } catch (err) {
                console.error("Failed to load movie titles", err);
                setAllMovieTitles([]);
            }
        }

        loadAllMovieTitles();
    }, []);

    const handleSearchChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const value = e.target.value;
        setSearchQuery(value);

        if (value.trim().length > 0) {
            const filtered = allMovieTitles.filter((movie) =>
                movie.title.toLowerCase().includes(value.toLowerCase())
            );
            setFilteredSuggestions(filtered);
        } else {
            setFilteredSuggestions([]);
        }
    };

    const handleSubmit = (e: FormEvent) => {
        e.preventDefault();
        const trimmedQuery = searchQuery.trim();

        if (trimmedQuery && filteredSuggestions.length > 0) {
            navigate(`/movie/${filteredSuggestions[0].id}`);
            setSearchQuery('');
            setFilteredSuggestions([]);
        }
    };

    const handleSuggestionClick = (movie: MovieOptions) => {
        navigate(`/movie/${movie.id}`);
        setSearchQuery('');
        setFilteredSuggestions([]);
    };

    return (
        <div className="search-bar-container">
            <form onSubmit={handleSubmit}>
                <input
                    type="text"
                    placeholder="Type to search..."
                    value={searchQuery}
                    onChange={handleSearchChange}
                    className="search-bar-input"
                />
            </form>
            {filteredSuggestions.length > 0 && (
                <ul className="suggestions-list">
                    {filteredSuggestions.map((movie) => (
                        <li key={movie.id}>
                            <button
                                type="button"
                                onClick={() => handleSuggestionClick(movie)}
                                className="suggestion-item"
                            >
                                {movie.title}
                            </button>
                        </li>
                    ))}
                </ul>

            )}
        </div>
    );
};

export default SearchBar;