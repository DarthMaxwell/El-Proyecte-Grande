import { useParams } from "react-router-dom";

export default function UserPage() {
    const { userId } = useParams();
    return (
        <div style={{ padding: 20 }}>
            <h1>User page</h1>
            <p>userId: {userId}</p>
            {/* Later: fetch(`/api/users/${userId}`) */}
        </div>
    );
}
