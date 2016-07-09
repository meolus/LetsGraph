using System;

namespace LetsGraph.Model.Base.Graph
{
    /// <summary>
    /// A directed edge.
    /// </summary>
    /// <typeparam name="TNode">Type of the nodes.</typeparam>
    public class Edge<TNode>
    {
        /// <summary>
        /// Node where the egde starts.
        /// </summary>
        private readonly TNode _source;

        /// <summary>
        /// Node where the edge ends.
        /// </summary>
        private readonly TNode _target;


        /// <summary>
        /// Initializes the edge.
        /// </summary>
        /// <param name="source">Start node.</param>
        /// <param name="target">End node.</param>
        public Edge(TNode source, TNode target)
        {
            if (source == null)
            {
                throw new ArgumentNullException("Edge's 'source' can't be null.");
            }

            if (target == null)
            {
                throw new ArgumentNullException("Edge's 'target' can't be null.");
            }

            _source = source;
            _target = target;
        }


        /// <summary>
        /// Gets the start node.
        /// </summary>
        public TNode Source
        {
            get
            {
                return _source;
            }
        }


        /// <summary>
        /// Gets the target node.
        /// </summary>
        public TNode Target
        {
            get
            {
                return _target;
            }
        }


        /// <summary>
        /// Returns string representation of edge.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString()
        {
            return Source + "->" + Target;
        }
    }
}
