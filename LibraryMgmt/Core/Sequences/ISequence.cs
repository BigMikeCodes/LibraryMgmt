namespace LibraryMgmt.Core.Sequences;

/// <summary>
/// Class that is able to produce sequences of type T
///
/// Implementations should ensure that Next() should return unique values so that the returned value is of use to
/// cany potential consumers/ callers.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ISequence<T>
{
    T Next();
}