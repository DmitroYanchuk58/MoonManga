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

        public ReadItemJoinProvider(CatalogDBContext context)
        {
            _join = new JoinRepository<ReadItem, ReadItemTag, Tag>(context);
            _convertorReadItemTag = new ConvertorFromTagDTOIntoTag();
            _convertorReadItem = new ConvertorReadItemDTOIntoReadItem();
        }

        public async Task<ReadItemDTO> GetReadItemWithTagAsync(Guid idReadItem)
        {
            if(idReadItem == Guid.Empty)
            {
                throw new ArgumentException("ID cannot be empty.", nameof(idReadItem));
            }

            var (entity, tags) = await _join.GetCombinedDataByIdAsync(
                idReadItem,
                manga => manga.ReadItemTags,
                (ReadItemTag link) => link.Tag
            );

            if (entity == null)
            {
                throw new KeyNotFoundException($"ReadItem with ID '{idReadItem}' was not found.");
            }

            if(tags == null || tags.Count == 0)
            {
                throw new KeyNotFoundException($"No tags found for ReadItem with ID '{idReadItem}'.");
            }

            var entityDto = _convertorReadItem.ConvertToDto(entity);

            entityDto.Tags = tags.Select(tag => _convertorReadItemTag.ConvertToDto(tag)).ToList();

            return entityDto;
        }
    }
}
