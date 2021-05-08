using System;

namespace Projet
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            int[] valeur = { 2, 3 };
            bool MurDeLOuest(int[,] tab)
            {

                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {

                        if (Array.Exists(valeur, element => element == Matrice[i, j])) return true;
                    }
                }
                return false;
            }
        }
    }
}
