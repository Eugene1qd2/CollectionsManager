namespace CollectionManager.Models.Socials
{
    public class LikeModel
    {
        public string LikeModelId { get; set; }
        public string ItemId { get; set; }
        public string UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public LikeModel() { }
        public LikeModel(string ItemId, string UserId)
        {
            this.ItemId = ItemId;
            this.UserId = UserId;
            LikeModelId = Guid.NewGuid().ToString();
            CreationDate = DateTime.Now;
        }
    }
}
