import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "./styles/variables.css";
import "./styles/clear-default-styles.css";
import "./styles/main.css";
import "./styles/scrollbar.css";
import "../public/font/fonts.css";
import App from "./App.tsx";

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <App />
  </StrictMode>,
);
