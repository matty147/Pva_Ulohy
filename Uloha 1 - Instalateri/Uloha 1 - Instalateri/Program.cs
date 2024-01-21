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

		public override bool Equals(object obj)
		{
			return obj is Point point &&
				   X == point.X &&
				   Y == point.Y &&
				   Z == point.Z;
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

	public enum RelativePosition
	{
		Same,
		Adjacent,
		Opositte
	}

	public class Room
	{
		public int Size { get; }

		public Room(int size)
		{
			Size = size;
		}

		static int Distance(int a, int b)
		{
			return Math.Abs(a - b);
		}

		bool SameWall(int a, int b)
		{
			return a == b && (a == 0 || a == Size);
		}

		bool OppositeWall(int a, int b)
		{
			return Math.Abs(a - b) == Size;
		}

		Side whatFaceIsThePointOn(Point point)
		{
			if (Size == point.X)
			{
				return Side.Front;
			}
			else if (0 == point.X)
			{
				return Side.Back;
			}
			else if (Size == point.Y)
			{
				return Side.Top;
			}
			else if (0 == point.Y)
			{
				return Side.Botom;
			}
			else if (Size == point.Z)
			{
				return Side.Right;
			}
			else if (0 == point.Z)
			{
				return Side.Left;
			}
			throw new ArgumentException($"The point {point} is not on any side ");
		}

		public RelativePosition GetRelativePosition(Point point1, Point point2)
		{
			if (SameWall(point1.X, point2.X) || SameWall(point1.Y, point2.Y) || SameWall(point1.Z, point2.Z))
			{
				return RelativePosition.Same;
			}
			else if (OppositeWall(point1.X, point2.X) || OppositeWall(point1.Y, point2.Y) || OppositeWall(point1.Z, point2.Z))
			{
				return RelativePosition.Opositte;
			}
			else
			{
				return RelativePosition.Adjacent;
			}
		}

		public int calculatePipeLength(Point point1, Point point2)
		{
			switch (GetRelativePosition(point1, point2))
			{
				case RelativePosition.Same:
				case RelativePosition.Adjacent:
					return Distance(point1.X, point2.X) + Distance(point1.Y, point2.Y) + Distance(point1.Z, point2.Z);
				case RelativePosition.Opositte:
					return -3;
				default:
					throw new InvalidOperationException();
			}
		}

		public double calculateHoseLength(Point point1, Point point2)
		{
			switch (GetRelativePosition(point1, point2))
			{
				case RelativePosition.Same:
					return Math.Sqrt(
						Math.Pow(Distance(point1.X, point2.X), 2) +
						Math.Pow(Distance(point1.Y, point2.Y), 2) +
						Math.Pow(Distance(point1.Z, point2.Z), 2)
					);
				case RelativePosition.Adjacent:
					return -2;
				case RelativePosition.Opositte:
					return -3;
				default:
					throw new InvalidOperationException();
			}
		}


	}



	internal class Program
	{

		public static bool ExactlyOneTrue(params bool[] conditions)
		{
			return conditions.Count(c => c) == 1;
		}

		private static void checkArgument(bool test, string message)
		{
			if (!test) throw new ArgumentException(message);
		}

		public static (int, Point, Point) ParseInput(string input)
		{
			string[] splitInput = input.Split(',');
			checkArgument(splitInput.Length == 7, "Expecting 7 integers.");

			int[] parsedValues = new int[splitInput.Length];

			for (int i = 0; i < splitInput.Length; i++)
			{
				if (!int.TryParse(splitInput[i], out parsedValues[i]))
				{
					checkArgument(false, $"Item #{i} ({splitInput[i]}) is not an integer.");
				}
			}

			int roomSize = parsedValues[0];
			checkArgument(0 < roomSize, "Room size must be positive.");

			Point point1 = new Point(parsedValues[1], parsedValues[2], parsedValues[3]);
			Point point2 = new Point(parsedValues[4], parsedValues[5], parsedValues[6]);

			// within range
			for (int i = 1; i < splitInput.Length; i++)
			{
				int number = parsedValues[i];
				checkArgument(
					number == 0 || number == roomSize || (20 <= number && number <= roomSize - 20),
					$"Point {parsedValues[i]} out of range."
				);
			}

			// on one wall
			checkArgument(ExactlyOneTrue(
				point1.X == 0 || point1.X == roomSize,
				point1.Y == 0 || point1.Y == roomSize,
				point1.Z == 0 || point1.Z == roomSize),
				"The point must be on exactly one wall"
			);

			// not outside of the room

			return (roomSize, point1, point2);
		}


		static void Main(string[] args)
		{
			Console.Write("a: ");
			string inputString = Console.ReadLine();

			(int roomsize, Point point1, Point point2) = ParseInput(inputString);

			Room room = new Room(roomsize);
			int pipeLen = room.calculatePipeLength(point1, point2);
			double hoseLen = room.calculateHoseLength(point1, point2);


			Console.WriteLine("roomsize: " + roomsize);

			Console.WriteLine($"Point 1:  {point1}");
			Console.WriteLine($"Point 2:  {point2}");

			Console.WriteLine($"pipeLength: {pipeLen}");
			Console.WriteLine($"hoseLenght: {hoseLen}");

			Console.ReadKey();
		}
	}
}
