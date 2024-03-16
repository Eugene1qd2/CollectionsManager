using CollectionManager.Models.Collection;

namespace CollectionManager.Models.User
{
    public class UserProfileDataModel
    {
        public EntireUserViewModel User { get; set; }
        public List<EntireCollectionViewModel> Collections { get; set; }
        public UserProfileDataModel(EntireUserViewModel user, List<EntireCollectionViewModel> collections)
        {
            User = user;
            Collections = collections;
        }
        public bool SetAsOwner {  get; set; }
    }
}
