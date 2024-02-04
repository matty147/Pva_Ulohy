using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ciselne_palindromy
{
    internal class Program
    {

        public static int nextPalindrome(long from, int radix, ref long next)
        {
            if (2 > radix || radix > 36)
            {
                return 0;  // error
            }

            for (long i = from; ;i++)
            {
                if (IsPalindromic(i, radix))
                {
                    next = i;
                    return 1;
                }
            }
        }

        public static string ConvertToBase(long number, int radix)
        {
            if (radix < 2 || radix > 36)
            {
                throw new ArgumentException("Radix must be between 2 and 36.");
            }

            string result = "";

            while (number > 0)
            {
                int remainder = (int)(number % radix);
                char digit = (char)(remainder < 10 ? remainder + '0' : remainder - 10 + 'a');
                result = digit + result;
                number /= radix;
            }

            return result == "" ? "0" : result;
        }


        static void printNextPalindrome(long from, int radix)
        {
            Console.Write($"{from}/{radix} > ");
            long next = 0;
            long tmp = nextPalindrome(from, radix, ref next);
            if (tmp == 0)
            {
                Console.WriteLine("Error");
            }
            else
            {
                Console.WriteLine(next);
            }

        }

        public static bool IsPalindromic(long numb, int radix)
        {
            string numberInBase = ConvertToBase(numb, radix);
            char[] digits = numberInBase.ToCharArray();
            return digits.SequenceEqual(digits.Reverse());
        }


        static void Main(string[] args)
        {


            printNextPalindrome(15,5);
            printNextPalindrome(16,2);
            printNextPalindrome(181,10);
            printNextPalindrome(181,16);

            Console.ReadKey();
        }
    }
}
