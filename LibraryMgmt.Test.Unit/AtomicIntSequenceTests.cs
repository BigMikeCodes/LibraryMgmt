using LibraryMgmt.Core.Sequences;

namespace LibraryMgmt.Test.Unit;

[TestFixture]
public class AtomicIntSequenceTests
{

    [Test]
    public void Next_Increments_Sequence()
    {
        var sequence = new AtomicIntSequence();
        
        var initialPosition = sequence.Position;
        var next = sequence.Next();
        
        Assert.That(next, Is.GreaterThan(initialPosition));
    }
    
}