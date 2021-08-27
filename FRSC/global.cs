using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FRSC
{
    class global
    {
        public static string loggedUsername;
        public static string loggedFname;
        public static string loggedLname;

        public static string activeForm;

        public static Color inputBad = Color.FromArgb(244, 218, 218);
        public static Color inputGood = Color.FromArgb(197, 233, 205);
        public static Color inputDefault = Color.FromArgb(255, 255, 255);

        public const string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=frsc;";
        public const string blowFishKey = "br@viTec@#)(";

        public static string upperFirst(string str)
        {
            if (str == "")
                return "";
            return str;

            str = str.ToLower().Trim();
            return char.ToUpper(str[0]) + str.Substring(1);
        }
    }
}
