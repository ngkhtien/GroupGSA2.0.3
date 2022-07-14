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
   [Transaction(TransactionMode.Manual)]
   public class CmdAbout : IExternalCommand
   {
      public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
      {
         UIApplication uiapp = commandData.Application;
         UIDocument uidoc = uiapp.ActiveUIDocument;
         Application app = uiapp.Application;
         Document Doc = uidoc.Document;

         string dllFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
         AssemblyLoader.LoadAllRibbonAssemblies(dllFolder);

         string actionName = "GSA | About";

         using (TransactionGroup transGroup = new TransactionGroup(Doc))
         {
            transGroup.Start(actionName);

            About window = new About();

            bool? showDialog = window.ShowDialog();

            transGroup.Assimilate();
            return Result.Succeeded;
         }
      }
   }
}
