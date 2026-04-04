import { api } from "./base-api";
import { ReadItem } from "../../entities/model/read-item/read-item";

export const mangaApi = {
  getAll: async (): Promise<ReadItem[]> => {
    const response = await api.get<ReadItem[]>("/readitem");
    return response.data;
  },

  getById: async (id: string): Promise<ReadItem> => {
    const response = await api.get<ReadItem>(`/readitem/${id}`);
    return response.data;
  },

  create: async (item: Omit<ReadItem, "id">): Promise<void> => {
    await api.post("/readitem", item);
  },
};
