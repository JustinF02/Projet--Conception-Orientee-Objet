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
                            if (j == 0 || Matrice[i, j - 1] >= 8) //Toutes les conditions pour que l'unité ai une frontière à l'ouest
                            {
                                if (Matrice[i, j] % 2 == 1)
                                {
                                    int increment = 0;
                                    bool parcelleTrouve = false;
                                    while ((!parcelleTrouve) && (j + increment < 9) && Matrice[i, j + increment] % 2 == 1 && (Matrice[i, (j + increment)] < 8))
                                    {
                                        increment++;
                                        if ((Matrice[i, (j + increment) - 1] < 8) && (Matrice[i, j + increment] % 2 == 0))
                                            parcelleTrouve = true;
                                    }
                                    if (parcelleTrouve == false)
                                    {
                                        Matrice_Traduit[i, j] = Convert.ToChar('a' + nbparcelles);
                                        nbparcelles++;
                                    }
                                    else Matrice_Traduit[i, j] = Matrice_Traduit[i - 1, j + increment];

                                }
                                else Matrice_Traduit[i, j] = Matrice_Traduit[i - 1, j];
                            }
                            else Matrice_Traduit[i, j] = Matrice_Traduit[i, j - 1];
                        }
                    }
                }
            }
            return Matrice_Traduit;
        }
        public static void AfficheChar(char[,] Matrice)
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

        public static void AfficheInt(int[,] Matrice)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write($"{Convert.ToString(Matrice[i, j]).PadLeft(3)} ");
                }
                Console.Write("\n");
            }
        }
        public static int[,] Chiffrage(char[,] Matrice)
        {
            int[,] Matrice_Decode = new int[10, 10];

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i == 0 || Matrice[i, j] != Matrice[i - 1, j])
                    {
                        Matrice_Decode[i, j] += 1;
                        if (i > 0)
                            Matrice_Decode[i - 1, j] += 4;
                    }

                    if (j == 0 || Matrice[i, j] != Matrice[i, j - 1])
                    {
                        Matrice_Decode[i, j] += 2;
                        if (j > 0)
                            Matrice_Decode[i, j - 1] += 8;
                    }

                    if (i == 9)
                        Matrice_Decode[i, j] += 4;

                    if (j == 9)
                        Matrice_Decode[i, j] += 8;

                    switch (Matrice[i, j])
                    {
                        case 'M':
                            Matrice_Decode[i, j] = Matrice_Decode[i, j] + 64;
                            break;
                        case 'F':
                            Matrice_Decode[i, j] = Matrice_Decode[i, j] + 32;
                            break;
                    }
                }
            }
            return Matrice_Decode;
        }
        public static string Convert_To_Trame(int[,] Matrice)
        {
            string trame = "";
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++) trame += (j == 0) ? $"{Matrice[i, j]}" : $":{Matrice[i, j]}";
                trame += "|";
            return trame;
        }
        static void Main(string[] args)
        {
            string Fichier = @"C:\Users\admin\Desktop\Projet-CSI-M2104-master\Algo Déchiffrage Trame\trametest.chiffre"; //adresse fichier avec @;

            string trame = Trame(Fichier);

            string[,] Matrice_Trame = OrganiseTrame(trame);
            int[,] Matrice_Convertit = Convertit(Matrice_Trame);
            char[,] Matrice_Carte = Traduit(Matrice_Convertit);
            AfficheChar(Matrice_Carte);
            int[,] Matrice_Chiffre = Chiffrage(Matrice_Carte);
            AfficheInt(Matrice_Chiffre);

            string trame_out = Convert_To_Trame(Matrice_Chiffre);
            Console.WriteLine(trame_out);
        }
    }
}