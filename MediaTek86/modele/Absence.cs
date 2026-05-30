using System;

namespace MediaTek86.modele
{
    /// <summary>
    /// Classe metier representant une absence d'un membre du personnel.
    /// </summary>
    public class Absence
    {
        /// <summary>
        /// Identifiant du personnel concerne par l'absence.
        /// </summary>
        public int Idpersonnel { get; set; }

        /// <summary>
        /// Date de debut de l'absence.
        /// </summary>
        public DateTime Datedebut { get; set; }

        /// <summary>
        /// Date de fin de l'absence.
        /// </summary>
        public DateTime Datefin { get; set; }

        /// <summary>
        /// Identifiant du motif de l'absence.
        /// </summary>
        public int Idmotif { get; set; }

        /// <summary>
        /// Libelle du motif de l'absence (pour l'affichage).
        /// </summary>
        public string Motif { get; set; }

        /// <summary>
        /// Constructeur d'une absence.
        /// </summary>
        /// <param name="idpersonnel">Identifiant du personnel concerne.</param>
        /// <param name="datedebut">Date de debut de l'absence.</param>
        /// <param name="datefin">Date de fin de l'absence.</param>
        /// <param name="idmotif">Identifiant du motif.</param>
        /// <param name="motif">Libelle du motif.</param>
        public Absence(int idpersonnel, DateTime datedebut, DateTime datefin,
                       int idmotif, string motif)
        {
            this.Idpersonnel = idpersonnel;
            this.Datedebut = datedebut;
            this.Datefin = datefin;
            this.Idmotif = idmotif;
            this.Motif = motif;
        }
    }
}
