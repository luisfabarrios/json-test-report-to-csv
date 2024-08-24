
namespace TestResultsConverter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Path information
            string inputFolderPath = "InputFiles";
            string outputFolderPath = "OutputFiles";
            var inputFiles = Directory.GetFiles(inputFolderPath, "*.json");
            var outputFiles = Directory.GetFiles(inputFolderPath, "*.csv");

            //Get the list of all the .json files in the specified path
            var files = inputFiles.Select(x => Path.GetFileNameWithoutExtension(x));

            //Get the list of all the .csv files in the specified path
            var createdReports = outputFiles.Select(x => Path.GetFileNameWithoutExtension(x));

            //Update the .son files list with only the ones that has no associated report
            files = files.Except(createdReports);

            //Create a report for every .son file without assciated report
            foreach(var fileName in files){
                Report.CreateReport(inputFolderPath, outputFolderPath, fileName);
            }  
        }
    }
}