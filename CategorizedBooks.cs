using System.Globalization;
using BookStore;
using CsvHelper;

public class CategorizedBooks
{
  public static CategorizedBooks Create(string category, string link)
  {
    return new CategorizedBooks(category, link);
  }
  private CategorizedBooks(string category, string link)
  {
    Category = category;
    Link = link;
  }
  public string Category { get; set; }
  public string Link { get; set; }
  public List<Book> Books { get; set; } = new List<Book>();
  public void ExportToCSV()
  {
    using var writer = new StreamWriter($"./BooksOfCategory_{Category.Trim()}.csv");

    using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

    csv.WriteRecords(Books);

  }
}