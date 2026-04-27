import { useEffect, useState, useMemo } from "react";
import { MultipleLinesList } from "../widgets/list/multiple-lines-list";
import { Pagination } from "../widgets/collection/pagination";
import { MangaCollectionManager } from "../features/MangaCatalogManager";
import { ReadItem } from "../entities/model/read-item/read-item";
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

  return (
    <div className="page">
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
