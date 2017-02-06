namespace KnapsackSolver.Algorithms.BranchAndBound
{
    public interface ITreeNode<TNode>
    {
        TNode Parent { get; set; }
    }
}