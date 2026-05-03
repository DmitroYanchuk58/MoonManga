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

  useEffect(() => {
    const loadData = async () => {
      await updateReadItems();
    };
    loadData();
  }, [collectionManager]);

  const updateReadItems = async () => {
    const data = await collectionManager.getItems();
    setReadItems(data);
  };

  const searchReadItemsByTitle = async (title: string) => {
    const data = await collectionManager.find(title);
    setReadItems(data);
  };

  return (
    <div className="page">
      <SearchField onSearch={searchReadItemsByTitle} />
      <MultipleLinesList items={readItems} />
      <Pagination
        onPageClick={(page) => {
          collectionManager.setCurrentPageNumber(page);
          updateReadItems();
        }}
        currentPage={collectionManager.getCurrentPageNumber()}
        maxPageNumber={collectionManager.getPageMaxNumber()}
        onRightArrowClick={async () => {
          await collectionManager.moveRight();
          await updateReadItems();
        }}
        onLeftArrowClick={async () => {
          await collectionManager.moveLeft();
          await updateReadItems();
        }}
      />
    </div>
  );
};
