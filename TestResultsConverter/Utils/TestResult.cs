using System.Text.Json.Serialization;

public class TestResult{

    [JsonPropertyName("testCase")]
    public string TestCase {get; set;}
    
    [JsonPropertyName("status")]
    public string Status {get; set;}
    
    [JsonPropertyName("executionTime")]
    public int ExecutionTime {get; set;}
    
    [JsonPropertyName("startTime")]
    public string StartTime {get; set;}
    
    [JsonPropertyName("endTime")]
    public string EndTime {get; set;}
}