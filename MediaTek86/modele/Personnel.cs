using System;

namespace MediaTek86.modele
{
    /// <summary>
    /// Classe metier representant un membre du personnel d'une mediatheque.
    /// </summary>
    public class Personnel
    {
        /// <summary>
        /// Identifiant du personnel.
        /// </summary>
        public int Idpersonnel { get; set; }

        /// <summary>
        /// Nom du personnel.
        /// </summary>
        public string Nom { get; set; }

        /// <summary>
        /// Prenom du personnel.
        /// </summary>
        public string Prenom { get; set; }

        /// <summary>
        /// Numero de telephone du personnel.
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// Adresse mail du personnel.
        /// </summary>
        public string Mail { get; set; }

        /// <summary>
        /// Identifiant du service d'affectation du personnel.
        /// </summary>
        public int Idservice { get; set; }

        /// <summary>
        /// Nom du service d'affectation (pour l'affichage).
        /// </summary>
        public string Service { get; set; }

        /// <summary>
        /// Constructeur d'un personnel.
        /// </summary>
        /// <param name="idpersonnel">Identifiant du personnel.</param>
        /// <param name="nom">Nom du personnel.</param>
        /// <param name="prenom">Prenom du personnel.</param>
        /// <param name="tel">Telephone du personnel.</param>
        /// <param name="mail">Mail du personnel.</param>
        /// <param name="idservice">Identifiant du service d'affectation.</param>
        /// <param name="service">Nom du service d'affectation.</param>
        public Personnel(int idpersonnel, string nom, string prenom, string tel,
                         string mail, int idservice, string service)
        {
            this.Idpersonnel = idpersonnel;
            this.Nom = nom;
            this.Prenom = prenom;
            this.Tel = tel;
            this.Mail = mail;
            this.Idservice = idservice;
            this.Service = service;
        }
    }
}
