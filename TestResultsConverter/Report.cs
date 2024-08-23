using System.Text;
using System.Text.Json;

public class Report{

    public static void CreateReport(string inputFolderPath, string outputFolderPath, string fileName){
        
        string inputFilePath = Path.Combine(inputFolderPath, $"{fileName}.json");
        string outputFilePath = Path.Combine(outputFolderPath, $"{fileName}.csv");
       
        try
        {
            string jsonString = File.ReadAllText(inputFilePath);

            // Deserialize JSON string into a C# object
            var jsonData = JsonSerializer.Deserialize<List<TestResult>>(jsonString);
            
            var testsExecuted = jsonData.Count;
            var testPassed = jsonData.Where(x => x.Status.Equals("Passed")).Count();
            var testFailed = jsonData.Where(x => x.Status.Equals("Failed")).Count();
            var avgExecTime = jsonData.Select(x => x.ExecutionTime).Average();
            var minExecTime = jsonData.Select(x => x.ExecutionTime).Min();
            var maxExecTime = jsonData.Select(x => x.ExecutionTime).Max();

            StringBuilder csvContent = new StringBuilder();

            csvContent.AppendLine($"Total number of test cases executed,{testsExecuted}");
            csvContent.AppendLine($"Number of test cases passed,{testPassed}");
            csvContent.AppendLine($"Number of test cases failed,{testFailed}");
            csvContent.AppendLine($"Average execution time for all test cases,{avgExecTime}");
            csvContent.AppendLine($"Minimum execution time among all test cases,{minExecTime}");
            csvContent.AppendLine($"Maximum execution time among all test cases,{maxExecTime}");

            csvContent.AppendLine("TestCase,Status,executionTime,startTime,endTime");

            foreach (var ts in jsonData)
            {
                csvContent.AppendLine($"{ts.TestCase},{ts.Status},{ts.ExecutionTime},{ts.StartTime},{ts.EndTime}");
            }

            using (FileStream fs = File.Create(outputFilePath))
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.Write(csvContent.ToString());
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File not found: " + inputFilePath);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error reading JSON file: " + e.Message);
        }
    }
}