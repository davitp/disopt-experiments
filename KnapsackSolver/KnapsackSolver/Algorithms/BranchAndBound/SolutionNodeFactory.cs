namespace KnapsackSolver.Algorithms.BranchAndBound
{
    /// <summary>
    /// The solution node factory module
    /// </summary>
    public class SolutionNodeFactory
    {
        /// <summary>
        /// Creates a root for the tree
        /// </summary>
        /// <param name="room"></param>
        /// <param name="value"></param>
        /// <param name="optimisticEvaluation"></param>
        /// <returns></returns>
        public SolutionIntegralNode BuildRoot(int room, int value,
            double optimisticEvaluation)
        {
            // create a root
            return new SolutionIntegralNode
            {
                Room = room,
                Value = value,
                OptimisticEvaluation = optimisticEvaluation,
                Variable = 0
            };
        }

        /// <summary>
        /// Extend and return new node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="varaible"></param>
        /// <param name="decision"></param>
        /// <param name="value"></param>
        /// <param name="room"></param>
        /// <param name="optimisticEvaluation"></param>
        /// <returns></returns>
        public SolutionIntegralNode ExtendNode(SolutionIntegralNode node, int varaible, int decision, int value, int room, double optimisticEvaluation)
        {
            return new SolutionIntegralNode
            {
                Decision = decision,
                Value = value,
                Variable = varaible,
                Parent = node,
                Room = room,
                OptimisticEvaluation = optimisticEvaluation
            };
        }
    }
}