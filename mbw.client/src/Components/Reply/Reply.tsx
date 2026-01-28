import "./Reply.css"

interface ReplyProps {
    Username: string;
    Content: string;
}

const Reply = ({Username, Content}: ReplyProps) => {
  return (
      <div className="reply">
          <p>{Content} - {Username}</p>
      </div>
  )
}

export default Reply;