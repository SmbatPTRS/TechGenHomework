using System;

namespace HomeWorkTechGen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter a float number: ");

            string input = Console.ReadLine();
            float number = float.Parse(input);

            int sign;

            if (number < 0)
            {
                sign = 1;
            }
            else
            {
                sign = 0;
            }

            number = absolut(number);

            int integerPart = SplitNumber(number);

            float fractionalPart = number - integerPart;

            string integerBinary = IntToBinary(integerPart);

            string fractionBinary = FractionToBinary(fractionalPart, 30);

            Console.WriteLine("Integer Binary: " + integerBinary);
            Console.WriteLine("Fraction Binary: " + fractionBinary);

            string combinedBinary = integerBinary + "." + fractionBinary;

            Console.WriteLine("Combined Binary: " + combinedBinary);

            int exponent = integerBinary.Length - 1;

            int storedExponent = exponent + 127;

            string exponentBinary = IntToBinary(storedExponent);

            while (exponentBinary.Length < 8)
            {
                exponentBinary = "0" + exponentBinary;
            }

            string mantissa = integerBinary.Substring(1) + fractionBinary;

            while (mantissa.Length < 23)
            {
                mantissa += "0";
            }

            if (mantissa.Length > 23)
            {
                mantissa = mantissa.Substring(0, 23);
            }

            string finalBinary = sign + exponentBinary + mantissa;

            Console.WriteLine("IEEE 754 32-bit Representation:");
            Console.WriteLine(finalBinary);
        }

        public static float absolut(float num)
        {
            if (num < 0)
            {
                return 0 - num;
            }
            else
            {
                return num;
            }
        }

        public static int SplitNumber(float num)
        {
            int integerPart = 0;

            while (num >= 1)
            {
                num--;
                integerPart++;
            }

            return integerPart;
        }

        public static string IntToBinary(int num)
        {
            if (num == 0)
            {
                return "0";
            }

            string binary = "";

            while (num > 0)
            {
                int remainder = num % 2;

                binary = remainder + binary;

                num = num / 2;
            }

            return binary;
        }

        public static string FractionToBinary(float fraction, int maxBits)
        {
            string binary = "";

            int count = 0;

            while (fraction > 0 && count < maxBits)
            {
                fraction = fraction * 2;

                if (fraction >= 1)
                {
                    binary += "1";
                    fraction = fraction - 1;
                }
                else
                {
                    binary += "0";
                }

                count++;
            }

            return binary;
        }
    }
}
