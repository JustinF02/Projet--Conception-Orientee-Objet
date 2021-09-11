using System;
using System.IO;
using System.Threading;

namespace projetGuybrush
{
    class Program
    {
        static void Main(string[] args)
        {

            var TableauASCII = new[]
            {
                @"[===========================================================================================================================================================================================]",
                @"                      ,--,                        ____                                                                                                                                ,--,   ",
                @"   ,-.----.         ,--.'|                      ,'  , `.                                       ,----..                    ,---,    ,---,. ,-.----.                  .--.--.         ,--.'|   ",
                @"   \    /  \     ,--,  | :         ,--,      ,-+-,.' _ |              ,---,                   /   /   \           ,--, ,`--.' |  ,'  .'  \\    /  \           ,--, /  /    '.    ,--,  | :   ",
                @"   ;   :    \ ,---.'|  : '       ,'_ /|   ,-+-. ;   , ||            ,---.'|                  |   :     :        ,'_ /| |   :  :,---.' .' |;   :    \        ,'_ /||  :  /`. / ,---.'|  : '   ",
                @"   |   | .\ : |   | : _' |  .--. |  | :  ,--.'|'   |  ;|            |   | :                  .   |  ;. /   .--. |  | : :   |  '|   |  |: ||   | .\ :   .--. |  | :;  |  |--`  |   | : _' |   ",
                @"   .   : |: | :   : |.'  |,'_ /| :  . | |   |  ,', |  ':            |   | |   ,---.          .   ; /--`  ,'_ /| :  . | |   :  |:   :  :  /.   : |: | ,'_ /| :  . ||  :  ;_    :   : |.'  |   ",
                @"   |   |  \ : |   ' '  ; :|  ' | |  . . |   | /  | |  ||          ,--.__| |  /     \         ;   | ;  __ |  ' | |  . . '   '  ;:   |    ; |   |  \ : |  ' | |  . . \  \    `. |   ' '  ; :   ",
                @"   |   : .  / '   |  .'. ||  | ' |  | | '   | :  | :  |,         /   ,'   | /    /  |        |   : |.' .'|  | ' |  | | |   |  ||   :     \|   : .  / |  | ' |  | |  `----.   \'   |  .'. |   ",
                @"   ;   | |  \ |   | :  | ':  | | :  ' ; ;   . |  ; |--'         .   '  /  |.    ' / |        .   | '_.' ::  | | :  ' ; '   :  ;|   |   . |;   | |  \ :  | | :  ' ;  __ \  \  ||   | :  | '   ",
                @"   |   | ;\  \'   : |  : ;|  ; ' |  | ' |   : |  | ,            '   ; |:  |'   ;   /|        '   ; : \  ||  ; ' |  | ' |   |  ''   :  '; ||   | ;\  \|  ; ' |  | ' /  /`--'  /'   : |  : ;   ",
                @"   :   ' | \.'|   | '  ,/ :  | : ;  ; | |   : '  |/             |   | '/  ''   |  / |        '   | '/  .':  | : ;  ; | '   :  ||   |  | ; :   ' | \.':  | : ;  ; |'--'.     / |   | '  ,/    ",
                @"   :   : :-'  ;   : ;--'  '  :  `--'   \;   | |`-'              |   :    :||   :    |        |   :    /  '  :  `--'   \;   |.' |   :   /  :   : :-'  '  :  `--'   \ `--'---'  ;   : ;--'     ",
                @"   |   |.'    |   ,/      :  ,      .-./|   ;/                   \   \  /   \   \  /          \   \ .'   :  ,      .-./'---'   |   | ,'   |   |.'    :  ,      .-./           |   ,/         ",
                @"   `---'      '---'        `--`----'    '---'                     `----'     `----'            `---`      `--`----'            `----'     `---'       `--`----'               '---'          ",
                @"[===========================================================================================================================================================================================]"
            };

            Console.WindowWidth = 190;

            foreach (string line in TableauASCII)
            {
                Console.WriteLine(line);
            }

            string nomFichier;
            bool decision = false;
            bool quitter = false;

            Console.Write("\nEntrez le nom du fichier demandé avec l'extension (.chiffre / .clair) : ");
            nomFichier = Console.ReadLine();

            string filepath = "";
            bool Exist = false;

            do
            {
                filepath = System.IO.Directory.GetCurrentDirectory();

                for (int i = 0; i < 3; i++)
                    filepath = Convert.ToString(Directory.GetParent(filepath));         //Remonte jusqu'au dossier contenant le dossier carte

                filepath = Convert.ToString(filepath + $"\\carte\\{nomFichier}");

                if (File.Exists(filepath)) Exist = true;
                else
                {
                    Console.Write("Fichier inexistant, retapez un nom valide : ");
                    nomFichier = Console.ReadLine();
                }

            } while (!Exist);

            Console.Write($"\nChemin de fichier : ");

            Console.ForegroundColor = ConsoleColor.DarkGreen;

            Console.Write($"{filepath}\n\n", Console.ForegroundColor);

            Console.ForegroundColor = ConsoleColor.White;
            if (filepath.EndsWith(".chiffre"))
            {
                CarteChiffre currentCarteChiffre = new CarteChiffre(filepath);
                do
                {
                    Console.Write("Souhaitez vous decoder cette carte ('Y'/'N') ? : ");
                    string decode = Console.ReadLine();
                    switch (decode)
                    {
                        case "Y":
                            currentCarteChiffre.Decode();
                            currentCarteChiffre.Affiche();
                            decision = true;
                            break;
                        case "N":
                            decision = true;
                            quitter = true;
                            Console.WriteLine("\nVous ne souhaitez pas décoder la carte, nous ne pouvons rien de plus, au revoir !");
                            break;
                        default:
                            Console.WriteLine("Saisissez 'Y' ou 'N'");
                            break;
                    }

                } while (!decision);

                while (!quitter)
                {
                    Console.WriteLine("\nQuelle operation voulez vous effectuer ?\n1. Afficher la taille moyenne des Parcelles\n2. Afficher la taille d'une parcelle ?\n" +
                        "3. Afficher les parcelles d'une taille minimale\n4. Quitter");
                    Console.Write("Saisissez le numero de l'action a effectuer : ");

                    switch (Console.ReadLine())
                    {
                        case "1":
                            currentCarteChiffre.Taille_moyenne_Parcelle();
                            Console.WriteLine();
                            break;
                        case "2":
                            Console.Write("\nDe quelle parcelle voulez vous la taille ? : ");
                            char parcelle = Convert.ToChar(Console.ReadLine());
                            currentCarteChiffre.Taille_parcelle(parcelle);
                            Console.WriteLine();
                            break;
                        case "3":
                            Console.Write("\nQuelle est la taille minimale des parcelles que vous voulez consulter ? : ");
                            int tailleMini = Convert.ToInt32(Console.ReadLine());
                            currentCarteChiffre.Taille_relative_parcelle(tailleMini);
                            Console.WriteLine();
                            break;
                        case "4":
                            Console.WriteLine("\nMerci d'avoir consulte nos offres avec CSI Tourisme, a une prochaine fois peut etre :)\n");
                            quitter = true;
                            break;
                    }
                }
                Thread.Sleep(3000);                   // Attends 3 secondes avant de terminer le programme, question de lisibilité et d'ergonomie
            }

            else
            {
                if (filepath.EndsWith(".clair"))
                {
                    CarteClaire currentCarteClaire = new CarteClaire(filepath);
                    currentCarteClaire.Affiche();
                    do
                    {
                        Console.Write("Souhaitez vous chiffrer cette carte ('Y'/'N') ? : ");
                        string code = Console.ReadLine();
                        switch (code)
                        {
                            case "Y":
                                currentCarteClaire.Chiffrer();
                                decision = true;
                                break;
                            case "N":
                                decision = true;
                                break;
                            default:
                                Console.WriteLine("Saisissez 'Y' ou 'N'");
                                break;
                        }

                    } while (!decision);

                    do
                    {
                        Console.WriteLine("\nQuelle operation voulez vous effectuer ?\n1. Afficher la taille moyenne des Parcelles\n2. Afficher la taille d'une parcelle ?\n" +
                            "3. Afficher les parcelles d'une taille minimale\n4. Quitter");
                        Console.Write("Saisissez le numero de l'action a effectuer : ");

                        switch (Console.ReadLine())
                        {
                            case "1":
                                currentCarteClaire.Taille_moyenne_Parcelle();
                                Console.WriteLine();
                                break;
                            case "2":
                                Console.Write("\nDe quelle parcelle voulez vous la taille ? : ");
                                char parcelle = Convert.ToChar(Console.ReadLine());
                                currentCarteClaire.Taille_parcelle(parcelle);
                                Console.WriteLine();
                                break;
                            case "3":
                                Console.Write("\nQuelle est la taille minimale des parcelles que vous voulez consulter ? : ");
                                int tailleMini = Convert.ToInt32(Console.ReadLine());
                                currentCarteClaire.Taille_relative_parcelle(tailleMini);
                                Console.WriteLine();
                                break;
                            case "4":
                                Console.WriteLine("\nMerci d'avoir consulte nos offres avec CSI Tourisme, a une prochaine fois peut etre :)\n");
                                quitter = true;
                                break;
                        }
                    } while (!quitter);

                    Thread.Sleep(3000);                   // Attends 3 secondes avant de terminer le programme, question de lisibilité et d'ergonomie
                }
                else Console.WriteLine("Le fichier ne comporte pas l'une des extensions valides.");
            }
        }
    }
}
