using System;
using System.IO;
using System.Linq;
using static Parser.Utils;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using Newtonsoft.Json;
using MongoDB.Bson;
using MongoDB.Driver.Core;
using MongoDB.Driver;

namespace Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            //var driver = new ChromeDriver();

            var listOfDates = GetDatesFromSpecificDate(2017, 7, 1);

           //List<string> tableCategorieslist = File.ReadAllLines("C:\\scores\\categories.txt").ToList();

            foreach (var date in listOfDates)
            {
                var url = "https://www.parimatch.com/en/bet.html?ha=" + date;                                // URL для парсинга
                var web = new HtmlWeb();
                var doc = web.Load(url);
                var res = doc.DocumentNode;

                ModelObject mo = new ModelObject(date, res.OuterHtml);

                var json = JsonConvert.SerializeObject(mo);

                //File.AppendAllText("C:\\scores\\" + date + ".txt", res.OuterHtml);

                var connectionString = "mongodb://admin:admin123@ds038319.mlab.com:38319/betram";

                var client = new MongoClient(connectionString);

                var db = client.GetDatabase("betram");

                var collection = db.GetCollection<ModelObject>("parimatch_htmls");

                collection.InsertOne(new ModelObject("20170423", "html code"));

                //driver.Navigate().GoToUrl(url);

                //var allTables = driver.FindElements(By.CssSelector("#f1 > div"));                           // Выгружаем в переменную все таблицы страницы

                //.AppendAllText("C:\\scores\\" + date + ".txt", );

                //foreach (var tableItem in tableCategorieslist)
                //{
                //    var txtPath = "C:\\scores\\" + Regex.Replace(tableItem, "[.\r\n]", "").Replace(" ","_") + "_" + date + ".txt";
                //    IWebElement element = allTables.FirstOrDefault(table => table.Text.StartsWith(tableItem));    // Проход по всем таблицам в поисках нашего названия таблицы

                //    Engine engine = new Engine();
                //    engine.RunEngine(element, txtPath);
                //}
            }

            /*var reader = File.ReadAllLines(txtPath);

            var jsonPath = "C:\\scores\\scores.json";
            FileInfo file2 = new System.IO.FileInfo(jsonPath);
            if (!file2.Directory.Exists)
                file2.Directory.Create();
            else File.WriteAllText(file2.FullName, string.Empty);

            foreach (var el in reader)
            {
                var values = el.Split(',');
                PremierLiagueModel item = new PremierLiagueModel(values[0], values[1], values[2], values[3],
                    values[4], values[5], values[6], values[7], values[8], values[9], values[10], values[11],
                    values[12], values[13], values[14], values[15], values[16], values[17], values[18], values[19],
                    values[20], values[21], values[22], values[25].Replace(" ", ""));

                var sz = JsonConvert.SerializeObject(item);

                File.AppendAllText(jsonPath, sz);
            }*/

            //Wait(driver, 10);

            //driver.Quit();
        }   
    }

    class ModelObject
    {

        public string Date { get; set; }
        public string HtmlCode { get; set; }

        public ModelObject(string date, string htmlCode)
        {
            Date = date;
            HtmlCode = htmlCode;
        }

    }

    //    public int Number { get; set; }
    //    public  string Date { get; set; }
    //    public   string Time { get; set; }
    //    public   string TeamFirst { get; set; } //Event
    //    public   string TeamSecond { get; set; } //Event
    //    public   string HandicapValueFirst { get; set; }
    //    public   string HandicapValueSecond { get; set; }
    //    public   string HandicapCoefficientFirst { get; set; } //Odds
    //    public   string HandicapCoefficientSecond { get; set; } // Odds
    //    public   string Total { get; set; }
    //    public   string Over { get; set; }
    //    public   string Under { get; set; }
    //    public   string HomeWin { get; set; }
    //    public   string Draw { get; set; }
    //    public   string AwayWin { get; set; }
    //    public   string HomeWinOrDraw { get; set; }
    //    public   string HomeOrAwayWin { get; set; }
    //    public   string AwayWinOrDraw { get; set; }
    //    public   string TeamTotalFirst { get; set; }
    //    public   string TeamTotalSecond { get; set; }
    //    public   string OverTotalFirst { get; set; }
    //    public   string OverTotalSecond { get; set; }
    //    public   string UnderTotalFirst { get; set; }
    //    public   string UnderTotalSecond { get; set; }
    //    public   string Score { get; set; }

    //    public PremierLiagueModel(int number, string date, string time, string teamFirst, string teamSecond, string handicapValueFirst, string handicapValueSecond, string handicapCoefficientFirst, string handicapCoefficientSecond, string total, string over, string under, string homeWin, string draw, string awayWin, string homeWinOrDraw, string homeOrAwayWin, string teamTotalFirst, string teamTotalSecond,string overTotalFirst , string overTotalSecond, string underTotalFirst, string underTotalSecond, string score)
    //    {
    //        Number = number;
    //        Date = date;
    //        Time = time;
    //        TeamFirst = teamFirst;
    //        TeamSecond = teamSecond;
    //        HandicapValueFirst = handicapValueFirst;
    //        HandicapValueSecond = handicapValueSecond;
    //        HandicapCoefficientFirst = handicapCoefficientFirst;
    //        HandicapCoefficientSecond = handicapCoefficientSecond;
    //        Total = total;
    //        Over = over;
    //        Under = under;
    //        HomeWin = homeWin;
    //        Draw = draw;
    //        AwayWin = awayWin;
    //        HomeWinOrDraw = homeWinOrDraw;
    //        HomeOrAwayWin = homeOrAwayWin;
    //        TeamTotalFirst = teamTotalFirst;
    //        TeamTotalSecond = teamTotalSecond;
    //        OverTotalFirst = overTotalFirst;
    //        OverTotalSecond = overTotalSecond;
    //        UnderTotalFirst = underTotalFirst;
    //        UnderTotalSecond = underTotalSecond;
    //        Score = score;


    //    }

        
    //}

}


//private static void Engine(ReadOnlyCollection<IWebElement> rows, FileInfo file)
//{
//    foreach (var row in rows.Skip(1))
//    {

//        if (row.Text.Equals(""))
//        {
//            File.AppendAllText(file.FullName, Environment.NewLine);
//            continue;
//        }


//        StringBuilder sb = new StringBuilder(row.Text);
//        for (int i = 0; i < sb.Length; i++)
//        {
//            if (!sb[i].ToString().Any(char.IsLetter))
//            {
//                sb[i] = '\b';

//            }
//            else break;
//        }


//        var newSb = sb.Replace(' ', ',').Replace("\b", "");

//        for (int i = 0; i < newSb.Length - 1; i++)
//        {
//            if (newSb[i].Equals(','))
//                if (newSb[i + 1].ToString().Any(char.IsLetter) &&
//                    newSb[i - 1].ToString().Any(char.IsLetter) ||
//                    newSb[i + 1].Equals('&') ||
//                    newSb[i - 1].Equals('&'))
//                    newSb[i] = ' ';
//        }

//        sb.Replace(Environment.NewLine, ",");
//        File.AppendAllText(file.FullName, newSb.ToString());
//    }
//}