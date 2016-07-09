using System.Collections.Generic;
using LetsGraph.Model.Base.Graph;

namespace LetsGraph.Model.Base
{
    /// <summary>
    /// Base class of all repositories for collected knowledge in this model.
    /// </summary>
    public class Repository
    {
        /// <summary>
        /// Underlaying model for the collected knowledge is a digraph.
        /// </summary>
        private readonly Digraph<Item, Relation> _graph;


        public Repository()
        {
            _graph = new Digraph<Item, Relation>();
        }


        /// <summary>
        /// Creates a new item with name and type, and adds it to this repository.
        /// </summary>
        /// <param name="name">Name of the item.</param>
        /// <param name="type">Type of the item.</param>
        /// <returns>Added item.</returns>
        public Item AddNewItem(string name, ItemType type)
        {
            var item = new Item(this, name, type);
            _graph.AddNode(item);
            return item;
        }


        /// <summary>
        /// Creates relation of given type between source and target and adds it to this repository.
        /// </summary>
        /// <param name="source">Start item of the relation.</param>
        /// <param name="target">End item of the relation.</param>
        /// <param name="type">Type of the relation.</param>
        public Relation AddNewRelation(Item source, Item target, RelationType type)
        {
            var relation = new Relation(source, target, type);
            _graph.AddEdge(relation);
            return relation;
        }


        /// <summary>
        /// Returns enumeration of all items in repository.
        /// </summary>
        /// <returns>Enumeration of all items.</returns>
        public IEnumerable<Item> GetAllItems()
        {
            return _graph.Nodes;
        }


        /// <summary>
        /// Returns enumeration of all relations in repository.
        /// </summary>
        /// <returns>Enumeration of all relations.</returns>
        public IEnumerable<Relation> GetAllRelations()
        {
            return _graph.Edges;
        }


        /// <summary>
        /// Returns enumeration of incoming relations if item exists in repository (else null).
        /// </summary>
        /// <param name="item">Reference item.</param>
        /// <returns>Enumeration of incoming relations or null.</returns>
        public IEnumerable<Relation> GetIncomingRelations(Item item)
        {
            if (item == null || !_graph.ContainsNode(item))
            {
                return null;
            }

            return _graph.GetIncomingEdges(item);
        }


        /// <summary>
        /// Returns total item count in repository.
        /// </summary>
        /// <returns>Item count.</returns>
        public int GetItemCount()
        {
            return _graph.NodeCount;
        }


        /// <summary>
        /// Returns total relation count in repository.
        /// </summary>
        /// <returns>Relation count.</returns>
        public int GetRelationCount()
        {
            return _graph.EdgeCount;
        }


        /// <summary>
        /// Returns enumeration of outgoing relations if item exists in repository (else null).
        /// </summary>
        /// <param name="item">Reference item.</param>
        /// <returns>Enumeration of outgoing relations or null.</returns>
        public IEnumerable<Relation> GetOutgoingRelations(Item item)
        {
            if (item == null || !_graph.ContainsNode(item))
            {
                return null;
            }

            return _graph.GetOutgoingEdges(item);
        }
    }
}
