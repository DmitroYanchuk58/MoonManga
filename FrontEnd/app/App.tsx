import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import { CollectionReadItemsPage } from "../pages/collection-read-items-page/collection-read-items-page";
import { SingleReadItemPage } from "../pages/single-read-item-page/single-read-item-page";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/read-items" element={<CollectionReadItemsPage />} />

        <Route path="/read-items/:id" element={<SingleReadItemPage />} />

        <Route path="/" element={<Navigate to="/read-items" replace />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
