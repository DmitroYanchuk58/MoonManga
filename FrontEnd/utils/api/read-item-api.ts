import { api } from "./base-api";
import { type ReadItem } from "../../entities/model/read-item/read-item";

export const ReadItemApi = {
  getAll: async (): Promise<ReadItem[]> => {
    const response = await api.get<ReadItem[]>("/ReadItem");
    return response.data;
  },

  getCollection: async (
    page: number,
    pageSize: number,
  ): Promise<ReadItem[]> => {
    const response = await api.get<ReadItem[]>("/ReadItem", {
      params: {
        page,
        pageSize,
      },
    });
    return response.data;
  },

  getReadItemsCount: async () => {
    const response = await api.get<number>("/ReadItem/count");
    return response.data;
  },

  getReadItem: async (id: string): Promise<ReadItem> => {
    const response = await api.get<ReadItem>(`/ReadItem/${id}`);
    return response.data;
  },

  getFullInfoReadItem: async (id: string): Promise<ReadItem> => {
    const response = await api.get<ReadItem>(`/ReadItem/${id}`, {
      params: { includeTags: true, includeChapters: true },
    });
    return response.data;
  },

  findReadItemByTitle: async (title: string): Promise<ReadItem[]> => {
    const response = await api.get<ReadItem[]>("/ReadItem", {
      params: { title },
    });
    return response.data;
  },
};
