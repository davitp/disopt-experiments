using System;

namespace KnapsackSolver.Common
{
    /// <summary>
    /// The memorization
    /// </summary>
    public class IntegerTableMemorization : IMemorization<Tuple<int, int>, int>
    {
        /// <summary>
        /// The rows count
        /// </summary>
        private readonly int rows;

        /// <summary>
        /// The columns count
        /// </summary>
        private readonly int columns;

        /// <summary>
        /// The inner table
        /// </summary>
        private readonly int?[,] innerTable;

        /// <summary>
        /// Creates new Integer table memorization oracle
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        public IntegerTableMemorization(int columns, int rows)
        {
            this.rows = rows;
            this.columns = columns;
            this.innerTable = new int?[this.columns, this.rows];

        }

        /// <summary>
        /// Checks memorization
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsMemorized(Tuple<int, int> key)
        {
            var col = key.Item1;
            var row = key.Item2;

            if (col >= 0 && col < this.columns &&  row >= 0 && row < this.rows)
            {
                return this.innerTable[col, row] != null;
            }

            return false;
        }

        /// <summary>
        /// Gets memorized
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int GetMemorized(Tuple<int, int> key)
        {
            var result = this.innerTable[key.Item1, key.Item2];
            if (result.HasValue)
            {
                return result.Value;
            }

            throw new ArgumentException(string.Format("Value with key ({0}, {1}) is not memorized", key.Item1, key.Item2));
        }

        /// <summary>
        /// Get or default
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public int GetMemorized(Tuple<int, int> key, int defaultValue)
        {
            if (this.IsMemorized(key))
            {
                return this.GetMemorized(key);
            }    

            return defaultValue;
        }


        /// <summary>
        /// Try memorize
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Memorize(Tuple<int, int> key, int value)
        {
            if (this.columns <= key.Item1 || this.rows <= key.Item2)
            {
                return false;
            }

            this.innerTable[key.Item1, key.Item2] = value;
            return true;
        }

        /// <summary>
        /// Print the values
        /// </summary>
        public void Print()
        {
            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < this.columns; j++)
                {
                    Console.Write("{0} ", this.innerTable[j, i] ?? 0 );
                }
                Console.WriteLine();
            }
        }
    }
}