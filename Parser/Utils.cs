using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Parser
{
    class Utils
    {
        public static void Wait(IWebDriver s, int seconds)
        {
            s.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(seconds));
        }

        public static void CreateFile(string txtPath)
        {
            FileInfo file = new System.IO.FileInfo(txtPath);

            if (!file.Directory.Exists)
                file.Directory.Create();
            else File.WriteAllText(file.FullName, "");
        }

        public static List<string> GetDatesFromSpecificDate(int year, int month, int day)
        {
            DateTime date1 = new DateTime(year, month, day);
            List<string> list = new List<string>();

            while (date1 < DateTime.Today)
            {
                date1 = date1.AddDays(1);
                list.Add(date1.ToString("yyyyMMdd"));
            }

            return list;
        }

    }
}
