import { useState } from "react";
import type { ChangeEvent } from "react";
import "./search-field.css";

interface SearchFieldProps {
  onSearch: (query: string) => void;
}

export const SearchField = ({ onSearch }: SearchFieldProps) => {
  const [text, setText] = useState("");

  const handleSearch = () => {
    onSearch(text);
    setText("");
  };

  return (
    <div className="search-block">
      <div className="search-field">
        <input
          type="text"
          value={text}
          onChange={(e: ChangeEvent<HTMLInputElement>) =>
            setText(e.target.value)
          }
        />

        <button onClick={handleSearch}>
          <img src="/icons/search.svg" alt="search" className="search-icon" />
        </button>
      </div>
    </div>
  );
};
