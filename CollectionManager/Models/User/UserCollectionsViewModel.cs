using CollectionManager.Models.Collection;

namespace CollectionManager.Models.User
{
    public class UserCollectionsViewModel
    {
        public EntireUserViewModel User { get; set; }
        public List<UserCollectionModel> Collections { get; set; }
    }
}
