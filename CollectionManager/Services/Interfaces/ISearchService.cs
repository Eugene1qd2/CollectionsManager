using CollectionManager.Models.Collection;
using CollectionManager.Models.Fulltext;

namespace CollectionManager.Services.Interfaces
{
    public interface ISearchService
    {
        public Task<IEnumerable<FulltextItemResult>> SearchItems(string searchString);
        public Task<IEnumerable<FulltextCollectionResult>> SearchCollections(string searchString);
        public Task<IEnumerable<FulltextCommentResult>> SearchComments(string searchString);
    }
}
