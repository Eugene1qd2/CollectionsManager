using Microsoft.AspNetCore.Cors;
using System.CodeDom;
using System.ComponentModel.DataAnnotations;

namespace CollectionManager.Models.Collection
{
    public enum CollectionFieldState
    {
        FALSE = 0,
        TRUE = 1,
    }
    public class EntireCollectionViewModel
    {
        [Display(Name ="Id")]
        public string EntireCollectionViewModelId { get; set; }
        [Display(Name ="Owner id")]
        public string OwnerId { get; set; }
        [Display(Name ="Title")]
        public string? Title { get; set; }
        [Display(Name = "Description")]
        public string? Description { get; set; }
        [Display(Name = "Theme\\Topic")]
        public string Theme { get; set; }
        [Display(Name = "Picture link")]
        public string? PictureLink { get; set; }
        [Display(Name = "Picture name")]
        public string? CloudPictureName { get; set; }

        public CollectionFieldState CustomStringState1 { get; set; }
        string? _customStringName1;
        public string? CustomStringName1
        {
            get { return _customStringName1; }
            set { _customStringName1 = value; CustomStringState1 = value != null ? CollectionFieldState.TRUE : CollectionFieldState.FALSE; }
        }
        public CollectionFieldState CustomStringState2 { get; set; }
        string? _customStringName2;
        public string? CustomStringName2
        {
            get { return _customStringName2; }
            set { _customStringName2 = value; CustomStringState2 = value != null ? CollectionFieldState.TRUE : CollectionFieldState.FALSE; }
        }
        public CollectionFieldState CustomStringState3 { get; set; }
        string? _customStringName3;
        public string? CustomStringName3
        {
            get { return _customStringName3; }
            set { _customStringName3 = value; CustomStringState3 = value != null ? CollectionFieldState.TRUE : CollectionFieldState.FALSE; }
        }
        public CollectionFieldState CustomIntState1 { get; set; }
        string? _customIntName1;
        public string? CustomIntName1
        {
            get { return _customIntName1; }
            set { _customIntName1 = value; CustomIntState1 = value != null ? CollectionFieldState.TRUE : CollectionFieldState.FALSE; }
        }
        public CollectionFieldState CustomIntState2 { get; set; }
        string? _customIntName2;
        public string? CustomIntName2
        {
            get { return _customIntName2; }
            set { _customIntName2 = value; CustomIntState2 = value != null ? CollectionFieldState.TRUE : CollectionFieldState.FALSE; }
        }
        public CollectionFieldState CustomIntState3 { get; set; }
        string? _customIntName3;
        public string? CustomIntName3
        {
            get { return _customIntName3; }
            set { _customIntName3 = value; CustomIntState3 = value != null ? CollectionFieldState.TRUE : CollectionFieldState.FALSE; }
        }
        public CollectionFieldState CustomTextState1 { get; set; }
        string? _customTextName1;
        public string? CustomTextName1
        {
            get { return _customTextName1; }
            set { _customTextName1 = value; CustomTextState1 = value != null ? CollectionFieldState.TRUE : CollectionFieldState.FALSE; }
        }
        public CollectionFieldState CustomTextState2 { get; set; }
        string? _customTextName2;
        public string? CustomTextName2
        {
            get { return _customTextName2; }
            set { _customTextName2 = value; CustomTextState2 = value != null ? CollectionFieldState.TRUE : CollectionFieldState.FALSE; }
        }
        public CollectionFieldState CustomTextState3 { get; set; }
        string? _customTextName3;
        public string? CustomTextName3
        {
            get { return _customTextName3; }
            set { _customTextName3 = value; CustomTextState3 = value != null ? CollectionFieldState.TRUE : CollectionFieldState.FALSE; }
        }
        public CollectionFieldState CustomBoolState1 { get; set; }
        string? _customBoolName1;
        public string? CustomBoolName1
        {
            get { return _customBoolName1; }
            set { _customBoolName1 = value; CustomBoolState1 = value != null ? CollectionFieldState.TRUE : CollectionFieldState.FALSE; }
        }
        public CollectionFieldState CustomBoolState2 { get; set; }
        string? _customBoolName2;
        public string? CustomBoolName2
        {
            get { return _customBoolName2; }
            set { _customBoolName2 = value; CustomBoolState2 = value != null ? CollectionFieldState.TRUE : CollectionFieldState.FALSE; }
        }
        public CollectionFieldState CustomBoolState3 { get; set; }
        string? _customBoolName3;
        public string? CustomBoolName3
        {
            get { return _customBoolName3; }
            set { _customBoolName3 = value; CustomBoolState3 = value != null ? CollectionFieldState.TRUE : CollectionFieldState.FALSE; }
        }
        public CollectionFieldState CustomDateState1 { get; set; }
        string? _customDateName1;
        public string? CustomDateName1
        {
            get { return _customDateName1; }
            set { _customDateName1 = value; CustomDateState1 = value != null ? CollectionFieldState.TRUE : CollectionFieldState.FALSE; }
        }
        public CollectionFieldState CustomDateState2 { get; set; }
        string? _customDateName2;
        public string? CustomDateName2
        {
            get { return _customDateName2; }
            set { _customDateName2 = value; CustomDateState2 = value != null ? CollectionFieldState.TRUE : CollectionFieldState.FALSE; }
        }
        public CollectionFieldState CustomDateState3 { get; set; }
        string? _customDateName3;
        public string? CustomDateName3
        {
            get { return _customDateName3; }
            set { _customDateName3 = value; CustomDateState3 = value != null ? CollectionFieldState.TRUE : CollectionFieldState.FALSE; }
        }
        public EntireCollectionViewModel() { }
        public EntireCollectionViewModel(EntireCollectionViewModel model)
        {
            this.EntireCollectionViewModelId = model.EntireCollectionViewModelId;  
            this.OwnerId = model.OwnerId;
            this.PictureLink = model.PictureLink;
            this.Title = model.Title;
            this.Description = model.Description;
            this.Theme = model.Theme;
            this.CloudPictureName = model.CloudPictureName;

            this.CustomBoolName1 = model.CustomBoolName1;
            this.CustomBoolName2 = model.CustomBoolName2;
            this.CustomBoolName3 = model.CustomBoolName3;
            this.CustomStringName1 = model.CustomStringName1;
            this.CustomStringName2 = model.CustomStringName2;
            this.CustomStringName3 = model.CustomStringName3;
            this.CustomIntName1 = model.CustomIntName1;
            this.CustomIntName2 = model.CustomIntName2;
            this.CustomIntName3 = model.CustomIntName3;
            this.CustomTextName1 = model.CustomTextName1;
            this.CustomTextName2 = model.CustomTextName2;
            this.CustomTextName3 = model.CustomTextName3;
            this.CustomDateName1 = model.CustomDateName1;
            this.CustomDateName2 = model.CustomDateName2;
            this.CustomDateName3 = model.CustomDateName3;

            this.CustomBoolState1 = model.CustomBoolState1;
            this.CustomBoolState2 = model.CustomBoolState2;
            this.CustomBoolState3 = model.CustomBoolState3;
            this.CustomStringState1 = model.CustomStringState1;
            this.CustomStringState2 = model.CustomStringState2;
            this.CustomStringState3 = model.CustomStringState3;
            this.CustomIntState1 = model.CustomIntState1;
            this.CustomIntState2 = model.CustomIntState2;
            this.CustomIntState3 = model.CustomIntState3;
            this.CustomTextState1 = model.CustomTextState1;
            this.CustomTextState2 = model.CustomTextState2;
            this.CustomTextState3 = model.CustomTextState3;
            this.CustomDateState1 = model.CustomDateState1;
            this.CustomDateState2 = model.CustomDateState2;
            this.CustomDateState3 = model.CustomDateState3;
        }
    }
}
