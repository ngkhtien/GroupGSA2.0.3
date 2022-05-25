using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;


namespace GroupGSA.PresentationWPF.ViewModels
{
   public class DeleteScheduleViewModel : ViewModelBase
   {
      #region Field
      private UIDocument _uidoc;
      private Document _doc;
      public bool _selectAllChecked;
      public bool _selectNoneChecked;
      public List<ViewSchedule> allSchedule = new List<ViewSchedule>();
      List<string> scheduleName = new List<string>();
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

      public ObservableCollection<ScheduleExtension> AllSchedulesExtension { get; set; } = new ObservableCollection<ScheduleExtension>();
      public List<ScheduleExtension> SelectedSchedulesExtension { get; set; }
      public ObservableCollection<ScheduleExtension> AllSchedulesToRetain { get; set; } = new ObservableCollection<ScheduleExtension>();
      public List<ScheduleExtension> SelectedSchedulesToDelete { get; set; } = new List<ScheduleExtension>();
      #endregion

      #region Constructor
      /// <summary>
      /// Constructor
      /// </summary>
      /// <param name="uidoc"></param>
      public DeleteScheduleViewModel(UIDocument uidoc)
      {
         // Save data from Revit to our fields
         _uidoc = uidoc;
         _doc = uidoc.Document;

         // Identify WPF
         allSchedule = new FilteredElementCollector(_doc).OfClass(typeof(ViewSchedule)).Cast<ViewSchedule>().ToList();

         foreach (ViewSchedule viewSchedule in allSchedule)
         {
            if ((viewSchedule.IsTitleblockRevisionSchedule == false) & (viewSchedule.IsInternalKeynoteSchedule == false))
            {
               ScheduleExtension level1 = new ScheduleExtension(viewSchedule);
               AllSchedulesExtension.Add(level1);
            }
         }

         SortScheduleList(scheduleName, AllSchedulesExtension);

         OnPropertyChanged("AllScheduleExtension");
      }
      #endregion

      public void SortScheduleList(List<string> sName, ObservableCollection<ScheduleExtension> sExtension)
      {
         sName = new List<string>();

         foreach (ScheduleExtension schedule in sExtension)
         {
            sName.Add(schedule.Name);
         }

         sName.Sort();

         for (int i = 0; i < sName.Count; i++)
         {
            for (int j = 0; j < sName.Count; j++)
            {
               if (sExtension[j].Name == sName[i])
               {
                  sExtension.Move(j, i);
                  break;
               }
            }
         }
      }

      public void AddSchedules()
      {
         List<ScheduleExtension> allSchedulesExtension = new List<ScheduleExtension>(AllSchedulesExtension);

         foreach (ScheduleExtension scheduleExtension in allSchedulesExtension)
         {
            if (scheduleExtension.IsSelected)
            {
               AllSchedulesToRetain.Add(scheduleExtension);
               AllSchedulesExtension.Remove(scheduleExtension);
            }
         }

         SortScheduleList(scheduleName, AllSchedulesToRetain);
      }

      public void RemoveSchedules()
      {
         List<ScheduleExtension> selectedSchedulesToDelete = new List<ScheduleExtension>(SelectedSchedulesToDelete);

         foreach (ScheduleExtension scheduleExtension in selectedSchedulesToDelete)
         {
            AllSchedulesExtension.Add(scheduleExtension);
            AllSchedulesToRetain.Remove(scheduleExtension);
            scheduleExtension.IsSelected = false;
         }

         SortScheduleList(scheduleName, AllSchedulesExtension);
      }

      public void DeleteSchedule()
      {
         int countScheduleDelete = 0;

         IList<ElementId> scheduleIdToDelete = new List<ElementId>();

         foreach (ScheduleExtension iScheduleExtension in AllSchedulesExtension)
         {
            if (iScheduleExtension.Schedule.Id != _doc.ActiveView.Id)
            {
               scheduleIdToDelete.Add(iScheduleExtension.Schedule.Id);
            }
         }

         string caption = "GSA | Delete Schedule";

         // Transaction delete sheets in project
         using (Transaction trans = new Transaction(_doc))
         {
            trans.Start(caption);

            foreach (ElementId elementId in scheduleIdToDelete)
            {
               try
               {
                  _doc.Delete(elementId);
                  countScheduleDelete += 1;
               }
               catch { }

            }

            trans.Commit();
         }

         if (countScheduleDelete > 1)
         {
            MessageBox.Show("Deleted " + countScheduleDelete + " Schedules!", caption, MessageBoxButton.OK, (MessageBoxImage)MessageBoxIcon.Information);
         }
         else
         {
            MessageBox.Show("Deleted " + countScheduleDelete + " Schedule!", caption, MessageBoxButton.OK, (MessageBoxImage)MessageBoxIcon.Information);
         }
        
      }
   }
}
