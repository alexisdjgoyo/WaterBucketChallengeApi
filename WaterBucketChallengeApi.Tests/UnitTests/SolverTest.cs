using Xunit;
using System.Linq;
using WaterBucketChallengeApi.Interfaces;
using WaterBucketChallengeApi.Services;

namespace WaterBucketChallengeApi.Tests.UnitTests
{
    public class SolverTests
    {
        private readonly IWaterBucketService _solver = new WaterBucketService();

        [Fact]
        public void Solvable_Case_With_GCD_Should_Return_Solution()
        {
            // Caso clásico: X=3, Y=5, Z=4. GCD(3, 5) = 1. 4 es múltiplo de 1.
            var result = _solver.Solve(xCap: 3, yCap: 5, zTarget: 4);

            Assert.NotNull(result);
            // La solución más corta para (3, 5, 4) es 6 pasos
            Assert.Equal(6, result.Count); 
            // Verificar que el último paso esté "Solved"
            Assert.Equal("Solved", result.Last().status); 
        }

        [Fact]
        public void Unsolvable_Case_Due_To_GCD_Should_Return_Null()
        {
            // Caso irresoluble: X=6, Y=9. GCD(6, 9) = 3. Z=5 (5 no es múltiplo de 3).
            var result = _solver.Solve(xCap: 6, yCap: 9, zTarget: 5);

            Assert.Null(result);
        }

        [Fact]
        public void Unsolvable_Case_Due_To_Target_Too_Large_Should_Return_Null()
        {
            // Caso irresoluble: Z es mayor que la capacidad máxima.
            var result = _solver.Solve(xCap: 2, yCap: 10, zTarget: 11);

            Assert.Null(result);
        }

        [Fact]
        public void Simplest_Case_Target_Is_Capacity_Should_Be_One_Step()
        {
            // X=10, Y=3, Z=10. Solución: Fill X.
            var result = _solver.Solve(xCap: 10, yCap: 3, zTarget: 10);

            Assert.NotNull(result);
            Assert.Single(result); // Solo 1 paso
            Assert.Equal("Fill bucket X", result.First().action);
            Assert.Equal("Solved", result.First().status);
        }
    }
}