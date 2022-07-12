using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace GroupGSA.PresentationWPF.ViewModels
{
   public class DeleteLinkViewModel : ViewModelBase
   {
      #region Field
      private UIDocument _uidoc;
      private Document _doc;
      public bool _selectAllChecked;
      public bool _selectNoneChecked;
      public List<RevitLinkType> allRevitLink = new List<RevitLinkType>();
      public List<CADLinkType> allCADLink = new List<CADLinkType>();
      List<string> linkName = new List<string>();
      #endregion

      #region Properties
      /// <summary>
      /// Event select all
      /// </summary>
      public bool SelectAllChecked
      {
         get
         {
            return _selectAllChecked;
         }
         set
         {
            _selectAllChecked = value;

            OnPropertyChanged();
         }
      }

      /// <summary>
      /// Event unselect all
      /// </summary>
      public bool SelectNoneChecked
      {
         get
         {
            return _selectNoneChecked;
         }
         set
         {
            _selectNoneChecked = value;

            OnPropertyChanged();
         }
      }

      public ObservableCollection<LinkExtension> AllLinksExtension { get; set; } = new ObservableCollection<LinkExtension>();
      public List<LinkExtension> SelectedLinksExtension { get; set; }
      public ObservableCollection<LinkExtension> AllLinksToRetain { get; set; } = new ObservableCollection<LinkExtension>();
      public List<LinkExtension> SelectedLinksToDelete { get; set; } = new List<LinkExtension>();
      #endregion

      #region Constructor
      /// <summary>
      /// Constructor
      /// </summary>
      /// <param name="uidoc"></param>
      public DeleteLinkViewModel(UIDocument uidoc)
      {
         // Save data from Revit to our fields
         _uidoc = uidoc;
         _doc = uidoc.Document;

         // Find all RVT Links
         allRevitLink = new FilteredElementCollector(_doc).OfClass(typeof(RevitLinkType)).Cast<RevitLinkType>().ToList();

         LinkExtension RVTLinks = new LinkExtension("Revit Links");

         foreach (RevitLinkType revitLink in allRevitLink)
         {
            LinkExtension RVTLinkType = new LinkExtension(revitLink);
            RVTLinks.LinkItems.Add(RVTLinkType);
         }

         if (allRevitLink.Count == 0)
         {
            RVTLinks.Name = "No Revit Link found";
         }

         // Find all CAD Links
         allCADLink = new FilteredElementCollector(_doc).OfClass(typeof(CADLinkType)).Cast<CADLinkType>().ToList();

         LinkExtension CADLinks = new LinkExtension("CAD Links");

         foreach (CADLinkType CADLink in allCADLink)
         {
            LinkExtension linkCAD = new LinkExtension(CADLink);
            CADLinks.LinkItems.Add(linkCAD);
         }

         if (allCADLink.Count == 0)
         {
            CADLinks.Name = "No CAD Link found";
         }

         // Identify WPF 
         AllLinksExtension = new ObservableCollection<LinkExtension>();
         AllLinksExtension.Add(RVTLinks);
         AllLinksExtension.Add(CADLinks);

         SortLinkList(linkName, AllLinksExtension);

         OnPropertyChanged("AllLinksExtension");
      }
      #endregion

      /// <summary>
      /// Sort link list by name
      /// </summary>
      /// <param name="lName"></param>
      /// <param name="lExtension"></param>
      public void SortLinkList(List<string> lName, ObservableCollection<LinkExtension> lExtension)
      {
         lName = new List<string>();

         // Sort RVT Links
         LinkExtension RVTLinks = lExtension[0];

         List<LinkExtension> RVTLink = RVTLinks.LinkItems.ToList();

         foreach (LinkExtension linkExtension in RVTLink)
         {
            lName.Add(linkExtension.Name);
         }

         lName.Sort();

         for (int i = 0; i < lName.Count; i++)
         {
            for (int j = 0; j < lName.Count; j++)
            {
               if (lExtension[0].LinkItems[j].Name == lName[i]) 
               {
                  lExtension[0].LinkItems.Move(j, i);
                  break;
               }
            }
         }

         // Sort CAD Links
         lName = new List<string>();

         LinkExtension CADLinks = lExtension[1];

         List<LinkExtension> CADLink = CADLinks.LinkItems.ToList();

         foreach (LinkExtension linkExtension in CADLink)
         {
            lName.Add(linkExtension.Name);
         }

         lName.Sort();

         for (int i = 0; i < lName.Count; i++)
         {
            for (int j = 0; j < lName.Count; j++)
            {
               if (lExtension[1].LinkItems[j].Name == lName[i])
               {
                  lExtension[1].LinkItems.Move(j, i);
                  break;
               }
            }
         }
      }

      /// <summary>
      /// Add Links to List Retain
      /// </summary>
      public void AddLinks()
      {
         ObservableCollection<LinkExtension> allLinksExtension = new ObservableCollection<LinkExtension>(AllLinksExtension);

         foreach (LinkExtension linkExtension in allLinksExtension)
         {
            LinkExtension level1 = linkExtension;
            List<LinkExtension> level2
                = level1.LinkItems.ToList();

            foreach (LinkExtension lv2 in level2)
            {
               if (lv2.IsSelected)
               {
                  AllLinksToRetain.Add(lv2);
                  level1.LinkItems.Remove(lv2);
               }
            }

            if (!level1.LinkItems.Any())
            {
               AllLinksExtension.Remove(level1);
            }
         }

         SortLinkList(linkName, AllLinksExtension);
      }

      /// <summary>
      /// Remove Links from List Retain
      /// </summary>
      public void RemoveLinks()
      {
         List<LinkExtension> selectedLinksToDelete = new List<LinkExtension>(SelectedLinksToDelete);

         foreach (LinkExtension linkExtension in selectedLinksToDelete)
         {
            AllLinksToRetain.Remove(linkExtension);

            if (linkExtension.IsRVTLink)
            {
               AllLinksExtension[0].LinkItems.Add(linkExtension);
               linkExtension.IsSelected = false;
            }
            else
            {
               AllLinksExtension[1].LinkItems.Add(linkExtension);
               linkExtension.IsSelected = false;
            }
         }

         SortLinkList(linkName, AllLinksExtension);
      }

      /// <summary>
      /// Event check on tree view
      /// </summary>
      public void CheckOnTreeView()
      {
         foreach (LinkExtension linkExtension in AllLinksExtension)
         {
            LinkExtension linkType = linkExtension;

            if (linkType.IsSelected == true)
            {
               foreach (LinkExtension linkSelected in linkType.LinkItems)
               {
                  linkSelected.IsSelected = true;
               }
            }
         }
      }

      /// <summary>
      /// Event uncheck on tree view
      /// </summary>
      public void UncheckTreeView()
      {
         foreach (LinkExtension linkExtension in AllLinksExtension)
         {
            LinkExtension linkType = linkExtension;

            if (linkType.IsSelected == false)
            {
               foreach (LinkExtension linkSelected in linkType.LinkItems)
               {
                  linkSelected.IsSelected = false;
               }
            }
         }
      }

      /// <summary>
      /// Purge Links in project
      /// </summary>
      public void DeleteLink()
      {
         // Get RVTLink Id
         IList<ElementId> RVTLinkToDelete = new List<ElementId>();

         foreach (LinkExtension RVTLink in AllLinksExtension[0].LinkItems)
         {
            RVTLinkToDelete.Add(RVTLink.RevitLink.Id);
         }

         // Get CADLink Id
         IList<ElementId> CADLinkToDelete = new List<ElementId>();

         foreach (LinkExtension CADLinkDelete in AllLinksExtension[1].LinkItems)
         {
            CADLinkToDelete.Add(CADLinkDelete.CADLink.Id);
         }

         string caption = "GSA | Purge Links";

         // Transaction Purge Links in project
         int countRVTLink = 0;
         int countCADLink = 0;

         using (Transaction trans = new Transaction(_doc))
         {
            trans.Start(caption);

            // Delete RVT Links
            foreach (ElementId elementId in RVTLinkToDelete)
            {
               try
               {
                  _doc.Delete(elementId);
                  countRVTLink += 1;
               }
               catch { }
            }

            // Delete CAD Links
            foreach (ElementId elementId in CADLinkToDelete)
            {
               try
               {
                  _doc.Delete(elementId);
                  countCADLink += 1;
               }
               catch { }
            }

            trans.Commit();
         }

         // Print RVT Link result
         if (countRVTLink > 1)
         {
            MessageBox.Show("Deleted " + countRVTLink + " RVT Links!", caption, MessageBoxButton.OK, (MessageBoxImage)MessageBoxIcon.Information);
         }
         else
         {
            MessageBox.Show("Deleted " + countRVTLink + " RVT Link!", caption, MessageBoxButton.OK, (MessageBoxImage)MessageBoxIcon.Information);
         }

         // Print CAD Link result
         if (countCADLink > 1)
         {
            MessageBox.Show("Deleted " + countCADLink + " CAD Links!", caption, MessageBoxButton.OK, (MessageBoxImage)MessageBoxIcon.Information);
         }
         else
         {
            MessageBox.Show("Deleted " + countCADLink + " CAD Link!", caption, MessageBoxButton.OK, (MessageBoxImage)MessageBoxIcon.Information);
         }
      }
   }
}
