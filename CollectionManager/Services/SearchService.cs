using CollectionManager.Data.Interfaces;
using CollectionManager.Data.Repositories;
using CollectionManager.Models.Collection;
using CollectionManager.Models.Fulltext;
using CollectionManager.Models.Socials;
using CollectionManager.Services.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace CollectionManager.Services
{
    public class SearchService : ISearchService
    {
        private readonly ICollectionsRepository _collectionsRepository;
        private readonly ICollectionItemsRepository _collectionItemsRepository;
        private readonly ICommentsRepository _commentsRepository;
        public SearchService(ICollectionsRepository collectionsRepository, ICollectionItemsRepository collectionItemsRepository, ICommentsRepository commentsRepository)
        {
            _collectionsRepository = collectionsRepository;
            _collectionItemsRepository = collectionItemsRepository;
            _commentsRepository = commentsRepository;
        }

        public async Task<IEnumerable<CollectionItemDataPair>> SearchItems(string searchText)
        {
            return await _collectionItemsRepository.FulltextSearch(searchText);
        }

        public async Task<IEnumerable<DataCollectionViewModel>> SearchCollections(string searchText)
        {
            return await _collectionsRepository.FulltextSearch(searchText);
        }

        public async Task<IEnumerable<CommentModel>> SearchComments(string searchText)
        {
            return await _commentsRepository.FulltextSearch(searchText);
        }
    }
}
