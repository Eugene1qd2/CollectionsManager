using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace CollectionManager.Models.Collection
{
    public class UserCollectionModel
    {
        [NotMapped]
        public CollectionFields _collectionFields;
        public string Id { get; set; }
        public string OwnerId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string Theme { get; set; }
        public string? PictureLink { get; set; }

        public string CollectionFields
        {
            get
            {
                return JsonSerializer.Serialize(_collectionFields);
            }
            set
            {
                _collectionFields = JsonSerializer.Deserialize<CollectionFields>(value);
            }
        }
    }
}
