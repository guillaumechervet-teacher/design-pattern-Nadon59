using Basket;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BasketTest
{
    class BasketOperation_CalculateBasketAmountShould
    {
        private int amountTotal;
        private bool articleDatabases;
        private object basketLineArticle;

        public class BasketTest
        {
            public List<BasketLineArticle> BasketLineArticles { get; set; }
            public int ExpectedPrice { get; set; }
        }
        private static IEnumerable<Object[]> Baskets
        {
            get
            {
                return new[]
                {
                    new Object[]
                    {
                        new BasketTest(){ BasketLineArticles = new List<BasketLineArticle>
                        {
                            new BasketLineArticle{Id = "1" , Number = 12 , Label ="Banana"},
                            new BasketLineArticle{Id = "2" , Number = 1 , Label ="Fridge electrolux"},
                            new BasketLineArticle{Id = "3" , Number = 4 , Label ="Chair"},
                        },
                            ExpectedPrice =84868 }
                    },
                    new object[]
                    {
                        new BasketTest(){ BasketLineArticles = new List<BasketLineArticle>
                        {
                            new BasketLineArticle{Id = "1" , Number = 20 , Label ="Banana"},
                            new BasketLineArticle{Id = "2" , Number = 6 , Label ="Chair"}
                        },
                        ExpectedPrice = 37520 }
                    },
                };
            }
        }
        [TestMethod]
        [DynamicData("Baskets")]
        public void ReturnCorrectAmountGivenBasket(BasketTest basketTest)
        {
            var amoutTotal = 0;
            foreach (var BasketLineArticle in basketTest.BasketLineArticles)
            {
                //retrive article from databas
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                var assemblyDirectory = Path.GetDirectoryName(path);
                var jsonPath = Path.Combine(assemblyDirectory, "articledatabase.json");
                IList<ArticleDatabase> articleDatabases =
                JsonConvert.DeserializeObject<List<ArticleDatabase>>(File.ReadAllText(jsonPath));
                var article = articleDatabases.First(articleDatabase =>
                articleDatabase.Id == BasketLineArticle.Id);
                // Calculate amount
                var amount = 0;
                switch (article.Category)
                {
                    case "food":
                        amount += article.Price * 100 + article.Price * 12;
                        break;
                    case "electronic":
                        amount += article.Price * 100 + article.Price * 20 + 4;
                        break;
                    case "desktop":
                        amount += article.Price * 100 + article.Price * 20;
                        break;
                }
                amountTotal += amount * BasketLineArticle.Number;
            }
            Assert.AreEqual(amountTotal, basketTest.ExpectedPrice);
        }
    }
}

