using System;
using System.IO;

namespace Algo_Déchiffrage_Trame
{
    class Program
    {
        public static string Trame(string Chemin_Fichier)
        {
            string[] Lignes_Fichier = File.ReadAllLines(Chemin_Fichier);
            return Lignes_Fichier[0];
        }
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
            int nbparcelles = 0;
            char[,] Matrice_Traduit = new char[10, 10];

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (Matrice[i, j] >= 65 && Matrice[i, j] <= 79) Matrice_Traduit[i, j] = 'M';
                    else
                    {
                        if (Matrice[i, j] >= 33 && Matrice[i, j] <= 47) Matrice_Traduit[i, j] = 'F';
                        else
                        {
                            if (FrontiereOuest(Matrice[i, j]))
                            {
                                if ((Matrice[i, j] % 2) == 1)
                                {
                                    Matrice_Traduit[i, j] = Convert.ToChar('a' + nbparcelles);
                                    nbparcelles++;
                                }
                                else
                                {
                                    Matrice_Traduit[i, j] = Matrice_Traduit[i - 1, j];
                                }

                            }
                            else
                            {
                                Matrice_Traduit[i, j] = Matrice_Traduit[i, j - 1];
                            }
                        }
                    }
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
        public static bool FrontiereOuest(int code)
        {
            int[] valeurs = { 2, 3, 6, 7, 10, 11, 14 };
            return Array.Exists(valeurs, element => element == code);
        }
        static void Main(string[] args)
        {
            string Fichier = ""; //adresse fichier avec @;

            string trame = Trame(Fichier);

            string[,] Matrice_Trame = OrganiseTrame(trame);
            int[,] Matrice_Convertit = Convertit(Matrice_Trame);
            char[,] Matrice_Carte = Traduit(Matrice_Convertit);
            Affiche(Matrice_Carte);
        }
    }
}
