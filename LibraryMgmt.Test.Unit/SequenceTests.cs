using LibraryMgmt.Core;

namespace LibraryMgmt.Test.Unit;

[TestFixture]
public class SequenceTests
{

    [Test]
    public void Next_Increments_Sequence()
    {
        var sequence = new Sequence();
        
        var initialPosition = sequence.Position;
        var next = sequence.Next();
        
        Assert.That(next, Is.GreaterThan(initialPosition));
    }
    
}