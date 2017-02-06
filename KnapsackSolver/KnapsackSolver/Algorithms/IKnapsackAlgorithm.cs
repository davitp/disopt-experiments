namespace KnapsackSolver.Algorithms
{
    /// <summary>
    /// The knapsack algorithm
    /// </summary>
    public interface IKnapsackAlgorithm<out TOutput>
    {
        TOutput Execute();
    }
}