using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace projetGuybrush
{
    /// <summary>
    /// Classe CarteChiffre : Instancie une carte chiffrée pour la décoder par la suite
    /// </summary>
    class CarteChiffre : Carte
    {
        #region Attributs
        /// <summary>
        /// Chemin du fichier de la carte chiffrée
        /// </summary>
        private string cheminFichier;
        
        /// <summary>
        /// Trame chiffrée de la carte
        /// </summary>
        private string trame;
        
        /// <summary>
        /// Matrice représentant la carte chiffrée
        /// </summary>
        private string[,] matriceCarte;
        
        /// <summary>
        /// Version int de matriceCarte
        /// </summary>
        private int[,] matriceCarteint;
        
        /// <summary>
        /// Matrice représantant la carte décodée
        /// </summary>
        private char[,] carteDecode;
        
        /// <summary>
        /// Nombre de parcelles de la carte déchiffrée
        /// </summary>
        private int nbParcelles;

        #endregion

        #region Constructeur
        
        /// <summary>
        /// Constructeur de la classe <see cref="projetGuybrush.CarteChiffre"/>
        /// </summary>
        /// <param name="filePath">Chemin du fichier de la carte chiffrée</param>
        public CarteChiffre(string filePath)
        {
            cheminFichier = filePath;     
            
        }

        #endregion

        #region Méthodes_Principales
        
        /// <summary>
        /// Permet de lire le fichier contenant la trame de la carte chiffrée
        /// </summary>
        private void Trame()
        {
            try
            {
                string[] Lignes_Fichier = File.ReadAllLines(cheminFichier);
                trame = Lignes_Fichier[0];
            } catch (Exception)
            {
                Console.WriteLine("Impossible d'extraire la trame du fichier");
            }
        }

        /// <summary>
        /// Met les unités codées dans une matrice 
        /// </summary>
        private void OrganiseTrametoMatrixString()
        {
            string[] Decomposition_Colonne = trame.Split('|');
            matriceCarte = new string[10,10];

            for (int i = 0; i < 10; i++)
            {
                string[] Decompostion_Ligne = Decomposition_Colonne[i].Split(':');
                for (int j = 0; j < 10; j++)
                {
                    matriceCarte[i, j] = Decompostion_Ligne[j];
                }
            }
        }

        /// <summary>
        /// Convertit les unités codées dans matriceCarte en entiers
        /// </summary>
        private void ConvertitToInt()
        {
            matriceCarteint = new int[10, 10];

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    matriceCarteint[i, j] = Convert.ToInt32(matriceCarte[i, j]);
                }
            }

        }

        /// <summary>
        /// Décode la carte chiffrée
        /// </summary>
        public void Decode()
        {
            Trame();
            OrganiseTrametoMatrixString();
            ConvertitToInt();

            nbParcelles = 0;
            carteDecode = new char[10, 10];

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (matriceCarteint[i, j] >= 65 && matriceCarteint[i, j] <= 79) carteDecode[i, j] = 'M';
                    else
                    {
                        if (matriceCarteint[i, j] >= 33 && matriceCarteint[i, j] <= 47) carteDecode[i, j] = 'F';
                        else
                        {
                            if (j == 0 || matriceCarteint[i, j - 1] >= 8) //Toutes les conditions pour que l'unité ai une frontière à l'ouest
                            {
                                if (matriceCarteint[i, j] % 2 == 1)
                                {
                                    int increment = 0;
                                    bool parcelleTrouve = false;
                                    while ((!parcelleTrouve) && (j + increment < 9) && matriceCarteint[i, j + increment] % 2 == 1 
                                            && (matriceCarteint[i, (j + increment)] < 8))
                                    {
                                        increment++;
                                        if ((matriceCarteint[i, (j + increment) - 1] < 8) && (matriceCarteint[i, j + increment] % 2 == 0))
                                            parcelleTrouve = true;
                                    }
                                    if (parcelleTrouve == false)
                                    {
                                        carteDecode[i, j] = Convert.ToChar('a' + nbParcelles);
                                        nbParcelles++;
                                    }
                                    else carteDecode[i, j] = carteDecode[i - 1, j + increment];

                                }
                                else carteDecode[i, j] = carteDecode[i - 1, j];
                            }
                            else carteDecode[i, j] = carteDecode[i, j - 1];
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// Affiche la carte déchiffrée
        /// </summary>
        public override void Affiche()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (carteDecode[i, j] == 'M')
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("M ", Console.ForegroundColor);
                    }
                    else
                        if (carteDecode[i, j] == 'F')
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("F ", Console.ForegroundColor);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write($"{carteDecode[i, j]} ", Console.ForegroundColor);
                    }
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\n", Console.ForegroundColor);
            }
            
            Liste_parcelles();

        }
        #endregion

        #region Méthodes_Auxiliaires
        
        /// <summary>
        /// Affiche la liste des parcelles et les coordonnées des unités
        /// </summary>
        public override void Liste_parcelles()
        {
            string pos;
            int nbUnite;
            Console.Write("\n");
            Console.WriteLine("Voici la liste des parcelles : ");
            for (int i = 0; i < nbParcelles - 1; i++)
            {
                pos = ""; nbUnite = 0;
                for (int j = 0; j < 10; j++)
                {
                    for (int k = 0; k < 10; k++)
                    {
                        if (carteDecode[j, k] == (Convert.ToChar('a' + i)))
                        {
                            nbUnite++;
                            pos += ($" ({j},{k})");
                        }
                    }
                }
                Console.WriteLine($"Parcelle {Convert.ToChar('a' + i)} : {nbUnite}");
                Console.WriteLine(pos);
                Console.WriteLine();
            }
        }
        
        /// <summary>
        /// Affiche la taille moyenne des parcelles
        /// </summary>
        public override void Taille_moyenne_Parcelle()
        {
            float moyenne = 0;
            int nbUnite = 0;
            Console.Write("\n Voici la taille moyenne des parcelles : ");
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (carteDecode[i, j] != 'M' && carteDecode[i, j] != 'F')
                        nbUnite++;
                }
            }
            moyenne = (float)nbUnite / nbParcelles;
            Console.Write(moyenne.ToString("0.00"));
        }
        
        /// <summary>
        /// Affiche les parcelles d'une taille minimale
        /// </summary>
        /// <param name="tailleMini">Taille minimale des parcelles à afficher</param>
        public override void Taille_relative_parcelle(int tailleMini)
        {
            int nbUnite = 0;
            Console.WriteLine($"\n\n Voici les parcelles dont la taille est supérieur ou égale à {tailleMini}");
            for (int i = 0; i < nbParcelles - 1; i++)
            {
                nbUnite = 0;
                for (int j = 0; j < 10; j++)
                {
                    for (int k = 0; k < 10; k++)
                    {
                        if (carteDecode[j, k] == (Convert.ToChar('a' + i)))
                        {
                            nbUnite++;
                        }
                    }
                }
                if (nbUnite >= tailleMini)
                {
                    Console.WriteLine($"Parcelle {Convert.ToChar('a' + i)} : {nbUnite} unites");
                    Console.WriteLine();
                }
            }
        }
        
        /// <summary>
        /// Affiche la taille d'une parcelle donnée
        /// </summary>
        /// <param name="Parcelle">Parcelle dont on veut afficher la taille</param>
        public override void Taille_parcelle(char Parcelle)
        {

            int nbUnite = 0;
            for (int i = 0; i < 10 - 1; i++)
            {
                for (int j = 0; j < 10 - 1; j++)
                {
                    if (carteDecode[i, j] == (Parcelle))
                    {
                        nbUnite++;
                    }
                }
            }
            Console.WriteLine((nbUnite == 0) ? $"\nParcelle {Parcelle} : inexistante" : $"\nTaille de la parcelle { Parcelle} : { nbUnite}");
        }
        

        #endregion

    }

}
