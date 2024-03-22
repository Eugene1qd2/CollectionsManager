using CollectionManager.Models.Collection;
using CollectionManager.Models.Fulltext;
using CollectionManager.Models.Socials;

namespace CollectionManager.Models.ViewModels
{
    public class SearchViewModel
    {
        public IEnumerable<CollectionItemDataPair> Items { get; set;}
        public IEnumerable<DataCollectionViewModel> Collections { get; set;}
        public IEnumerable<CommentModel> Comments { get; set;} 
        public IEnumerable<CollectionItemDataPair> General {  get; set;}
        public SearchViewModel() { }
    }
}
