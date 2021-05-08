using System;
using System.Collections.Generic;
using System.Text;

namespace Projet
{
    class Carte
    {
        protected int i = 0, j = 0;
        protected int[,] Matrice;
        protected bool[,] Array;
    }
    class chiffrement : Carte
    {
        private int[] valeur = { 2, 3 };
        private bool MurDeLOuest(int [,]tab) {
            
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