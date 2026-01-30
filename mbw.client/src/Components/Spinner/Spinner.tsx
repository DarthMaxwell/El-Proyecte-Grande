import { ClipLoader } from "react-spinners";
import "./Spinner.css"

type Props = {
    isLoading?: boolean
};

const Spinner = ({ isLoading = true }: Props) => {
    return (
        <div id="Spinner">
            <ClipLoader
                color="#0b1220"
                loading={isLoading}
                size={35}
                aria-label="Loading Spinner"
                data-testid="loader"
            />
        </div>
    );
}

export default Spinner;