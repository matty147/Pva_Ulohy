using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uloha_1___Instalateri
{
	public class Point
	{
		public int X { get;}
		public int Y { get;}
		public int Z { get;}

		public Point(int x, int y, int z)
		{
			X = x;
			Y = y;
			Z = z;
		}



		public override string ToString()
		{
			return $"X={X}, Y={Y}, Z={Z}";
		}

		public Point2D getPointOnSide(int roomsize)
		{
			if (X == 0 || X == roomsize)
			{
				return new Point2D(Y, Z);
			}
			if (Y == 0 || Y == roomsize)
			{
				return new Point2D(X, Z);
			}
			if (Z == 0 || Z == roomsize)
			{
				return new Point2D(X, Y);
			}
			throw new ArgumentException($"Wrong roomsize ({roomsize})");
		}

	}

	public class Point2D
	{
		public int X { get; }
		public int Y { get; }

		public Point2D(int x, int y)
		{
			X = x;
			Y = y;
		}

		public override string ToString()
		{
			return $"X={X}, Y={Y}";
		}

	}



	public enum Side
	{
		Front,
		Back,
		Top,
		Botom,
		Right,
		Left,
	}


	internal class Program
	{

		static Side whatFaceIsThePointOn(int wallSize, Point point)
		{
			if (wallSize == point.X)
			{
				return Side.Front;
			}
			else if (0 == point.X)
			{
				return Side.Back;
			}
			else if (wallSize == point.Y)
			{
				return Side.Top;
			}
			else if (0 == point.Y)
			{
				return Side.Botom;
			}
			else if (wallSize == point.Z)
			{
				return Side.Right;
			}
			else if (0 == point.Z)
			{
				return Side.Left;
			}
			throw new ArgumentException($"The point {point} is not on any side ");
		}

		static (int, Point, Point) ParseInput(string input)
		{
			string[] splitInput = input.Split(',');

			int[] parsedValues = new int[splitInput.Length];

			for (int i = 0; i < splitInput.Length; i++)
			{
				if (!int.TryParse(splitInput[i], out parsedValues[i]))
				{
					// Handle the case where parsing fails, return an error, or use a default value
					// For now, let's use 0 as a default value
					parsedValues[i] = 0;
				}
			}


			return (parsedValues[0], new Point(parsedValues[1], parsedValues[2], parsedValues[3]), new Point(parsedValues[4], parsedValues[5], parsedValues[6]));
		}

		static int calculatePipeLength(int roomsize,Point point1,Point point2)
		{
			return 5;
		}

		static int calculateHoseLength(int roomsize, Point point1, Point point2)
		{
			return 5;
		}

		static string type(int roomsize, Point point1, Point point2)
		{
			if (point1.X == point2.X || point1.Y == point2.Y || point1.Z == point1.Z)
			{
				return "Same face";
			}else if (Math.Abs(point1.X - roomsize) == point2.X || Math.Abs(point1.Y - roomsize) == point2.Y || Math.Abs(point1.Z - roomsize) == point2.Z)
			{
				return "Oposite face";
			}else
			{
				return "neighbor face";
			}
		}


		static void Main(string[] args)
		{
			Console.Write("a: ");
			string inputString = Console.ReadLine();

			(int roomsize, Point point1, Point point2) = ParseInput(inputString);


			float pipeLen = calculatePipeLength(roomsize, point1, point2);
			float hoseLen = calculateHoseLength(roomsize, point1, point2);


			Console.WriteLine("roomsize: " + roomsize);

			Console.WriteLine($"Point 1:  {point1}");
			Console.WriteLine($"Point 2:  {point2}");

			Console.WriteLine($"pipeLength: {pipeLen}");
			Console.WriteLine($"hoseLenght: {hoseLen}");

			Console.ReadKey();
		}
	}
}
