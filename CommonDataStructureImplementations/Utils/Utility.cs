using System.Globalization;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Web;
using CsvHelper;
using CsvHelper.Configuration;

namespace CommonDataStructureImplementations.Utils;

public class Address
{
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public int? ZipCode { get; set; }
}

public class PhoneNumber
{
    public string? Type { get; set; }
    public string? Number { get; set; }
}

public class User
{
    public string? Name { get; set; } = "Unknown";
    public int? Age { get; set; }
    public string? Email { get; set; } = "Unknown";
    public Address? Address { get; set; }
    public List<PhoneNumber>? PhoneNumbers { get; set; }
    public bool IsActive { get; set; }
    public DateTime? RegisteredDate { get; set; }
}

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
}

public class Utility
{

    #region FileHandling
    public static User ParseJsonStringToObject(string path)
    {
        string jsonString = File.ReadAllText(path);
        User user = JsonSerializer.Deserialize<User>(jsonString);
        return user;
    }
    
    public static JsonObject ParseJsonStringToDynamic(string path)
    {
        string jsonString = File.ReadAllText(path);
        return JsonSerializer.Deserialize<JsonObject>(jsonString);
    }
    
    public static List<User> ParseJsonListStringToListObject(string path)
    {
        string jsonString = File.ReadAllText(path);
        List<User> users = JsonSerializer.Deserialize<List<User>>(jsonString);
        return users;
    }
    
    public static string ListOfObjectToJsonString(string destination, List<User> users)
    {
        string jsonString = JsonSerializer.Serialize(users);
        File.WriteAllText(destination, jsonString);
        return jsonString;
    }
    
