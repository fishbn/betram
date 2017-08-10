using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenQA.Selenium;

namespace Parser
{
    class Engine
    {
        public void RunEngine(IWebElement element, string txtPath)
        {
            if (element != null)
            {
                List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
                var allRows = element.FindElements(By.TagName("tr"));
                var allHeaders = element.FindElements(By.TagName("th"));

                foreach (var tr in allRows.Skip(1))                                                     // Skip(1) Пропускаем первый ряд - хедеры таблицы
                {
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    var tds = tr.FindElements(By.TagName("td"));

                    for (int i = 0; i < tds.Count; i++)
                    {
                        if (tds[i].Text.Equals(" ") || tds[i].Text.Equals(""))
                            continue;
                        if (tds.Count < 5 && tds[i].Text.Contains(":"))
                            dict.Add("Score", tds[i].Text);
                        else
                            try
                            {
                                dict.Add(allHeaders[i].Text, tds[i].Text);
                            }
                            catch (ArgumentException)
                            {
                                dict.Add(allHeaders[i].Text + " ", tds[i].Text);
                            }
                    }

                    if (dict.Count != 0)
                        list.Add(dict);
                }

                StringBuilder serialized = new StringBuilder();
                foreach (var item in list)
                {
                    serialized.Append(JsonConvert.SerializeObject(item));
                }

                File.AppendAllText(txtPath, serialized.ToString());
            }
        }
    }
}
