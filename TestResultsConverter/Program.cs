
namespace TestResultsConverter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string inputFolderPath = "InputFiles";
            string outputFolderPath = "OutputFiles";
            var inputFiles = Directory.GetFiles(inputFolderPath, "*.json");
            var outputFiles = Directory.GetFiles(inputFolderPath, "*.csv");

            var files = inputFiles.Select(x => Path.GetFileNameWithoutExtension(x));
            var createdReports = outputFiles.Select(x => Path.GetFileNameWithoutExtension(x));

            files = files.Except(createdReports);

            foreach(var fileName in files){
                Report.CreateReport(inputFolderPath, outputFolderPath, fileName);
            }  
        }
    }
}