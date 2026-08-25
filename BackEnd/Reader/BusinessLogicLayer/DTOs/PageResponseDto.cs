namespace BusinessLogicLayer.DTOs
{
    public record PageResponseDto(
            Guid Id,
            Guid ChapterId,
            int Order,
            string StorageKey,
            string Url
        );
}
