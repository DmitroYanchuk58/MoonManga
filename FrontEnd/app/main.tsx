import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "./clear-default-styles.css";
import "./main.css";
import "../public/font/fonts.css";
import App from "./App.tsx";

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <App />
  </StrictMode>,
);
