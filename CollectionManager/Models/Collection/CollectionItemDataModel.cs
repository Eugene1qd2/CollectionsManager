using CollectionManager.Models.CollectionItem;

namespace CollectionManager.Models.Collection
{
    public class CollectionItemDataModel : EntireItemViewModel
    {
        public EntireCollectionViewModel collection { get; set; }

        public CollectionItemDataModel(string Id, string ownerId, EntireCollectionViewModel collectionOwner) : base(Id, ownerId)
        {
            collection = collectionOwner;
        }
        public CollectionItemDataModel(EntireItemViewModel model)
        {
            this.CollectionId = model.CollectionId;
            this.EntireItemViewModelId = model.EntireItemViewModelId;
            this.Name = model.Name;
            this.Tags = model.Tags;
            this.CustomTextField1 = model.CustomTextField1;
            this.CustomTextField2 = model.CustomTextField2;
            this.CustomTextField3 = model.CustomTextField3;
            this.CustomStringField1 = model.CustomStringField1;
            this.CustomStringField2 = model.CustomStringField2;
            this.CustomStringField3 = model.CustomStringField3;
            this.CustomIntField1 = model.CustomIntField1;
            this.CustomIntField2 = model.CustomIntField2;
            this.CustomIntField3 = model.CustomIntField3;
            this.CustomBoolField1 = model.CustomBoolField1;
            this.CustomBoolField2 = model.CustomBoolField2;
            this.CustomBoolField3 = model.CustomBoolField3;
            this.CustomDateField1 = model.CustomDateField1;
            this.CustomDateField2 = model.CustomDateField2;
            this.CustomDateField3 = model.CustomDateField3;
        }
        public CollectionItemDataModel()
        {
        }
    }
}
