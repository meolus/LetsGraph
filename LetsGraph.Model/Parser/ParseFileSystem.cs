using System.IO;
using LetsGraph.Model.Base;

namespace LetsGraph.Model.Parser
{
    /// <summary>
    /// Static helper class to parse file system and complement repository.
    /// </summary>
    public static class ParseFileSystem
    {
        /// <summary>
        /// Parses file system recursiv starting with path and creates items and relations in repository.
        /// </summary>
        /// <param name="repository">Repository to complement.</param>
        /// <param name="path">Path in file system where to start.</param>
        /// <returns></returns>
        public static Item ParsePathRecursive(Repository repository, string path)
        {
            if (repository == null || !Directory.Exists(path))
            {
                return null;
            }

            var directoryItem = repository.AddNewItem(Path.GetFileName(path), ItemType.Directory);

            var subDirectories = Directory.GetDirectories(path);
            foreach (var subDirectory in subDirectories)
            {
                var subDirectoryItem = ParsePathRecursive(repository, subDirectory);
                repository.AddNewRelation(directoryItem, subDirectoryItem, RelationType.Contains);
            }

            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                var fileItem = repository.AddNewItem(Path.GetFileName(file), ItemType.File);
                repository.AddNewRelation(directoryItem, fileItem, RelationType.Contains);
            }

            return directoryItem;
        }
    }
}
