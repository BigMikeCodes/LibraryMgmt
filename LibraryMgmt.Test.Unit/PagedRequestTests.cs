using LibraryMgmt.Core.Paging;

namespace LibraryMgmt.Test.Unit;

[TestFixture]
public class PagedRequestTests
{

    [Test]
    public void Defaults_Set_As_Expected()
    {
        var PagedRequest = new PagedRequest
        {
            PageSize = null,
            PageNumber = null,
            SortAscending = null
        };
        
        Assert.Multiple(() =>
        {
            Assert.That(PagedRequest.SortAscendingOrDefault, Is.True);
            Assert.That(PagedRequest.PageSizeOrDefault, Is.EqualTo(10));
            Assert.That(PagedRequest.PageNumberOrDefault, Is.EqualTo(1));
        });
    }
    
}