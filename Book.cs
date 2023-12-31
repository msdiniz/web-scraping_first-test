//declare this class into a namespace

namespace BookStore;
public class Book()
{
  private Book(string title, string price) : this()
  {
    Title = title;
    Price = price;
  }
  public static Book NullBook = new("", "");

  public static Book Create(string title, string price) => new Book(title, price);
    public string Title { get; set; }

    public string Price { get; set; }
}