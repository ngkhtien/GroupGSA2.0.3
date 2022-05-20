using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using GroupGSA.Utils;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;
using View = Autodesk.Revit.DB.View;

namespace GroupGSA.PresentationWPF.ViewModels
{
   public class DeleteSheetViewModel : ViewModelBase
   {
      #region Field
      private UIDocument _uidoc;
      private Document _doc;
      public bool _selectAllChecked;
      public bool _selectNoneChecked;
      public List<ViewSheet> allSheets = new List<ViewSheet>();
      #endregion

      #region Properties
      public bool SelectAllChecked
      {
         get
         {
            return _selectAllChecked;
         }
         set
         {
            _selectAllChecked = value;

            //UpdateAllViewExtensions();
            OnPropertyChanged();
         }
      }

      public bool SelectNoneChecked
      {
         get
         {
            return _selectNoneChecked;
         }
         set
         {
            _selectNoneChecked = value;

            //UpdateAllViewExtensions();
            OnPropertyChanged();
         }
      }

      public ObservableCollection<SheetExtension> AllSheetsExtension { get; set; } = new ObservableCollection<SheetExtension>();
      public List<ViewExtension> SelectedSheetsExtension { get; set; }
      public ObservableCollection<SheetExtension> AllSheetsToRetain { get; set; } = new ObservableCollection<SheetExtension>();
      public List<ViewExtension> SelectedSheetsToDelete { get; set; }
    = new List<ViewExtension>();
      #endregion

      #region Constructor
      /// <summary>
      /// Constructor
      /// </summary>
      /// <param name="uidoc"></param>
      public DeleteSheetViewModel(UIDocument uidoc)
      {
         // Save data from Revit to our fields
         _uidoc = uidoc;
         _doc = uidoc.Document;

         // Identify WPF
         allSheets = new FilteredElementCollector(_doc).OfClass(typeof(ViewSheet)).Cast<ViewSheet>().ToList();

         foreach (ViewSheet viewSheet in allSheets)
         {
            SheetExtension level1 = new SheetExtension(viewSheet);
            AllSheetsExtension.Add(level1);
         }

         OnPropertyChanged("AllSheetsExtension");

      }
      #endregion

   }
}