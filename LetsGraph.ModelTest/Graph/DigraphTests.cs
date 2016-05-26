using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LetsGraph.Model.Graph;

namespace LetsGraph.ModelTest.Graph
{
    [TestClass]
    public class DigraphTests
    {
        [TestMethod]
        public void TestDigraph_Create()
        {
            var g = new Digraph<int, Edge<int>>();
            Assert.IsNotNull(g);
            Assert.AreEqual(0, g.NodeCount);
            Assert.AreEqual(0, g.EdgeCount);
        }


        [TestMethod]
        public void TestDigraph_AddNode()
        {
            var g = new Digraph<int, Edge<int>>();

            var node1 = 1;
            Assert.IsFalse(g.ContainsNode(node1));
            Assert.IsTrue(g.AddNode(node1), "Was not able to add node1 the first time.");
            Assert.IsTrue(g.ContainsNode(node1));
            Assert.AreEqual(1, g.NodeCount);

            Assert.IsFalse(g.AddNode(node1), "Was able to add node1 the second time.");
            Assert.IsTrue(g.ContainsNode(node1));
            Assert.AreEqual(1, g.NodeCount);


            var node2 = 2;
            Assert.IsFalse(g.ContainsNode(node2));
            Assert.IsTrue(g.AddNode(node2), "Was not able to add node2 the first time.");
            Assert.IsTrue(g.ContainsNode(node2));
            Assert.AreEqual(2, g.NodeCount);

            Assert.IsFalse(g.AddNode(node2), "Was able to add node2 the second time.");
            Assert.IsTrue(g.ContainsNode(node2));
            Assert.AreEqual(2, g.NodeCount);
        }


        [TestMethod]
        public void TestDigraph_AddNode_FailsWithNull()
        {
            var g = new Digraph<string, Edge<string>>();

            string nullNode = null;

            try
            {
                Assert.IsFalse(g.AddNode(nullNode), "Was unexspectably able to add null-node.");
                Assert.Fail("Excpected exception.");
            }
            catch (ArgumentNullException)
            {
                Assert.AreEqual(0, g.NodeCount, "There should be no node in the graph.");
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected type '" + ex.GetType().ToString() + "' of exception.");
            }
        }


        [TestMethod]
        public void TestDigraph_AddEdge()
        {
            var g = new Digraph<int, Edge<int>>();

            var node1 = 1;
            var node2 = 2;
            g.AddNode(node1);
            g.AddNode(node2);

            var edge1 = new Edge<int>(node1, node2);
            Assert.IsFalse(g.ContainsEdge(edge1));
            Assert.IsTrue(g.AddEdge(edge1), "Was not able to add edge1 the first time.");
            Assert.IsTrue(g.ContainsEdge(edge1));
            Assert.AreEqual(1, g.EdgeCount);

            Assert.IsFalse(g.AddEdge(edge1), "Was able to add edge1 the second time.");
            Assert.IsTrue(g.ContainsEdge(edge1));
            Assert.AreEqual(1, g.EdgeCount);


            // Second edge parallel to first one:
            var edge2 = new Edge<int>(node1, node2);
            Assert.AreEqual(edge1.Source, edge2.Source);
            Assert.AreEqual(edge1.Target, edge2.Target);

            Assert.IsFalse(g.ContainsEdge(edge2));
            Assert.IsTrue(g.AddEdge(edge2), "Was not able to add edge2 the first time.");
            Assert.IsTrue(g.ContainsEdge(edge2));
            Assert.AreEqual(2, g.EdgeCount);

            Assert.IsFalse(g.AddEdge(edge2), "Was able to add edge2 the second time.");
            Assert.IsTrue(g.ContainsEdge(edge2));
            Assert.AreEqual(2, g.EdgeCount);
        }


