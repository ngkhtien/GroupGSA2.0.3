using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GroupGSA.PresentationWPF.ViewModels
{
   public class ViewModelBase : INotifyPropertyChanged
   {
      #region Implementation of INotifyPropertyChanged

      public event PropertyChangedEventHandler PropertyChanged = delegate { };

      protected virtual void SetProperty<T>(ref T member, T val, [CallerMemberName] string propertyName = null)
      {
         if (object.Equals(member, val)) return;

         member = val;
         PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
      }

      protected virtual void OnPropertyChanged([CallerMemberName] string propertyName="")
      {
         OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
      }

      protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
      {
         PropertyChanged(this, args);
      }

      #endregion
   }
}
