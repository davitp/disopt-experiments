using System;
using System.Collections.Generic;
using System.Linq;

namespace KnapsackSolver
{
    /// <summary>
    /// The Knapsack Input
    /// </summary>
    public class KnapsackInput <TWeight, TValue> where TWeight : struct, IComparable<TWeight>, IConvertible
    {
        /// <summary>
        /// The Capacity
        /// </summary>
        public TWeight Capacity { get; }

        /// <summary>
        /// Number of variables
        /// </summary>
        public int Variables { get { return this.Items.Count; } }

        /// <summary>
        /// The Items
        /// </summary>
        protected IList<KnapsackItem<TWeight, TValue>> Items { get;  set; }

        /// <summary>
        /// Creates new instance of input
        /// </summary>
        /// <param name="capacity"></param>
        /// <param name="items"></param>
        public KnapsackInput
            (TWeight capacity,
            IList<KnapsackItem<TWeight, TValue>> items)
        {
            this.Capacity = capacity;
            this.Items = items;
        }

        

        /// <summary>
        /// Gets by index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public KnapsackItem<TWeight, TValue> GetByIndex(int index)
        {
            if (index <= this.Items.Count)
            {
                return this.Items[index - 1];
            }

            throw new ArgumentException();
        }
    }
}