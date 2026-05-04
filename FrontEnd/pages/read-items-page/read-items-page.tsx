import { useEffect, useState, useMemo } from "react";
import { MultipleLinesList } from "../../widgets/list/multiple-lines-list";
import { SearchField } from "../../shared/components/search/search-field";
import { Pagination } from "../../shared/components/pagination/pagination";
import { MangaCollectionManager } from "../../features/MangaCatalogManager";
import type { ReadItem } from "../../entities/model/read-item/read-item";
import "./read-items-page.css";

export const ReadItemsPage = () => {
  const collectionManager = useMemo(() => new MangaCollectionManager(), []);
  const [readItems, setReadItems] = useState<ReadItem[]>([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [maxPage, setMaxPage] = useState(1);

  useEffect(() => {
    const loadData = async () => {
      await updateReadItems();
    };
    loadData();
  }, [collectionManager]);

  const updateReadItems = async () => {
    await collectionManager.loadItems();
    setReadItems(await collectionManager.getItems());
    syncPagination();
  };

  const handleMove = async (direction: "left" | "right") => {
    if (direction === "left") await collectionManager.moveLeft();
    else await collectionManager.moveRight();

    await updateReadItems();
  };

  const searchReadItemsByTitle = async (title: string) => {
    const data = await collectionManager.find(title);
    setReadItems(data);
  };

  const syncPagination = () => {
    setCurrentPage(collectionManager.getCurrentPageNumber());
    setMaxPage(collectionManager.getPageMaxNumber());
  };

  const handlePageChange = async (page: number) => {
    collectionManager.setCurrentPageNumber(page);
    await updateReadItems();
    syncPagination();
  };

  return (
    <div className="page">
      <SearchField onSearch={searchReadItemsByTitle} />
      <MultipleLinesList items={readItems} />
      <Pagination
        onRightArrowClick={() => handleMove("right")}
        onLeftArrowClick={() => handleMove("left")}
        currentPage={currentPage}
        maxPageNumber={maxPage}
        onPageClick={handlePageChange}
      />
    </div>
  );
};