    public static async Task<string> DownloadFileFromURL(string url, string destination)
    {
        using HttpClient client = new HttpClient();
        try
        {
            Uri uri = new Uri(url, UriKind.Absolute);
            HttpResponseMessage response = await client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            var jsongString = await response.Content.ReadAsStringAsync();
            File.WriteAllText(destination, jsongString);
            return jsongString;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
    
    public static void ReadTextFile(string src, string dst)
    {
        string text = File.ReadAllText(src);
        
        var lines = File.ReadAllLines(src);
        
        var arr = text.Split(" ");
        var regex = new Regex(@"[^\w\s]");
        foreach (var line in lines)
        {
            var tmp = regex.Replace(line, "");
            var a = tmp.Split(" ");
            foreach (var word in a) File.AppendAllText(dst, string.Join(" ", a) + "\n");
            File.AppendAllText(dst, "\n");
        }
    }
    
    public static void ReadCSVFile(string path)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
        };
        List<Person> records;
        using (var reader = new StreamReader(path))
        using (var csv = new CsvReader(reader, config))
        {
            records = csv.GetRecords<Person>().ToList();
            foreach (var record in records) record.Age += 1;
        }
        using (var writer = new StreamWriter(path))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(records);
        }
    }
    
    #endregion
    
    #region ParseUrl
    public static void ParseUrl(string url)
    {
        Uri uri = new Uri(url);
        Console.WriteLine($"Scheme: {uri.Scheme}");
        Console.WriteLine($"Host: {uri.Host}");
        Console.WriteLine($"Port: {uri.Port}");
        Console.WriteLine($"Path: {uri.AbsolutePath}");
        Console.WriteLine($"Query: {uri.Query}");
        Console.WriteLine($"Fragment: {uri.Fragment}");

        var query = uri.Query;
        var queryParams = HttpUtility.ParseQueryString(query);
        foreach (var key in queryParams.AllKeys) Console.WriteLine($"{key}: {queryParams[key]}");
        UriBuilder builder = new UriBuilder
        {
            Scheme = uri.Scheme,
            Host = uri.Host,
            Path = uri.LocalPath,
            Query = uri.Query,
        };
        Console.WriteLine(builder.ToString());
    }
    #endregion
    
    #region DateTime
    public static void TimeDateFunction()
    {
        string dateString = "2023-09-10";
        DateTime date = DateTime.Parse(dateString);
        Console.WriteLine(date);  // Output: 9/10/2023 12:00:00 AM
        string customDateString = "10-Sep-2023";
        date = DateTime.ParseExact(customDateString, "dd-MMM-yyyy", null);
        Console.WriteLine(date);
        DateTime now = DateTime.Now;
        Console.WriteLine(now.ToString("yyyy-MM-dd"));  // Output: 2023-09-10
        Console.WriteLine(now.ToString("MMMM dd, yyyy"));  // Output: September 10, 2023
        Console.WriteLine(now.ToString("hh:mm tt"));  // // Output: 9/10/2023 12:00:00 AM
        now = DateTime.Now;
        int year = now.Year;
        int month = now.Month;
        int day = now.Day;
        Console.WriteLine($"Year: {year}, Month: {month}, Day: {day}");
        int hours = now.Hour;
        int minutes = now.Minute;
        int seconds = now.Second;
        Console.WriteLine($"Hours: {hours}, Minutes: {minutes}, Seconds: {seconds}");
        now = DateTime.Now;
        DateTime futureDate = now.AddDays(5).AddHours(3);
        Console.WriteLine(futureDate);
        DateTime pastDate = now.AddDays(-7);
        Console.WriteLine(pastDate);
        DateTime date1 = new DateTime(2023, 09, 01);
        DateTime date2 = DateTime.Now;
        TimeSpan difference = date2 - date1;
        Console.WriteLine($"Difference in days: {difference.Days}");// Output: [Current Date - 7 Days]
    }
    #endregion
    
    #region Regex
    public static bool IsRegexMatch(string pattern, string s)
    {
        return Regex.IsMatch(s, pattern);
    }
    public static int GetNumberOfMatches(string pattern, string s)
    {
        MatchCollection matches = Regex.Matches(s, pattern);
        return matches.Count;
    }
    public static List<string> ExtractWords(string paragraph)
    {
        // Define a regex pattern to match words (A-Z, a-z, 0-9)
        const string pattern = @"\b\w+\b";
        
        List<string> words = new List<string>();
        MatchCollection matches = Regex.Matches(paragraph, pattern);
        foreach (Match match in matches) words.Add(match.Value);
        return words;
    }
    //Phone number
    public static string Replace(string pattern, string text, string r)
    {
        string s = Regex.Replace(text, pattern, r);
        string result = Replace(@"\d", "My phone number is 1234567890", "*");
        Console.WriteLine(result);
        pattern = @"(\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4})";
        // Use Regex.Replace to find phone numbers and surround them with square brackets
        text = "Contact us at 123-456-7890 or (123) 456-7890 or 123.456.7890.";
        result = Regex.Replace(text, pattern, "[$1]");
        Console.WriteLine(result);
        return result;
    }
    public static bool IsValidEmail(string email)
    {
        string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, emailPattern);
    }
    public static List<string> ExtractHTMLTagsFromText(string html)
    {
        List<string> hrefs = new List<string>();
        string anchorPattern = @"<a\b[^>]*>(.*?)</a>";
        MatchCollection anchorMatches = Regex.Matches(html, anchorPattern, RegexOptions.IgnoreCase);
        string hrefPattern = @"href\s*=\s*[""']([^""']+)[""']";
        foreach (Match match in anchorMatches)
        {
            string anchorTag = match.Value;  // Get the entire <a> ... </a> tag
            Match hrefMatch = Regex.Match(anchorTag, hrefPattern, RegexOptions.IgnoreCase);
            if (hrefMatch.Success)
            {
                hrefs.Add(hrefMatch.Groups[1].Value);
            }
        }
        return hrefs;
    }
    #endregion Regex
   
           
    public static async Task Main()
    {
        string path =
            "/Users/goti/RiderProjects/CommonDataStructureImplementations/CommonDataStructureImplementations/Utils/User.json";
        User user = ParseJsonStringToObject(path);
        Console.WriteLine(user.Age);
        
        JsonObject user2 = ParseJsonStringToDynamic(path);
        Console.WriteLine(user2["Name"]);
        
        path =
            "/Users/goti/RiderProjects/CommonDataStructureImplementations/CommonDataStructureImplementations/Utils/Users.json";

        List<User> users = ParseJsonListStringToListObject(path);
        Console.WriteLine(users.Count);
        
        path =
            "/Users/goti/RiderProjects/CommonDataStructureImplementations/CommonDataStructureImplementations/Utils/Users2.json";
        string jsonString = ListOfObjectToJsonString(path, users);
        
        path =
            "/Users/goti/RiderProjects/CommonDataStructureImplementations/CommonDataStructureImplementations/Utils/test.json";

        var res = await DownloadFileFromURL("https://jsonplaceholder.typicode.com/todos/1", path);
        Console.WriteLine(res);
        
        string url = "https://example.com/path?param1=value1&param2=value2&param3=value3";
        ParseUrl(url);
        
        path =
            "/Users/goti/RiderProjects/CommonDataStructureImplementations/CommonDataStructureImplementations/Utils/test.txt";
        var dst =
            "/Users/goti/RiderProjects/CommonDataStructureImplementations/CommonDataStructureImplementations/Utils/testWrite.txt";
        ReadTextFile(path, dst);
        
        path =
            "/Users/goti/RiderProjects/CommonDataStructureImplementations/CommonDataStructureImplementations/Utils/person.csv";

        ReadCSVFile(path);

        TimeDateFunction();
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
}