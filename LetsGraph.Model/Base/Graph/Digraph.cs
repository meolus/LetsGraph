using System;
using System.Collections.Generic;

namespace LetsGraph.Model.Base.Graph
{
    /// <summary>
    /// Directed graph with nodes and edges.
    /// </summary>
    public class Digraph<TNode, TEdge>
        where TEdge : Edge<TNode>
    {
        /// <summary>
        /// Dictionary of nodes to list of outgoing edges.
        /// </summary>
        private Dictionary<TNode, List<TEdge>> _node2OutgoingEdges = new Dictionary<TNode, List<TEdge>>();

        /// <summary>
        /// Dictionary of nodes to list of incoming edges.
        /// </summary>
        private Dictionary<TNode, List<TEdge>> _node2IncomingEdges = new Dictionary<TNode, List<TEdge>>();

        /// <summary>
        /// Count of edges.
        /// </summary>
        private int _edgeCount = 0;


        /// <summary>
        /// Constructor.
        /// </summary>
        public Digraph()
        {
        }


        /// <summary>
        /// Adds edge to the graph.
        /// </summary>
        /// <param name="edge">Edge to add.</param>
        /// <returns>>True, if edge was successfully added. False, if edge already exists in graph.</returns>
        public bool AddEdge(TEdge edge)
        {
            if (ContainsEdge(edge))
            {
                return false;
            }

            _node2OutgoingEdges[edge.Source].Add(edge);
            _node2IncomingEdges[edge.Target].Add(edge);

            _edgeCount++;

            return true;
        }


        /// <summary>
        /// Adds node to the graph.
        /// </summary>
        /// <param name="node">Node to add.</param>
        /// <returns>True, if node was successfully added. False, if node already exists in graph.</returns>
        public bool AddNode(TNode node)
        {
            if (ContainsNode(node))
            {
                return false;
            }

            _node2OutgoingEdges.Add(node, new List<TEdge>());
            _node2IncomingEdges.Add(node, new List<TEdge>());

            return true;
        }


        /// <summary>
        /// Checks, if edge is contained in graph.
        /// </summary>
        /// <param name="edge">Edge to check.</param>
        /// <returns>True, if edge is contained in graph. False, if not.</returns>
        public bool ContainsEdge(TEdge edge)
        {
            if (edge == null)
            {
                throw new ArgumentNullException("Considered 'edge' can't be null.");
            }

            List<TEdge> outEdges;
            return _node2OutgoingEdges.TryGetValue(edge.Source, out outEdges)
                && outEdges.Contains(edge);
        }


        /// <summary>
        /// Checks, if node is contained in graph.
        /// </summary>
        /// <param name="node">Node to check.</param>
        /// <returns>True, if node is contained in graph. False, if not.</returns>
        public bool ContainsNode(TNode node)
        {
            return _node2OutgoingEdges.ContainsKey(node);
        }


        /// <summary>
        /// Returns count of edges in graph.
        /// </summary>
        public int EdgeCount
        {
            get
            {
                return _edgeCount;
            }
        }


        /// <summary>
        /// Get enumeration of all edges in graph.
        /// </summary>
        public IEnumerable<TEdge> Edges
        {
            get
            {
                foreach (var edges in _node2OutgoingEdges.Values)
                {
                    foreach (var edge in edges)
                    {
                        yield return edge;
                    }
                }
            }
        }


        /// <summary>
        /// Returns count of nodes in graph.
        /// </summary>
        public int NodeCount
        {
            get
            {
                return _node2OutgoingEdges.Count;
            }
        }


        /// <summary>
        /// Get enumeration of all nodes in graph.
        /// </summary>
        public IEnumerable<TNode> Nodes
        {
            get
            {
                return _node2OutgoingEdges.Keys;
            }
        }


        /// <summary>
        /// Removes edge from graph.
        /// </summary>
        /// <param name="edge">Edge to remove.</param>
        /// <returns>True, if successful removed. False, if not found in graph.</returns>
        public bool RemoveEdge(TEdge edge)
        {
            if (!ContainsEdge(edge))
            {
                return false;
            }

            _node2OutgoingEdges[edge.Source].Remove(edge);
            _node2IncomingEdges[edge.Target].Remove(edge);
            _edgeCount--;

            return true;
        }


        /// <summary>
        /// Removes node from graph.
        /// </summary>
        /// <param name="node">Node to remove.</param>
        /// <returns>True, if successful removed. False, if not found in graph.</returns>
        public bool RemoveNode(TNode node)
        {
            if (!ContainsNode(node))
            {
                return false;
            }

            // Remove references to other ends of edges to remove
            var removedEdgeCount = 0;
            foreach (var outEdge in _node2OutgoingEdges[node])
            {
                _node2IncomingEdges[outEdge.Target].Remove(outEdge);
                removedEdgeCount++;
            }
            foreach (var inEdge in _node2IncomingEdges[node])
            {
                _node2OutgoingEdges[inEdge.Source].Remove(inEdge);
                removedEdgeCount++;
            }

            _node2OutgoingEdges.Remove(node);
            _node2IncomingEdges.Remove(node);
            _edgeCount -= removedEdgeCount;

            return true;
        }


        /// <summary>
        /// Returns enumeration of incoming edges of node.
        /// </summary>
        /// <param name="node">Reference node.</param>
        /// <returns>Enumeration of incoming edges.</returns>
        public IEnumerable<TEdge> GetIncomingEdges(TNode node)
        {
            return _node2IncomingEdges[node];
        }


        /// <summary>
        /// Returns enumeration of outgoing edges of node.
        /// </summary>
        /// <param name="node">Reference node.</param>
        /// <returns>Enumeration of outgoing edges.</returns>
        public IEnumerable<TEdge> GetOutgoingEdges(TNode node)
        {
            return _node2OutgoingEdges[node];
        }
    }
}
