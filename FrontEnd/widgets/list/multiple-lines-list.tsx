import { type ReadItem } from "../../entities/model/read-item/read-item";
import { ReadItemCard } from "../../shared/components/read-item-card/read-item-card";
import "./multiple-lines-list.css";

interface MultipleLinesListProps {
  items: ReadItem[];
}

export const MultipleLinesList = ({ items }: MultipleLinesListProps) => {
  return (
    <div className="multiple-lines-list">
      {items.map((readItem) => (
        <ReadItemCard key={readItem.id} data={readItem} />
      ))}
    </div>
  );
};
