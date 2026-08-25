namespace BusinessLogicLayer.DTOs
{
    public record UpdatePageDto(
            Guid PageId,
            int Order,
            Stream? NewFileStream,
            string? NewFileName,
            string? NewContentType
        );
}
