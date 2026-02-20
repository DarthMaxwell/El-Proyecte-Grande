import { Link } from "react-router-dom";
import "./PageNotFound.css";

export default function PageNotFound() {
    return (
        <main className="nf404">
            <section className="nf404__wrap">
                <h1 className="nf404__title">Page not found 404</h1>

                <img className="nf404__image" src="/pagofinted.png" alt="Bogos Binted" />

                <Link to="/" className="nf404__link">
          <span className="nf404__linkText">
            <span className="nf404__linkName">Main Page</span>
            <span className="nf404__arrow" aria-hidden="true">→</span>
          </span>
                </Link>
            </section>
        </main>
    );
}
