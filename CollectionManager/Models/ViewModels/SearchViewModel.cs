using CollectionManager.Models.Collection;
using CollectionManager.Models.Fulltext;
using CollectionManager.Models.Socials;
using System.ComponentModel.DataAnnotations;

namespace CollectionManager.Models.ViewModels
{
    public class SearchViewModel
    {
        public IEnumerable<CollectionItemDataPair> Items { get; set;}
        public IEnumerable<DataCollectionViewModel> Collections { get; set;}
        public IEnumerable<CommentModel> Comments { get; set;} 
        public IEnumerable<CollectionItemDataPair> General {  get; set;}

        [MaxLength(255)]
        public string searchString {  get; set;}
        [MaxLength(255)]
        public string searchTag {  get; set;}

        public SearchViewModel() { }

        public SearchViewModel(string searchString, string searchTag)
        {
            this.searchString = searchString;
            this.searchTag = searchTag;
        }
    }
}
