import type { ReadItem } from "../entities/model/read-item/read-item";
import { ReadItemApi } from "../utils/api/read-item-api";

export class MangaCollectionManager {
  private items: ReadItem[] = [];
  private pageNumber: number = 1;
  private maxPageNumber: number = 1;
  private minPageNumber: number = 1;
  private countReadItemsOnPage: number = 30;

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

  public getCurrentPageNumber(): number {
    return this.pageNumber;
  }

  public setCurrentPageNumber(numberPage: number): void {
    this.pageNumber = numberPage;
  }

  public async find(title: string): Promise<ReadItem[]> {
    return await ReadItemApi.findReadItemByTitle(title);
  }

  public async loadItems(): Promise<void> {
    try {
      // Execute both requests concurrently to eliminate waterfall delays
      const [data, count] = await Promise.all([
        ReadItemApi.getCollection(this.pageNumber, this.countReadItemsOnPage),
        ReadItemApi.getReadItemsCount(),
      ]);

      this.items = data || [];
      this.maxPageNumber = Math.max(
        1,
        Math.ceil((count || 0) / this.countReadItemsOnPage),
      );
    } catch (error) {
      console.error("Failed to load manga collection:", error);
      this.items = [];
    }
  }

  public getPageMaxNumber(): number {
    return this.maxPageNumber;
  }
}
