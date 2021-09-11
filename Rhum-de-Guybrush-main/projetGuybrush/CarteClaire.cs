using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace projetGuybrush
{
    /// <summary>
    /// Classe CarteClaire : instancie une carte brute qui n'est pas chiffrée
    /// </summary>
    class CarteClaire : Carte 
    {
        #region Attributs
        /// <summary>
        /// chemin du fichier de la carte claire
        /// </summary>
        private string cheminFichier;

        /// <summary>
        /// Trame chiffrée de la carte claire 
        /// </summary>
        private string trame;

        /// <summary>
        /// Representation de la carte sous forme de matrice
        /// </summary>
        private char[,] matriceCarte;
        
        /// <summary>
        /// Nombre de parcelles de la carte 
        /// </summary>
        private int nbParcelles;

        /// <summary>
        /// Représentation de la trame de la carte chiffrée sous forme de matrice
        /// </summary>
        private int[,] matriceChiffre;

        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur de la classe <see cref="projetGuybrush.CarteClaire"/>
        /// </summary>
        /// <param name="filePath">chemin du fichier de la carte claire</param>
        public CarteClaire(string filePath)
        {
            cheminFichier = filePath;
        }
        #endregion
        
        #region Méthodes_Principales
        /// <summary>
        /// Converti le fichier txt en matrice de caractères
        /// </summary>
        public void Convert_To_Matrice()
        {
            matriceCarte = new char[10, 10];

            List <char[]> Lignes = new List<char[]>();

            try
            {
                StreamReader Lecteur = new StreamReader(cheminFichier);
                string Ligne;
                char[] LigneTab;

                while ((Ligne = Lecteur.ReadLine()) != null)
                {
                    LigneTab = Ligne.ToCharArray();
                    Lignes.Add(LigneTab);
                }
            } catch (Exception e)
            {
                Console.WriteLine(e);
            }

            int i = 0;
            foreach (char[] Ligne in Lignes)
            {
                int j = 0;
                foreach (char Cara in Ligne)
                {
                    matriceCarte[i, j] = Cara;
                    j++;
                }
                i++;
            }
        }
        
        /// <summary>
        /// Affiche la carte claire non chiffrée
        /// </summary>
        public override void Affiche()
        {
            Convert_To_Matrice();

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (matriceCarte[i, j] == 'M')
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("M ", Console.ForegroundColor);
                    }
                    else
                        if (matriceCarte[i, j] == 'F')
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("F ", Console.ForegroundColor);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write($"{matriceCarte[i, j]} ", Console.ForegroundColor);
                    }
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\n", Console.ForegroundColor);
            }
            Liste_parcelles();
        }

        /// <summary>
        /// Permet de créer un fichier txt .chiffre pour y écrire la trame chiffrée
        /// </summary>
        private void EcrireTrame()
        {
            string Nom_Fichier = "";
            Console.Write("Donné un nom au fichier sans espace et sans extension (on s'occupe de tout, service premium) : ");
            Nom_Fichier = Console.ReadLine();

            while (Nom_Fichier.IndexOf(' ') != -1)                                  //While tant qu'aucun nom de fichier n'est rentré 
            {
                Console.Write("Nom de fichier invalide, recommencez : ");
                Nom_Fichier = Console.ReadLine();
            }

            string cheminFichier = System.IO.Directory.GetCurrentDirectory();      //Récupère le dossier courant

            for (int i = 0; i < 3; i++)
                cheminFichier = Convert.ToString(Directory.GetParent(cheminFichier));

            cheminFichier = Convert.ToString(cheminFichier + $"\\carte");         

            string Fichier = $"{cheminFichier}\\" + Nom_Fichier + ".chiffre";      //Donne automatiquement l'extension .chiffre au fichier contenant la trame chifrée de la carte

            while (File.Exists(Fichier))                                           //Tant que l'utilisateur donne un nom de fichier déjà existant
            {
                Console.Write("Fichier existant, retapez un nom valide : ");
                Nom_Fichier = Console.ReadLine();
                Fichier = $"{cheminFichier}\\" + Nom_Fichier + ".chiffre";
            }

            FileStream StreamFichier = new FileStream(Fichier, FileMode.OpenOrCreate);             //crée un fichier texte avec l'extension .chiffre
            using (StreamWriter Saisi = new StreamWriter(StreamFichier)) Saisi.Write(trame);   //ecrit dans le fichier texte

            Console.WriteLine($"chemin du fichier : {Fichier}");
        }
        
        /// <summary>
        /// Chiffre la carte claire donnée
        /// </summary>
        public void Chiffrer()
        {
            matriceChiffre = new int[10, 10];

            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    matriceChiffre[i, j] = 0;


            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i == 0 || matriceCarte[i, j] != matriceCarte[i - 1, j])
                    {
                        matriceChiffre[i, j] += 1;
                        if (i > 0)
                            matriceChiffre[i - 1, j] += 4;
                    }

                    if (j == 0 || matriceCarte[i, j] != matriceCarte[i, j - 1])
                    {
                        matriceChiffre[i, j] += 2;
                        if (j > 0)
                            matriceChiffre[i, j - 1] += 8;
                    }

                    if (i == 9)
                        matriceChiffre[i, j] += 4;

                    if (j == 9)
                        matriceChiffre[i, j] += 8;

                    switch (matriceCarte[i, j])
                    {
                        case ('M'):
                            matriceChiffre[i, j] = matriceChiffre[i, j] + 64;
                            break;
                        case ('F'):
                            matriceChiffre[i, j] = matriceChiffre[i, j] + 32;
                            break;
                    }

                }
            }
            trame = "";
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    trame += (j == 0) ? $"{matriceChiffre[i, j]}" : $":{matriceChiffre[i, j]}";
                }
                
                trame += "|";
            }
                
            EcrireTrame();
        }

        #endregion
        
        #region Méthodes_Auxiliaires

        /// <summary>
        /// Compte le nombre de parcelles dans la carte
        /// </summary>
        private void CompteParcelles()
        {
            nbParcelles = 0;
            List<char> listeParcelles = new List<char>();
            for(int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if(!(listeParcelles.Contains(matriceCarte[i,j])) && matriceCarte[i,j] != 'M' && matriceCarte[i,j] != 'F')
                    {
                        listeParcelles.Add(matriceCarte[i, j]);
                        nbParcelles++;
                    } 
                }
            }
        }

        /// <summary>
        /// Affiche la liste des parcelles de la carte et les coordonnées des unités
        /// </summary>
        public override void Liste_parcelles()
        {
            CompteParcelles();
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
                        if (matriceCarte[j, k] == (Convert.ToChar('a' + i)))
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
        /// Affiche la taille moyenne des parcelles de la carte
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
                    if (matriceCarte[i, j] != 'M' && matriceCarte[i, j] != 'F')
                        nbUnite++;
                }
            }
            moyenne = (float)nbUnite / nbParcelles;
            Console.Write(moyenne.ToString("0.00"));
        }

        /// <summary>
        /// Affiche la taille d'une parcelle donnée
        /// </summary>
        /// <param name="parcelle">Parcelle dont l'utilisateur veut afficher la taille</param>
        public override void Taille_parcelle(char parcelle)
        {
            int nbUnite = 0;
            for (int i = 0; i < 10 - 1; i++)
            {
                for (int j = 0; j < 10 - 1; j++)
                {
                    if (matriceCarte[i, j] == (parcelle))
                    {
                        nbUnite++;
                    }
                }
            }
            Console.WriteLine((nbUnite == 0) ? $"Parcelle {parcelle} : inexistante" : $"Taille de la parcelle { parcelle} : { nbUnite}");
        }

        /// <summary>
        /// Affiche les parcelles d'une taille minimale donnée
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
                        if (matriceCarte[j, k] == (Convert.ToChar('a' + i)))
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

        #endregion

        
    }
}
