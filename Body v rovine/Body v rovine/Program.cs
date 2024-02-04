using System;
using System.Collections.Generic;

namespace Body_v_rovině
{
    internal class Program
    {
        static bool AreCoincident(List<float> point1, List<float> point2)
        {
            return point1[0] == point2[0] && point1[1] == point2[1];
        }

        static void Main(string[] args)
        {
            double tolerance = 0.01;

            List<float> PointA = new List<float>();
            List<float> PointB = new List<float>();
            List<float> PointC = new List<float>();

            List<string> AllPoints = new List<string>();

            for (; ; )
            {
                string inputLine = Console.ReadLine();
                string[] numbers = inputLine.Split(',');
                AllPoints.AddRange(numbers);
                if (AllPoints.Count == 6)
                {
                    float.TryParse(AllPoints[0], out float number);
                    PointA.Add(number);
                    float.TryParse(AllPoints[1], out number);
                    PointA.Add(number);
                    float.TryParse(AllPoints[2], out number);
                    PointB.Add(number);
                    float.TryParse(AllPoints[3], out number);
                    PointB.Add(number);
                    float.TryParse(AllPoints[4], out number);
                    PointC.Add(number);
                    float.TryParse(AllPoints[5], out number);
                    PointC.Add(number);
                    break;
                }
                else Console.WriteLine("Invalid count of numbers");
            }

            if (AreCoincident(PointA, PointB) || AreCoincident(PointA, PointC) || AreCoincident(PointB, PointC))
            {
                Console.WriteLine("Two or more points are coincident!");
            }
            else
            {
                char middlePointName = FindMiddlePoint(PointA, PointB, PointC);
                Console.WriteLine("The middle point is: " + middlePointName);

                float vectorABx = PointB[0] - PointA[0];
                float vectorABy = PointB[1] - PointA[1];
                float vectorBCx = PointC[0] - PointB[0];
                float vectorBCy = PointC[1] - PointB[1];

                if (Math.Abs(vectorABx * vectorBCy - vectorABy * vectorBCx) < tolerance)
                {
                    Console.WriteLine("The points are in line!!!");
                }
                else
                {
                    Console.WriteLine("The points are not in line!!!");
                }
            }

            Console.ReadKey();
        }

        // Function to find the middle point among three points and return its name
        static char FindMiddlePoint(List<float> point1, List<float> point2, List<float> point3)
        {
            char middlePointName = ' ';

            if ((point1[0] == point2[0] && point1[1] == point2[1]) || (point1[0] == point3[0] && point1[1] == point3[1]))
            {
                middlePointName = 'C';
            }
            else if (point2[0] == point3[0] && point2[1] == point3[1])
            {
                middlePointName = 'A';
            }
            else
            {   
                middlePointName = 'B';
            }

            return middlePointName;
        }
    }
}
