import { useState } from "react";
import "./pagination-input.css";

const PageInput = ({ onConfirm }: { onConfirm: (page: number) => void }) => {
  const [val, setVal] = useState("");

  const handleKeyDown = (e: React.KeyboardEvent) => {
    if (e.key === "Enter" && val) {
      const num = parseInt(val);
      if (!isNaN(num)) onConfirm(num);
    }
  };

  return (
    <input
      type="number"
      className="pagination-input"
      autoFocus
      value={val}
      onChange={(e) => setVal(e.target.value)}
      onKeyDown={handleKeyDown}
      onBlur={() => {
        const num = parseInt(val);
        if (!isNaN(num)) onConfirm(num);
      }}
    />
  );
};

export default PageInput;
