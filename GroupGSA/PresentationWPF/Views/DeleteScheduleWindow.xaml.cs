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

      /// <summary>
      /// Constructor
      /// </summary>
      /// <param name="viewModel"></param>
      public DeleteScheduleWindow(DeleteScheduleViewModel viewModel)
      {
         InitializeComponent();

         _viewModel = viewModel;
         DataContext = viewModel;
      }

      /// <summary>
      /// Event select all
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
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

      /// <summary>
      /// Event unselect all
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
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

      /// <summary>
      /// Event add schedule to retain schedule list
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void BtnAddSchedulesClick(object sender, RoutedEventArgs e)
      {
         _viewModel.AddSchedules();
      }

      /// <summary>
      /// Event remove schedule from retain schedule list
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void BtnRemoveSchedulesClick(object sender, RoutedEventArgs e)
      {
         _viewModel.RemoveSchedules();
      }

      /// <summary>
      /// Event delete schedules
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void DeleteScheduleClick(object sender, RoutedEventArgs e)
      {
         _viewModel.DeleteSchedule();
         Close();
      }

      /// <summary>
      /// Event close
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void BtnCloseClick(object sender, RoutedEventArgs e)
      {
         Close();
      }
   }
}
