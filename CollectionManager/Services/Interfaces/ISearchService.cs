using CollectionManager.Models.Collection;
using CollectionManager.Models.Fulltext;
using CollectionManager.Models.Socials;

namespace CollectionManager.Services.Interfaces
{
    public interface ISearchService
    {
        public Task<IEnumerable<CollectionItemDataPair>> SearchItems(string searchString);
        public Task<IEnumerable<DataCollectionViewModel>> SearchCollections(string searchString);
        public Task<IEnumerable<CommentModel>> SearchComments(string searchString);
    }
}
