using Microsoft.AspNetCore.Razor.Language;
using CollectionManager.Models.Tag;
using CollectionManager.Models.Collection;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace CollectionManager.Models.CollectionItem
{
    public class EntireItemViewModel
    {
        public string EntireItemViewModelId { get; set; }
        public string CollectionId { get; set; }
        [Display(Name ="Item name")]
        public string Name { get; set; }
        [Display(Name ="Creation date")]
        public DateTime CreationDate { get; set; }
        [Display(Name ="Tags")]

        [NotMapped]
        public List<TagModel> Tags { get; set; }
        public string? CustomStringField1 { get; set; }
        public string? CustomStringField2 { get; set; }
        public string? CustomStringField3 { get; set; }
        public int? CustomIntField1 { get; set; }
        public int? CustomIntField2 { get; set; }
        public int? CustomIntField3 { get; set; }
        public string? CustomTextField1 { get; set; }
        public string? CustomTextField2 { get; set; }
        public string? CustomTextField3 { get; set; }
        public bool CustomBoolField1 { get; set; }
        public bool CustomBoolField2 { get; set; }
        public bool CustomBoolField3 { get; set; }
        public DateTime? CustomDateField1 { get; set; }
        public DateTime? CustomDateField2 { get; set; }
        public DateTime? CustomDateField3 { get; set; }

        public EntireItemViewModel(string Id,string OwnerId)
        {
            EntireItemViewModelId = Id;
            CollectionId = OwnerId;
            Tags = new List<TagModel>();
            CreationDate = DateTime.Now;
        }
        public EntireItemViewModel()
        {
            Tags = new List<TagModel>();
            CreationDate = DateTime.Now;
        }
    }
}
