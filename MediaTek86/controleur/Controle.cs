using System;
using System.Collections.Generic;
using MediaTek86.dal;
using MediaTek86.modele;

namespace MediaTek86.controleur
{
    /// <summary>
    /// Controleur de l'application : fait le lien entre les vues et la
    /// couche d'acces aux donnees (dal). Il recoit les demandes des vues
    /// et sollicite la classe Dao pour y repondre.
    /// </summary>
    public class Controle
    {
        /// <summary>
        /// Constructeur du controleur.
        /// </summary>
        public Controle()
        {
        }

        /// <summary>
        /// Controle l'authentification du responsable.
        /// </summary>
        /// <param name="login">Login saisi.</param>
        /// <param name="pwd">Mot de passe saisi.</param>
        /// <returns>True si l'authentification reussit, false sinon.</returns>
        public bool ControleAuthentification(string login, string pwd)
        {
            return Dao.ControleAuthentification(login, pwd);
        }

        /// <summary>
        /// Recupere la liste des services.
        /// </summary>
        /// <returns>Liste des services.</returns>
        public List<Service> GetLesServices()
        {
            return Dao.GetLesServices();
        }

        /// <summary>
        /// Recupere la liste des motifs.
        /// </summary>
        /// <returns>Liste des motifs.</returns>
        public List<Motif> GetLesMotifs()
        {
            return Dao.GetLesMotifs();
        }

        /// <summary>
        /// Recupere la liste des personnels.
        /// </summary>
        /// <returns>Liste des personnels.</returns>
        public List<Personnel> GetLesPersonnels()
        {
            return Dao.GetLesPersonnels();
        }

        /// <summary>
        /// Demande l'ajout d'un personnel.
        /// </summary>
        /// <param name="personnel">Le personnel a ajouter.</param>
        public void AjouterPersonnel(Personnel personnel)
        {
            Dao.AjouterPersonnel(personnel);
        }

        /// <summary>
        /// Demande la modification d'un personnel.
        /// </summary>
        /// <param name="personnel">Le personnel a modifier.</param>
        public void ModifierPersonnel(Personnel personnel)
        {
            Dao.ModifierPersonnel(personnel);
        }

        /// <summary>
        /// Demande la suppression d'un personnel.
        /// </summary>
        /// <param name="personnel">Le personnel a supprimer.</param>
        public void SupprimerPersonnel(Personnel personnel)
        {
            Dao.SupprimerPersonnel(personnel);
        }

        /// <summary>
        /// Recupere la liste des absences d'un personnel
        /// (de la plus recente a la plus ancienne).
        /// </summary>
        /// <param name="idpersonnel">Identifiant du personnel.</param>
        /// <returns>Liste des absences.</returns>
        public List<Absence> GetLesAbsences(int idpersonnel)
        {
            return Dao.GetLesAbsences(idpersonnel);
        }

        /// <summary>
        /// Demande l'ajout d'une absence.
        /// </summary>
        /// <param name="absence">L'absence a ajouter.</param>
        public void AjouterAbsence(Absence absence)
        {
            Dao.AjouterAbsence(absence);
        }

        /// <summary>
        /// Demande la modification d'une absence.
        /// </summary>
        /// <param name="absence">L'absence avec ses nouvelles valeurs.</param>
        /// <param name="ancienneDatedebut">Date de debut d'origine (cle).</param>
        public void ModifierAbsence(Absence absence, DateTime ancienneDatedebut)
        {
            Dao.ModifierAbsence(absence, ancienneDatedebut);
        }

        /// <summary>
        /// Demande la suppression d'une absence.
        /// </summary>
        /// <param name="absence">L'absence a supprimer.</param>
        public void SupprimerAbsence(Absence absence)
        {
            Dao.SupprimerAbsence(absence);
        }
    }
}
