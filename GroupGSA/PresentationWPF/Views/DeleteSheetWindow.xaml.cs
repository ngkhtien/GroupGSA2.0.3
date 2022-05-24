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
   /// Interaction logic for DeleteSheetWindow.xaml
   /// </summary>
   public partial class DeleteSheetWindow
   {
      private DeleteSheetViewModel _viewModel;

      /// <summary>
      /// Constructor
      /// </summary>
      /// <param name="viewModel"></param>
      public DeleteSheetWindow(DeleteSheetViewModel viewModel)
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
            foreach (SheetExtension sheetExtension in _viewModel.AllSheetsExtension)
            {
               sheetExtension.IsSelected = true;
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
            foreach (SheetExtension sheetExtension in _viewModel.AllSheetsExtension)
            {
               sheetExtension.IsSelected = false;
            }
         }
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

      /// <summary>
      /// Event delete sheets
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void DeleteSheetClick(object sender, RoutedEventArgs e)
      {
         _viewModel.DeleteSheet();
         Close();
      }

      /// <summary>
      /// Event add sheet to retain sheet list
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void BtnAddSheetsClick(object sender, RoutedEventArgs e)
      {
         _viewModel.AddSheets();
      }

      /// <summary>
      /// Event remove sheet from retain sheet list
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void BtnRemoveSheetsClick(object sender, RoutedEventArgs e)
      {
         _viewModel.RemoveSheets();
      }
   }
}
