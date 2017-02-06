namespace KnapsackSolver.Algorithms.BranchAndBound
{
    public class SolutionIntegralNode : ITreeNode <SolutionIntegralNode>
    {
        /// <summary>
        /// The room estimation
        /// </summary>
        public int Room { get; set; }

        /// <summary>
        /// The optimistic evaluation
        /// </summary>
        public double OptimisticEvaluation { get; set; }

        /// <summary>
        /// The value
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// The target variable
        /// </summary>
        public int Variable { get; set; }

        /// <summary>
        /// The variable Decision
        /// </summary>
        public int Decision { get; set; }

        /// <summary>
        /// The parent decision
        /// </summary>
        public SolutionIntegralNode Parent { get; set; }

        /// <summary>
        /// The children
        /// </summary>
        public SolutionIntegralNode LeftNode { get; set; }

        /// <summary>
        /// The children
        /// </summary>
        public SolutionIntegralNode RightNode { get; set; }
        /// <summary>
        /// The feasibility check
        /// </summary>
        /// <returns></returns>
        public bool IsFeasible()
        {
            return this.Room >= 0;
        }
    }
}