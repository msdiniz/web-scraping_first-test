# Web Scraping scafold first test
This is a fully working **Web Scraping C# Console App** towards the ***books.toscrape.com***.

It is based on a [tutorial](https://dev.to/oxylabs-io/web-scraping-with-c-48bd) but I did expand it to grab all Book Categories, nesting the original code in a `List<CategorizedBooks>`.

I modified class initialization to Factory Methods and have to deal with some XPath that I did not know before, but my knowledge of XPath is too shalow...

__List of TODO:__
1. Refactor towards more generic scraping, renaming Books and CategorizedBooks to Items and CategorizedItems.
2. Allowing to configure and pass XPath snippets.
3. Extract Program.cs code to a ScrapingEngine class and get rid of static methods.
