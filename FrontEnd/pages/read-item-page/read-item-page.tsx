import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { MangaDetailsPage } from "../../widgets/manga-details-page/manga-details-page";
import { ReadItemApi } from "../../utils/api/read-item-api";
import { type ReadItem } from "../../entities/model/read-item/read-item";
import "./read-item-page.css";

export const ReadItemPage = () => {
  const { id } = useParams<{ id: string }>();
  const [readItem, setReadItem] = useState<ReadItem | null>(null);
  const [loading, setLoading] = useState<boolean>(true);

  useEffect(() => {
    if (!id) return;

    const loadData = async () => {
      try {
        setLoading(true);
        const data = await ReadItemApi.getReadItem(id);
        setReadItem(data);
      } catch (error) {
        console.error("Failed to fetch read item:", error);
      } finally {
        setLoading(false);
      }
    };

    loadData();
  }, [id]);

  if (loading) return <div>Loading...</div>;
  if (!readItem) return <div>Item not found</div>;

  return (
    <div className="read-item-page">
      <MangaDetailsPage readItem={readItem} />
    </div>
  );
};
