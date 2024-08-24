using System.Text.Json.Serialization;

/// <summary>
/// Template to map the json values to the c# object
/// </summary>
public class TestResult{

    [JsonPropertyName("testCase")]
    public required string TestCase {get; set;}
    
    [JsonPropertyName("status")]
    public required string Status {get; set;}
    
    [JsonPropertyName("executionTime")]
    public required int ExecutionTime {get; set;}
    
    [JsonPropertyName("startTime")]
    public required string StartTime {get; set;}
    
    [JsonPropertyName("endTime")]
    public required string EndTime {get; set;}
}