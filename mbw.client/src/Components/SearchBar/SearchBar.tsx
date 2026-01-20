import React, {type FormEvent, useState} from 'react';
import "./SearchBar.css"

interface SearchBarProps {
    placeholder?: string;
    onSearch: (query: string) => void;
    suggestions?: string[];
}
const SearchBar: React.FC<SearchBarProps> = ({placeholder= "Search...", onSearch, suggestions = []}) => {
    const [searchQuery, setSearchQuery] = useState('');
    const [filteredSuggestions, setSuggestions] = useState<string[]>([]);

    const handleSearchChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const value = e.target.value;
        setSearchQuery(value);
        if(suggestions.length > 0 && value.trim().length > 0) {
            const filtered = suggestions.filter((item) => item.toLowerCase().includes(value.toLowerCase())
            );
            setSuggestions(filtered);
        } else {
            setSuggestions([]);
        }
    };
    const handleSubmit = (e: FormEvent) => {
        e.preventDefault();
        const trimmedQuery = searchQuery.trim();
        if(trimmedQuery) onSearch(trimmedQuery);
    };
    const handleSuggestionClick = (suggestions : string) =>{
        setSearchQuery(suggestions);
        setSuggestions([]);
        onSearch(suggestions);
    }
    return (
        <div className="search-bar-container">
            <form onSubmit={handleSubmit}>
            <input
                type="text"
                placeholder={placeholder}
                value={searchQuery}
                onChange={handleSearchChange}
                className="search-bar-input"
            />
            </form>
            {filteredSuggestions.length > 0 && (
                <ul className="suggestions-list">
            {filteredSuggestions.map((item, index) => (<li key = {index} 
                                                           onMouseDown={(e) => e.preventDefault()}
                                                           onClick={() => handleSuggestionClick(item)}> 
                {item}
            </li>
            ))} 
            </ul>
                )}
        </div>
    );
};
export default SearchBar;