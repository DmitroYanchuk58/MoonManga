import { ReadItem } from "../entities/model/read-item/read-item";
import { ReadItemApi } from "../utils/api/read-item-api";

export class MangaCollectionManager {
  private items: ReadItem[] = [];
  private pageNumber: number = 1;
  private maxPageNumber: number = 101;
  private minPageNumber: number = 1;

  private fetchMangas = async (): Promise<ReadItem[]> => {
    try {
      const data = await ReadItemApi.getCollection(this.pageNumber, 30);
      return data || [];
    } catch (error) {
      console.error("Помилка завантаження манги:", error);
      return [];
    }
  };

  private async loadItems(): Promise<void> {
    const data = await ReadItemApi.getCollection(this.pageNumber, 30);
    this.items = data || [];
  }

  public async getItems(): Promise<ReadItem[]> {
    await this.loadItems();
    return this.items;
  }

  public async moveRight(): Promise<void> {
    if (this.pageNumber < this.maxPageNumber) {
      this.pageNumber++;
      await this.loadItems;
    }
  }

  public async moveLeft(): Promise<void> {
    if (this.pageNumber > this.minPageNumber) {
      this.pageNumber--;
      await this.loadItems;
    }
  }

  public getPageMaxNumber() {
    return this.maxPageNumber;
  }

  public getCurrentPageNumber() {
    return this.pageNumber;
  }

  public setCurrentPageNumber(numberPage: number) {
    this.pageNumber = numberPage;
  }
}
