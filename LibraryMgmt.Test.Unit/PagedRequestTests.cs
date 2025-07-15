using LibraryMgmt.Core.Paging;

namespace LibraryMgmt.Test.Unit;

[TestFixture]
public class PagedRequestTests
{

    [Test]
    public void Defaults_Set_As_Expected()
    {
        var pagedRequest = new PagedRequest
        {
            PageSize = null,
            PageNumber = null,
            SortAscending = null
        };
        
        Assert.Multiple(() =>
        {
            Assert.That(pagedRequest.SortAscendingOrDefault, Is.True);
            Assert.That(pagedRequest.PageSizeOrDefault, Is.EqualTo(10));
            Assert.That(pagedRequest.PageNumberOrDefault, Is.EqualTo(1));
        });
    }
    
}