using Autodesk.Revit.DB;
using System.Collections.ObjectModel;

namespace GroupGSA.PresentationWPF.ViewModels
{
   public class ViewExtension : ViewModelBase
   {
      #region Field
      private bool _isSelected;
      private string _name;
      #endregion

      #region Properties
      public View View { get; set; }
      public ViewType ViewType { get; set; }

      public ObservableCollection<ViewExtension> ViewItems { get; set; }

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
      #endregion

      #region Constructor
      public ViewExtension(View view)
      {
         ViewItems = new ObservableCollection<ViewExtension>();
         View = view;
         Name = view.get_Parameter(BuiltInParameter.VIEW_NAME).AsString();
         ViewType = view.ViewType;
         IsSelected = false;
      }
      public ViewExtension(string name)
      {
         ViewItems = new ObservableCollection<ViewExtension>();
         Name = name;
         IsSelected = false;
      }
      public ViewExtension(ViewType viewType)
      {
         ViewItems = new ObservableCollection<ViewExtension>();
         Name = viewType.ToString();

         if (Name == "ThreeD")
         {
            Name = "3D Views";
         }
         else if (Name == "FloorPlan")
         {
            Name = "Floor Plans";
         }
         else if (Name == "CeilingPlan")
         {
            Name = "Ceiling Plans";
         }
         else if (Name == "Elevation")
         {
            Name = "Elevations";
         }

         ViewType = viewType;
         IsSelected = false;
      }
      #endregion

   }
}
