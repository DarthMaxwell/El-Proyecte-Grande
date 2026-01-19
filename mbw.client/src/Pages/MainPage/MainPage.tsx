import SearchBar from "../../Components/SearchBar/SearchBar.tsx";

function MainPage() {
        const sampleSuggestions = ["Apple", "Banana", "Orange", "Mango", "Grapes"];

        const handleSearch = (query: string) => {
            console.log("Searching for:", query);
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