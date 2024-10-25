using System.Collections.Generic;
using animals1;

namespace TundraSimulationTests
{
    [TestClass]
    public class TundraTests
    {
        [TestMethod]
        public void TestLemmingPopulationUpdate()
        {
            Lemming lemming = new Lemming("LemmingColony", 50);

            lemming.UpdatePopulation(2, 0); // Second turn, no predators
            //it doubles on every second turn

            Assert.AreEqual(100, lemming.num, "Lemming population should double on the second turn.");
        }

        [TestMethod]
        public void TestLemmingPopulationWithPredators()
        {
            Lemming lemming = new Lemming("LemmingColony", 50);

        lemming.UpdatePopulation(2, 5); // Second turn, 5 predators

        int expectedPopulation = (50 * 2) - (4 * 5);
        Console.WriteLine(expectedPopulation);
        Assert.AreEqual(expectedPopulation, lemming.num, "Lemming population should double then decrease by 4 times the number of predators.");
        }

        [TestMethod]
        public void TestTundraSimulation()
        {
            Tundra tundra = new Tundra("input.txt"); 

            tundra.Simulate();

            
            foreach (Colony colony in tundra.Colonies)
            {
                if (colony is Lemming)
                {
                    Assert.IsTrue(colony.num >= 0, "Lemming population should not be negative.");
                }
            }
        }

        [TestMethod]
        public void TestEdgeCaseEmptyFile()
        {
            Tundra tundra = new Tundra("emptyFile.txt"); // Path to an empty file

            tundra.Simulate();

            Assert.AreEqual(0, tundra.Colonies.Count, "Tundra should have no colonies with an empty input file.");
        }

        [TestMethod]
        public void TestEdgeCaseMaxPopulation()
        {
            Lemming lemming = new Lemming("LemmingColony", 199);

            lemming.UpdatePopulation(2, 0); // Second turn, no predators

            //the rule of less than 200

            Assert.AreEqual(30, lemming.num, "Lemming population should reset to 30 if it exceeds 200.");
        }
    }
}