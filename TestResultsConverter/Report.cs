using System.Text;
using System.Text.Json;

public class Report{

    private static Metric ExtractMetrics(string inputFilePath){
        
        var result = new Metric();
         
         try{
            string jsonString = File.ReadAllText(inputFilePath);

            // Deserialize JSON string into a C# object
            var jsonData = JsonSerializer.Deserialize<List<TestResult>>(jsonString);
            
            var testsExecuted = jsonData.Count;
            var testPassed = jsonData.Where(x => x.Status.Equals("Passed")).Count();
            var testFailed = jsonData.Where(x => x.Status.Equals("Failed")).Count();
            var avgExecTime = jsonData.Select(x => x.ExecutionTime).Average();
            var minExecTime = jsonData.Select(x => x.ExecutionTime).Min();
            var maxExecTime = jsonData.Select(x => x.ExecutionTime).Max();

            List<string> records = new List<string>();
            records.Add("TestCase,Status,executionTime,startTime,endTime");

            foreach (var ts in jsonData)
            {
                records.Add($"{ts.TestCase},{ts.Status},{ts.ExecutionTime},{ts.StartTime},{ts.EndTime}");
            }

            result = new Metric(testsExecuted, testPassed, testFailed, avgExecTime, minExecTime, maxExecTime, records);

        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File not found: " + inputFilePath);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error reading JSON file: " + e.Message);
        }
        return result;
    }

    public static void CreateReport(string inputFolderPath, string outputFolderPath, string fileName){
        
        string inputFilePath = Path.Combine(inputFolderPath, $"{fileName}.json");
        string outputFilePath = Path.Combine(outputFolderPath, $"{fileName}.csv");

        try{
            var metrics = ExtractMetrics(inputFilePath);

            StringBuilder csvContent = new StringBuilder();

            csvContent.AppendLine($"Total number of test cases executed,{metrics.TestsExecuted}");
            csvContent.AppendLine($"Number of test cases passed,{metrics.TestsPassed}");
            csvContent.AppendLine($"Number of test cases failed,{metrics.TestsFailed}");
            csvContent.AppendLine($"Average execution time for all test cases,{metrics.AvgExecTime}");
            csvContent.AppendLine($"Minimum execution time among all test cases,{metrics.MinExecTime}");
            csvContent.AppendLine($"Maximum execution time among all test cases,{metrics.MaxExecTime}");

            foreach (var record in metrics.RawData)
            {
                csvContent.AppendLine(record);
            }

            using (FileStream fs = File.Create(outputFilePath))
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.Write(csvContent.ToString());
            }
        } catch (NullReferenceException e)
        {
            Console.WriteLine("Error processing the data: " + e.Message);
        }
        
    }
}