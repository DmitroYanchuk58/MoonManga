import { ReadItemType } from "./read-item-type";

export interface ReadItem {
  id: string;
  title: string;
  description: string;
  type: ReadItemType;
  coverImage: string;
}
