using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SoccerStandParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var driver = new ChromeDriver();
            String parentHandle = driver.CurrentWindowHandle.ToString();
            driver.Navigate().GoToUrl("http://www.soccerstand.com/soccer/england/championship-2016-2017/results/");
            
            var listOfDicts = new List<Dictionary<string, string>>();
            var round = "";
            var seeMore = "#tournament-page-results-more > tbody > tr > td > a"; 
            var leagueTables = "#fs-results > table:nth-child(2) > tbody > tr";
            var oddsComparison = "a-match-odds-comparison";
            var tHeaders = "#odds_1x2 > thead > tr > th";
            var tBody = "#odds_1x2 > tbody > tr";

            while (driver.FindElement(By.CssSelector(seeMore)).Displayed)
            {
                driver.FindElement(By.CssSelector(seeMore)).Click();
                Thread.Sleep(1500);
            }

            var rows = driver.FindElements(By.CssSelector(leagueTables));

            foreach (var row in rows)
            {
                if (row.GetAttribute("class").Equals("event_round"))
                {
                    Console.WriteLine(row.Text);
                    round = row.Text;
                    continue;
                }

                row.Click();
                Thread.Sleep(5000);
                driver.SwitchTo().Window(driver.WindowHandles[1]);
                Thread.Sleep(2000);
                driver.FindElement(By.Id(oddsComparison)).Click();

                Thread.Sleep(2000);
                var headers = driver.FindElementsByCssSelector(tHeaders);
                var rws = driver.FindElementsByCssSelector(tBody);

                var dict = new Dictionary<string, string>();
               
                var list = rws[0].Text.Split('\n').ToList();

                for (int i = 0; i < headers.Count; i++)
                {
                    dict.Add(headers[i].Text, list[i]);
                }

                listOfDicts.Add(dict);
                var infoDateTeamsScore = row.Text.Split('\n').ToList();

                driver.Close();
                driver.SwitchTo().Window(driver.WindowHandles[0]);
                round = "";
            }
            driver.Close();
           
        }
    }
}
