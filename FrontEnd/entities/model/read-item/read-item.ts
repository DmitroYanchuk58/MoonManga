import { ReadItemType } from "./read-item-type";
export interface ReadItemDto {
  id: string;
  title: string;
  type: ReadItemType;
}
