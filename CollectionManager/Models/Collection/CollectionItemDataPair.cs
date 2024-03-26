using CollectionManager.Models.CollectionItem;
using CollectionManager.Models.Socials;

namespace CollectionManager.Models.Collection
{
    public class CollectionItemDataPair
    {
        public EntireCollectionViewModel collection { get; set; }
        public EntireItemViewModel item { get; set; }
        public bool SetAsOwner = false;
        public string? OwnerName { get; set; }   
        public int likesCount { get; set; }

        public CollectionItemDataPair(EntireItemViewModel itemViewModel,EntireCollectionViewModel collectionViewModel)
        {
            item= itemViewModel;
            collection= collectionViewModel;
        }
        public CollectionItemDataPair(EntireItemViewModel itemViewModel, EntireCollectionViewModel collectionViewModel,string OwnerName)
        {
            item = itemViewModel;
            collection = collectionViewModel;
            this.OwnerName = OwnerName;
        }
        public CollectionItemDataPair()
        {
        }
    }
}
