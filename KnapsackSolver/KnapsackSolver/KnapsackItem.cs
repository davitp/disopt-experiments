namespace KnapsackSolver
{
    public class KnapsackItem<TWeight, TValue>
    {
        /// <summary>
        /// The index
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// The wight
        /// </summary>
        public TWeight Weight { get; private set; }

        /// <summary>
        /// The value
        /// </summary>
        public TValue Value { get; private set; }

        /// <summary>
        /// Creates new instance of item
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="value"></param>
        /// <param name="index"></param>
        public KnapsackItem(TWeight weight, TValue value, int index)
        {
            this.Weight = weight;
            this.Value = value;
            this.Index = index;
        }
    }
}