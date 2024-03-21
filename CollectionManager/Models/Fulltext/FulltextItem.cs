using System.Text;

namespace CollectionManager.Models.Fulltext
{
    public class FulltextItem
    {
        public string FulltextItemId { get; set; }
        public string? ItemId { get; set; }
        public string? CollectionId { get; set; }
        public string? CommentId { get; set; }
        public string Fulltext { get; set; }
        public string FoundInType { get; set; }
        public FulltextItem() { }
        public FulltextItem(string itemId, string fulltext, string foundInType = "Item", string collectionId = null, string commentId = null)
        {
            FulltextItemId = Guid.NewGuid().ToString();
            ItemId = itemId;
            CollectionId = collectionId;
            Fulltext = fulltext;
            FoundInType = foundInType;
            CommentId = commentId;
        }
    }
}
