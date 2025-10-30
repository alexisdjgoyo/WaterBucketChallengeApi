using System;
using WaterBucketChallengeApi.Interfaces;
using WaterBucketChallengeApi.Models;

namespace WaterBucketChallengeApi.Services;

public class WaterBucketService : IWaterBucketService
{
    private record State(int X, int Y, List<SolutionStep> Path);
    private int CalculateGcd(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }


    /**
    * Función  para verificar si se puede encontrar solución con los datos ingresados
    */
    public List<SolutionStep>? Solve(int xCapacity, int yCapacity, int zAmount)
    {
        if (zAmount > Math.Max(xCapacity, yCapacity))
        {
            return null;
        }

        int gcd = CalculateGcd(xCapacity, yCapacity);

        if (zAmount % gcd != 0)
        {
            return null;
        }

        return ShortestSolution(xCapacity, yCapacity, zAmount);
    }

    private List<SolutionStep>? ShortestSolution(int xCapacity, int yCapacity, int zAmount)
    {
        var queue = new Queue<State>();
        var visited = new HashSet<string>();

        queue.Enqueue(new State(0, 0, new List<SolutionStep>()));
        visited.Add("0_0");

        int stepCounter = 1;

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            int x = current.X;
            int y = current.Y;
            var path = current.Path;

            if (x == zAmount || y == zAmount)
            {
                var solution = path.ToList();
                return solution;
            }

            stepCounter = path.Any() ? path.Last().step + 1 : 1;

            var nextStates = new List<(int newX, int newY, string action)>();

            nextStates.Add((xCapacity, y, "Fill bucket X"));
            nextStates.Add((x, yCapacity, "Fill bucket Y"));
            nextStates.Add((0, y, "Empty bucket X"));
            nextStates.Add((x, 0, "Empty bucket Y"));

            int transferToY = Math.Min(x, yCapacity - y);
            if (transferToY > 0)
                nextStates.Add((x - transferToY, y + transferToY, "Transfer from bucket X to Y"));

            int transferToX = Math.Min(y, xCapacity - x);
            if (transferToX > 0)
                nextStates.Add((x + transferToX, y - transferToX, "Transfer from bucket Y to X"));

            foreach (var state in nextStates)
            {
                string key = $"{state.newX}_{state.newY}";

                if (!visited.Contains(key))
                {
                    visited.Add(key);

                    var newStep = new SolutionStep
                    {
                        step = stepCounter,
                        bucketX = state.newX,
                        bucketY = state.newY,
                        action = state.action
                    };

                    if (state.newX == zAmount || state.newY == zAmount)
                    {
                        newStep.status = "Solved";
                    }

                    var newPath = new List<SolutionStep>(path)
                        {
                            newStep
                        };

                    queue.Enqueue(new State(state.newX, state.newY, newPath));
                }
            }
        }

        return null;
    }
}
