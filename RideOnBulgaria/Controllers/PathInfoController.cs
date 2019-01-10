using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using RideOnBulgaria.Models;
using RideOnBulgaria.Web.Helpers;

namespace RideOnBulgaria.Controllers
{
    public class PathInfoController : Controller
    {
        private const int NewsToShow = 10;
        private const int NameWordsCountLimit = 150;

        private const string SourceUrl = "http://www.api.bg/index.php/bg/promeni";
        private const string DateRegex = @"\d{2,2}.\d{2,2}.\d{4,4} \d{2,2}:\d{2,2}";
        private const string ContentXpath = "//*[@id=\"content\"]/div/div//div/*";

        [Route("api/[controller]")]
        public List<TrafficInformation> CurrentTrafficSituation()
        {
            List<string> contentList = GetConentList();

            contentList.RemoveAll(string.IsNullOrWhiteSpace);

            var result = new List<TrafficInformation>(NewsToShow);

            for (int i = 0; i < NewsToShow; ++i)
            {
                var token = new TrafficInformation();
                result.Add(token);
                var counter = 0;

                foreach (var item in contentList)
                {

                    bool isNameNull = result[i].Name == null;
                    bool isPostedOnNull = result[i].PostedOn == null;
                    bool isContentNull = result[i].Content == null;
                    
                    bool isTokenDone = !isNameNull && !isPostedOnNull && !isContentNull && item.Length < NameWordsCountLimit;

                    if (isTokenDone)
                    {
                        break;
                    }
                    counter++;


                    if (item.Length > NameWordsCountLimit && !isNameNull && !isPostedOnNull && !isContentNull)
                    {
                        token.Content = String.Concat(token.Content, item);
                    }


                    if (isNameNull && isPostedOnNull && isContentNull)
                    {
                        token.Name = item;
                        continue;
                    }

                    if (isPostedOnNull)
                    {
                        Regex rgx = new Regex(DateRegex);
                        if (rgx.IsMatch(item))
                        {
                            token.PostedOn = item;
                            continue;
                        }
                    }

                    if (isContentNull)
                    {
                        token.Content = item;
                    }
                }
                contentList.RemoveRange(0, count: counter);

            }
            return result;
        }

        private static List<string> GetConentList()
        {
            List<string> api = new List<string>();

            var web = new HtmlWeb();
            var doc = web.Load(SourceUrl);

            var contents = doc.DocumentNode.SelectNodes(ContentXpath);

            foreach (var node in contents.Select(x => x.InnerText))
            {
                api.Add(node);
            }

            return api;
        }
    }
}