        [TestMethod]
        public void TestDigraph_AddEdge_FailsWithMissingNodes()
        {
            var g = new Digraph<int, Edge<int>>();

            var sourceInside = 1;
            var targetInside = 2;
            g.AddNode(sourceInside);
            g.AddNode(targetInside);

            var sourceOutside = 3;
            var targetOutside = 4;

            foreach (var source in new[] { sourceInside, sourceOutside })
            {
                foreach (var target in new[] { targetInside, targetOutside })
                {
                    var signature = "source=" + source + "; target=" + target;

                    var edge = new Edge<int>(source, target);
                    Assert.IsNotNull(edge);
                    var isValid = (source < 3) && (target < 3); // The edge is valid, if both nodes are in the graph.

                    try
                    {
                        Assert.AreEqual(isValid,
                                        g.AddEdge(edge),
                                        "Should be able to add edge only if edge is valid. @signature=" + signature);
                        if (!isValid)
                        {
                            Assert.Fail("Expect an exception if edge is invalid. @signature=" + signature);
                        }
                    }
                    catch (KeyNotFoundException)
                    {
                        Assert.IsTrue(!isValid,
                                      "Expect this exception only if edge is invalid. @signature=" + signature);
                    }
                    catch (Exception ex)
                    {
                        Assert.Fail("Unexpected type '" + ex.GetType().ToString() + "' of exception. @signature=" + signature);
                    }
                }
            }
        }


        [TestMethod]
        public void TestDigraph_AddEdge_FailsWithNull()
        {
            var g = new Digraph<int, Edge<int>>();

            try
            {
                Assert.IsFalse(g.AddEdge(null));
                Assert.Fail();
            }
            catch (ArgumentNullException)
            {
                Assert.AreEqual(0, g.EdgeCount, "There should be no edge in the graph.");
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected type '" + ex.GetType().ToString() + "' of exception.");
            }
        }


        [TestMethod]
        public void TestDigraph_RemoveEdge()
        {
            var g = new Digraph<int, Edge<int>>();

            var sourceNode = 1;
            var targetNode = 2;
            g.AddNode(sourceNode);
            g.AddNode(targetNode);

            var edge = new Edge<int>(sourceNode, targetNode);
            Assert.AreEqual(0, g.EdgeCount);
            Assert.IsTrue(g.AddEdge(edge));
            Assert.AreEqual(1, g.EdgeCount);
            Assert.IsTrue(g.RemoveEdge(edge));
            Assert.AreEqual(0, g.EdgeCount);

            // Try to remove already removed / not existing edge:
            Assert.IsFalse(g.RemoveEdge(edge));
            Assert.AreEqual(0, g.EdgeCount);
        }


        [TestMethod]
        public void TestDigraph_RemoveEdge_FailsWithNull()
        {
            var g = new Digraph<int, Edge<int>>();

            try
            {
                Assert.IsFalse(g.RemoveEdge(null));
                Assert.Fail();
            }
            catch (ArgumentNullException)
            {
                Assert.AreEqual(0, g.EdgeCount, "There should be no edge in the graph.");
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected type '" + ex.GetType().ToString() + "' of exception.");
            }
        }


        [TestMethod]
        public void TestDigraph_RemoveNode()
        {
            var g = new Digraph<int, Edge<int>>();

            var node1 = 1;
            var node2 = 2;
            g.AddNode(node1);
            g.AddNode(node2);
            Assert.AreEqual(2, g.NodeCount);

            var edge1 = new Edge<int>(node1, node2);
            var edge2 = new Edge<int>(node2, node1);
            Assert.AreEqual(0, g.EdgeCount);
            g.AddEdge(edge1);
            g.AddEdge(edge2);
            Assert.AreEqual(2, g.EdgeCount);

            Assert.IsTrue(g.RemoveNode(node1));
            Assert.AreEqual(1, g.NodeCount);
            Assert.AreEqual(0, g.EdgeCount);

            // Try to remove already removed / not existing node:
            Assert.IsFalse(g.RemoveNode(node1));
            Assert.AreEqual(1, g.NodeCount);
        }


        [TestMethod]
        public void TestDigraph_RemoveNode_FailsWithNull()
        {
            var g = new Digraph<string, Edge<string>>();

            try
            {
                Assert.IsFalse(g.RemoveNode(null));
                Assert.Fail();
            }
            catch (ArgumentNullException)
            {
                Assert.AreEqual(0, g.NodeCount, "There should be no node in the graph.");
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected type '" + ex.GetType().ToString() + "' of exception.");
            }
        }
    }
}
