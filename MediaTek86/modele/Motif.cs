using System;

namespace MediaTek86.modele
{
    /// <summary>
    /// Classe metier representant un motif d'absence
    /// (vacances, maladie, motif familial, conge parental).
    /// </summary>
    public class Motif
    {
        /// <summary>
        /// Identifiant du motif.
        /// </summary>
        public int Idmotif { get; set; }

        /// <summary>
        /// Libelle du motif.
        /// </summary>
        public string Libelle { get; set; }

        /// <summary>
        /// Constructeur d'un motif.
        /// </summary>
        /// <param name="idmotif">Identifiant du motif.</param>
        /// <param name="libelle">Libelle du motif.</param>
        public Motif(int idmotif, string libelle)
        {
            this.Idmotif = idmotif;
            this.Libelle = libelle;
        }

        /// <summary>
        /// Affichage du libelle du motif (utilise notamment dans les ComboBox).
        /// </summary>
        /// <returns>Le libelle du motif.</returns>
        public override string ToString()
        {
            return Libelle;
        }
    }
}
