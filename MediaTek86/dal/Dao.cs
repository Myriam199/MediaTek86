using System;
using System.Collections.Generic;
using MediaTek86.bddmanager;
using MediaTek86.modele;
using MySql.Data.MySqlClient;

namespace MediaTek86.dal
{
    /// <summary>
    /// Classe technique d'acces aux donnees (Data Access Object).
    /// Exploite la classe BddManager pour repondre aux demandes du controleur,
    /// suivant la meme logique que l'application Habilitations.
    /// </summary>
    public class Dao
    {
        /// <summary>
        /// Chaine de connexion a la base de donnees MySQL.
        /// (login/pwd de l'utilisateur applicatif cree dans le script SQL).
        /// </summary>
        private static readonly string connectionString =
            "server=localhost;user id=mediatek86user;password=mediatek86pwd;database=mediatek86;SslMode=none;AllowPublicKeyRetrieval=true;";

        /// <summary>
        /// Recupere la chaine de connexion a la base de donnees.
        /// </summary>
        /// <returns>La chaine de connexion.</returns>
        public static string GetChaineConnexion()
        {
            return connectionString;
        }

        /// <summary>
        /// Controle l'authentification du responsable.
        /// Le mot de passe est chiffre en SHA2(256) cote SGBD pour la comparaison.
        /// </summary>
        /// <param name="login">Login saisi.</param>
        /// <param name="pwd">Mot de passe saisi (en clair).</param>
        /// <returns>True si le couple login/pwd existe, false sinon.</returns>
        public static bool ControleAuthentification(string login, string pwd)
        {
            bool authentifie = false;
            string req = "SELECT login FROM responsable ";
            req += "WHERE login = @login AND pwd = SHA2(@pwd, 256);";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@login", login },
                { "@pwd", pwd }
            };
            BddManager curs = BddManager.GetInstance(connectionString);
            MySqlDataReader reader = curs.ReqSelect(req, parameters);
            if (reader.Read())
            {
                authentifie = true;
            }
            reader.Close();
            return authentifie;
        }

        /// <summary>
        /// Recupere et retourne la liste de tous les services.
        /// </summary>
        /// <returns>Liste des services.</returns>
        public static List<Service> GetLesServices()
        {
            List<Service> lesServices = new List<Service>();
            string req = "SELECT idservice, nom FROM service ORDER BY nom;";
            BddManager curs = BddManager.GetInstance(connectionString);
            MySqlDataReader reader = curs.ReqSelect(req);
            while (reader.Read())
            {
                int idservice = (int)reader["idservice"];
                string nom = (string)reader["nom"];
                lesServices.Add(new Service(idservice, nom));
            }
            reader.Close();
            return lesServices;
        }

        /// <summary>
        /// Recupere et retourne la liste de tous les motifs d'absence.
        /// </summary>
        /// <returns>Liste des motifs.</returns>
        public static List<Motif> GetLesMotifs()
        {
            List<Motif> lesMotifs = new List<Motif>();
            string req = "SELECT idmotif, libelle FROM motif ORDER BY libelle;";
            BddManager curs = BddManager.GetInstance(connectionString);
            MySqlDataReader reader = curs.ReqSelect(req);
            while (reader.Read())
            {
                int idmotif = (int)reader["idmotif"];
                string libelle = (string)reader["libelle"];
                lesMotifs.Add(new Motif(idmotif, libelle));
            }
            reader.Close();
            return lesMotifs;
        }

        /// <summary>
        /// Recupere et retourne la liste de tous les personnels,
        /// avec le nom de leur service d'affectation.
        /// </summary>
        /// <returns>Liste des personnels.</returns>
        public static List<Personnel> GetLesPersonnels()
        {
            List<Personnel> lesPersonnels = new List<Personnel>();
            string req = "SELECT p.idpersonnel, p.nom, p.prenom, p.tel, p.mail, ";
            req += "p.idservice, s.nom AS service ";
            req += "FROM personnel p JOIN service s ON p.idservice = s.idservice ";
            req += "ORDER BY p.nom, p.prenom;";
            BddManager curs = BddManager.GetInstance(connectionString);
            MySqlDataReader reader = curs.ReqSelect(req);
            while (reader.Read())
            {
                int idpersonnel = (int)reader["idpersonnel"];
                string nom = (string)reader["nom"];
                string prenom = (string)reader["prenom"];
                string tel = reader["tel"] is DBNull ? "" : (string)reader["tel"];
                string mail = reader["mail"] is DBNull ? "" : (string)reader["mail"];
                int idservice = (int)reader["idservice"];
                string service = (string)reader["service"];
                lesPersonnels.Add(new Personnel(idpersonnel, nom, prenom, tel, mail, idservice, service));
            }
            reader.Close();
            return lesPersonnels;
        }

