using CollectionManager.Models.Collection;
using CollectionManager.Models.Fulltext;

namespace CollectionManager.Models.ViewModels
{
    public class SearchViewModel
    {
        public IEnumerable<FulltextItemResult> Items { get; set;}
        public IEnumerable<FulltextCollectionResult> Collections { get; set;}
        public IEnumerable<FulltextCommentResult> Comments { get; set;} 
        public IEnumerable<CollectionItemDataPair> General {  get; set;}
        public SearchViewModel() { }
    }
}
