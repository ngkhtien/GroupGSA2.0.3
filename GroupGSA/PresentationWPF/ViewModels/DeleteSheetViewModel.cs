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
      List<string> sheetNumber = new List<string>();
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

         foreach (ViewSheet viewSheet in allSheets)
         {
            SheetExtension level1 = new SheetExtension(viewSheet);
            AllSheetsExtension.Add(level1);
         }

         SortSheetList(sheetNumber, AllSheetsExtension);

         OnPropertyChanged("AllSheetsExtension");
      }
      #endregion

      /// <summary>
      /// Sort sheet list by sheet number
      /// </summary>
      /// <param name="sNumber"></param>
      /// <param name="sExtension"></param>
      public void SortSheetList(List<string> sNumber, ObservableCollection<SheetExtension> sExtension)
      {
         sNumber = new List<string>();

         foreach (SheetExtension sheet in sExtension)
         {
            sNumber.Add(sheet.SheetNumber);
         }

         sNumber.Sort();

         for (int i = 0; i < sNumber.Count; i++)
         {
            for (int j = 0; j < sNumber.Count; j++)
            {
               if (sExtension[j].SheetNumber == sNumber[i])
               {
                  sExtension.Move(j, i);
                  break;
               }
            }
         }
      }

      /// <summary>
      /// Add sheets to retain
      /// </summary>
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

         SortSheetList(sheetNumber, AllSheetsToRetain);
      }

      /// <summary>
      /// Add sheet to remove
      /// </summary>
      public void RemoveSheets()
      {
         List<SheetExtension> selectedSheetsToDelete = new List<SheetExtension>(SelectedSheetsToDelete);

         foreach (SheetExtension sheetExtension in selectedSheetsToDelete)
         {
            AllSheetsExtension.Add(sheetExtension);
            AllSheetsToRetain.Remove(sheetExtension);
            sheetExtension.IsSelected = false;
         }

         SortSheetList(sheetNumber, AllSheetsExtension);
      }

      /// <summary>
      /// Delete sheet
      /// </summary>
      public void DeleteSheet()
      {
         int countSheetDelete = 0;

         IList<ElementId> sheetIdToDelete = new List<ElementId>();

         foreach (SheetExtension iSheetExtension in AllSheetsExtension)
         {
            if (iSheetExtension.Sheet.Id != _doc.ActiveView.Id)
            {
               sheetIdToDelete.Add(iSheetExtension.Sheet.Id);
            }
         }

         string caption = "GSA | Delete Sheet";

         // Transaction delete sheets in project
         using (Transaction trans = new Transaction(_doc))
         {
            trans.Start(caption);

            foreach (ElementId elementId in sheetIdToDelete)
            {
               try
               {
                  _doc.Delete(elementId);
                  countSheetDelete += 1;
               }
               catch { }

            }

            trans.Commit();
         }

         MessageBox.Show("Deleted " + countSheetDelete + " Sheets!", caption, MessageBoxButton.OK, (MessageBoxImage)MessageBoxIcon.Information);
      }
   }
}