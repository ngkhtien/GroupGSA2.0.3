#region Namespaces

using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using GroupGSA.PresentationWPF.ViewModels;
using GroupGSA.PresentationWPF.Views;
using System.IO;
using System.Reflection;
using Application = Autodesk.Revit.ApplicationServices.Application;

#endregion Namespaces

namespace GroupGSA.Command
{
   /// <summary>
   /// Delete unused views
   /// </summary>
   [Transaction(TransactionMode.Manual)]
   public class CmdDeleteSchedule : IExternalCommand
   {
      /// <summary>
      /// 
      /// </summary>
      /// <param name="commandData"></param>
      /// <param name="message"></param>
      /// <param name="elements"></param>
      /// <returns></returns>
      public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
      {
         UIApplication uiapp = commandData.Application;
         UIDocument uidoc = uiapp.ActiveUIDocument;
         Application app = uiapp.Application;
         Document Doc = uidoc.Document;

         string dllFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
         AssemblyLoader.LoadAllRibbonAssemblies(dllFolder);

         string actionName = "GSA | Delete Schedules";

         using (TransactionGroup transGroup = new TransactionGroup(Doc))
         {
            transGroup.Start(actionName);

            DeleteScheduleViewModel viewModel = new DeleteScheduleViewModel(uidoc);

            DeleteScheduleWindow window = new DeleteScheduleWindow(viewModel);

            bool? showDialog = window.ShowDialog();

            if (showDialog == null || showDialog == false)
            {
               //return Result.Cancelled;
            }

            transGroup.Assimilate();
            return Result.Succeeded;
         }
      }
   }
}
