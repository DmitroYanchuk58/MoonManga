import { type ReadItem } from "../../entities/model/read-item/read-item";
import { ReadItemType } from "../../entities/model/read-item/read-item-type";
import "./read-item-card.css";

interface ReadItemCardProps {
  data: ReadItem;
}

export const ReadItemCard = ({ data }: ReadItemCardProps) => {
  const typeName = ReadItemType[data.type];
  return (
    <div className="read-item-card">
      <div className="read-item-image">
        <img src="../public/vagabond.png" alt="main image" />
      </div>
      <div className="read-item-info">
        <div className="read-item-title">
          <h3>{data.title}</h3>
        </div>
        <div className="read-item-type">
          <h4>{data.type}</h4>
        </div>
      </div>
    </div>
  );
};
