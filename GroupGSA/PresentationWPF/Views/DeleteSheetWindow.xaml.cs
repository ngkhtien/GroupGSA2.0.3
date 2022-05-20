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
      public DeleteSheetWindow(DeleteSheetViewModel viewModel)
      {
         InitializeComponent();

         _viewModel = viewModel;
         DataContext = viewModel;
      }

      private void SelectAllChecked(object sender, RoutedEventArgs e)
      {

      }

      private void SelectNoneChecked(object sender, RoutedEventArgs e)
      {

      }

      private void BtnCloseClick(object sender, RoutedEventArgs e)
      {

      }

      private void DeleteSheetClick(object sender, RoutedEventArgs e)
      {

      }

      private void BtnAddViewsClick(object sender, RoutedEventArgs e)
      {

      }

      private void BtnRemoveViewsClick(object sender, RoutedEventArgs e)
      {

      }

      private void TreeViewOnChecked(object sender, RoutedEventArgs e)
      {

      }

      private void TreeViewUnChecked(object sender, RoutedEventArgs e)
      {

      }
   }
}
