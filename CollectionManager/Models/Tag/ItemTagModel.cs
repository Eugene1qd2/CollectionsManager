using Microsoft.EntityFrameworkCore;

namespace CollectionManager.Models.Tag
{
    public class ItemTagModel
    {
        public string Id { get; set; }
        public string TagModelId { get; set; }
        public string ItemId { get; set; }
        public ItemTagModel(string tagId, string itemId)
        {
            TagModelId = tagId;
            ItemId = itemId;
            Id=Guid.NewGuid().ToString();
        }
        public ItemTagModel()
        {

        }
    }
}
