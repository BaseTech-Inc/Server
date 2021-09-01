using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public static class HtmlAgilityPackExtensions
    {
        private static string idSelector = "#";
        private static string classSelector = ".";

        public static HtmlNodeCollection querySelectorAll(this HtmlNode document, string selectors)
        {
            return null;
        }

        public static HtmlNode querySelector(this HtmlNode document, string selectors)
        {
            switch (selectors)
            {
                case null: throw new ArgumentNullException(nameof(selectors));
                case "": throw new ArgumentException($"{nameof(selectors)} cannot be empty", nameof(selectors));
                default:
                    {
                        if (selectors.Contains(idSelector))
                        {
                            var documentNodes = querySelectorByIdOrClass(document, selectors, idSelector).FirstOrDefault();

                            return documentNodes;
                        }
                        else if (selectors.Contains(classSelector))
                        {
                            var documentNodes = querySelectorByIdOrClass(document, selectors, classSelector).FirstOrDefault();

                            return documentNodes;
                        }

                        throw new ArgumentNullException(nameof(selectors));
                    }

            }
        }

        private static HtmlNodeCollection querySelectorByIdOrClass(HtmlNode document, string selectors, string selector)
        {
            var values = selectors.Split(selector);
            var element = values[0];
            var valueToGet = values[1];

            if (element == "")
            {
                element = "div";
            }

            HtmlNodeCollection documentNodes;

            if (selector == idSelector)
                documentNodes = document.SelectNodes($"//{ element }[@id='{ valueToGet }']");
            else if (selector == classSelector)
                documentNodes = document.SelectNodes($"//{ element }[@class='{ valueToGet }']");
            else
                throw new ArgumentNullException(nameof(selectors));

            return documentNodes;
        }
    }
}
