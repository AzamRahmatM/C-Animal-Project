using TextFile;
namespace animals1{



class Program
{
    static void Main(string[] args)
    {
        string inputFilePath = "input.txt";

        // Create a Tundra object with the input file
        Tundra tundra = new Tundra(inputFilePath);

        // Simulate the process
        tundra.Simulate();

        
        //Console.WriteLine(tundra.AttackingPredator());

  

        




    }

}
}

