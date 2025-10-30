using System;
using System.Text.Json.Serialization;
namespace WaterBucketChallengeApi.Models;

public class SolutionStep
{
    public int bucketX { get; set; }
    public int bucketY { get; set; }
    public int step { get; set; }
    public string action { get; set; } = string.Empty;
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? status { get; set; }
}
