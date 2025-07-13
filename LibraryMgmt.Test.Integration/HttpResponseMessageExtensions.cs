namespace LibraryMgmt.Test.Integration;

public static class HttpResponseMessageExtensions
{
    public static int? BookIdFromLocation(this HttpResponseMessage response)
    {
        var location = response.Headers.Location;

        if (location is null)
        {
            return null;
        }

        if (int.TryParse(location.ToString().Split("/").Last(), out var id))
        {
            return id;
        };
        
        return null;
    }
}