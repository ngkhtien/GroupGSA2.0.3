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
   public class DeleteViewViewModel : ViewModelBase
   {
      #region Field
      private UIDocument _uidoc;
      private Document _doc;
      public bool _selectAllChecked;
      public bool _selectNoneChecked;
      public List<View> allViews = new List<View>();
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

            UpdateAllViewExtensions();
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

            UpdateAllViewExtensions();
            OnPropertyChanged();
         }
      }

      public ObservableCollection<ViewExtension> AllViewsExtension { get; set; } = new ObservableCollection<ViewExtension>();
      public List<ViewExtension> SelectedViewsExtension { get; set; }
      public ObservableCollection<ViewExtension> AllViewsToRetain { get; set; } = new ObservableCollection<ViewExtension>();
      public List<ViewExtension> SelectedViewsToDelete { get; set; }
    = new List<ViewExtension>();

      #endregion

      #region Constructor
      /// <summary>
      /// Constructor
      /// </summary>
      /// <param name="uidoc"></param>
      public DeleteViewViewModel(UIDocument uidoc)
      {
         // Save data from Revit to our fields
         _uidoc = uidoc;
         _doc = uidoc.Document;

         // Identify WPF
         Initialize();
         UpdateAllViewExtensions();
      }
      #endregion

      private void Initialize()
      {
         allViews = new FilteredElementCollector(_doc).OfClass(typeof(View)).Cast<View>().ToList();
      }

      /// <summary>
      /// Update all view extension
      /// </summary>
      private void UpdateAllViewExtensions()
      {
         ViewExtension level1 = new ViewExtension("Views (all)");
         List<ViewType> viewTypes = ViewUtils.GetAllViewsType(_doc, SelectAllChecked, allViews);

         foreach (ViewType viewType in viewTypes)
         {
            List<View> allViewOfViewType = ViewUtils.GetAllViewsWithViewType(_doc, viewType,
               SelectAllChecked, allViews);

            if (allViewOfViewType.Count() == 0)
            {
               continue;
            }

            ViewExtension level2 = new ViewExtension(viewType);

            level1.ViewItems.Add(level2);

            foreach (View view in allViewOfViewType)
            {
               ViewExtension level3 = new ViewExtension(view);
               level2.ViewItems.Add(level3);
            }
         }

         AllViewsExtension = new ObservableCollection<ViewExtension>();
         AllViewsExtension.Add(level1);

         OnPropertyChanged("AllViewsExtension");
      }

      /// <summary>
      /// Add View to List Retain
      /// </summary>
      public void AddViews()
      {
         foreach (ViewExtension ve in AllViewsExtension)
         {
            ViewExtension level1 = ve;
            List<ViewExtension> level2
                = level1.ViewItems.ToList();

            foreach (ViewExtension lv2 in level2)
            {
               List<ViewExtension> views
                   = new List<ViewExtension>(lv2.ViewItems);
               foreach (var v in views)
               {
                  if (v.IsSelected)
                  {
                     AllViewsToRetain.Add(v);
                     lv2.ViewItems.Remove(v);
                  }
               }

               if (!lv2.ViewItems.Any())
               {
                  level1.ViewItems.Remove(lv2);
               }
            }
         }
      }

      /// <summary>
      /// Remove view from list retain
      /// </summary>
      public void RemoveViews()
      {
         ViewExtension level1 = AllViewsExtension[0];
         List<ViewExtension> level2List = level1.ViewItems.ToList();

         // Nếu remove trực tiếp property SelectedViewsToRename thì sẽ báo lỗi
         List<ViewExtension> selectedViewsToDelete
            = new List<ViewExtension>(SelectedViewsToDelete);

         foreach (ViewExtension v in selectedViewsToDelete)
         {
            AllViewsToRetain.Remove(v);

            bool isHaveLevel2 = false;
            foreach (ViewExtension lv2 in level2List)
            {
               string viewTypeName = v.ViewType.ToString();
               if (viewTypeName == "ThreeD")
               {
                  viewTypeName = "3D Views";
               }
               else if (viewTypeName == "FloorPlan")
               {
                  viewTypeName = "Floor Plans";
               }
               else if (viewTypeName == "CeilingPlan")
               {
                  viewTypeName = "Ceiling Plans";
               }
               else if (viewTypeName == "Elevation")
               {
                  viewTypeName = "Elevations";
               }

               if (lv2.Name.Equals(viewTypeName))
               {
                  lv2.ViewItems.Add(v);
                  v.IsSelected = false;
                  isHaveLevel2 = true;
                  break;
               }
            }

            if (!isHaveLevel2)
            {
               ViewExtension lv2
                   = new ViewExtension(v.ViewType);
               level1.ViewItems.Add(lv2);
               level2List = level1.ViewItems.ToList();
               lv2.ViewItems.Add(v); 
               v.IsSelected = false;
            }
         }
      }

      /// <summary>
      /// Event check on tree view
      /// </summary>
      public void CheckOnTreeView()
      {
         foreach (ViewExtension ve in AllViewsExtension)
         {
            ViewExtension level1 = ve;
            
            if (level1.IsSelected == true)
            {
               foreach (ViewExtension vT in level1.ViewItems)
               {
                  vT.IsSelected = true;

                  foreach (ViewExtension vE in vT.ViewItems)
                  {
                     vE.IsSelected = true;
                  }
               }
            }
            else
            {
               foreach (ViewExtension vT in level1.ViewItems)
               {
                  if (vT.IsSelected == true)
                  {
                     foreach (ViewExtension vE in vT.ViewItems)
                     {
                        vE.IsSelected = true;
                     }
                  }
               }
            }

         }
      }

      /// <summary>
      /// Event uncheck on tree view
      /// </summary>
      public void UnCheckTreeView()
      {
         foreach (ViewExtension ve in AllViewsExtension)
         {
            ViewExtension level1 = ve;

            foreach (ViewExtension vT in level1.ViewItems)
            {
               if (vT.IsSelected == false)
               {
                  foreach (ViewExtension vE in vT.ViewItems)
                  {
                     vE.IsSelected = false;
                  }
               }
            }
         }
      }

      /// <summary>
      /// Delete views in project
      /// </summary>
      public void DeleteView()
      {
         int countViewDelete = 0;

         IList<ElementId> viewIdToDelete = new List<ElementId>();

         ViewExtension allViews = AllViewsExtension[0];
         List<ViewExtension> allViewType = allViews.ViewItems.ToList();

         foreach (ViewExtension iViewType in allViewType)
         {
            List<ViewExtension> views = new List<ViewExtension>(iViewType.ViewItems);

            foreach (ViewExtension v in views)
            {
               if (v.View.Id != _doc.ActiveView.Id)
               {
                  viewIdToDelete.Add(v.View.Id);
               }
            }
         }

         // Transaction delete views in project
         using (Transaction trans = new Transaction(_doc))
         {
            trans.Start("Delete Views");

            foreach (ElementId elementId in viewIdToDelete)
            {
               try
               {
                  _doc.Delete(elementId);
                  countViewDelete += 1;
               }
               catch { }

            }

            trans.Commit();
         }

         MessageBox.Show("Deleted " + countViewDelete + " Views!", "Delete View", MessageBoxButton.OK, (MessageBoxImage)MessageBoxIcon.Information);
      }
   }
}
