﻿using CollectionManager.Models.CollectionItem;
using CollectionManager.Models.ModelAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;


namespace CollectionManager.Models.Collection
{
    public class DataCollectionViewModel : EntireCollectionViewModel
    {
        [DataType(DataType.Upload)]
        [MaxFileSize(1 * 1024 * 1024)]
        [PermittedExtensions(new string[] { ".jpeg", ".png", ".jpg", ".gif" })]
        public virtual IFormFile? ImageFile { get; set; }
        public bool SetAsOwner {  get; set; }
        public List<EntireItemViewModel> Items { get; set; }
        public DataCollectionViewModel() : base() 
        {
            Items= new List<EntireItemViewModel>();
        }
        public DataCollectionViewModel(EntireCollectionViewModel model) : base(model) 
        {
            Items= new List<EntireItemViewModel>();
        }
    }
}
