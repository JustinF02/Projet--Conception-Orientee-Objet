using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace projetGuybrush
{
    /// <summary>
    /// Classe Abstraite Carte : Classe mère abstraite des classes CarteClaire et CarteChiffre
    /// </summary>
    abstract class Carte
    {
        #region Méthodes_Abstraites
        /// <summary>
        /// Affiche la carte déchiffrée
        /// </summary>
        public abstract void Affiche();
        
        /// <summary>
        /// Affiche la liste des parcelles et les coordonnées des unités
        /// </summary>
        public abstract void Liste_parcelles();
        
        /// <summary>
        /// Affiche la taille d'une parcelle donnée
        /// </summary>
        /// <param name="parcelle">Parcelle dont on veut afficher la taille</param>
        public abstract void Taille_parcelle(char parcelle);
        
        /// <summary>
        /// Affiche les parcelles d'une taille minimale
        /// </summary>
        /// <param name="tailleMini">Taille minimale des parcelles à afficher</param>
        public abstract void Taille_relative_parcelle(int tailleMini);
        
        /// <summary>
        /// Affiche la taille moyenne des parcelles
        /// </summary>
        public abstract void Taille_moyenne_Parcelle();
        
        #endregion

    }
}
