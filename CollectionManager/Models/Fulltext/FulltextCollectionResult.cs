using CollectionManager.Models.Collection;
using CollectionManager.Models.CollectionItem;
using CollectionManager.Models.Socials;

namespace CollectionManager.Models.Fulltext
{
    public class FulltextCollectionResult : FulltextItem
    {
        public DataCollectionViewModel collection {  get; set; }
        public FulltextCollectionResult(string itemId, string fulltext, string foundInType = "Item", string collectionId = null, string commentId = null) : base(itemId, fulltext, foundInType, collectionId, commentId) { }
        public FulltextCollectionResult(){}
    }
}
