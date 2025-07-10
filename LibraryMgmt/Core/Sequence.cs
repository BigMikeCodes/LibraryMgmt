namespace LibraryMgmt.Core;

public class Sequence
{
    private int _position = 0;
        
    public int Next()
    {
        return Interlocked.Increment(ref _position);
    }

}