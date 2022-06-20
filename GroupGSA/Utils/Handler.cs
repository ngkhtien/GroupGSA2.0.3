using Autodesk.Revit.UI;
using GroupGSA.PresentationWPF.ViewModels;
using System;
using System.Windows.Forms;

namespace GroupGSA.Utils
{
   public class Handler : IExternalEventHandler
   {
      public DeleteViewViewModel ViewModel;

      /// <summary>
      /// Thực hiện các lệnh khi được Raise() lên
      /// </summary>
      /// <param name="app"></param>
      public void Execute(UIApplication app)
      {
         try
         {
            ViewModel.DeleteView();
         }
         catch (Exception e)
         {
            MessageBox.Show(e.ToString());
         }
      }

      public string GetName()
      {
         return "Q'Apps Solutions External Event";
      }
   }
}
