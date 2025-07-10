namespace LibraryMgmt.Books.Domain;

public class InvalidYearException: ArgumentException
{
    public InvalidYearException(int providedYear, int currentYear)
        : base($"Invalid year {providedYear}, current year is {currentYear}")
    {
    }
}