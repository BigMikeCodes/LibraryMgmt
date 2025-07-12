using LibraryMgmt.Core.Paging;

namespace LibraryMgmt.Test.Unit;

public class PageIEnumerableExtensionTests
{
    
    [Test]
    public void Page_Returns_Expected_Pages()
    {
        var data = Enumerable.Range(1, 101);
        
        var firstPage = data.Page( 10, 1);
        var lastPage = data.Page( 10, 11);
        
        Assert.Multiple(() =>
        {
            // Expect 1 to 10 in data
            Assert.That(firstPage.Data, Is.EquivalentTo(Enumerable.Range(1, 10)));
            Assert.That(firstPage.TotalPages, Is.EqualTo(11));
            Assert.That(firstPage.TotalItems, Is.EqualTo(101));
            Assert.That(firstPage.CurrentPage, Is.EqualTo(1));
            Assert.That(firstPage.PageSize, Is.EqualTo(10));
            Assert.That(firstPage.HasMorePages, Is.True);
            
            // expect just 101 on the last page
            Assert.That(lastPage.Data, Is.EquivalentTo(Enumerable.Range(101, 1)));
            Assert.That(lastPage.TotalPages, Is.EqualTo(11));
            Assert.That(lastPage.TotalItems, Is.EqualTo(101));
            Assert.That(lastPage.CurrentPage, Is.EqualTo(11));
            Assert.That(lastPage.PageSize, Is.EqualTo(10));
            Assert.That(lastPage.HasMorePages, Is.False);
        });

    }

    [Test]
    public void Page_Gracefully_Handles_Out_Of_Range()
    {
        var data = Enumerable.Range(1, 10);
        
        var page = data.Page(10, 10);
        
        Assert.Multiple(() =>
        {
            Assert.That(page.HasMorePages, Is.False);
            Assert.That(page.Data, Is.Empty);
            Assert.That(page.TotalItems, Is.EqualTo(data.Count()));
            Assert.That(page.TotalPages, Is.EqualTo(1));
            Assert.That(page.CurrentPage, Is.EqualTo(10));
        });
        
    }
    
}