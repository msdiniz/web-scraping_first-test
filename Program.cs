// See https://aka.ms/new-console-template for more information
//First, we need to see the robots.txt of the site
//https://towardsdatascience.com/web-scraping-basics-82f8b5acd45c:

//https://dev.to/oxylabs-io/web-scraping-with-c-48bd

// Parses the URL and returns HtmlDocument object

using System.Text;
using BookStore;
using HtmlAgilityPack;

// Remove the unused Main method
// static void Main(string[] args)
// {

//   var bookLinks = GetBookLinks("http://books.toscrape.com/catalogue/category/books/mystery_3/index.html");

//   Console.WriteLine("Found {0} links", bookLinks.Count);

//   var books = GetBookDetails(bookLinks);
var categoriesLinks = GetCategoriesFilledWithBooks("http://books.toscrape.com/catalogue/category/books_1/index.html");

Console.WriteLine("Found {0} categories", categoriesLinks.Count);

//var books = GetBookDetails(categoriesLinks);
//exportToCSV(categoriesLinks);
foreach (var category in categoriesLinks)
{
    category.ExportToCSV();
}

// }
static List<CategorizedBooks> GetCategoriesFilledWithBooks(string url)
{
    var categoriesFilledWithBooks = new List<CategorizedBooks>();
    // var bookLinks = new List<string>();
    HtmlDocument doc = GetDocument(url);
    HtmlNodeCollection linkNodes = doc.DocumentNode.SelectNodes("//ul[not(@class)]/li/a");
    var baseUri = new Uri(url);
    foreach (var category in linkNodes)
    {
        string href = category.Attributes["href"].Value;
        var fullUrl = new Uri(baseUri, href).AbsoluteUri;
        Console.WriteLine("Cataloging {0}: ", category.InnerText.Trim());
        var categorizedBooks = CategorizedBooks.Create(category.InnerText.Trim(), fullUrl);
        var bookLinks = GetBookLinks(fullUrl);
        categorizedBooks.Books = GetBookDetails(bookLinks);
        Console.WriteLine("\tFound {0} books in this category\n", categorizedBooks.Books.Count);
        categoriesFilledWithBooks.Add(categorizedBooks);
    }
    return categoriesFilledWithBooks;

}
static List<string> GetBookLinks(string url)
{

    var bookLinks = new List<string>();

    HtmlDocument doc = GetDocument(url);

    HtmlNodeCollection linkNodes = doc.DocumentNode.SelectNodes("//h3/a");

    var baseUri = new Uri(url);

    foreach (var link in linkNodes)

    {

        string href = link.Attributes["href"].Value;

        bookLinks.Add(new Uri(baseUri, href).AbsoluteUri);

    }

    return bookLinks;

}
static HtmlDocument GetDocument(string url)
{

    HtmlWeb web = new HtmlWeb();
    web.OverrideEncoding = Encoding.UTF8;
    HtmlDocument doc = web.Load(url);

    return doc;

}
static List<Book> GetBookDetails(List<string> urls)
{

    var books = new List<Book>();
    //var book = Book.NullBook;
    foreach (var url in urls)

    {

        HtmlDocument document = GetDocument(url);

        var titleXPath = "//h1";

        var priceXPath = "//div[contains(@class,'col-sm-6 product_main')]/p[@class='price_color']";

        //book = Book.NullBook;

        // book.Title = document.DocumentNode.SelectSingleNode(titleXPath).InnerText;
        // book.Price = document.DocumentNode.SelectSingleNode(priceXPath).InnerText;
        var book = Book.Create(
            document.DocumentNode.SelectSingleNode(titleXPath).InnerText, 
            document.DocumentNode.SelectSingleNode(priceXPath).InnerText.Trim()
            );
        books.Add(book);
    }
    return books;

}
// static void exportToCSV<T>(List<T> books)
// {

//     using (var writer = new StreamWriter("./CategoriesWithBooks.csv"))

//     using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))

//     {

//         csv.WriteRecords(books);

//     }

// }