using Autodesk.Revit.DB;
using System.Collections.ObjectModel;

namespace GroupGSA.PresentationWPF.ViewModels
{
   public class LinkExtension : ViewModelBase
   {
      #region Field
      private bool _isSelected;
      private bool _isRVTLink;
      private string _name;
      #endregion

      #region Properties
      public RevitLinkType RevitLink { get; set; }
      public CADLinkType CADLink { get; set; }

      public ObservableCollection<LinkExtension> LinkItems { get; set; }

      public string Name
      {
         get => _name;
         set
         {
            _name = value;
            OnPropertyChanged();
         }
      }

      public bool IsSelected
      {
         get => _isSelected;

         set
         {
            _isSelected = value;
            OnPropertyChanged();
         }
      }

      public bool IsRVTLink
      {
         get => _isRVTLink;
         set
         {
            _isRVTLink = value;
         }
      }
      #endregion

      #region Constructor
      public LinkExtension(RevitLinkType revitLink)
      {
         LinkItems = new ObservableCollection<LinkExtension>();
         RevitLink = revitLink;
         Name = revitLink.Name;
         IsSelected = false;
         _isRVTLink = true;
      }

      public LinkExtension(CADLinkType cadLink)
      {
         LinkItems = new ObservableCollection<LinkExtension>();
         CADLink = cadLink;
         Name = cadLink.Name;
         IsSelected = false;
         _isRVTLink = false;
      }

      public LinkExtension(string name)
      {
         LinkItems = new ObservableCollection<LinkExtension>();
         Name = name;
         IsSelected = false;
      }
      #endregion
   }
}
