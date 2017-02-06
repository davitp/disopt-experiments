using System;
using System.Collections.Generic;
using System.Linq;
using KnapsackSolver.Algorithms;

namespace KnapsackSolver
{
    public class KnapsackIntegralInput : KnapsackInput<int, int>
    {
        /// <summary>
        /// Gets item ratio
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private static double ItemRatio(KnapsackItem<int, int> item)
        {
            return (double)item.Value/item.Weight;
        }
        
        /// <summary>
        /// The initial optimistic evaluation
        /// </summary>
        /// <returns></returns>
        public double GetOptimisticEvaluation(int currentWeight, int currentValue, int currentVariable)
        { 
            if (!this.Items.Any())
            {
                return 0;
            }

            var evaluation = (double) currentValue;
            var weight = currentWeight;


            for (var index = currentVariable + 1; index <= this.Items.Count; ++index)
            {
                var variable = index;

                var item = this.GetByIndex(variable);


                // if capacity is full break process
                if (this.Capacity <= weight)
                {
                    break;
                }

                // if adding weight will not exceed the capacity
                // add evaluation value and increase weight
                if (weight + item.Weight <= this.Capacity)
                {
                    evaluation += item.Value;
                    weight += item.Weight;
                }
                // if after adding will go out of capacity
                // add a fraction of value
                else
                {
                    // get remaining capacity
                    var remainingCapacity = this.Capacity - weight;

                    // add fraction by remaining capacity
                    evaluation += remainingCapacity*ItemRatio(item);

                    // increase weight
                    weight = this.Capacity;
                }
            }

            return evaluation;
        }

        /// <summary>
        /// Get ratio difference
        /// </summary>
        /// <returns></returns>
        public double RatioDifference()
        {
            return ItemRatio(this.Items.First()) - ItemRatio(this.Items.Last());
        }

        /// <summary>
        /// Creates a integral based knapsack input
        /// </summary>
        /// <param name="capacity"></param>
        /// <param name="items"></param>
        public KnapsackIntegralInput(int capacity, IList<KnapsackItem<int, int>> items) : base(capacity, items)
        {
            // sort items by ratio
            this.Items = this.Items.OrderByDescending(ItemRatio).ThenByDescending(it => it.Weight).ToList();
        }
    }
}