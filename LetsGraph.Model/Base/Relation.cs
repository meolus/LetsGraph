using LetsGraph.Model.Base.Graph;

namespace LetsGraph.Model.Base
{
    /// <summary>
    /// Base class of all relations in this model.
    /// </summary>
    public class Relation : Edge<Item>
    {
        /// <summary>
        /// The type of this relation.
        /// </summary>
        public RelationType Type { get; private set; }


        /// <summary>
        /// Creates relation of given type between source and target item.
        /// </summary>
        /// <param name="source">Start item of the relation.</param>
        /// <param name="target">End item of the relation.</param>
        /// <param name="type">Type of the relation.</param>
        internal Relation(Item source, Item target, RelationType type) : base(source, target)
        {
            Type = type;
        }
    }
}
