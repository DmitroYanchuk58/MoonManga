import { type ReadItem } from "../../entities/model/read-item/read-item";
import "./read-item-card.css";

interface ReadItemCardProps {
  data: ReadItem;
}

export const ReadItemCard = ({ data }: ReadItemCardProps) => {
  const imageSrc = data.coverImage
    ? `data:image/jpeg;base64,${data.coverImage}`
    : "/vagabond.png";

  return (
    <div className="read-item-card">
      <div className="read-item-image">
        <img
          src={imageSrc}
          alt={data.title}
          onError={(e) => {
            e.currentTarget.src = "/vagabond.png";
          }}
        />
      </div>

      <div className="read-item-title">
        <h3>{data.title}</h3>
      </div>
    </div>
  );
};
