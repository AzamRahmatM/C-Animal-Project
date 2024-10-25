using System;
using System.Collections.Generic;
using System.IO;

namespace animals1{
public class Tundra
{
    public List<Colony> Colonies { get;}
    private int turn = 1;
    private Random random = new Random();

    public Tundra(string inputFilePath)
    {
        try
        {
            Colonies = CreateColoniesFromFile(inputFilePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error initializing Tundra: {ex.Message}");
            throw; // Rethrow to handle it in the Main method
        }
    }

    private List<Colony> CreateColoniesFromFile(string inputFilePath)
    {
        try
        {
            using (var reader = new StreamReader(inputFilePath))
            {
                int m = 0;
                int n = 0;
                string firstLine = reader.ReadLine();
                if (firstLine != null)
                {
                    string[] firstLineData = firstLine.Split(' ');
                    m = int.Parse(firstLineData[0]);
                    n = int.Parse(firstLineData[1]);
                }

                List<Colony> colonies = new List<Colony>();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (string.IsNullOrEmpty(line))
                    {
                        continue;
                    }

                    string[] data = line.Split(' ');

                    if (data.Length < 3)
                    {
                        Console.WriteLine("Error: Invalid data format in line: {0}", line);
                        continue;
                    }

                    string name = data[0];
                    char specie = char.Parse(data[1]);
                    int num = int.Parse(data[2]);

                    switch (specie)
                    {
                        case 'l':
                            colonies.Add(new Lemming(name, num));
                            break;
                        case 'h':
                            colonies.Add(new Hare(name, num));
                            break;
                        case 'g':
                            colonies.Add(new Gopher(name, num));
                            break;
                        case 'f':
                            colonies.Add(new Fox(name, num));
                            break;
                        case 'w':
                            colonies.Add(new Wolf(name, num));
                            break;
                        case 'o':
                            colonies.Add(new Owl(name, num));
                            break;
                        default:
                            Console.WriteLine("Error: Unknown specie in line: {0}", line);
                            break;
                    }
                }

                int totalColonies = m + n;
                if (colonies.Count != totalColonies)
                {
                    Console.WriteLine("Error: Inconsistency in the number of colonies in the input file.");
                }
                else
                {
                    Console.WriteLine("Successfully processed " + totalColonies + " colonies.");
                }

                return colonies;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading colonies from file: {ex.Message}");
            throw;
        }
    }

    public void Simulate()
    {
        try
        {
            while (true)
            {
                Console.WriteLine($"Turn {turn}");

                // Update prey populations based on turn and predator interactions
                foreach (Colony prey in Colonies)
                {
                    if (prey.IsPrey())
                    {
                        List<Colony> predators = new List<Colony>();

                        foreach (Colony predator in Colonies)
                        {
                            if (predator.IsPredator() && predator.num > 0)
                            {
                                predators.Add(predator);
                            }
                        }

                        if (predators.Count > 0)
                        {
                            Colony randomPredator = predators[random.Next(predators.Count)];
                            prey.UpdatePopulation(turn, randomPredator.num);

                            // Check if prey population goes negative
                            if (prey.num < 0)
                            {
                                prey.num = 0;
                            }
                        }
                    }
                }

                // Update predator populations
                foreach (Colony predator in Colonies)
                {
                    if (predator.IsPredator())
                    {
                        predator.UpdatePopulation(turn, 0);
                    }
                }

                PrintState();

                // Check end conditions
                if (PreyDead() || PreyQuadruples())
                {
                    Console.WriteLine("Simulation ends.");
                    break;
                }

                turn++;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during simulation: {ex.Message}");
        }
    }

    private bool PreyDead()
    {
        foreach (Colony prey in Colonies)
        {
            if (prey.IsPrey() && prey.num > 0)
                return false;
        }
        return true;
    }

    private bool PreyQuadruples()
    {
        foreach (Colony prey in Colonies)
        {
            if (prey.IsPrey() && prey.num >= prey.StartingNum * 4)
                return true;
        }
        return false;
    }

    private void PrintState()
    {
        Console.WriteLine($"Turn {turn}");
        foreach (Colony colony in Colonies)
        {
            Console.WriteLine($"Colony: {colony.name}, specie: {colony.specie}, Population: {colony.num}");
        }
        Console.WriteLine();
    }
}
}