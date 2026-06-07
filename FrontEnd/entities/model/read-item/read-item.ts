import { ReadItemType } from "./read-item-type";
import { type Tag } from "../tag/tag";
import { Chapter } from "../chapter/chapter";

export interface ReadItem {
  id: string;
  title: string;
  description: string;
  type: ReadItemType;
  coverImage: string;
  tags: Array<Tag>;
  chapters: Array<Chapter>;
}
