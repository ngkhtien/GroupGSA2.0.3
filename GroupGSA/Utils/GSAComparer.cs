using System;
using System.Collections.Generic;
using Autodesk.Revit.DB;

namespace GroupGSA.Utils
{
    public class GSAComparer : IComparer<View>, 
        IComparer<ViewType>
    {
        public int Compare(View x, View y)
        {
            return string.Compare(x.Name, y.Name, StringComparison.Ordinal);
        }

        public int Compare(ViewType x, ViewType y)
        {
            return string.Compare(x.ToString(), y.ToString(), StringComparison.Ordinal);
        }

        //public int Compare(ViewExtension2 x, ViewExtension2 y)
        //{
        //    return string.Compare(x.View.Name, y.View.Name, StringComparison.Ordinal);
        //}
    }
}