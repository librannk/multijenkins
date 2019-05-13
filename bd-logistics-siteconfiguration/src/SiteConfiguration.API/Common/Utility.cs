using SiteConfiguration.API.RoutingRules.RequestReponceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SiteConfiguration.API.Common
{
    public static class Utility
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Guid GetNewGuid()
        {
            return Guid.NewGuid();
        }

        /// <summary>
        /// to validate the input string is in GUID format or not..
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool ValidateGUID(List<string> str)
        {
            bool IsGuid = true;

            foreach (var item in str)
            {
                IsGuid = Regex.IsMatch(item, @"(?im)^[{(]?[0-9A-F]{8}[-]?(?:[0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$", RegexOptions.Compiled);

                if (!IsGuid)
                {
                    IsGuid = false;
                    break;
                }
            }
            return IsGuid;
        }

        /// <summary>
        /// to convert the input string in GUID format..
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Guid ParseStringToGuid(string str)
        {
            return Guid.Parse(str);
        }

    }
}
