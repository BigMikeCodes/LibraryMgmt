using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMgmt.Test.Integration;

public static class ProblemDetailsExtensions
{
    /// <summary>
    /// Extract extra details from any extensions. For example:
    /// {
    ///   "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
    ///   "title": "One or more validation errors occurred.",
    ///   "status": 400,
    ///   "errors": {
    ///       "Title": ["'Title' must not be empty."]
    ///   }
    /// }
    /// Will return everything under the errors key as a Dictionary where each key is a field in the original request
    /// and each value is an individual message.
    /// </summary>
    /// <param name="problemDetails"></param>
    /// <returns></returns>
    public static Dictionary<string, List<string>> GetErrors(this ProblemDetails problemDetails)
    {
        var errorsDictionary = new Dictionary<string, List<string>>();
        var extensions = problemDetails.Extensions;

        if (!extensions.TryGetValue("errors", out var errors)) return errorsDictionary;
        if (errors is not JsonElement jsonElement) return errorsDictionary;
        
        foreach (var error in jsonElement.EnumerateObject())
        {
            var messagesList = new List<string>();
            if (error.Value.ValueKind == JsonValueKind.Array)
            {
                foreach (var message in error.Value.EnumerateArray())
                {
                    var messageString = message.GetString();
                    if (messageString is not null)
                    {
                        messagesList.Add(messageString);
                    }
                }
            }
            errorsDictionary[error.Name] = messagesList;
                    
        }
        return errorsDictionary;   
    }
}