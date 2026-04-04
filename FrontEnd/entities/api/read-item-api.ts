import { api } from "../../shared/api/base";
import { ReadItemDto } from "../model/read-item/read-item";

export const mangaApi = {
  getAll: async (): Promise<ReadItemDto[]> => {
    const response = await api.get<ReadItemDto[]>("/readitem");
    return response.data;
  },

  getById: async (id: string): Promise<ReadItemDto> => {
    const response = await api.get<ReadItemDto>(`/readitem/${id}`);
    return response.data;
  },

  create: async (item: Omit<ReadItemDto, "id">): Promise<void> => {
    await api.post("/readitem", item);
  },
};
