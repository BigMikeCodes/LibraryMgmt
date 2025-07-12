namespace LibraryMgmt.Core.Sequences;

public class AtomicIntSequence: ISequence<int>
{
    private int _position = 0;
    
    public int Position => _position; 
        
    public int Next()
    {
        return Interlocked.Increment(ref _position);
    }

}