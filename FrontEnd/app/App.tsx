import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import { ReadItemsPage } from "../pages/read-items-page/read-items-page";
import { ReadItemPage } from "../pages/read-item-page/read-item-page";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/read-items" element={<ReadItemsPage />} />

        <Route path="/read-items/:id" element={<ReadItemPage />} />

        <Route path="/" element={<Navigate to="/read-items" replace />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
