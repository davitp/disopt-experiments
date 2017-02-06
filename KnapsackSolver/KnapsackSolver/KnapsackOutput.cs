using System.Collections.Generic;

namespace KnapsackSolver
{
    /// <summary>
    /// The output of knapsack problem
    /// </summary>
    /// <typeparam name="TVariable">The variable type</typeparam>
    public class KnapsackOutput<TVariable>
    {
        /// <summary>
        /// The collection variable values
        /// </summary>
        public IList<TVariable> VariableValues { get; private set; }
        
        /// <summary>
        /// The objective value
        /// </summary>
        public int Objective { get; private set; }

        /// <summary>
        /// Total selected elements
        /// </summary>
        public int Optimal { get; private set; }

        /// <summary>
        /// The size
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Creates new instance of outpu
        /// </summary>
        /// <param name="variables"></param>
        /// <param name="objectiveValue"></param>
        /// <param name="size"></param>
        public KnapsackOutput(IList<TVariable> variables, int objectiveValue, int size)
        {
            this.VariableValues = variables;
            this.Objective = objectiveValue;
            this.Optimal = 1;
            this.Size = size;
        }
    }
}