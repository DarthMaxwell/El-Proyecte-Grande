export interface PostData {
    Id: number;
    MovieTitle: string;
    MovieId: number;
    MovieReleaseDate: string;
    MovieLength: number;
    MovieGenre: string;
    Username: string;
    Title: string;
    Content: string;
}

export interface MovieData {
    id: number;
    releaseDate: string;
    length: number;
    title: string;
    director: string;
    description: string;
    genre: string;
}

export interface Post {
    id: number;
    username: string;
    movieId: number;
    content: string;
    title: string;
}

export interface Reply {
    id: number,
    parentPostId: number,
    content: string;
    username: string;
}