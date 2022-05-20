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
   /// Interaction logic for DeleteViewView.xaml
   /// </summary>
   public partial class DeleteViewWindow
   {
      private DeleteViewViewModel _viewModel;

      /// <summary>
      /// Constructor
      /// </summary>
      public DeleteViewWindow(DeleteViewViewModel viewModel)
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
            foreach (ViewExtension v in _viewModel.AllViewsExtension)
            {
               v.IsSelected = true;

               foreach (ViewExtension vT in v.ViewItems)
               {
                  vT.IsSelected = true;

                  foreach (ViewExtension vE in vT.ViewItems)
                  {
                     vE.IsSelected = true;
                  }
               }
            }
         }
      }

      /// <summary>
      /// Event select none
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void SelectNoneChecked(object sender, RoutedEventArgs e)
      {
         foreach (ViewExtension v in _viewModel.AllViewsExtension)
         {
            v.IsSelected = false;

            foreach (ViewExtension vT in v.ViewItems)
            {
               vT.IsSelected = false;

               foreach (ViewExtension vE in vT.ViewItems)
               {
                  vE.IsSelected = false;
               }
            }
         }
      }

      /// <summary>
      /// Event close click
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void BtnCloseClick(object sender, RoutedEventArgs e)
      {
         //DialogResult = false;
         Close();
      }

      /// <summary>
      /// Event OK click
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void DeleteViewClick(object sender, RoutedEventArgs e)
      {
         _viewModel.DeleteView();
         Close();
      }

      /// <summary>
      /// Event check on tree view
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void TreeViewOnChecked(object sender, RoutedEventArgs e)
      {
         _viewModel.CheckOnTreeView();
      }

      /// <summary>
      /// Event uncheck on tree view
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void TreeViewUnChecked(object sender, RoutedEventArgs e)
      {
         _viewModel.UnCheckTreeView();
      }

      /// <summary>
      /// Event Add View ">"
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void BtnAddViewsClick(object sender, RoutedEventArgs e)
      {
         _viewModel.AddViews();
      }

      /// <summary>
      /// Event remove view "<"
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void BtnRemoveViewsClick(object sender, RoutedEventArgs e)
      {
         _viewModel.RemoveViews();
      }

   }
}
