import { type ReadItem } from "../../../entities/model/read-item/read-item";
import { useNavigate } from "react-router-dom";
import "./read-item-card.css";

interface ReadItemCardProps {
  data: ReadItem;
}

export const ReadItemCard = ({ data }: ReadItemCardProps) => {
  const navigate = useNavigate();

  const handleCardClick = () => {
    navigate(`/read-items/${data.id}`);
  };

  const imageSrc = data.coverImage
    ? `data:image/jpeg;base64,${data.coverImage}`
    : "/default-manga.png";

  return (
    <div className="read-item-card" onClick={handleCardClick}>
      <div className="read-item-image">
        <img
          src={imageSrc}
          alt={data.title}
          onError={(e) => {
            e.currentTarget.src = "/default-manga.png";
          }}
        />
      </div>

      <div className="read-item-title">
        <h3>{data.title}</h3>
      </div>
    </div>
  );
};
