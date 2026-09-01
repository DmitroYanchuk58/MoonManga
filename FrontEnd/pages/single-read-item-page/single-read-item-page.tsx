import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";

import { MangaDetailsPage } from "../../widgets/manga-details-page/manga-details-page";
import { TopBar } from "../../shared/components/top-bar/top-bar";

import { ReadItemApi } from "../../utils/api/read-item-api";

import { type ReadItem } from "../../entities/model/read-item/read-item";

import "./single-read-item-page.css";

export const SingleReadItemPage = () => {
  const { id } = useParams<{ id: string }>();
  const [readItem, setReadItem] = useState<ReadItem | null>(null);
  const [loading, setLoading] = useState<boolean>(true);

  useEffect(() => {
    if (!id) return;

    const loadData = async () => {
      try {
        setLoading(true);
        const data = await ReadItemApi.getFullInfoReadItem(id);
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
      <TopBar />
      <MangaDetailsPage readItem={readItem} />
    </div>
  );
};
