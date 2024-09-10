using System.Globalization;
using System.Text.Json;
using CsvHelper;
using CsvHelper.Configuration;

namespace CommonDataStructureImplementations.FileManipulation;

public class User
{
    public string Name { get; set; } = "Unknown";
    public int? Age { get; set; }
    public string? Occupation { get; set; }
    public string? City { get; set; }
}

public static class JsonCSVReadFile
{

    // public static void Main()
    // {
    //     var input = File.ReadAllText("TestFiles/Users.json");
    //     var users = JsonSerializer.Deserialize<List<User>>(input);
    //     foreach (var user in users)
    //     {
    //         if (user.Age != null) user.Age++;
    //     }
    //
    //     var jsonString = JsonSerializer.Serialize(users, new JsonSerializerOptions {WriteIndented = true});
    //     
    //     File.WriteAllText("TestFiles/userUpdated.json", jsonString);
    //
    //     var filePath = "TestFiles/users.csv";
    //
    //     string[] lines = File.ReadAllLines(filePath);
    //     foreach (var line in lines)
    //     {
    //         string[] values = line.Split(',');
    //         foreach (var val in values)
    //         {
    //             Console.WriteLine(val);
    //         }
    //         Console.WriteLine();
    //     }
    //     
    //     var config = new CsvConfiguration(CultureInfo.InvariantCulture)
    //     {
    //         HasHeaderRecord = false,  // This tells CsvHelper there are no headers
    //     };
    //
    //     using (var reader = new StreamReader(filePath))
    //     using (var csv = new CsvReader(reader, config))
    //     {
    //         var records = csv.GetRecords<User>();
    //         foreach (var record in records)
    //         {
    //             Console.WriteLine(record.Age);
    //         }
    //     }
    // }
}