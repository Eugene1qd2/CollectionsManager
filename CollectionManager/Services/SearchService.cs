using CollectionManager.Data.Interfaces;
using CollectionManager.Data.Repositories;
using CollectionManager.Models.Collection;
using CollectionManager.Models.Fulltext;
using CollectionManager.Services.Interfaces;
using Microsoft.CodeAnalysis;

namespace CollectionManager.Services
{
    public class SearchService : ISearchService
    {
        IFulltextRepository _fulltextRepository;
        public SearchService(IFulltextRepository fulltextRepository)
        {
            _fulltextRepository = fulltextRepository;
        }

        public async Task<IEnumerable<FulltextCollectionResult>> SearchCollections(string searchString)
        {
            return await _fulltextRepository.FindCollections(searchString);

        }

        public async Task<IEnumerable<FulltextCommentResult>> SearchComments(string searchString)
        {
            return await _fulltextRepository.FindComments(searchString);

        }

        public async Task<IEnumerable<FulltextItemResult>> SearchItems(string searchString)
        {
            return await _fulltextRepository.FindItems(searchString);
        }
    }
}
