using System;
using System.Security.Cryptography;
using System.Threading.Tasks;


namespace KnapsackSolver.Common
{
    /// <summary>
    /// The List based memorization strategy
    /// </summary>
    public class ListMemorization : IMemorization<Tuple<int, int>, int>
    {
        /// <summary>
        /// Creates new instance of memorization object
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="rows"></param>
        public ListMemorization(int columns, int rows)
        {
            this.rows = rows;
            this.columns = columns;
            this.innerList = new int[this.rows][];

            // instantiate new sets for each
            for (var i = 0; i < this.innerList.Length; ++i)
            {
                this.innerList[i] = new int[this.columns];
            }
        }

        /// <summary>
        /// The rows count
        /// </summary>
        private readonly int rows;

        /// <summary>
        /// The columns count
        /// </summary>
        private readonly int columns;

        /// <summary>
        /// The inner list
        /// </summary>
        private readonly int[][] innerList;

        /// <summary>
        /// If memorized or not
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsMemorized(Tuple<int, int> key)
        {
            return this.InBound(key); // && this.innerList[key.Item2][key.Item1] != null;
        }


        /// <summary>
        /// Get memorized value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int GetMemorized(Tuple<int, int> key)
        {
            if (!this.InBound(key))
            {
                throw new IndexOutOfRangeException();
            }

            //var value = this.innerList[key.Item2][key.Item1];

            //if (value.HasValue)
            //{
            //    return value.Value;
            //}

            //throw new ArgumentException();

            return this.innerList[key.Item2][key.Item1];
        }

        /// <summary>
        /// Get memorized value or default
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public int GetMemorized(Tuple<int, int> key, int defaultValue)
        {
            return this.IsMemorized(key) ? this.GetMemorized(key) : defaultValue;
        }


        /// <summary>
        /// Memorize the value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Memorize(Tuple<int, int> key, int value)
        {
            if (this.InBound(key))
            {
                this.innerList[key.Item2][key.Item1] = value;
            }

            return false;
        }

        /// <summary>
        /// Checks if in bound or not
        /// </summary>
        /// <returns></returns>
        private bool InBound(Tuple<int, int> key)
        {
            return  key.Item1 >= 0 && key.Item1 < this.columns  && key.Item2 >= 0 && key.Item2 < this.rows;
        }

        public void Print()
        {
            
        }
    }
}