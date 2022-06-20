using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Interop;
using Application = Autodesk.Revit.ApplicationServices.Application;

namespace GroupGSA.Utils
{
   public static class Extensions
   {
      public static string ValueString(this Parameter para)
      {

         string valueString = string.Empty;
         if (para != null)
         {
            if (para.StorageType == StorageType.Integer)
            {
               valueString = para.AsInteger().ToString();
            }
            else if (para.StorageType == StorageType.Double)
            {
#if Version2020
                    double internalUnit = UnitUtils.ConvertFromInternalUnits(para.AsDouble(), para.DisplayUnitType);
                    valueString = internalUnit.ToString();
#else
               double internalUnit = UnitUtils.ConvertFromInternalUnits(para.AsDouble(), para.GetUnitTypeId());
               valueString = internalUnit.ToString();
#endif
            }
            else if (para.StorageType == StorageType.String || para.StorageType == StorageType.ElementId)
            {
               valueString = para.AsString();
               if (valueString == string.Empty || valueString == null)
               {

                  valueString = para.AsValueString();
               }
            }
         }

         if (valueString == null)
         {
            return valueString = string.Empty;
         }
         return valueString;
      }
      public static bool IsPhysicalElement(this Element e)
      {
         if (e.Category == null) return false;
         // does this produce same result as 
         // WhereElementIsViewIndependent ?
         if (e.ViewSpecific) return false;
         if (e.GetTypeId().IntegerValue == -1) return false;
         // exclude specific unwanted categories
         if (((BuiltInCategory)e.Category.Id.IntegerValue)
           == BuiltInCategory.OST_HVAC_Zones)
         {
            return false;
         }
         return e.Category.CategoryType == CategoryType.Model && e.Category.CanAddSubcategory;
      }

      /// <summary>
      /// Check valid Rule String in RuleDefine View
      /// </summary>
      /// <param name="ruleString"></param>
      /// <returns></returns>
      //public static bool CheckRuleString(string ruleString)
      //{
      //   bool checkResult = true;
      //   string[] arrRuleString = ruleString.Split(new[] { " " }, StringSplitOptions.None);
      //   int countOpenParenthesis = arrRuleString.Count(s => s == "(");
      //   int countCloseParenthesis = arrRuleString.Count(s => s == ")");
      //   if (countCloseParenthesis != countOpenParenthesis)
      //   {
      //      checkResult = false;
      //      return checkResult;
      //   }
      //   for (int i = 0; i < arrRuleString.Count(); i++)
      //   {
      //      if (i + 2 < arrRuleString.Count())
      //      {
      //         if (arrRuleString[i] == OperatorEnum.AND.ToString() ||
      //         arrRuleString[i] == OperatorEnum.OR.ToString() ||
      //         arrRuleString[i] == OperatorEnum.NOT.ToString())
      //         {
      //            if (arrRuleString[i + 1] == "" && arrRuleString[i + 2] == ")")
      //            {
      //               checkResult = false;
      //               return checkResult;
      //            }
      //         }

      //         if (arrRuleString[i] == "(" && arrRuleString[i + 1] == "" && arrRuleString[i + 2] == ")")
      //         {
      //            checkResult = false;
      //            return checkResult;
      //         }
      //      }
      //   }
      //   return checkResult;
      //}

      /// <summary>
      /// Show a dialog in same screen with Revit
      /// </summary>
      /// <param name="dialog"></param>
      /// <param name="app"></param>
      /// <returns></returns>
      public static bool? ShowDialog(Window dialog, UIApplication app)
      {
         new WindowInteropHelper(dialog).Owner = app.MainWindowHandle;
         return dialog.ShowDialog();
      }

      /// <summary>
      /// Show a dialog in same screen with Revit (used for case can not use show dialog)
      /// </summary>
      /// <param name="dialog"></param>
      /// <param name="app"></param>
      /// <returns></returns>
      public static void Show(Window dialog, UIApplication app)
      {
         new WindowInteropHelper(dialog).Owner = app.MainWindowHandle;
         dialog.Show();
      }

      /// <summary>
      /// Load All Model Option And Single Option
      /// </summary>
      /// <param name="doc"></param>
      /// <param name="app"></param>
      //public static void LoadAllModelOptionAndSingleOption(Document doc, Application app)
      //{
      //   if (BlackPointData.CurrentDocumentData.IsFirst == false)
      //   {
      //      BlackPointData.CurrentDocumentData.ModelOptions = SaveAndLoadOption.LoadModelOption(doc, true);
      //      SaveAndLoadOption.LoadKovaOption(doc);
      //      BlackPointData.CurrentDocumentData.SingleOptions = SaveAndLoadOption.LoadSingleOption(doc, app, true);
      //      BlackPointData.CurrentDocumentData.IsFirst = true;
      //   }
      //}

      ///// <summary>
      ///// Save Model Option And Single Option
      ///// </summary>
      ///// <param name="doc"></param>
      //public static void SaveAllOptionAndSingleOption(Document doc)
      //{
      //   SaveAndLoadOption.GetPathOfModelOptionAndSingleOption(doc, out string pathToKovaModelOption);
      //   SaveAndLoadOption.SaveModelOption(BlackPointData.CurrentDocumentData.ModelOptions, doc);
      //   SaveAndLoadOption.SaveSingleOption(BlackPointData.CurrentDocumentData.SingleOptions, doc);
      //}
   }
}
