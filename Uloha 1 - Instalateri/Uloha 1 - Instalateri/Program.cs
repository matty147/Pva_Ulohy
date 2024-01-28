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

	/** Relative position of the walls with the two points */
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

		bool IsWall(int a)
		{
			return a == 0 || a == Size;
		}


		bool SameWall(int a, int b)
		{
			return IsWall(a) && a == b;
		}

		bool OppositeWall(int a, int b)
		{
			return IsWall(a) && IsWall(b) && a != b;
		}

		/* Project a point on the 2D wall it is on. */
		public Point2D project(Point point)
		{
			if (IsWall(point.X))
			{
				return new Point2D(point.Y, point.Z);
			}
			if (IsWall(point.Y))
			{
				return new Point2D(point.X, point.Z);
			}

			return new Point2D(point.X, point.Y);
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
					Point2D p1 = project(point1);
					Point2D p2 = project(point2);

					return Math.Min(
						Math.Min(p1.X + p2.X, (Size - p1.X) + (Size - p2.X)) + Distance(p1.Y, p2.Y),
						Math.Min(p1.Y + p2.Y, (Size - p1.Y) + (Size - p2.Y)) + Distance(p1.X, p2.X)
					) + Size;

				default:
					throw new InvalidOperationException();
			}
		}

		public double calculateHoseLength(Point point1, Point point2)
		{
			switch (GetRelativePosition(point1, point2))
			{
				case RelativePosition.Same:
					return Math.Sqrt(	// pythagoras theorem (one of the items is zero)
						Math.Pow(Distance(point1.X, point2.X), 2) +
						Math.Pow(Distance(point1.Y, point2.Y), 2) +
						Math.Pow(Distance(point1.Z, point2.Z), 2)
					);
				case RelativePosition.Adjacent:
					int dx = Distance(point1.X, point2.X);
					int dy = Distance(point1.Y, point2.Y);
					int dz = Distance(point1.Z, point2.Z);

					// there is one shared dimension for point1 and point2;
					// and two other two dimensions, for each point one of them is on the wall
					if (IsWall(point1.X))
					{
						if (IsWall(point2.Y))
						{
							return pythagorastheorem(dx + dy, dz);
						}
						else
						{
							return pythagorastheorem(dx + dz, dy);
						}
					}
					if (IsWall(point1.Y))
					{
						if (IsWall(point2.X))
						{
							return pythagorastheorem(dx + dy, dz);
						}
						else
						{
							return pythagorastheorem(dy + dz, dx);
						}
					}
					if (IsWall(point1.Z))
					{
						if (IsWall(point2.X))
						{
							return pythagorastheorem(dx + dz, dy);
						}
						else
						{
							return pythagorastheorem(dy + dz, dx);
						}
					}
					throw new InvalidOperationException();

				case RelativePosition.Opositte:
					// try all four directions (right, left, up, down)
					return Math.Min(
								Math.Min(
									pythagorastheorem(Distance(point1.Y, point2.Y), (Size - point1.X) + Size + (Size - point2.X)),
									pythagorastheorem(Distance(point1.Y, point2.Y), point1.X + Size + point2.X)
								),
								Math.Min(
									pythagorastheorem(Distance(point1.X, point2.X), (Size - point1.Y) + Size + (Size - point2.Y)),
									pythagorastheorem(Distance(point1.X, point2.X), point1.Y + Size + point2.Y)
								)
							);
				default:
					throw new InvalidOperationException();
			}
		}

		static double pythagorastheorem(int a, int b)
		{
			return Math.Sqrt(Math.Pow(a,2)+ Math.Pow(b,2));
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
			checkArgument(40 < roomSize, "Room size must be at least 40.");

			Point point1 = new Point(parsedValues[1], parsedValues[2], parsedValues[3]);
			Point point2 = new Point(parsedValues[4], parsedValues[5], parsedValues[6]);

			// within range
			for (int i = 1; i < splitInput.Length; i++)
			{
				int number = parsedValues[i];
				checkArgument(
					number == 0 || number == roomSize || (20 <= number && number <= roomSize - 20),
					$"Point {parsedValues[i]} out of range (20 .. {roomSize-20})."
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
			Console.WriteLine("Enter 7 integers; size of the room, and the coordinates of two points. Use the comma as a separator.");
			string inputString = Console.ReadLine();

			int roomsize; Point point1; Point point2;
			try {
				(roomsize, point1, point2) = ParseInput(inputString);
			}
			catch (Exception ex) {
				Console.WriteLine($"ERROR: {ex.Message}");
				Console.ReadKey();
				Environment.Exit(0);
				return;
			}

			Room room = new Room(roomsize);
			int pipeLen = room.calculatePipeLength(point1, point2);
			double hoseLen = room.calculateHoseLength(point1, point2);


			Console.WriteLine($"Roomsize:   {roomsize}");
			Console.WriteLine($"Point 1:    {point1}");
			Console.WriteLine($"Point 2:    {point2}");
			Console.WriteLine($"PipeLength: {pipeLen}");
			Console.WriteLine($"HoseLenght: {hoseLen}");

		 
		}
	}
}
