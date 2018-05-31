using System;
using System.Collections.Generic;
using System.Text;

namespace Basket
{
    public class ImperativeProgramming
    {
        public static int CalculateBasketAmount(IList<BasketLineArticle>
        basketLineArticles)
        {
            int i = 0;
            return i;
            // here your code implementation
        }
        public static ArticleDatabase GetArticleDatabase(string id)
        {
            // here your code implementation
            return null;
        }

        public static ArticleDatabase GetArticleDatabaseMock(string id)
        {
            switch (id)
            {
                case "1":
                    return new ArticleDatabase
                    {
                        Id = "1",
                        Price = 1,
                        Stock = 35,
                        Label = "Banana",
                        Category = "food"
                    };
                case "2":
                    return new ArticleDatabase
                    {
                        Id = "2",
                        Price = 500,
                        Stock = 20,
                        Label = "Fridge electrolux",
                        Category = "electronic"
                    };
                case "3":
                    return new ArticleDatabase
                    {
                        Id = "3",
                        Price = 49,
                        Stock = 68,
                        Label = "Chair",
                        Category = "desktop"
                    };
                default:
                    throw new NotImplementedException();
            }
        }
    }
}