export type UserRef = { id: string; username: string };
export type MovieRef = { id: number; title: string };

export type Post = {
    id: string;
    title: string;
    body: string;
    movie: MovieRef;
    author: UserRef;
    createdAt: string; // ISO
};

export type Comment = {
    id: string;
    postId: string;
    author: UserRef;
    text: string;
    createdAt: string; // ISO
};
