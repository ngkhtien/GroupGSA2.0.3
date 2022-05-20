﻿using System;
using System.IO;
using System.Windows.Media.Imaging;
using Autodesk.Revit.ApplicationServices;

namespace GroupGSA.Utils
{
   internal class GSAConstraint
   {
      /// <summary>
      /// C:\ProgramData\Autodesk\ApplicationPlugins\GroupGSA.bundle\Contents
      /// </summary>
      internal string ContentsFolder;

      /// <summary>
      /// C:\ProgramData\Autodesk\ApplicationPlugins\GroupGSA.bundle\Contents\Resources
      /// </summary>
      internal string ResourcesFolder;

      /// <summary>
      /// "C:\ProgramData\Autodesk\ApplicationPlugins\GroupGSA.bundle\Contents\Resources\Help"
      /// </summary>
      internal string HelpFolder;

      /// <summary>
      /// C:\ProgramData\Autodesk\ApplicationPlugins\GroupGSA.bundle\Contents\Resources\Image
      /// </summary>
      internal string ImageFolder;

      /// <summary>
      /// C:\ProgramData\Autodesk\ApplicationPlugins\GroupGSA.bundle\Contents\Resources\Setting
      /// </summary>
      internal static string SettingFolder
          = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
              "Autodesk", "ApplicationPlugins", "GroupGSA");

      /// <summary>
      ///  C:\ProgramData\Autodesk\ApplicationPlugins\GroupGSA.bundle\Contents\2019\dll
      /// </summary>
      internal string DllFolder;

      /// <summary>
      /// GROUP GSA TOOLS 2.0.3
      /// </summary>
      internal static string MessageBoxCaption = "GROUP GSA TOOLS 2.0.3";

      /// <summary>
      /// tên file ảnh 32x32 Other
      /// </summary>
      internal static string Other32x32 = "Other32x32.png";

      /// <summary>
      /// tên file ảnh 16x16 Other
      /// </summary>
      internal static string Other16x16 = "Other16x16.png";

      /// <summary>
      /// "C:\ProgramData\Autodesk\ApplicationPlugins\GroupGSA.bundle\Contents\Resources\Help\GroupGSA Tools 2.0.3 Guideline.pdf
      /// </summary>
      internal string HelperPath;

      /// <summary>
      /// Tên của file Share parameter
      /// </summary>
      internal string SharedParamsPath;

      /// <summary>
      /// file .dll chính
      /// </summary>
      internal string MainDllFileName;

      internal GSAConstraint(ControlledApplication a = null)
      {
         ContentsFolder =
             "C:\\ProgramData\\Autodesk\\ApplicationPlugins\\GroupGSA.bundle\\Contents";

         ResourcesFolder = Path.Combine(ContentsFolder, "Resources");
         HelpFolder = Path.Combine(ResourcesFolder, "Help");
         ImageFolder = Path.Combine(ResourcesFolder, "Image");
         SettingFolder = Path.Combine(ResourcesFolder, "Setting");

         SharedParamsPath = Path.Combine(SettingFolder, "GroupGSA_SharedParameter.txt");
         HelperPath = Path.Combine(HelpFolder, "GroupGSA Tools 2.0.3 Guideline.pdf");

         MainDllFileName = "GroupGSA_2.0.3.dll";

         if (a != null)
         {
            switch (a.VersionNumber)
            {
               case "2019":
                  DllFolder = Path.Combine(ContentsFolder, "2019");
                  break;
               case "2020":
                  DllFolder = Path.Combine(ContentsFolder, "2020");
                  break;
               case "2021":
                  DllFolder = Path.Combine(ContentsFolder, "2021");
                  break;
               case "2022":
                  DllFolder = Path.Combine(ContentsFolder, "2022");
                  break;
               case "2023":
                  DllFolder = Path.Combine(ContentsFolder, "2023");
                  break;
               case "2024":
                  DllFolder = Path.Combine(ContentsFolder, "2024");
                  break;
               case "2025":
                  DllFolder = Path.Combine(ContentsFolder, "2025");
                  break;
            }
         }
      }
   }
}
