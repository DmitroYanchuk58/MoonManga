using BusinessLogicLayer.DTOs;
using DatabaseAccessLayer.DatabaseContext;
using DatabaseAccessLayer.Repositories;
using DatabaseAccessLayer.Services;
using DatabaseLogicLayer.Entities;
using DatabaseLogicLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class PageService : IPageService
    {
        private readonly ICRUD_Repository<Page> _pageRepository;
        private readonly IStorageService _storageService;
        private const string BucketName = "manga-pages";
        private const string BaseStorageUrl = "http://localhost:9000";

        public PageService(ICRUD_Repository<Page> pageRepository, IStorageService storageService)
        {
            _pageRepository = pageRepository;
            _storageService = storageService;
        }

        public async Task<PageResponseDto> CreateAsync(CreatePageDto dto)
        {
            var extension = Path.GetExtension(dto.FileName).ToLowerInvariant();
            var storageKey = $"chapters/{dto.ChapterId}/page_{dto.Order:D3}_{Guid.NewGuid():N}{extension}";

            await _storageService.UploadFileAsync(BucketName, storageKey, dto.FileStream, dto.ContentType);

            try
            {
                var page = new Page
                {
                    Id = Guid.NewGuid(),
                    IdChapter = dto.ChapterId,
                    Order = dto.Order,
                    StorageKey = storageKey
                };

                await _pageRepository.CreateAsync(page);

                return MapToDto(page);
            }
            catch
            {
                await _storageService.DeleteFileAsync(BucketName, storageKey);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var page = await _pageRepository.GetByIdAsync(id);
            if (page == null)
                return false;

            await _pageRepository.DeleteAsync(id);

            await _storageService.DeleteFileAsync(BucketName, page.StorageKey);

            return true;
        }

        public async Task<List<Page_DTO>> GetAllAsync()
        {
            var dbPages = await _pageRepository.GetAllAsync();
            return dbPages.Select(page => new Page_DTO(page)).ToList();
        }

        public async Task<PageResponseDto?> GetByIdAsync(Guid id)
        {
            var page = await _pageRepository.GetByIdAsync(id);
            return page == null ? null : MapToDto(page);
        }

        public async Task<List<Page_DTO>> GetAllByChapterId(Guid idChapter)
        {
            if (idChapter == Guid.Empty)
            {
                throw new ArgumentException("Chapter ID cannot be an empty GUID.", nameof(idChapter));
            }

            var dbPages = await _pageRepository.GetByConditionAsync(p => p.IdChapter == idChapter);

            return dbPages.Select(page => new Page_DTO(page)).ToList();
        }

        public async Task<PageResponseDto> UpdateAsync(UpdatePageDto dto)
        {
            var page = await _pageRepository.GetByIdAsync(dto.PageId);
            if (page == null)
                throw new KeyNotFoundException($"Page with ID {dto.PageId} not found.");

            string? oldStorageKey = null;

            if (dto.NewFileStream != null && !string.IsNullOrWhiteSpace(dto.NewFileName))
            {
                var extension = Path.GetExtension(dto.NewFileName).ToLowerInvariant();
                var newStorageKey = $"chapters/{page.IdChapter}/page_{dto.Order:D3}_{Guid.NewGuid():N}{extension}";

                await _storageService.UploadFileAsync(BucketName, newStorageKey, dto.NewFileStream, dto.NewContentType!);

                oldStorageKey = page.StorageKey;
                page.StorageKey = newStorageKey;
            }

            page.Order = dto.Order;

            try
            {
                await _pageRepository.UpdateAsync(page);

                if (oldStorageKey != null)
                {
                    await _storageService.DeleteFileAsync(BucketName, oldStorageKey);
                }

                return MapToDto(page);
            }
            catch
            {
                if (oldStorageKey != null)
                {
                    await _storageService.DeleteFileAsync(BucketName, page.StorageKey);
                }
                throw;
            }
        }

        private static PageResponseDto MapToDto(Page page)
        {
            var url = $"{BaseStorageUrl}/{BucketName}/{page.StorageKey}";
            return new PageResponseDto(page.Id, page.IdChapter, page.Order, page.StorageKey, url);
        }
    }
}
