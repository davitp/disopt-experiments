using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace KnapsackSolver.Common
{
    /// <summary>
    /// The compressed matrix strategy memorizatoin
    /// </summary>
    public class CompressionMatrixMemorization : IMemorization<Tuple<int, int>, int>
    {
        /// <summary>
        /// The value region descriptor
        /// </summary>
        private class RangeValue : IComparable<RangeValue>
        {
            /// <summary>
            /// The start  index
            /// </summary>
            public int StartIndex { get;}

            /// <summary>
            /// The End index
            /// </summary>
            public int EndIndex { get; set; }

            /// <summary>
            /// The Value
            /// </summary>
            public int Value { get; }

            /// <summary>
            /// Columns
            /// </summary>
            private readonly int columns;

            /// <summary>
            /// Rows
            /// </summary>
            private readonly int rows;

            /// <summary>
            /// Creates
            /// </summary>
            /// <param name="value"></param>
            /// <param name="startIndex"></param>
            /// <param name="endIndex"></param>
            /// <param name="columns"></param>
            /// <param name="rows"></param>
            public RangeValue(int value, int startIndex, int endIndex, int columns, int rows)
            {
                this.Value = value;
                this.StartIndex = startIndex;
                this.EndIndex = endIndex;
                this.columns = columns;
                this.rows = rows;
            }


            /// <summary>
            /// Convert to index
            /// </summary>
            /// <param name="i"></param>
            /// <param name="j"></param>
            /// <param name="columns"></param>
            /// <param name="rows"></param>
            /// <returns></returns>
            public static int ConvertIndex(int i, int j, int columns, int rows)
            {
                return i;
            }

            /// <summary>
            /// Contains
            /// </summary>
            /// <param name="i"></param>
            /// <param name="j"></param>
            /// <returns></returns>
            public bool Contains(int i, int j)
            {
                var idx = ConvertIndex(i, j, this.columns, this.rows);
                return this.StartIndex <= idx && idx <= this.EndIndex;
            }

            /// <summary>
            /// Can continue
            /// </summary>
            /// <param name="i"></param>
            /// <param name="j"></param>
            /// <returns></returns>
            public bool CanContinueWith(int i, int j)
            {
                return this.EndIndex + 1 == i;
            }

            /// <summary>
            /// Compare by value
            /// </summary>
            /// <param name="other"></param>
            /// <returns></returns>
            public int CompareTo(RangeValue other)
            {
                return this.EndIndex.CompareTo(other.EndIndex);
            }
        }

        /// <summary>
        /// the values storage
        /// </summary>
        private readonly List<List<RangeValue>> values;

        /// <summary>
        /// Creates new Compression matrix 
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="rows"></param>
        public CompressionMatrixMemorization(int columns, int rows)
        {
            this.columns = columns;
            this.rows = rows;
            this.values = Enumerable.Repeat<List<RangeValue>>(null, rows).ToList();
        }

        /// <summary>
        /// Number of columns
        /// </summary>
        private readonly int columns;

        /// <summary>
        /// Number of rows
        /// </summary>
        private readonly int rows;

        /// <summary>
        /// Checks if key is in bound or not
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private bool InBound(Tuple<int, int> key)
        {
            return key.Item1 < this.columns && key.Item2 < this.rows;
        }

        private RangeValue GetRange(int column, int row)
        {
            if (row >= this.values.Count)
            {
                return null;
            }

            var flatIndex = RangeValue.ConvertIndex(column, row, this.columns, this.rows);

            // row ranges
            var rowRanges = this.values[row];

            //// the range index
            //var rangeIndex = rowRanges.BinarySearch(new RangeValue(0, flatIndex, flatIndex, this.columns, this.rows));

            //if (rangeIndex < 0) rangeIndex = ~rangeIndex;

            var rangeIndexes = new[] { rowRanges.Count - 1, rowRanges.Count - 2};

            foreach (var index in rangeIndexes)
            {
                if (index < 0)
                {
                    continue;
                }
                var possibleRange = rowRanges[index];
                if (possibleRange.Contains(column, row))
                {
                    return possibleRange;
                }
            }

           
            return null;
        }

        /// <summary>
        /// Is memorized 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsMemorized(Tuple<int, int> key)
        {
            return this.InBound(key) && this.GetRange(key.Item1, key.Item2) != null;
        }

        /// <summary>
        /// Get memorized value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int GetMemorized(Tuple<int, int> key)
        {
            if (this.InBound(key))
            {
                var range = this.GetRange(key.Item1, key.Item2);
                if (range != null)
                {
                    return range.Value;
                }
            }

            throw new ArgumentException();
        }

        public int GetMemorized(Tuple<int, int> key, int defaultValue)
        {
            if (this.InBound(key))
            {
                var range = this.GetRange(key.Item1, key.Item2);
                if (range != null)
                {
                    return range.Value;
                }
            }

            return defaultValue;
        }

        /// <summary>
        /// Memorize the value with given key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Memorize(Tuple<int, int> key, int value)
        {
            // check bounds
            if (!this.InBound(key))
            {
                return false;
            }

            // get indexes of key
            var column = key.Item1;
            var row = key.Item2;

            // get flat index
            var flatIndex = RangeValue.ConvertIndex(column, row, this.columns, this.rows);

            if (this.values[row] == null)
            {
                // add range list for row
                this.values[row] = new List<RangeValue>();
            }
            // the row
            var rowRanges = this.values[row];

            //// get index of certain range
            //var index = rowRanges.BinarySearch(new RangeValue(0, flatIndex, flatIndex, 0, 0));

            //// reverse index
            //if (index < 0) index = ~index;
            var index = 1;
            var indexes = new[]{ index -1 , index };

            foreach (var idx in indexes)
            {
                if (idx < 0)
                {
                    continue;
                }

                if (idx == rowRanges.Count)
                {
                    rowRanges.Insert(idx, new RangeValue(value, flatIndex, flatIndex, this.columns, this.rows));
                    return true;
                }

                var range = rowRanges[idx];
                if (range.CanContinueWith(column, row) && range.Value == value)
                {
                    this.values[row][idx].EndIndex++;
                    return true;
                }
                
            }


            return false;
        }

        public void Print()
        {
            //throw new NotImplementedException();
        }
    }
}