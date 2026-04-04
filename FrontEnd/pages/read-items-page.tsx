import { MultipleLinesList } from "../widgets/list/multiple-lines-list";
import { type ReadItem } from "../entities/model/read-item/read-item";
import { ReadItemType } from "../entities/model/read-item/read-item-type";

export const ReadItemsPage = () => {
  const randomMangas: ReadItem[] = [
    {
      id: "1",
      title: "Berserk",
      type: ReadItemType.Manga,
    },
    {
      id: "2",
      title: "Solo Leveling",
      type: ReadItemType.Manhwa,
    },
    {
      id: "3",
      title: "Vagabond",
      type: ReadItemType.Manga,
    },
    {
      id: "4",
      title: "The Beginning After the End",
      type: ReadItemType.Manhwa,
    },
  ];

  return (
    <div className="page-container">
      <h1>Мій Список Читання</h1>
      <MultipleLinesList items={randomMangas} />
    </div>
  );
};
