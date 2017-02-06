using System;
using System.Collections.Generic;
using System.Linq;


namespace KnapsackSolver.Algorithms.BranchAndBound
{
    /// <summary>
    /// The branch and bound algorithm
    /// </summary>
    public class KnapsackBranchAndBoundAlgorithm : IKnapsackAlgorithm<KnapsackOutput<int>>
    {
        /// <summary>
        /// The solution node factory
        /// </summary>
        private readonly SolutionNodeFactory solutionNodeFactory;

        /// <summary>
        /// Creates new algorithm instance
        /// </summary>
        /// <param name="input">The input</param>
        public KnapsackBranchAndBoundAlgorithm(KnapsackIntegralInput input)
        {
            this.Input = input;
            this.Capacity = input.Capacity;
            this.Variables = input.Variables;
            this.solutionNodeFactory = new SolutionNodeFactory();
        }

        /// <summary>
        /// The input variables count
        /// </summary>
        public int Variables { get; }

        /// <summary>
        /// The Knapsack capacity
        /// </summary>
        public int Capacity { get; }

        /// <summary>
        /// The input data
        /// </summary>
        public KnapsackIntegralInput Input { get; }

        /// <summary>
        /// Executes the algorithm and generates the output
        /// </summary>
        /// <returns></returns>
        public KnapsackOutput<int> Execute()
        {
            // initial relaxation for root node
            var initialRelaxation = this.Input.GetOptimisticEvaluation(0, 0, 0);

            // the root node
            var best = this.solutionNodeFactory.BuildRoot(this.Input.Capacity, 0, initialRelaxation);

            // the queue
            var queue = new Stack<SolutionIntegralNode>(new [] { best });

            // while there is any element to explore
            while (queue.Count > 0)
            {
                // get out from queue
                var current = queue.Pop();

                // the next variable
                var nextVariable = current.Variable + 1;

                // break if down of tree
                if (nextVariable == this.Variables + 1)
                {
                    if (best.Value < current.Value)
                    {
                        best = current;
                    }
                    
                    continue;
                }

                // the left decision
                var leftDecision = 1;

                // the right decision
                var rightDecision = 0;

                // get certain item
                var item = this.Input.GetByIndex(nextVariable);
                
                // the value
                var leftValue = current.Value + leftDecision * item.Value;

                // the right value
                var rightValue = current.Value + rightDecision*item.Value;

                // the weight
                var leftRoom = current.Room - leftDecision * item.Weight;

                // the right weight
                var rightRoom = current.Room - rightDecision * item.Weight;

                // left relaxation
                var leftOptimistic = this.Input.GetOptimisticEvaluation(this.Capacity - leftRoom, leftValue, nextVariable);

                // right relaxation
                var rightOptimistic = this.Input.GetOptimisticEvaluation(this.Capacity - rightRoom, rightValue, nextVariable);

                // create left node
                current.LeftNode = current.LeftNode ??
                                    this.solutionNodeFactory
                                    .ExtendNode
                                    (
                                        current,
                                        nextVariable, 
                                        leftDecision, 
                                        leftValue, 
                                        leftRoom, 
                                        leftOptimistic
                                    );


                // create right node
                current.RightNode = current.RightNode ??
                                    this.solutionNodeFactory
                                    .ExtendNode
                                    (
                                        current,
                                        nextVariable, 
                                        rightDecision, 
                                        rightValue, 
                                        rightRoom, 
                                        rightOptimistic
                                    );

                // if right is feasible and still can be improved by optimistic evaluation
                if (current.RightNode.IsFeasible() && current.RightNode.OptimisticEvaluation > best.Value)
                {
                    // add to queue
                    queue.Push(current.RightNode);
                }


                // if left is feasible and still can be improved by optimistic evaluation
                if (current.LeftNode.IsFeasible() && current.LeftNode.OptimisticEvaluation > best.Value)
                {
                    // add to queue
                    queue.Push(current.LeftNode);
                }

            }

            var outputList = new Tuple<KnapsackItem<int, int>, int>[this.Variables];
            
            best.RollUpExclusive(n => outputList[n.Variable - 1] = Tuple.Create(this.Input.GetByIndex(n.Variable), n.Decision));

            // build output and return it
            return new KnapsackOutput<int>(outputList.OrderBy(item => item.Item1.Index).Select(a => a.Item2).ToList(), best.Value, this.Input.Capacity - best.Room);
        }
    }
}