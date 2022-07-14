using System.Diagnostics;
using System.Windows.Navigation;

namespace GroupGSA.PresentationWPF.Views
{
   /// <summary>
   /// Interaction logic for About.xaml
   /// </summary>
   public partial class About
   {
      public About()
      {
         InitializeComponent();
      }
      private void OnNavigate(object sender, RequestNavigateEventArgs e)
      {
         Process.Start(e.Uri.AbsoluteUri);
         e.Handled = true;
      }
   }
}
