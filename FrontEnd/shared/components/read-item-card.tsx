import { type ReadItem } from "../../entities/model/read-item/read-item";
import "./read-item-card.css";

interface ReadItemCardProps {
  data: ReadItem;
}

export const ReadItemCard = ({ data }: ReadItemCardProps) => {
  return (
    <div className="read-item-card">
      <div className="read-item-image">
        <img src="../public/vagabond.png" alt="main image" />
      </div>
      <div className="read-item-title">
        <h3>{data.title}</h3>
      </div>
    </div>
  );
};
