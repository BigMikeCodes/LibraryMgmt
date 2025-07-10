namespace LibraryMgmt.Core;

public class Sequence
{
    private int _position = 0;
    
    public int Position => _position; 
        
    public int Next()
    {
        return Interlocked.Increment(ref _position);
    }

}