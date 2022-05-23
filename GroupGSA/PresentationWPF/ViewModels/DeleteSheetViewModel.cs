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
      public List<SheetExtension> SelectedSheetsExtension { get; set; }
      public ObservableCollection<SheetExtension> AllSheetsToRetain { get; set; } = new ObservableCollection<SheetExtension>();
      public List<SheetExtension> SelectedSheetsToDelete { get; set; } = new List<SheetExtension>();
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

         List<string> sheetNumber = new List<string>();

         foreach (ViewSheet viewSheet in allSheets)
         {
            SheetExtension level1 = new SheetExtension(viewSheet);
            AllSheetsExtension.Add(level1);

            sheetNumber.Add(viewSheet.get_Parameter(BuiltInParameter.SHEET_NUMBER).AsString());
         }

         sheetNumber.Sort();

         for (int i = 0; i < sheetNumber.Count; i++)
         {
            for (int j = 0; j < sheetNumber.Count; j++)
            {
               if (AllSheetsExtension[j].SheetNumber == sheetNumber[i])
               {
                  AllSheetsExtension.Move(j, i);
                  break;
               }
            }         
         }

         OnPropertyChanged("AllSheetsExtension");
      }
      #endregion

      public void AddSheets()
      {
         List<SheetExtension> allSheetsExtension = new List<SheetExtension>(AllSheetsExtension);

         foreach (SheetExtension sheetExtension in allSheetsExtension)
         {
            if (sheetExtension.IsSelected)
            {
               AllSheetsToRetain.Add(sheetExtension);
               AllSheetsExtension.Remove(sheetExtension);
            }
         }
      }

      public void RemoveSheets()
      {
         List<SheetExtension> selectedSheetsToDelete = new List<SheetExtension>(SelectedSheetsToDelete);

         foreach (SheetExtension sheetExtension in selectedSheetsToDelete)
         {
            AllSheetsExtension.Add(sheetExtension);
            AllSheetsToRetain.Remove(sheetExtension);
            sheetExtension.IsSelected = false;
         }
      }
   }
}