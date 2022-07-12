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
   /// Interaction logic for DeleteLinkWindow.xaml
   /// </summary>
   public partial class DeleteLinkWindow
   {
      private DeleteLinkViewModel _viewModel;

      /// <summary>
      /// Constructor
      /// </summary>
      /// <param name="viewModel"></param>
      public DeleteLinkWindow(DeleteLinkViewModel viewModel)
      {
         InitializeComponent();

         _viewModel = viewModel;
         DataContext = viewModel;
      }

      /// <summary>
      /// Event delete link
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void DeleteLinkClick(object sender, RoutedEventArgs e)
      {
         _viewModel.DeleteLink();
         Close();
      }

      /// <summary>
      /// Event close click
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void BtnCloseClick(object sender, RoutedEventArgs e)
      {
         Close();
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
            foreach (LinkExtension linkExtension in _viewModel.AllLinksExtension)
            {
               linkExtension.IsSelected = true;

               foreach (LinkExtension linkType in linkExtension.LinkItems)
               {
                  linkType.IsSelected = true;
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
         if (_viewModel != null)
         {
            foreach (LinkExtension linkExtension in _viewModel.AllLinksExtension)
            {
               linkExtension.IsSelected = false;

               foreach (LinkExtension linkType in linkExtension.LinkItems)
               {
                  linkType.IsSelected = false;
               }
            }
         }
      }

      /// <summary>
      /// Event add link
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void BtnAddLinksClick(object sender, RoutedEventArgs e)
      {
         _viewModel.AddLinks();
      }

      /// <summary>
      /// Event remove link
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void BtnRemoveLinksClick(object sender, RoutedEventArgs e)
      {
         _viewModel.RemoveLinks();
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
         _viewModel.UncheckTreeView();
      }
   } 
}
