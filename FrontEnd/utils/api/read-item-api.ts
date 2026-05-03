import { api } from "./base-api";
import { type ReadItem } from "../../entities/model/read-item/read-item";

export const ReadItemApi = {
  getAll: async (): Promise<ReadItem[]> => {
    const response = await api.get<ReadItem[]>("/ReadItem/GetReadItems");
    return response.data;
  },

  getCollection: async (
    collectionNumber: number,
    collectionSize: number,
  ): Promise<ReadItem[]> => {
    const response = await api.get<ReadItem[]>(
      "/ReadItem/GetReadItemsCollection",
      {
        params: {
          collectionNumber,
          collectionSize,
        },
      },
    );
    return response.data;
  },

  getReadItemsCount: async () => {
    const response = await api.get<number>("/ReadItem/GetCountReadItems");
    return response.data;
  },

  getReadItem: async (id: string): Promise<ReadItem> => {
    const response = await api.get<ReadItem>("/ReadItem/GetReadItem", {
      params: { id },
    });
    return response.data;
  },

  findReadItemByTitle: async (title: string): Promise<ReadItem[]> => {
    const response = await api.get<ReadItem[]>(
      "/ReadItem/FindReadItemByTitle",
      {
        params: { title },
      },
    );
    return response.data;
  },
};
