using GroupGSA.PresentationWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GroupGSA.PresentationWPF.Views
{
   /// <summary>
   /// Interaction logic for DeleteScheduleWindow.xaml
   /// </summary>
   public partial class DeleteScheduleWindow
   {
      private DeleteScheduleViewModel _viewModel;

      public DeleteScheduleWindow(DeleteScheduleViewModel viewModel)
      {
         InitializeComponent();

         _viewModel = viewModel;
         DataContext = viewModel;
      }

      private void SelectAllChecked(object sender, RoutedEventArgs e)
      {
         if (_viewModel != null)
         {
            foreach (ScheduleExtension scheduleExtension in _viewModel.AllSchedulesExtension)
            {
               scheduleExtension.IsSelected = true;
            }
         }
      }

      private void SelectNoneChecked(object sender, RoutedEventArgs e)
      {
         if (_viewModel != null)
         {
            foreach (ScheduleExtension scheduleExtension in _viewModel.AllSchedulesExtension)
            {
               scheduleExtension.IsSelected = false;
            }
         }
      }

      private void BtnAddSchedulesClick(object sender, RoutedEventArgs e)
      {
         _viewModel.AddSchedules();
      }

      private void BtnRemoveSchedulesClick(object sender, RoutedEventArgs e)
      {
         _viewModel.RemoveSchedules();
      }



      private void DeleteScheduleClick(object sender, RoutedEventArgs e)
      {
         _viewModel.DeleteSchedule();
         Close();
      }

      private void BtnCloseClick(object sender, RoutedEventArgs e)
      {
         Close();
      }
   }
}
