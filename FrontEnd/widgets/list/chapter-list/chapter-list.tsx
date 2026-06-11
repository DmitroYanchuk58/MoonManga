import { type Chapter } from "../../../entities/model/chapter/chapter";
import "./chapter-list.css";

interface ChapterListProps {
  items: Chapter[];
}

export const ChapterList = ({ items }: ChapterListProps) => {
  const sortedChapters = [...items].sort((a, b) => a.order - b.order);

  return (
    <div className="chapter-list">
      {sortedChapters.map((chapter) => (
        <div className="chapter" key={chapter.id || chapter.order}>
          <label>
            Ch. {chapter.order} - {chapter.title}
          </label>
        </div>
      ))}
    </div>
  );
};
