using System;

namespace LetsGraph.Model.Base
{
    /// <summary>
    /// Base class of all items in this model.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Repository where this item belongs to.
        /// </summary>
        private readonly Repository _repository;

        /// <summary>
        /// The name of this item.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The type of this item.
        /// </summary>
        public ItemType Type { get; private set; }


        /// <summary>
        /// Creates a new item with name, type and reference to repository.
        /// </summary>
        /// <param name="repository">Repository where this item will be added by caller.</param>
        /// <param name="name">Name of the item.</param>
        /// <param name="type">Type of the item.</param>
        internal Item(Repository repository, string name, ItemType type)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("Item's 'repository' can't be null.");
            }

            _repository = repository;
            Name = name;
            Type = type;
        }


        /// <summary>
        /// Gets the repository containing this item.
        /// </summary>
        public Repository Repository
        {
            get
            {
                return _repository;
            }
        }
    }
}
