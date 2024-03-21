using Microsoft.AspNetCore.Razor.Language;
using CollectionManager.Models.Tag;
using CollectionManager.Models.Collection;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace CollectionManager.Models.CollectionItem
{
    public class GeneralItemViewModel
    {
        public string ItemId { get; set; }
        public string CollectionId { get; set; }
        public string UserId { get; set; }
        public GeneralItemViewModel(string itemId, string collectionId, string userId)
        {
            ItemId = itemId;
            CollectionId = collectionId;
            UserId = userId;
        }
    }
}
