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

    }
}
