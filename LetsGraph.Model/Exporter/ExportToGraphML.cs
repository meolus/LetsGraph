using System.Collections.Generic;
using System.IO;
using System.Linq;
using LetsGraph.Model.Base;

namespace LetsGraph.Model.Exporter
{
    /// <summary>
    /// Static helper class to export repository to GraphML file format.
    /// </summary>
    public static class ExportToGraphML
    {
        /// <summary>
        /// Exports repository as GraphML to file with filename.
        /// </summary>
        /// <param name="repository">Repository to export.</param>
        /// <param name="filename">Destination file.</param>
        public static void ExportToFile(Repository repository, string filename)
        {
            using (var outputFile = new StreamWriter(filename))
            {
                // Write file header:
                outputFile.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                outputFile.WriteLine("<graphml xmlns=\"http://graphml.graphdrawing.org/xmlns\"");
                outputFile.WriteLine("         xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"");
                outputFile.WriteLine("         xsi:schemaLocation=\"http://graphml.graphdrawing.org/xmlns/1.0/graphml.xsd\">");
                outputFile.WriteLine("\t<graph id=\"G\" edgedefault=\"directed\">");

                // Write items and generate node ids:
                var items = repository.GetAllItems();
                var itemIds = new Dictionary<Item, string>(items.Count());
                var nodeIdCount = 0;
                foreach (var item in items)
                {
                    var id = "n" + (nodeIdCount++);
                    outputFile.WriteLine("\t\t<node id=\"" + id + "\" />");
                    itemIds.Add(item, id);
                }

                // Write relations:
                var relations = repository.GetAllRelations();
                var edgeIdCount = 0;
                foreach (var relation in relations)
                {
                    var id = "e" + (edgeIdCount++);
                    outputFile.WriteLine("\t\t<edge id=\"" + id + "\" source=\"" + itemIds[relation.Source]
                                                                + "\" target=\"" + itemIds[relation.Target] + "\" />");
                }

                // Write file footer:
                outputFile.WriteLine("\t</graph>");
                outputFile.WriteLine("</graphml>");
            }
        }
    }
}
