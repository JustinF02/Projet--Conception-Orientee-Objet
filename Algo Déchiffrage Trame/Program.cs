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
                    try { if (Matrice[i - 1, j] != Matrice[i, j]) Matrice_Decode[i, j] = Matrice_Decode[i, j] + 1; }
                        catch (Exception) { if (i == 0) Matrice_Decode[i, j] = Matrice_Decode[i, j] + 1; }

                    try { if (Matrice[i, j - 1] != Matrice[i, j]) Matrice_Decode[i, j] = Matrice_Decode[i, j] + 2; }
                        catch (Exception) { if (j == 0) Matrice_Decode[i, j] = Matrice_Decode[i, j] + 2; }

                    try { if (Matrice[i + 1, j] != Matrice[i, j]) Matrice_Decode[i, j] = Matrice_Decode[i, j] + 4; }
                        catch (Exception) { if (i == 9) Matrice_Decode[i, j] = Matrice_Decode[i, j] + 4; }

                    try { if (Matrice[i, j + 1] != Matrice[i, j]) Matrice_Decode[i, j] = Matrice_Decode[i, j] + 8; }
                        catch (Exception) { if (j == 9)  Matrice_Decode[i, j] = Matrice_Decode[i, j] + 8; }

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

        public static void Create_Carte_Clair_File (char[,] Matrice)
        {
            string Nom_Fichier = "";
            Console.Write("Donné un nom au fichier sans espace : ");
            Nom_Fichier = Console.ReadLine();
            
            while (Nom_Fichier.IndexOf(' ') != -1)
            {
                Console.Write("Nom de fichier invalide, recommencez : ");
                Nom_Fichier = Console.ReadLine();
            }

            string Fichier = @"D:\Mes_Trucs\DUT Informatique\1ère Année\Semestre 2\Programmation C#\Projet\Algorithme\Algo Déchiffrage Trame - Matrice Carte\Cartes Claires - Trames\" + Nom_Fichier + ".clair";

            if (File.Exists(Fichier))
            {
                Console.Write("Fichier existant, retapez un nom valide : ");
                Nom_Fichier = Console.ReadLine();
            }

            FileStream StreamFichier = new FileStream(Fichier, FileMode.OpenOrCreate);
            using (StreamWriter Saisi = new StreamWriter(StreamFichier))
            {
                for (int i = 0; i < 10; i++){
                    for (int j = 0; j < 10; j++)
                    {
                        Saisi.Write(Matrice[i, j]);
                    }
                    Saisi.Write("\n");
                }
            }
        }

        public static void Create_Trame_File (string trame)
        {
            string Nom_Fichier = "";
            Console.Write("Donné un nom au fichier sans espace : ");
            Nom_Fichier = Console.ReadLine();

            while (Nom_Fichier.IndexOf(' ') != -1)
            {
                Console.Write("Nom de fichier invalide, recommencez : ");
                Nom_Fichier = Console.ReadLine();
            }

            string Fichier = @"D:\Mes_Trucs\DUT Informatique\1ère Année\Semestre 2\Programmation C#\Projet\Algorithme\Algo Déchiffrage Trame - Matrice Carte\Cartes Claires - Trames\" + Nom_Fichier + ".chiffre";

            if (File.Exists(Fichier))
            {
                Console.Write("Fichier existant, retapez un nom valide : ");
                Nom_Fichier = Console.ReadLine();
            }

            FileStream StreamFichier = new FileStream(Fichier, FileMode.OpenOrCreate);
            using (StreamWriter Saisi = new StreamWriter(StreamFichier)) Saisi.Write(trame);
        }

        static void Main(string[] args)
        {
            string Fichier = @"D:\Mes_Trucs\DUT Informatique\1ère Année\Semestre 2\Programmation C#\Projet\Algorithme\Algo Déchiffrage Trame - Matrice Carte\Cartes Claires - Trames\trametest.chiffre";

            string trame = Trame(Fichier);

            string[,] Matrice_Trame = OrganiseTrame(trame);
            int[,] Matrice_Convertit = Convertit(Matrice_Trame);
            char[,] Matrice_Carte = Traduit(Matrice_Convertit);
            AfficheChar(Matrice_Carte);
            int[,] Matrice_Chiffre = Chiffrage(Matrice_Carte);
            AfficheInt(Matrice_Chiffre);

            string trame_out = Convert_To_Trame(Matrice_Chiffre);
            Console.WriteLine(trame_out);

            Create_Carte_Clair_File(Matrice_Carte);
            Create_Trame_File(trame);
        }
    }
}