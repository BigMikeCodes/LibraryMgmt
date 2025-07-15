using LibraryMgmt.Core.Paging;

namespace LibraryMgmt.Test.Unit;

[TestFixture]
public class PageTests
{
    [Test]
    public void Dynamic_Properties_Correct()
    {
        var page = new Page<string>
        {
            Data = [],
            PageSize = 10,
            TotalItems = 95,
            CurrentPage = 10,
        };
        
        Assert.Multiple(() =>
        {
            Assert.That(page.HasMorePages, Is.False);
            Assert.That(page.TotalPages, Is.EqualTo(10));
        });
    }
}