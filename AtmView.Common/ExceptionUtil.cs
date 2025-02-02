using System;

namespace AtmView.Common
{
    public class ExceptionUtil
    {
        public static string ExceptionMessage(Exception ex)
        {
            var exmessage = " exmessage: " + ex.Message + Environment.NewLine;
            var innerex = ex.InnerException;
            if (innerex != null)
            {
                exmessage += "Inner ex: ";
                while (innerex != null)
                {
                    exmessage += innerex.Message + Environment.NewLine;
                    innerex = innerex.InnerException;
                }
            }
            exmessage += ex.StackTrace.ToString() + Environment.NewLine;
            return exmessage;
        }
    }
}