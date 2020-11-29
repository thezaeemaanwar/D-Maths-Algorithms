using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discrete_Maths_Algos
{
    class Program
    {
         /// <summary>
         /// Prime Factors Method.
         /// </summary>
         /// <param name="n">The Number whose factors are to be calculated</param>
         /// <returns>
         ///    List of Factors
         /// </returns>
        static List<int> PrimeFactors(long n)
        {
            List<int> factorList = new List<int>();
            while (n != 1)
            {
                for (int i = 2; i<=n; i++)
                {
                    if (n % i == 0)
                    {
                        factorList.Add(i);
                        n /=  i;
                        break;
                    }
                }
            }
            return factorList;
        }

        /// <summary>
        /// Extended Euclidean Algorithm
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>
        ///     A vector [gcd(a,b), x, y] such that ax + by = gcd(a, b) 
        ///     [-1,-1,-1] if solution does not exist
        /// </returns>
        static List<int> ExtEucAlg(int a, int b)
        {
            // handling extreme conditions
            if (a == 0 || b == 0)
            {
                return new[] { -1, -1, -1 }.OfType<int>().ToList();
            }
            // Record keeping Vectors
            List<int> U = new[] { a, 1, 0 }.OfType<int>().ToList();
            List<int> V = new[] { b, 0, 1 }.OfType<int>().ToList();
            List<int> W = new[] { 0, 0, 0 }.OfType<int>().ToList();
           
            while (V[0] > 0)
            {
                W[0] = U[0] - (U[0] / V[0]) * V[0];
                W[1] = U[1] - (U[0] / V[0]) * V[1];
                W[2] = U[2] - (U[0] / V[0]) * V[2];

                // Update U = V
                U[0] = V[0];
                U[1] = V[1];
                U[2] = V[2];

                // Update V = W
                V[0] = W[0];
                V[1] = W[1];
                V[2] = W[2];

            }
            return U;

        }

        /// <summary>
        /// RSA Encryption Algortihm
        /// </summary>
        /// <param name="P">the plain text (an integer mod n)</param>
        /// <param name="e">the encryption exponent</param>
        /// <param name="n">the RSA mdulus</param>
        /// <returns>
        ///     A mod n integer representing the ciphertext
        /// </returns>
        static long RSAEncrypt(int P, int e, long n)
        {
            if (n == 0)
            {
                return 0;
            }
            long cPower = P % n;
            long C = 1;
            while (e > 0)
            {
                if (e % 2 == 1)
                {
                    C = (C * cPower) % n;
                }
                e /= 2;
                cPower = (cPower * cPower) % n;
            }
            return  C;
        }
        /// <summary>
        /// Generates the Private key for RSA decryption
        /// where public exponent and phi are given
        /// </summary>
        /// <param name="e"></param>
        /// <param name="n"></param>
        /// <returns>
        ///     The public exponent d
        /// </returns>
        static long PrivateKey(int e, long n)
        {
            long d = 1;
            long num = 1;
            while ((d*e)%n != 1)
            {
                while((num%e) != 0)
                {
                    num += n;
                    Console.WriteLine("num = " + (num%e).ToString());
                }
                d = num / e;
                Console.WriteLine("d = "+d.ToString());
            }
            Console.WriteLine("Out of while 2");
            return d;
        }
        /// <summary>
        /// RSA Decryption Algorithm
        /// </summary>
        /// <param name="C"></param>
        /// <param name="d"></param>
        /// <param name="n"></param>
        /// <returns>
        ///     A mod n integer representing the plaintext
        /// </returns>
        static long RSADecrypt(int C, long d, long n)
        {
            if (n == 0)
            {
                return 0;
            }
            long cPower = C % n;
            long P = 1;
            while (d > 0)
            {
                if (d % 2 == 1)
                {
                    P = (P * cPower) % n;
                }
                d /= 2;
                cPower = (cPower * cPower) % n;
            }
            return P;
        }
        /// <summary>
        /// Contains the menu options and selection feature
        /// </summary>
        /// <returns>
        ///     The choice(int) selected by user.
        /// </returns>
        static int Menu()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("\t1. Prime Factorisation");
            Console.WriteLine("\t2. Extended Eucliden Algorithm");
            Console.WriteLine("\t3. RSA Encryptiom");
            Console.WriteLine("\t4. RSA Decryption");
            Console.WriteLine("\t5. Exit Menu");
            Console.WriteLine("\n\tChoose an option : ");
            int n  = Convert.ToInt32(Console.ReadLine());
            if (n <= 0 || n >= 6)
            {
                Console.WriteLine("Enter from the available choices only");
                n = Menu();
            }
            Console.WriteLine("========================================");
            return n;
        }
        static void Main()
        {
            int choice = 0;
            while (choice < 5)
            {
                choice = Menu();
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("=======  PRIME FACTORISATION ========");
                        Console.WriteLine("Enter number to find prime factors : ");
                        long num = Convert.ToInt64( Console.ReadLine());
                        List<int> myFactors = PrimeFactors(num);
                        Console.WriteLine("[ " + string.Join(", ", myFactors)+" ]");
                        break;
                    case 2:
                        Console.WriteLine("=======  EXTENDED EUCLIDEAN ========");
                        Console.WriteLine("Equation : ax + by = GCD (a,b)");
                        Console.WriteLine("\tEnter Value of a : ");
                        int a = Convert.ToInt32 (Console.ReadLine());
                        Console.WriteLine("\tEnter Value of b : ");
                        int b = Convert.ToInt32 (Console.ReadLine());

                        // if a < b , swap a and b
                        if (a < b)
                        {
                            Console.WriteLine("As a < b, a and b would be swapped to make a > b");
                            a += b;
                            b = a - b;
                            a -= b;
                        }

                        List<int> Eucledian = ExtEucAlg(a, b);
                        Console.WriteLine("GCD (" + a.ToString() + " ," + b.ToString() + ") = " + Eucledian[0].ToString());
                        Console.WriteLine("x = " + Eucledian[1].ToString());
                        Console.WriteLine("y = " + Eucledian[2].ToString());
                        break;
                    case 3:
                        Console.WriteLine("=======  RSA ENCRYPTION ========");
                        Console.WriteLine("\t1. Using RSA modulus");
                        Console.WriteLine("\t2. Using primes");
                        Console.WriteLine("\tEnter your choice : ");
                        int i = Convert.ToInt32( Console.ReadLine());

                        long modulus;

                        if (i == 1)
                        {
                            Console.WriteLine("\tEnter the RSA Modulus : ");
                            modulus = Convert.ToInt32(Console.ReadLine());
                        }
                        else if (i == 2)
                        {
                            Console.WriteLine("\tEnter the 1st prime : ");
                            int p = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("\tEnter the 2nd prime : ");
                            int q = Convert.ToInt32(Console.ReadLine());
                            modulus = p * q;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Choice");
                            break;
                        }
                        Console.WriteLine("\tEnter the plain text : ");
                        int plainText = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("\tEnter public encryption exponent : ");
                        int exp = Convert.ToInt32(Console.ReadLine());
                        long cipherText = RSAEncrypt(plainText, exp, modulus);
                        Console.WriteLine("The cipher text is : "+ cipherText.ToString());
                        break;
                    case 4:
                        Console.WriteLine("=======  RSA DECRYPTION ========");
                        Console.WriteLine("\tEnter Cipher Text : ");
                        int cText = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("\tEnter the RSA Modulus : ");
                        long mod = Convert.ToInt64(Console.ReadLine());
                        Console.WriteLine("\tEnter private decryption exponent : ");
                        long expi = Convert.ToInt64(Console.ReadLine());
                        long ptext = RSADecrypt(cText, expi, mod);
                        Console.WriteLine("The plain text is : " + ptext.ToString());
                        break;
                }
            }
            // to exit the program when user chooses to
            Environment.Exit(0);
        }
    }
}
 