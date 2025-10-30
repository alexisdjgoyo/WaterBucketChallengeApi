using System;
using WaterBucketChallengeApi.Models;

namespace WaterBucketChallengeApi.Interfaces;

public interface IWaterBucketService
{
    List<SolutionStep>? Solve(int xCap, int yCap, int zTarget);
}
