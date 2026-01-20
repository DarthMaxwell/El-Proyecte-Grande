import React, { useMemo, useState } from "react";
import { Link } from "react-router-dom";
import "./Post.css";

type PostData = {
    id: string;
    title: string;
    movie: {id: number; title: string};
    author: {id: number; username: string};
    dateISO: string;
    body: string;
    created_at: string;
};
type CommentData = {
    id: string;
    postId: string;
    parentId: string | null;
    author: {id: number; username: string};
    text: string;
    created_at: number;
}

type ThreadedComment = CommentData & {children: ThreadedComment[] };

function toThreaded(comments: CommentData[]): ThreadedComment[] {
    const byId = new Map<string, ThreadedComment>(
        comments.map((c) => [c.id, {...c, children: []}])
    );
    
    const roots: ThreadedComment[] = [];
    for (const c of byId.values()) {
        if(c.parentId && byId.has(c.parentId)) {
            byId.get(c.parentId)!.children.push(c);
        } else {
            roots.push(c);
        }
    }
    const sortRecursibely = (arr: ThreadedComment[]) => {
        arr.sort((a, b) => a.created_at - b.created_at);
        for(const item of arr) sortRecursibely(item.children);
    };
    sortRecursibely(roots);
    
    return roots;
}

function formatDate(iso:string): string {
    const d = new Date(iso);
    return d.toLocaleDateString(undefined, {
        year: 'numeric',
        month: 'long',
        day: 'numeric',
    });
}
function timeAgo(ts:number):string {
    const s = Math.floor((Date.now() - ts)/ 1000);
    const mins = Math.floor(s/60);
    const hours = Math.floor(mins / 60);
    const days = Math.floor(hours / 24);
    if(days > 0) return `${days} days ago`;
    if(hours > 0) return `${hours} hours ago`;
    if(mins > 0) return `${mins} minutes ago`;
    return "just now";
}

function makeId(): string {
return typeof crypto !== "undefined" && "randomUUID" in crypto ? crypto.randomUUID(): `id-${Date.now()}-${Math.random().toString(16).slice(-2)}`;

}

type CommentFromProps = {
    submitLabel: string;
    compact?: boolean;
    onSubmit: (data: {author: string; text: string}) => void;
}

function CommentForm({submitLabel, compact, onSubmit}: CommentFromProps) {
    const [author, setAuthor] = useState("");
    const [text, setText] = useState("");
    const [error, setError] = useState("");
    
    function handleSubmit(e: React.FormEvent) {
        e.preventDefault();
        setError("");
        
        if(!author.trim()) {
            setError("Please enter your name");
            return;
        }
        if(!text.trim()) {
            setError("Please enter a comment");
            return;
        }
        onSubmit( {author: author.trim(), text: text.trim()});
        setAuthor("");
        setText("");
    }
    
    return(
        <form className="commentForm" onSubmit={handleSubmit}>
            <div className="commentFormRow">
                <input className="commentInput"
                       value={author}
                       onChange={(e) => setAuthor(e.target.value)}
                       placeholder="Your name"/>
                <button className="primaryButton" type="submit">
                    {submitLabel}
                </button>
            </div>
            <textarea className="commentTextArea"
                      value={text}
                      onChange={(e) => setText(e.target.value)}
                      rows={compact ? 2:3}
                      placeholder={compact ?"Write a reply...": "Write a comment..."} />
            {error ? <div className="formError">{error}</div> : null}
        </form>
    );
}

type CommentItemProps = {
    comment: ThreadedComment;
    depth: number;
    onReply: (parentId:string, data: {author: string; text: string}) => void;
};

