using Autodesk.Revit.DB;
using System.Collections.ObjectModel;

namespace GroupGSA.PresentationWPF.ViewModels
{
   public class ScheduleExtension : ViewModelBase
   {
      #region Field
      private bool _isSelected;
      private string _name;
      #endregion

      #region Properties
      public ViewSchedule Schedule { get; set; }

      public ObservableCollection<ScheduleExtension> ScheduleItems { get; set; }

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
      public ScheduleExtension(ViewSchedule viewSchedule)
      {
         ScheduleItems = new ObservableCollection<ScheduleExtension>();
         Schedule = viewSchedule;
         Name = viewSchedule.Name;
         IsSelected = false;
      }
      #endregion
   }
}
