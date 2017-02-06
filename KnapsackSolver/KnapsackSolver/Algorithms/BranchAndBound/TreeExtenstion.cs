using System;

namespace KnapsackSolver.Algorithms.BranchAndBound
{
    /// <summary>
    /// Extension methods on tree
    /// </summary>
    public static class TreeExtenstion
    {
        /// <summary>
        /// Rolls up the tree from a node
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <param name="node"></param>
        /// <param name="action"></param>
        public static void RollUp<TNode>(this TNode node, Action<TNode> action)
            where TNode : ITreeNode<TNode>
        {
            while (node != null)
            {
                // act on node
                action(node);

                // go up
                node = node.Parent;
            }
        }

        /// <summary>
        /// Rolls up the tree ignoring parrent
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <param name="node"></param>
        /// <param name="action"></param>
        public static void RollUpExclusive<TNode>(this TNode node, Action<TNode> action)
                    where TNode : ITreeNode<TNode>
        {
            // while not reached the root
            while (node != null && node.Parent != null)
            {
                // act on node
                action(node);

                // go up
                node = node.Parent;
            }
        }
    }
}