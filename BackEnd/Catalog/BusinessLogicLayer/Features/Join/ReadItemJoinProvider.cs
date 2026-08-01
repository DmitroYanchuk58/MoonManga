using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Helpers.Convertors;
using BusinessLogicLayer.Helpers.Convertors.Interfaces;
using DatabaseAccessLayer.DatabaseContext;
using DatabaseAccessLayer.Entities;
using DatabaseAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Features.Join
{
    public class ReadItemJoinProvider : IJoinProvider
    {
        private IJoin<ReadItem, ReadItemTag, Tag> _join;
        private IConvertorDTOIntoEntity<ReadItem, ReadItemDTO> _convertorReadItem;
        private IConvertorDTOIntoEntity<Tag, TagDTO> _convertorReadItemTag;
        private IConvertorDTOIntoEntity<Chapter, ChapterDTO> _convertorChapter;

        public ReadItemJoinProvider(CatalogDBContext context)
        {
            _join = new JoinRepository<ReadItem, ReadItemTag, Tag>(context);
            _convertorReadItemTag = new ConvertorFromTagDTOIntoTag();
            _convertorReadItem = new ConvertorReadItemDTOIntoReadItem();
            _convertorChapter = new ConvertorFromChapterDTOIntoChapter();
        }

        public async Task<ReadItemDTO> GetReadItemWithTagAsync(Guid idReadItem)
        {
            if (idReadItem == Guid.Empty)
            {
                throw new ArgumentException("ID cannot be empty.", nameof(idReadItem));
            }

            var (entity, tags) = await _join.GetManyToManyByIdAsync(
                idReadItem,
                manga => manga.ReadItemTags,
                (ReadItemTag link) => link.Tag
            );

            if (entity == null)
            {
                throw new KeyNotFoundException($"ReadItem with ID '{idReadItem}' was not found.");
            }

            var entityDto = _convertorReadItem.ConvertToDto(entity);

            entityDto.Tags = tags.Select(tag => _convertorReadItemTag.ConvertToDto(tag)).ToList();

            return entityDto;
        }

        public async Task<ReadItemDTO> GetReadItemWithChaptersAsync(Guid idReadItem)
        {
            if (idReadItem == Guid.Empty)
            {
                throw new ArgumentException("ID cannot be empty.", nameof(idReadItem));
            }

            var (entity, chapters) = await _join.GetOneToManyByIdAsync(
                idReadItem,
                manga => manga.Chapters
            );

            if (entity == null)
            {
                throw new KeyNotFoundException($"ReadItem with ID '{idReadItem}' was not found.");
            }

            var entityDto = _convertorReadItem.ConvertToDto(entity);

            entityDto.Chapters = chapters.Select(chapter => _convertorChapter.ConvertToDto(chapter)).ToList();

            return entityDto;
        }

        public async Task<ReadItemDTO> GetReadItemFullInfo(Guid idReadItem)
        {
            if (idReadItem == Guid.Empty)
            {
                throw new ArgumentException("ID cannot be empty.", nameof(idReadItem));
            }
            var (entity, tags) = await _join.GetManyToManyByIdAsync(
                idReadItem,
                manga => manga.ReadItemTags,
                (ReadItemTag link) => link.Tag
            );
            var (entityWithChapters, chapters) = await _join.GetOneToManyByIdAsync(
                idReadItem,
                manga => manga.Chapters
            );
            if (entity == null || entityWithChapters == null)
            {
                throw new KeyNotFoundException($"ReadItem with ID '{idReadItem}' was not found.");
            }
            var entityDto = _convertorReadItem.ConvertToDto(entity);
            entityDto.Tags = tags?.Select(tag => _convertorReadItemTag.ConvertToDto(tag)).ToList() ?? new List<TagDTO>();
            entityDto.Chapters = chapters?.Select(chapter => _convertorChapter.ConvertToDto(chapter)).ToList() ?? new List<ChapterDTO>();
            return entityDto;
        }
    }
}
