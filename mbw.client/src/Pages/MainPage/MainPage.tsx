import SearchBar from "../../Components/SearchBar/SearchBar";
import { useEffect, useState } from "react";
import "./MainPage.css";
import PostList from "../../Components/PostList/PostList.tsx";
import type {Post, PostData, MovieData} from "../../Types/Types.tsx";

function MainPage() {
    const [postData, setPostData] = useState<PostData[]>([]);
    const [isLoading, setIsLoading] = useState(true);
    
        const refetch = async () => {
            setIsLoading(true);
            try {
                // Get top 5 movies
                const moviesResponse = await fetch("/api/movie/topfive");
                if (!moviesResponse.ok) throw new Error(`HTTP ${moviesResponse.status}`);
                const movies: MovieData[] = await moviesResponse.json();

                // For each movie, fetch its posts
                const allPostData: PostData[] = [];

                for (const movie of movies) {
                    try {
                        const postsResponse = await fetch(`/api/posts/${movie.id}`);
                        if (postsResponse.ok) {
                            const posts: Post[] = await postsResponse.json();

                            // Add movie info to each post
                            posts.forEach(post => {
                                allPostData.push({
                                    Id: post.id,
                                    MovieTitle: movie.title,
                                    Username: post.username,
                                    Content: post.content,
                                    Title: post.title,
                                    MovieId: movie.id,
                                    MovieGenre: movie.genre,
                                    MovieLength: movie.length,
                                    MovieReleaseDate: movie.releaseDate,
                                });
                            });
                        }
                    } catch (err) {
                        console.error(`Failed to load posts for movie ${movie.id}`, err);
                    } 
                }

                setPostData(allPostData);
            } catch (err) {
                console.error("Failed to load movies and posts", err);
                setPostData([]);
            } finally {
                setIsLoading(false);
            }
        }
        useEffect(() => {
        refetch();
    }, []);
    

    return (
        <div className="mainPage">
            <div className="searchRow">
                <div className="searchWrap">
                    <SearchBar/>
                </div>
            </div>

            <PostList posts={postData} loading={isLoading} refetch={refetch}/>
        </div>
    );
}

export default MainPage;
