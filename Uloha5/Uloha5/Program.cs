using System;
using System.Collections.Generic;

class MainClass
{

    public static void Main(string[] args)
    {
        Console.WriteLine("Enter your input (separate multiple inputs with spaces):");
        string input = Console.ReadLine();

        string[] inputLines = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        List<int> xCoordinates = new List<int>();
        List<int> yCoordinates = new List<int>();
        List<string> brands = new List<string>();

        foreach (string line in inputLines)
        {
            string[] separatedValues = line.Split(new char[] { ',', ':' });

            if (separatedValues.Length != 3)
            {
                Console.WriteLine($"Invalid input format: {line}");
                continue;
            }

            if (int.TryParse(separatedValues[0], out int x))
                xCoordinates.Add(x);

            if (int.TryParse(separatedValues[1], out int y))
                yCoordinates.Add(y);

            brands.Add(separatedValues[2].Trim());
        }

        for (int i = 0; i+1 < brands.Count; i++)
        {
            for (int j = 0; j +1 < brands.Count; j++)
			{
                int A = Math.Abs(xCoordinates[i] - xCoordinates[i+1]);
                int B = Math.Abs(xCoordinates[j] - xCoordinates[j+1]);
			}
        }

        Console.ReadKey();
    }
}
