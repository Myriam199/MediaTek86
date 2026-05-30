using System;

namespace MediaTek86.modele
{
    /// <summary>
    /// Classe metier representant un service de la mediatheque
    /// (administratif, mediation culturelle, pret).
    /// </summary>
    public class Service
    {
        /// <summary>
        /// Identifiant du service.
        /// </summary>
        public int Idservice { get; set; }

        /// <summary>
        /// Nom (libelle) du service.
        /// </summary>
        public string Nom { get; set; }

        /// <summary>
        /// Constructeur d'un service.
        /// </summary>
        /// <param name="idservice">Identifiant du service.</param>
        /// <param name="nom">Nom du service.</param>
        public Service(int idservice, string nom)
        {
            this.Idservice = idservice;
            this.Nom = nom;
        }

        /// <summary>
        /// Affichage du nom du service (utilise notamment dans les ComboBox).
        /// </summary>
        /// <returns>Le nom du service.</returns>
        public override string ToString()
        {
            return Nom;
        }
    }
}
