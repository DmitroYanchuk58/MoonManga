export const ReadItemType = {
  Manga: 1,
  Manhwa: 2,
  Manhua: 3,
} as const;

export type ReadItemType = (typeof ReadItemType)[keyof typeof ReadItemType];
