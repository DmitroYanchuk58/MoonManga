import { type ReadItem } from "../../entities/model/read-item/read-item";
import { Badge } from "../../shared/components/badge/badge";
import { ChapterList } from "../list/chapter-list/chapter-list";

import "./manga-details-page.css";

interface MangaDetailsPageProps {
  readItem: ReadItem;
}

export const MangaDetailsPage = ({ readItem }: MangaDetailsPageProps) => {
  const imageSrc = readItem.coverImage
    ? `data:image/jpeg;base64,${readItem.coverImage}`
    : "/default-manga.png";

  return (
    <div className="full-block">
      <div className="promo">
        <img src="/default-promo.png" alt="promo" />
      </div>

      <div className="manga-image">
        <img
          src={imageSrc}
          alt={readItem.title}
          onError={(e) => {
            e.currentTarget.src = "/default-manga.png";
          }}
        />
      </div>

      <div className="information">
        <div className="name">
          <h1>{readItem.title}</h1>
        </div>

        <div className="tags">
          {readItem.tags.map((tag) => (
            <Badge key={tag.id || tag.name} label={tag.name}></Badge>
          ))}
        </div>

        <div className="description">
          <p>{readItem.description}</p>
        </div>
      </div>

      <div className="chapters">
        <ChapterList items={readItem.chapters}></ChapterList>
      </div>
    </div>
  );
};
