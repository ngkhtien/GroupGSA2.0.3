using Autodesk.Revit.DB;
using System.Collections.ObjectModel;

namespace GroupGSA.PresentationWPF.ViewModels
{
   public class SheetExtension : ViewModelBase
   {
      #region Field
      private bool _isSelected;
      private string _name;
      private string _sheetNumber;
      #endregion

      #region Properties
      public ViewSheet Sheet { get; set; }

      public ObservableCollection<SheetExtension> SheetItems { get; set; }

      public string Name
      {
         get => _name;
         set
         {
            _name = value;
            OnPropertyChanged();
         }
      }
      public string SheetNumber
      {
         get => _sheetNumber;
         set
         {
            _sheetNumber = value;
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
      public SheetExtension(ViewSheet viewSheet)
      {
         SheetItems = new ObservableCollection<SheetExtension>();
         Sheet = viewSheet;
         SheetNumber = viewSheet.get_Parameter(BuiltInParameter.SHEET_NUMBER).AsString();
         string sheetName = viewSheet.get_Parameter(BuiltInParameter.SHEET_NAME).AsString();
         Name = SheetNumber + " - " + sheetName;
         IsSelected = false;
      }
      #endregion

   }
}
