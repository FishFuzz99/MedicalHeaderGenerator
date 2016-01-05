using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MedicalHeaderBuilder
{
    public static class Class1
    {
        public static IEnumerable<Visual> GetChildren(this Visual parent)
        {
            if (parent != null)
            {
                int count = VisualTreeHelper.GetChildrenCount(parent);
                for (int i = 0; i < count; i++)
                {
                    var child = VisualTreeHelper.GetChild(parent, i) as Visual;

                    if (child != null)
                    {
                        yield return child;
                    }
                }
            }
        }
    }
}
