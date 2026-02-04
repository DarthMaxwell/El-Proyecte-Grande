import "./ReplyForm.css"

interface ReplyFormProps {
    closeForm: () => void,
    postId: number
}

export default function ReplyForm({closeForm, postId}: ReplyFormProps) {
    return (
        <div className="comment-form" id="commentForm">
            <form>
                <div className="form-group">
                    <label className="form-label" htmlFor="comment-text">Comment</label>
                    <textarea id="comment-text" className="form-textarea"
                              placeholder="Share your thoughts about this post..."></textarea>
                </div>
                <div className="form-actions">
                    <button type="submit" className="submit-btn">Post Comment</button>
                    <button type="button" className="cancel-btn" onClick={closeForm}>Cancel</button>
                </div>
            </form>
        </div>
    );
}