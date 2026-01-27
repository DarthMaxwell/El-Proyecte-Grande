import { useMemo, useState } from "react";
import { Link } from "react-router-dom";
import "./Post.css";
import { useAuth } from "../../Authenticate/AuthContext";

type User = { id: number; username: string };

type PostData = {
    id: string;
    title: string;
    movie: { id: number; title: string };
    author: User;
    dateISO: string;
    body: string;
    created_at: number;
};

type CommentData = {
    id: string;
    postId: string;
    parentId: string | null;
    author: User;
    text: string;
    created_at: number;
};

type ThreadedComment = CommentData & { children: ThreadedComment[] };

function toThreaded(comments: CommentData[]): ThreadedComment[] {
    const byId = new Map<string, ThreadedComment>(
        comments.map((c) => [c.id, { ...c, children: [] }])
    );

    const roots: ThreadedComment[] = [];
    for (const c of byId.values()) {
        if (c.parentId && byId.has(c.parentId)) {
            byId.get(c.parentId)!.children.push(c);
        } else {
            roots.push(c);
        }
    }

    const sortRecursively = (arr: ThreadedComment[]) => {
        arr.sort((a, b) => a.created_at - b.created_at);
        for (const item of arr) sortRecursively(item.children);
    };
    sortRecursively(roots);

    return roots;
}

function formatDate(iso: string): string {
    const d = new Date(iso);
    return d.toLocaleDateString(undefined, {
        year: "numeric",
        month: "long",
        day: "numeric",
    });
}

function timeAgo(ts: number): string {
    const s = Math.floor((Date.now() - ts) / 1000);
    const mins = Math.floor(s / 60);
    const hours = Math.floor(mins / 60);
    const days = Math.floor(hours / 24);
    if (days > 0) return `${days} day${days === 1 ? "" : "s"} ago`;
    if (hours > 0) return `${hours} hour${hours === 1 ? "" : "s"} ago`;
    if (mins > 0) return `${mins} minute${mins === 1 ? "" : "s"} ago`;
    return "just now";
}

function makeId(): string {
    return typeof crypto !== "undefined" && "randomUUID" in crypto
        ? crypto.randomUUID()
        : `id-${Date.now()}-${Math.random().toString(16).slice(-2)}`;
}

type CommentFormProps = {
    submitLabel: string;
    compact?: boolean;
    disabled?: boolean;
    onSubmit: (data: { text: string }) => void;
};

function CommentForm({ submitLabel, compact, disabled, onSubmit }: CommentFormProps) {
    const [text, setText] = useState("");
    const [error, setError] = useState("");

    function handleSubmit(e: React.FormEvent) {
        e.preventDefault();
        setError("");

        if (disabled) return;

        if (!text.trim()) {
            setError("Please enter a comment");
            return;
        }

        onSubmit({ text: text.trim() });
        setText("");
    }

    return (
        <form className="commentForm" onSubmit={handleSubmit}>
      <textarea
          className="commentTextArea"
          value={text}
          onChange={(e) => setText(e.target.value)}
          rows={compact ? 2 : 3}
          placeholder={compact ? "Write a reply..." : "Write a comment..."}
          disabled={disabled}
      />

            <div className="commentFormRow">
                <button className="primaryButton" type="submit" disabled={disabled}>
                    {submitLabel}
                </button>
            </div>

            {error ? <div className="formError">{error}</div> : null}
        </form>
    );
}

type CommentItemProps = {
    comment: ThreadedComment;
    depth: number;
    canReply: boolean;
    onReply: (parentId: string, data: { text: string }) => void;
};

function CommentItem({ comment, depth, canReply, onReply }: CommentItemProps) {
    const [replying, setReplying] = useState(false);

    return (
        <div className="comment" style={{ marginLeft: depth * 12 }}>
            <div className="commentHeader">
                <strong className="commentAuthor">{comment.author.username}</strong>
                <span className="commentTime">{timeAgo(comment.created_at)}</span>
            </div>

            <p className="commentText">{comment.text}</p>

            {canReply ? (
                <button
                    type="button"
                    className="linkButton"
                    onClick={() => setReplying((v) => !v)}
                >
                    {replying ? "Cancel" : "Reply"}
                </button>
            ) : (
                <span className="mutedText">
          <Link to="/login">Log in</Link> to reply
        </span>
            )}

            {replying && canReply ? (
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
    canReply: boolean;
    onReply: (parentId: string, data: { text: string }) => void;
};

function CommentList({ comments, depth, canReply, onReply }: CommentListProps) {
    return (
        <ul className="commentList">
            {comments.map((c) => (
                <li key={c.id} className="commentListItem">
                    <CommentItem comment={c} depth={depth} canReply={canReply} onReply={onReply} />
                    {c.children.length > 0 ? (
                        <CommentList
                            comments={c.children}
                            depth={depth + 1}
                            canReply={canReply}
                            onReply={onReply}
                        />
                    ) : null}
                </li>
            ))}
        </ul>
    );
}

export default function Post() {
    const { user } = useAuth();

    // Convert AuthUser -> User (id fallback for now)
    const currentUser: User | null = user
        ? {
            id: user.id ?? Number(localStorage.getItem("userId") ?? 0),
            username: user.username,
        }
        : null;

    const isLoggedIn = !!currentUser;

    const post = useMemo<PostData>(
        () => ({
            id: "post-1",
            title: "A Blog Post Component in React",
            movie: { id: 7, title: "Inception" },
            author: { id: 20, username: "You" },
            dateISO: "2026-01-19",
            body:
                "Veniam incididunt eiusmod minim nostrud ea lorem aliqua aliquip...\n\n" +
                "Quis adipiscing sed veniam eiusmod consequat amet magna...\n\n" +
                "Do labore lorem dolor minim consequat commodo exercitation...\n",
            created_at: new Date("2026-01-19").getTime(),
        }),
        []
    );

    const [comments, setComments] = useState<CommentData[]>(() => [
        {
            id: "c1",
            postId: "post-1",
            parentId: null,
            author: { id: 34, username: "Ada" },
            text: "Looks clean!",
            created_at: Date.now() - 1000 * 60 * 60,
        },
    ]);

    const threaded = useMemo(() => toThreaded(comments), [comments]);

    function addComment(text: string, parentId: string | null) {
        if (!currentUser) return;

        const newComment: CommentData = {
            id: makeId(),
            postId: post.id,
            parentId,
            author: currentUser,
            text,
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

                {!isLoggedIn ? (
                    <p className="emptyState">
                        You must <Link to="/login">log in</Link> to comment or reply.
                    </p>
                ) : null}

                <CommentForm
                    submitLabel="Add comment"
                    disabled={!isLoggedIn}
                    onSubmit={({ text }) => addComment(text, null)}
                />

                {threaded.length === 0 ? (
                    <p className="emptyState">Be the first to comment.</p>
                ) : (
                    <CommentList
                        comments={threaded}
                        depth={0}
                        canReply={isLoggedIn}
                        onReply={(parentId, { text }) => addComment(text, parentId)}
                    />
                )}
            </section>
        </div>
    );
}

