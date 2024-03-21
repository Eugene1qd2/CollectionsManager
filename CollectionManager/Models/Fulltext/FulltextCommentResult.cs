using CollectionManager.Models.Collection;
using CollectionManager.Models.CollectionItem;
using CollectionManager.Models.Socials;

namespace CollectionManager.Models.Fulltext
{
    public class FulltextCommentResult : FulltextItem
    {
        public CommentModel comment {  get; set; }
        public FulltextCommentResult(string itemId, string fulltext, string foundInType = "Item", string collectionId = null, string commentId = null) : base(itemId, fulltext, foundInType, collectionId, commentId) { }
        public FulltextCommentResult(){}
    }
}
