using System;
using System.Collections.Generic;
using KnapsackSolver.Common;

namespace KnapsackSolver.Algorithms
{
    /// <summary>
    /// The dynamic programming approach for knapsack
    /// </summary>
    public class KnapsackDynamicAlgorithm : IKnapsackAlgorithm<KnapsackOutput<int>>
    {
        /// <summary>
        /// The memorization object
        /// </summary>
        private readonly IMemorization<Tuple<int, int>, int> memorization;
        private readonly int Variables;
        private readonly int Capacity;
        private readonly KnapsackInput<int, int> Input;

        /// <summary>
        /// Executes the Algorithm
        /// </summary>
        /// <returns></returns>
        public KnapsackOutput<int> Execute()
        {
            var result = new List<int>();

            // objective and total
            var objective = 0;

            // check borders
            if (this.Capacity == 0 || this.Variables == 0)
            {
                return new KnapsackOutput<int>(result, objective, 0);
            }


            // for all weights with 0 variables values i zero
            for (var w = 0; w <= this.Capacity; ++w)
            {
                this.memorization.Memorize(Tuple.Create(w, 0), 0);
            }

            // for all variables, 0 capacity means no value
            for (var v = 0; v <= this.Variables; ++v)
            {
                // fill first column with zeros
                this.memorization.Memorize(Tuple.Create(0, v), 0);
            }

            // go over variable values
            for (var j = 1; j <= this.Variables; ++j)
            {
                // get current item details
                var item = this.Input.GetByIndex(j);

                // go over capacities
                for (var k = 1; k <= this.Capacity; ++k)
                {                    
                    var prevVarSameCapacity = Tuple.Create(k, j - 1);
                    var prevVarLessCapacity = Tuple.Create(k - item.Weight, j - 1);

                    // get previous item value with same capacity
                    var prevVarSameCapacityValue = this.memorization.GetMemorized(prevVarSameCapacity, int.MinValue);

                    // get less capacity and prev item value
                    var prevVarLessCapacityValue = this.memorization.GetMemorized(prevVarLessCapacity, int.MinValue);

                    // get max value
                    var maxValue = Math.Max(prevVarLessCapacityValue + item.Value, prevVarSameCapacityValue);

                    // corner cases
                    if (maxValue < 0)
                    {
                        maxValue = 0;
                    }

                    // store max value
                    this.memorization.Memorize(Tuple.Create(k, j), maxValue);
                }
                //Console.WriteLine(j + " "  + DateTime.Now);
            }

            // get the capacity
            var weight = this.Capacity;

            for (var v = this.Variables; v >= 1; --v)
            {
                var current = this.memorization.GetMemorized(Tuple.Create(weight, v));
                var prev = this.memorization.GetMemorized(Tuple.Create(weight, v - 1));

                var selected = 0;
                 
                // if value has changed insert 
                if (prev != current)
                {
                    selected = 1;
                }

                result.Insert(0, selected);

                var item = this.Input.GetByIndex(v);

                // get off weight of current selected element
                weight -= selected * item.Weight;

                // add total objective value
                objective += selected * item.Value;
            }

            return new KnapsackOutput<int>(result, objective, this.Capacity - weight);
        }
        
        /// <summary>
        /// Gets memorization object
        /// </summary>
        public IMemorization<Tuple<int, int>, int> Memorization { get { return this.memorization; } }

        /// <summary>
        /// Creates new algorithm instance
        /// </summary>
        /// <param name="input">The input</param>
        public KnapsackDynamicAlgorithm(KnapsackInput<int, int> input)
        {
            this.Input = input;
            this.Capacity = input.Capacity;
            this.Variables = input.Variables;
            this.memorization = new IntegerTableMemorization(input.Capacity + 1, input.Variables + 1);
        }

    }
}