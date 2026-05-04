import type { ReadItem } from "../entities/model/read-item/read-item";
import { ReadItemApi } from "../utils/api/read-item-api";

export class MangaCollectionManager {
  private items: ReadItem[] = [];
  private pageNumber: number = 1;
  private maxPageNumber: number = 101;
  private minPageNumber: number = 1;
  private countReadItemsOnPage: number = 30;

  private async setMaxPageNumber() {
    const count = await ReadItemApi.getReadItemsCount();
    this.maxPageNumber = Math.ceil(count / this.countReadItemsOnPage);
    if (this.maxPageNumber === 0) {
      this.maxPageNumber = 1;
    }
  }

  public async getItems(): Promise<ReadItem[]> {
    await this.loadItems();
    return this.items;
  }

  public async moveRight(): Promise<void> {
    if (this.pageNumber < this.maxPageNumber) {
      this.pageNumber++;
      await this.loadItems();
    }
  }

  public async moveLeft(): Promise<void> {
    if (this.pageNumber > this.minPageNumber) {
      this.pageNumber--;
      await this.loadItems();
    }
  }

  public getCurrentPageNumber() {
    return this.pageNumber;
  }

  public setCurrentPageNumber(numberPage: number) {
    this.pageNumber = numberPage;
  }

  public async find(title: string) {
    const data = await ReadItemApi.findReadItemByTitle(title);
    return data;
  }

  public async loadItems(): Promise<void> {
    const data = await ReadItemApi.getCollection(
      this.pageNumber,
      this.countReadItemsOnPage,
    );
    this.items = data || [];

    const count = await ReadItemApi.getReadItemsCount();
    this.maxPageNumber = Math.ceil(count / this.countReadItemsOnPage) || 1;
  }

  public getPageMaxNumber() {
    return this.maxPageNumber;
  }
}
