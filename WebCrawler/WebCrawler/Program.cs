using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Models;

namespace WebCrawler
{
    class Program
    {
        private static _WebCrawlerContext db = new _WebCrawlerContext();

        public static string ShiftString(string t)
        {
            //Flips days and years in a date string
            return t.Substring(6, 4) + "-" + t.Substring(3, 2) + "-" + t.Substring(0, 2);
        }

        static void Main()
        {
            try
            {
                //Calling async function in a sync method
                Crawl().ConfigureAwait(false).GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        async static Task Crawl()
        {
            try
            {
                //initialize Http instance
                HttpClient client = new HttpClient();
                //get request for the home page contents
                var response = await client.GetAsync("https://www.libyantenders.ly/");
                //read the response from libyantenders as a string
                var pageContents = await response.Content.ReadAsStringAsync();
                HtmlDocument pageDocument = new HtmlDocument();
                //Convert string to html proper doc
                pageDocument.LoadHtml(pageContents);

                //Get number of tenders using XPATH -this may change-
                var tendersCountArray = pageDocument.DocumentNode.SelectSingleNode("(//div[contains(@class,'section-header')]//h6)").InnerText.Where(Char.IsDigit).ToArray();


                string tendersCountString = "";

                foreach (var integer in tendersCountArray)
                {
                    tendersCountString += integer;
                }

                //Save the number as integer value
                var tendersCount = Convert.ToInt32(tendersCountString);

                //Get number of tenders stored in your db
                var dbTendersCount = (from t in db.LibyanTenders
                                      select t).Count();

                if (tendersCount <= dbTendersCount)
                {
                    //If there is no new tenders, exit the function
                    throw new TaskCanceledException();
                }
                else
                {
                    //otherwise, loop through new tenders only
                    var myCounter = tendersCount - dbTendersCount;

                    //define storage generic lists
                    var tenders = new List<string>();
                    var fields = new List<string>();
                    var dates = new List<DateTime>();

                    //Add targeted data to each list using XPATH expressions
                    //Note that those expressions depends on the page construction
                    for (int i = 1; i <= myCounter; i++)
                    {
                        tenders.Add(pageDocument.DocumentNode.SelectSingleNode("(//table[contains(@class,'display')]//tbody//tr//td//a)[" + i + "]").InnerText);
                    }

                    for (int i = 3; i <= myCounter * 5; i += 5)
                    {
                        fields.Add(pageDocument.DocumentNode.SelectSingleNode("(//table[contains(@class,'display')]//tbody//tr//td)[" + i + "]").InnerText.Replace("&amp;", "&"));
                    }

                    for (int i = 4; i <= myCounter * 5; i += 5)
                    {
                        dates.Add(Convert.ToDateTime(ShiftString(pageDocument.DocumentNode.SelectSingleNode("(//table[contains(@class,'display')]//tbody//tr//td)[" + i + "]").InnerText)));
                    }

                    //Add list items to a new object
                    for (int i = 0; i < myCounter; i++)
                    {
                        var record = new LibyanTenders
                        {
                            Title = tenders[i],
                            Field = fields[i],
                            CreatedOn = dates[i]
                        };
                        //push each object to your db
                        db.LibyanTenders.Add(record);
                    }

                    db.SaveChanges();
                }

                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
