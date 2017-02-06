namespace KnapsackSolver.Common
{
    /// <summary>
    /// The memorization interface
    /// </summary>
    /// <typeparam name="TValue">The value type</typeparam>
    /// <typeparam name="TKey">The key type</typeparam>
    public interface IMemorization<in TKey, TValue>
    {
        /// <summary>
        /// Checks for memorization
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns></returns>
        bool IsMemorized(TKey key);

        /// <summary>
        /// Gets the value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        TValue GetMemorized(TKey key);

        /// <summary>
        /// Gets value or default
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        TValue GetMemorized(TKey key, TValue defaultValue);

        /// <summary>
        /// Memorize
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        bool Memorize(TKey key, TValue value);

        /// <summary>
        /// Prints the result 
        /// </summary>
        void Print();
    }
}