        /// <summary>
        /// Ajoute un personnel dans la base de donnees.
        /// </summary>
        /// <param name="personnel">Le personnel a ajouter.</param>
        public static void AjouterPersonnel(Personnel personnel)
        {
            string req = "INSERT INTO personnel (nom, prenom, tel, mail, idservice) ";
            req += "VALUES (@nom, @prenom, @tel, @mail, @idservice);";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@nom", personnel.Nom },
                { "@prenom", personnel.Prenom },
                { "@tel", personnel.Tel },
                { "@mail", personnel.Mail },
                { "@idservice", personnel.Idservice }
            };
            BddManager curs = BddManager.GetInstance(connectionString);
            curs.ReqUpdate(req, parameters);
        }

        /// <summary>
        /// Modifie un personnel dans la base de donnees.
        /// </summary>
        /// <param name="personnel">Le personnel a modifier.</param>
        public static void ModifierPersonnel(Personnel personnel)
        {
            string req = "UPDATE personnel SET nom = @nom, prenom = @prenom, ";
            req += "tel = @tel, mail = @mail, idservice = @idservice ";
            req += "WHERE idpersonnel = @idpersonnel;";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@idpersonnel", personnel.Idpersonnel },
                { "@nom", personnel.Nom },
                { "@prenom", personnel.Prenom },
                { "@tel", personnel.Tel },
                { "@mail", personnel.Mail },
                { "@idservice", personnel.Idservice }
            };
            BddManager curs = BddManager.GetInstance(connectionString);
            curs.ReqUpdate(req, parameters);
        }

        /// <summary>
        /// Supprime un personnel de la base de donnees ainsi que ses absences.
        /// </summary>
        /// <param name="personnel">Le personnel a supprimer.</param>
        public static void SupprimerPersonnel(Personnel personnel)
        {
            BddManager curs = BddManager.GetInstance(connectionString);
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@idpersonnel", personnel.Idpersonnel }
            };
            // Suppression prealable des absences liees (integrite referentielle)
            curs.ReqUpdate("DELETE FROM absence WHERE idpersonnel = @idpersonnel;", parameters);
            curs.ReqUpdate("DELETE FROM personnel WHERE idpersonnel = @idpersonnel;", parameters);
        }

        /// <summary>
        /// Recupere et retourne la liste des absences d'un personnel,
        /// classees de la plus recente a la plus ancienne.
        /// </summary>
        /// <param name="idpersonnel">Identifiant du personnel.</param>
        /// <returns>Liste des absences du personnel.</returns>
        public static List<Absence> GetLesAbsences(int idpersonnel)
        {
            List<Absence> lesAbsences = new List<Absence>();
            string req = "SELECT a.idpersonnel, a.datedebut, a.datefin, a.idmotif, m.libelle AS motif ";
            req += "FROM absence a JOIN motif m ON a.idmotif = m.idmotif ";
            req += "WHERE a.idpersonnel = @idpersonnel ";
            req += "ORDER BY a.datedebut DESC;";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@idpersonnel", idpersonnel }
            };
            BddManager curs = BddManager.GetInstance(connectionString);
            MySqlDataReader reader = curs.ReqSelect(req, parameters);
            while (reader.Read())
            {
                DateTime datedebut = (DateTime)reader["datedebut"];
                DateTime datefin = (DateTime)reader["datefin"];
                int idmotif = (int)reader["idmotif"];
                string motif = (string)reader["motif"];
                lesAbsences.Add(new Absence(idpersonnel, datedebut, datefin, idmotif, motif));
            }
            reader.Close();
            return lesAbsences;
        }

        /// <summary>
        /// Ajoute une absence dans la base de donnees.
        /// </summary>
        /// <param name="absence">L'absence a ajouter.</param>
        public static void AjouterAbsence(Absence absence)
        {
            string req = "INSERT INTO absence (idpersonnel, datedebut, datefin, idmotif) ";
            req += "VALUES (@idpersonnel, @datedebut, @datefin, @idmotif);";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@idpersonnel", absence.Idpersonnel },
                { "@datedebut", absence.Datedebut },
                { "@datefin", absence.Datefin },
                { "@idmotif", absence.Idmotif }
            };
            BddManager curs = BddManager.GetInstance(connectionString);
            curs.ReqUpdate(req, parameters);
        }

        /// <summary>
        /// Modifie une absence dans la base de donnees.
        /// La date de debut faisant partie de la cle primaire, l'ancienne
        /// date de debut est utilisee pour identifier la ligne a modifier.
        /// </summary>
        /// <param name="absence">L'absence avec ses nouvelles valeurs.</param>
        /// <param name="ancienneDatedebut">Date de debut d'origine (cle).</param>
        public static void ModifierAbsence(Absence absence, DateTime ancienneDatedebut)
        {
            string req = "UPDATE absence SET datedebut = @datedebut, datefin = @datefin, ";
            req += "idmotif = @idmotif ";
            req += "WHERE idpersonnel = @idpersonnel AND datedebut = @ancienneDatedebut;";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@idpersonnel", absence.Idpersonnel },
                { "@datedebut", absence.Datedebut },
                { "@datefin", absence.Datefin },
                { "@idmotif", absence.Idmotif },
                { "@ancienneDatedebut", ancienneDatedebut }
            };
            BddManager curs = BddManager.GetInstance(connectionString);
            curs.ReqUpdate(req, parameters);
        }

        /// <summary>
        /// Supprime une absence de la base de donnees.
        /// </summary>
        /// <param name="absence">L'absence a supprimer.</param>
        public static void SupprimerAbsence(Absence absence)
        {
            string req = "DELETE FROM absence ";
            req += "WHERE idpersonnel = @idpersonnel AND datedebut = @datedebut;";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@idpersonnel", absence.Idpersonnel },
                { "@datedebut", absence.Datedebut }
            };
            BddManager curs = BddManager.GetInstance(connectionString);
            curs.ReqUpdate(req, parameters);
        }
    }
}
