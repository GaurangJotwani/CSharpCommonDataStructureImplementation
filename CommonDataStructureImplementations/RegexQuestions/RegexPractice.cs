using System.Text.RegularExpressions;

namespace CommonDataStructureImplementations.RegexQuestions;

public static class RegexPractice
{
    private static bool IsMatch(string input)
    {
        var pattern = @"^[0-9][^aeiou][^bcDF][^\r\n\t\f\s][^AEIOU][^\.,]$";
        var regex = new Regex(pattern);
        return regex.Match(input).Success;
    }
    
    // public static void Main()
    // {
    //     int N = int.Parse(Console.ReadLine());
    //
    //     for (int i = 0; i < N; i++)
    //     {
    //         string input = Console.ReadLine();
    //         if (IsMatch(input)) Console.WriteLine(input);
    //     }
    // } 
}

public static class Test
{
    
    
}