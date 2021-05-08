using System;

namespace Algo_Déchiffrage_Trame
{
    class Program
    {
        public static string[,] OrganiseTrame(string trame)
        {
            string[] Decomposition_Colonne = trame.Split('|');

            string[,] Matrice = new string[10, 10];

            for (int i = 0; i < 10; i++)
            {
                string[] Decompostion_Ligne = Decomposition_Colonne[i].Split(':');
                for (int j = 0; j < 10; j++)
                {
                    Matrice[i, j] = Decompostion_Ligne[j];
                }
            }
            return Matrice;
        }
        public static int[,] Convertit(string[,] Matrice)
        {
            int[,] Matrice_Traduit = new int[10,10];

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Matrice_Traduit[i, j] = Convert.ToInt32(Matrice[i, j]);
                }
            }
            return Matrice_Traduit;
        }
        public static char[,] Traduit(int[,] Matrice)
        {
            char[,] Matrice_Traduit = new char[10, 10];

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (Matrice[i, j] <= 79 && Matrice[i, j] >= 65) Matrice_Traduit[i, j] = 'M';
                    //if (Matrice[i, j] <= 15 && Matrice[i, j] >= 1)
                    else
                        if (Matrice[i, j] <= 47 && Matrice[i, j] >= 33) Matrice_Traduit[i, j] = 'F'; 
                        else Matrice_Traduit[i, j] = '0';
                }
            }
            return Matrice_Traduit;
        }
        public static void Affiche(char[,] Matrice)
        {

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (Matrice[i, j] == 'M')
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("M ", Console.ForegroundColor);
                    }
                    else
                        if (Matrice[i, j] == 'F')
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("F ", Console.ForegroundColor);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write($"{Matrice[i, j]} ", Console.ForegroundColor);
                        }
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\n", Console.ForegroundColor);
            }
        }
        public static bool MurDeLOuest(int[,] Matrice)
        {
            int[] valeur = {1,2,3};
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (Array.Exists(valeur, element => element == Matrice[i, j]))
                    {
                        switch (Matrice[i,j]){
                            case 1: Console.WriteLine($"frontière Ouest !         i : {i} | j : {j}"); return true;
                            case 2: Console.WriteLine($"frontière Nord !          i : {i} | j : {j}"); return true;
                            default:Console.WriteLine($"frontière Nord et Ouest ! i : {i} | j : {j}"); return true;
                        }
                    }
                }
            }
            return false;
        }
        static void Main(string[] args)
        {
            string trame = "3:9:71:69:65:65:65:65:65:73|2:8:3:9:70:68:64:64:64:72|6:12:2:8:3:9:70:68:64:72|11:11:6:12:6:12:3:9:70:76|10:10:11:11:67:73:6:12:3:9|14:14:10:10:70:76:7:13:6:12|3:9:14:14:11:7:13:3:9:75|2:8:7:13:14:3:9:6:12:78|6:12:3:1:9:6:12:35:33:41|71:77:6:4:12:39:37:36:36:44|";

            string[,] Matrice_Trame = OrganiseTrame(trame);
            int[,] Matrice_Convertit = Convertit(Matrice_Trame);
            char[,] Matrice_Carte = Traduit(Matrice_Convertit);
            MurDeLOuest(Matrice_Convertit);
            Affiche(Matrice_Carte);
        }
    }
}
