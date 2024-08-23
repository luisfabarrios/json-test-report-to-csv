
public class Metric{

    public int TestsExecuted {get; set;}
    public int TestsPassed {get; set;}
    public int TestsFailed {get; set;}
    public double AvgExecTime {get; set;}
    public int MinExecTime {get; set;}
    public int MaxExecTime {get; set;}
    public List<string> RawData {get; set;}

    public Metric()
    {
    }

    public Metric(int testsExecuted, int testsPassed, int testsFailed, double avgExecTime, int minExecTime, int maxExecTime, List<string> rawData){
        this.TestsExecuted = testsExecuted;
        this.TestsPassed = testsPassed;
        this.TestsFailed = testsFailed;
        this.AvgExecTime = avgExecTime;
        this.MinExecTime = minExecTime;
        this.MaxExecTime = maxExecTime;
        this.RawData = rawData;
    }

}