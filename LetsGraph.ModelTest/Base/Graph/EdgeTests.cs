using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LetsGraph.Model.Base.Graph;

namespace LetsGraph.ModelTest.Base.Graph
{
    [TestClass]
    public class EdgeTests
    {
        [TestMethod]
        public void TestEdge_Create()
        {
            int source = 1;
            int target = 2;
            Assert.AreNotEqual(source, target);

            var edge = new Edge<int>(source, target);
            Assert.IsNotNull(edge);
            Assert.AreEqual(source, edge.Source);
            Assert.AreEqual(target, edge.Target);
            Assert.AreNotEqual(edge.Source, edge.Target);
        }


        [TestMethod]
        public void TestEdge_Create_Loop()
        {
            int node = 1;

            var loopEdge = new Edge<int>(node, node);
            Assert.IsNotNull(loopEdge);
            Assert.AreEqual(loopEdge.Source, loopEdge.Target);
        }


        [TestMethod]
        public void TestEdge_Create_FailsWithNull()
        {
            foreach (var source in new[] { "1", null })
            {
                foreach (var target in new[] { "2", null })
                {
                    var signature = "source=" + source + "; target=" + target;

                    Edge<string> edge = null;
                    try
                    {
                        edge = new Edge<string>(source, target);
                        Assert.IsTrue(source != null && target != null, "Wrong situation [" + signature + "] for no exception.");
                        Assert.IsNotNull(edge, "Without exception the edge should be not null. @signature: " + signature);
                    }
                    catch (ArgumentNullException)
                    {
                        Assert.IsTrue(source == null || target == null, "Wrong situation [" + signature + "] for this exception.");
                        Assert.IsNull(edge, "With this exception the edge should not be created anyway. @signature: " + signature);
                    }
                    catch (Exception ex)
                    {
                        Assert.Fail("Unexpected type '" + ex.GetType().ToString() + "' of exception. @signature: " + signature);
                    }
                }
            }
        }
    }
}
