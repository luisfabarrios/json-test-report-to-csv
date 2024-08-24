using System.Text;
using System.Text.Json;

public class Report{

    /// <summary>
    /// This method extracts the data in the .json file located in the
    /// path and calculates the metrics
    /// </summary>
    /// <param name="inputFilePath">Path for the .json file to process</param>
    /// <returns>Metric object with the calculated metrics and the records in the .son file</returns>
    private static Metric ExtractMetrics(string inputFilePath){
        
        var result = new Metric();
         
         try{
            //Read the .son file with the test results
            string jsonString = File.ReadAllText(inputFilePath);

            // Deserialize JSON string into a C# object
            var jsonData = JsonSerializer.Deserialize<List<TestResult>>(jsonString);
            
            //Calculate the metrics based on the data
            var testsExecuted = jsonData.Count;
            var testPassed = jsonData.Where(x => x.Status.Equals("Passed")).Count();
            var testFailed = jsonData.Where(x => x.Status.Equals("Failed")).Count();
            var avgExecTime = jsonData.Select(x => x.ExecutionTime).Average();
            var minExecTime = jsonData.Select(x => x.ExecutionTime).Min();
            var maxExecTime = jsonData.Select(x => x.ExecutionTime).Max();

            //List of the original records to be added at the end of the record
            List<string> records = new List<string>();
            records.Add("TestCase,Status,executionTime,startTime,endTime");

            foreach (var ts in jsonData)
            {
                records.Add($"{ts.TestCase},{ts.Status},{ts.ExecutionTime},{ts.StartTime},{ts.EndTime}");
            }

            //Creation of the Metric object with all the data required for the report
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

    /// <summary>
    /// This method creates a .csv report with the calculated metrics based 
    /// on the provided .json file and places it in the specified path with the 
    /// same name as the original file but different extension
    /// </summary>
    /// <param name="inputFolderPath">Path for the .json file</param>
    /// <param name="outputFolderPath">Path for new reports</param>
    /// <param name="fileName">Name of the file to be process</param>
    public static void CreateReport(string inputFolderPath, string outputFolderPath, string fileName){
        
        //Origin path of the .json file and destination path for the final report
        string inputFilePath = Path.Combine(inputFolderPath, $"{fileName}.json");
        string outputFilePath = Path.Combine(outputFolderPath, $"{fileName}.csv");

        try{
            var metrics = ExtractMetrics(inputFilePath);

            StringBuilder csvContent = new StringBuilder();

            //Creation of the report structure
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

            //Verify output folder exits
            if (!Directory.Exists(outputFolderPath))
            {
                Directory.CreateDirectory(outputFolderPath);
            }

            //Creation of the final report
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