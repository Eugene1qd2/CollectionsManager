using CollectionManager.Models.Collection;
using CollectionManager.Models.Tag;
using Microsoft.AspNetCore.Razor.Language;

namespace CollectionManager.Models.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<TagModel> Tags { get; set; }
        public IEnumerable<DataCollectionViewModel> Collections { get; set; }
        public IEnumerable<CollectionItemDataPair> Items { get; set; }
        public IndexViewModel()
        {
            Tags = new List<TagModel>();
            Collections = new List<DataCollectionViewModel>();
            Items = new List<CollectionItemDataPair>();
        }

        public IndexViewModel(IEnumerable<TagModel> tags, IEnumerable<DataCollectionViewModel> collections, IEnumerable<CollectionItemDataPair> items)
        {
            Tags = tags;
            Collections = collections;
            Items = items;
        }
    }
}
