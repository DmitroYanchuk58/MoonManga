import { type ReadItem } from "../../entities/model/read-item/read-item";
import "./read-item-card.css";

interface ReadItemCardProps {
  data: ReadItem;
}

export const ReadItemCard = ({ data }: ReadItemCardProps) => {
  return (
    <div className="manga-card">
      <h3>{data.title}</h3>
    </div>
  );
};
