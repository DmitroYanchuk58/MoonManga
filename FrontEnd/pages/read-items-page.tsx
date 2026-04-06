import { useEffect, useState } from "react";
import { MultipleLinesList } from "../widgets/list/multiple-lines-list";
import { type ReadItem } from "../entities/model/read-item/read-item";
import { ReadItemApi } from "../utils/api/read-item-api";
import "./read-items-page.css";

export const ReadItemsPage = () => {
  const [mangas, setMangas] = useState<ReadItem[]>([]);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const fetchMangas = async () => {
      try {
        setIsLoading(true);
        const data = await ReadItemApi.getAll();
        setMangas(data);
      } catch (error) {
        console.error("Помилка завантаження манги:", error);
      } finally {
        setIsLoading(false);
      }
    };

    fetchMangas();
  }, []);

  if (isLoading) return <div>Завантаження...</div>;

  return (
    <div className="page">
      <h1>Каталог Манги</h1>
      <MultipleLinesList items={mangas} />
    </div>
  );
};
