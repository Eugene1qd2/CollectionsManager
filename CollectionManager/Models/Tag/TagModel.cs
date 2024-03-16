using System.ComponentModel.DataAnnotations.Schema;

namespace CollectionManager.Models.Tag
{
    public class TagModel
    {
        public string TagModelId { get; set; }
        public string Tag { get; set; }
        public string AbsTag { get; set; }

        [NotMapped]
        public int IsUserTag { get; set; }
        public TagModel(string tag)
        {
            Tag = tag;
            TagModelId = Guid.NewGuid().ToString();
            AbsTag = tag.ToUpper();
            IsUserTag = 0;
        }

        public TagModel(string tag,int isUserTag)
        {
            Tag = tag;
            TagModelId = Guid.NewGuid().ToString();
            AbsTag = tag.ToUpper();
            IsUserTag = isUserTag;
        }
        public TagModel()
        {
        }
    }
}
