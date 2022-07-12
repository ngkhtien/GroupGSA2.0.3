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

      private void DeleteLinkClick(object sender, RoutedEventArgs e)
      {
         _viewModel.DeleteLink();
         Close();
      }

      private void BtnCloseClick(object sender, RoutedEventArgs e)
      {
         Close();
      }

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

      private void BtnAddLinksClick(object sender, RoutedEventArgs e)
      {
         _viewModel.AddLinks();
      }

      private void BtnRemoveLinksClick(object sender, RoutedEventArgs e)
      {
         _viewModel.RemoveLinks();
      }

      private void TreeViewOnChecked(object sender, RoutedEventArgs e)
      {
         _viewModel.CheckOnTreeView();
      }

      private void TreeViewUnChecked(object sender, RoutedEventArgs e)
      {
         _viewModel.UncheckTreeView();
      }
   } 
}