function CommentItem({ comment, depth, onReply }: CommentItemProps) {
    const [replying, setReplying] = useState(false);


    return (
        <div className="comment" style={{ marginLeft: depth * 12 }}>
            <div className="commentHeader">
                <strong className="commentAuthor">{comment.author.username}</strong>
                <span className="commentTime">{timeAgo(comment.created_at)}</span>
            </div>

            <p className="commentText">{comment.text}</p>

            <button
                type="button"
                className="linkButton"
                onClick={() => setReplying((v) => !v)}
            >
                {replying ? "Cancel" : "Reply"}
            </button>

            {replying ? (
                <div className="replyBox">
                    <CommentForm
                        compact
                        submitLabel="Post reply"
                        onSubmit={(data) => {
                            onReply(comment.id, data);
                            setReplying(false);
                        }}
                    />
                </div>
            ) : null}
        </div>
    );
}
type CommentListProps = {
    comments: ThreadedComment[];
    depth: number;
    onReply: (parentId:string, data: {author: string; text: string}) => void;
};

function CommentList({ comments, depth, onReply }: CommentListProps) {
    return (
        <ul className="commentList">
            {comments.map((c) => (
                <li key={c.id} className="commentListItem">
                    <CommentItem comment={c} depth={depth} onReply={onReply} />
                    {c.children.length > 0 ? (
                        <CommentList comments={c.children} depth={depth + 1} onReply={onReply} />
                    ) : null}
                </li>
            ))}
        </ul>
    );
}

export default function Post() {
    const post = useMemo<PostData>(
        () => ({
            id: "post-1",
            title: "A Blog Post Component in React",
            movie: {id: 7, title: "Inception"},
            author: {id: 20, username: "You"},
            dateISO: "2026-01-19",
            body: "This is a post",
            created_at: "2026-01-19"
        }),
        []
    );

    const [comments, setComments] = useState<CommentData[]>(() => [
        {
            id: "c1",
            postId: "post-1",
            parentId: null,
            author: {id: 34 , username: "Ada"},
            text: "Looks clean!",
            created_at: Date.now() - 1000 * 60 * 60,
        },
    ]);

    const threaded = useMemo(() => toThreaded(comments), [comments]);

    function addComment(data: { author: {id: number, username: string}; text: string }, parentId: string | null) {
        const newComment: CommentData = {
            id: makeId(),
            postId: post.id,
            parentId,
            author: data.author,
            text: data.text,
            created_at: Date.now(),
        };
        setComments((prev) => [...prev, newComment]);
    }

    return (
        <div className="postPage">
            <article className="card">
                <header className="postHeader">
                    <h1 className="postTitle">
                        <Link className="titleLink" to={`/posts/${post.id}`}>
                            {post.title}
                        </Link>
                    </h1>

                    <div className="postMeta">
  <span>
    By{" "}
      <Link className="metaLink" to={`/users/${post.author.id}`}>
      {post.author.username}
    </Link>
  </span>

                        <span className="dot">•</span>

                        <span>
    Movie:{" "}
                            <Link className="metaLink" to={`/movies/${post.movie.id}`}>
      {post.movie.title}
    </Link>
  </span>

                        <span className="dot">•</span>

                        <time dateTime={post.dateISO}>{formatDate(post.dateISO)}</time>
                    </div>

                </header>

                <section className="postBody">
                    {post.body.split("\n").map((line, idx) => (
                        <p key={idx} className="postParagraph">
                            {line}
                        </p>
                    ))}
                </section>
            </article>

            <section className="card commentsCard">
                <h2 className="sectionTitle">Comments</h2>

                {/* For now, pass an author object. Later you’ll use the logged-in user */}
                <CommentForm
                    submitLabel="Add comment"
                    onSubmit={(data) =>
                        addComment({ author: { id: 999, username: data.author }, text: data.text }, null)
                    }
                />

                {threaded.length === 0 ? (
                    <p className="emptyState">Be the first to comment.</p>
                ) : (
                    <CommentList
                        comments={threaded}
                        depth={0}
                        onReply={(parentId, data) =>
                            addComment({ author: { id: 999, username: data.author }, text: data.text }, parentId)
                        }
                    />
                )}
            </section>
        </div>
    );
}