using System;

namespace Homewrok1ex2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int a = int.MaxValue;
            Console.WriteLine(unchecked(a + 1));

            long b = long.MaxValue;
            Console.WriteLine(unchecked(b + 1));

            string str = "1234567";
            string str1 = "25478";

            Add_char(str, str1);

            Console.ReadKey();
        }

        static void Add_char(string a, string b)
        {
            if (a == null || b == null)
            {
                Console.WriteLine("NULL strings");
                return;
            }

            int lenght;

            if (a.Length >= b.Length)
            {
                lenght = a.Length;
            }
            else
            {
                lenght = b.Length;
            }

            string result = "";

            int carry = 0;

            int index_a = a.Length - 1;
            int index_b = b.Length - 1;

            for (int i = lenght; i > 0; i--)
            {
                int sum = 0;

                int x_1 = 0;
                int x_2 = 0;

                if (index_a >= 0)
                {
                    char c_1 = a[index_a];
                    x_1 = Convert_char_int(c_1);
                }

                if (index_b >= 0)
                {
                    char c_2 = b[index_b];
                    x_2 = Convert_char_int(c_2);
                }

                sum = x_1 + x_2;

                if (carry == 1)
                {
                    sum += carry;
                    carry = 0;
                }

                if (sum > 9)
                {
                    sum -= 10;
                    carry = 1;
                }

                char c_3 = (char)(sum + '0');

                result = c_3 + result;

                index_a--;
                index_b--;
            }

            if (carry == 1)
            {
                result = '1' + result;
            }

            Console.WriteLine(result);
        }

        static int Convert_char_int(char c)
        {
            int x = c - '0';
            return x;
        }
    }
}