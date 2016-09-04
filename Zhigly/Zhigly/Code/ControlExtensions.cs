using System.Collections.Generic;
using System.Web.UI;

namespace Zhigly.Code
{
    public static class ControlExtensions
    {
        public static List<T> GetAll<T>(this Control parent) where T : Control
        {
            var result = new List<T>();

            foreach (Control control in parent.Controls)
            {
                if (control is T)
                {
                    result.Add((T)control);
                }

                if (control.HasControls())
                {
                    result.AddRange(control.GetAll<T>());
                }
            }

            return result;
        }
    }
}