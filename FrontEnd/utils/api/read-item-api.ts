import { api } from "./base-api";
import { type ReadItem } from "../../entities/model/read-item/read-item";

export const ReadItemApi = {
  getAll: async (): Promise<ReadItem[]> => {
    const response = await api.get<ReadItem[]>("/ReadItem/GetReadItems");
    return response.data;
  },

  getById: async (id: string): Promise<ReadItem> => {
    const response = await api.get<ReadItem>(`/readitem/${id}`);
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
};
