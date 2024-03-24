using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollectionManager.Models.Socials
{
    public class CommentModel
    {
        public string CommentModelId { get; set; }
        [NotMapped]
        public string? Username {  get; set; }
        public string ItemId { get; set; }
        public string UserId { get; set; }
        public string? CommentText { get; set; }
        public DateTime CreationDate { get; set; }
        public CommentModel() { }
        public CommentModel(string ItemId, string UserId, string Text)
        {
            this.ItemId = ItemId;
            this.UserId = UserId;
            this.CommentText = Text;
            CommentModelId = Guid.NewGuid().ToString();
            CreationDate = DateTime.Now;
        }
    }
}
