using System;
using System.Text.RegularExpressions;

namespace Karnak.Domain.Common
{
    public class GuidValidation
    {
        public static bool GuidTryParse(Guid guid)
        {
            string correctString = guid.ToString();
            Guid guidOutPut;
            bool guidResTrue = Guid.TryParse(correctString, out guidOutPut);

            return guidResTrue;
        }
    }
}
