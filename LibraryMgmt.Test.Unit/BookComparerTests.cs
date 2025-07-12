using LibraryMgmt.Books.Domain;

namespace LibraryMgmt.Test.Unit;

[TestFixture]
public class BookComparerTests
{
    
    [Test]
    public void Compare_Returns_0_When_Same_Title_PublishedYear_Id()
    {
        var x = new Book()
        {
            Id = 1,
            Title = "AAA",
            PublishedYear = 2020,
            AuthorId = 1,
            Isbn = " 978-1617297571"
        };        
        
        var y = new Book()
        {
            Id = 1,
            Title = "AAA",
            PublishedYear = 2020,
            AuthorId = 1,
            Isbn = " 978-1617297571"
        };

        var comparerAsc = new BookComparer(true);
        var comparerDesc = new BookComparer(false);

        Assert.Multiple(() =>
        {
            Assert.That(comparerAsc.Compare(x, y), Is.EqualTo(0));
            Assert.That(comparerDesc.Compare(x, y), Is.EqualTo(0));
        });
    }

    [Test]
    public void Compare_Handles_Natural_Ordering_When_Title_And_PublishedYear_Same()
    {
        var x = new Book()
        {
            Id = 1,
            Title = "AAA",
            PublishedYear = 2020,
            AuthorId = 1,
            Isbn = " 978-1617297571"
        };        
        
        var y = new Book()
        {
            Id = 2,
            Title = "AAA",
            PublishedYear = 2020,
            AuthorId = 1,
            Isbn = " 978-1617297571"
        };

        var comparerAsc = new BookComparer(true);
        var comparerDesc = new BookComparer(false);

        Assert.Multiple(() =>
        {
            Assert.That(comparerAsc.Compare(x, y), Is.LessThan(0));
            Assert.That(comparerDesc.Compare(x, y), Is.GreaterThan(0));
        });
    }
}