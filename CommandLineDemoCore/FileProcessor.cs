using System.Text.Json;
using System.Text.Json.Serialization;
using CommandLineDemoCore.Models;

namespace CommandLineDemoCore;
public class FileProcessor
{
    public void ProcessCsvFileToJsonFile(string sourceFile, string targetFile)
    {
        var sourceFileInfo = new FileInfo(sourceFile);
        if(!sourceFileInfo.Exists)
        {
            Console.WriteLine($"{sourceFileInfo.Name} does not exists!");
            return;
        }

        var csvContents = File.ReadAllText(sourceFileInfo.FullName);
        if(string.IsNullOrEmpty(csvContents))
        {
            Console.WriteLine("No content in the file");
            return;
        }
        var personId = GetPersonIdFromFilename(sourceFileInfo.Name);
        var person = MapToPerson(personId, csvContents);
        WriteToJsonFile(targetFile, person);
    }

    private static string GetPersonIdFromFilename(string fileName)
    {
        return fileName.Split('.')[0];
    }

    private static Person MapToPerson(string personId, string csvInformation)
    {
        var data = csvInformation.Split(',');
        var personGender = MapToGender(data[2]);
        var personDateOfBirth = MapToDateOnly(data[3]);
        return new Person()
        {
            Id = personId,
            Name = new Name()
            {
                FirstName = data[0],
                LastName = data[1]
            },
            Gender = personGender,
            DateOfBirth = personDateOfBirth
        };
    }

    private static DateOnly MapToDateOnly(string dateOfBirthString)
    {
        var dateParts = dateOfBirthString.Split('-');
        var year = int.Parse(dateParts[0]);
        var month = int.Parse(dateParts[1]);
        var day = int.Parse(dateParts[2]);

        try
        {
            return new DateOnly(year, month, day);
        }
        catch(Exception ex)
        {
            Console.WriteLine("Error parsing date of birth: " + ex.Message);
            throw;
        }
    }

    private static Gender MapToGender(string genderString)
    {
        return genderString.Trim().ToLower() switch
        {
            "male" => Gender.Male,
            "female" => Gender.Female,
            _ => Gender.Others
        };
    }

    private static void WriteToJsonFile(string targetFile, Person person)
    {
        if (!File.Exists(targetFile))
        {
            using(File.Create(targetFile));
            File.WriteAllText(targetFile, "[]", System.Text.Encoding.ASCII);
        }

        var people = ReadPeopleFromFile(targetFile);

        if (!people.Exists(p => p.Id.Equals(person.Id)))
        {
            people.Add(person);
            Console.WriteLine($"{person.Name} [{person.Id}] was added to the list!");
        }
        else
        {
            Console.WriteLine($"{person.Name} [{person.Id}] is already in the list!");
        }
        WritePeopleToFile(targetFile, people);
    }

    private static List<Person> ReadPeopleFromFile(string targetFile)
    {
        using FileStream jsonStream = File.OpenRead(targetFile);
        var people = JsonSerializer.Deserialize<List<Person>>(jsonStream, GetJsonSerializerOptions());
        jsonStream.Close();
        return people;
    }

    private static void WritePeopleToFile(string targetFile, List<Person> people)
    {
        var jsonContent = JsonSerializer.Serialize(people, GetJsonSerializerOptions());
        File.WriteAllText(targetFile, jsonContent);
    }

    private static JsonSerializerOptions GetJsonSerializerOptions()
    {
        return new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            Converters =
            {
                new JsonStringEnumConverter()
            }
        };
    }
}
