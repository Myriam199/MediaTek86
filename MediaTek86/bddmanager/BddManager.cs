using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace MediaTek86.bddmanager
{
    /// <summary>
    /// Classe technique (singleton) de gestion de la connexion a la base de
    /// donnees MySQL et de l'execution des requetes.
    /// Une seule instance est creee pour toute l'application.
    /// </summary>
    public class BddManager
    {
        /// <summary>
        /// Instance unique de la classe (pattern singleton).
        /// </summary>
        private static BddManager instance = null;

        /// <summary>
        /// Objet de connexion a la base de donnees a partir d'une chaine de connexion.
        /// </summary>
        private readonly MySqlConnection connection;

        /// <summary>
        /// Constructeur prive : ouvre la connexion a la base de donnees.
        /// </summary>
        /// <param name="stringConnect">Chaine de connexion a la base de donnees.</param>
        private BddManager(string stringConnect)
        {
            connection = new MySqlConnection(stringConnect);
            connection.Open();
        }

        /// <summary>
        /// Cree l'unique instance de la classe si elle n'existe pas encore,
        /// puis la retourne.
        /// </summary>
        /// <param name="stringConnect">Chaine de connexion a la base de donnees.</param>
        /// <returns>L'instance unique de BddManager.</returns>
        public static BddManager GetInstance(string stringConnect)
        {
            if (instance == null)
            {
                instance = new BddManager(stringConnect);
            }
            return instance;
        }

        /// <summary>
        /// Execute une requete de type SELECT et retourne le resultat
        /// sous forme d'un curseur (MySqlDataReader).
        /// </summary>
        /// <param name="stringQuery">Requete SQL a executer.</param>
        /// <param name="parameters">Dictionnaire des parametres de la requete.</param>
        /// <returns>Le curseur permettant de parcourir le resultat.</returns>
        public MySqlDataReader ReqSelect(string stringQuery, Dictionary<string, object> parameters = null)
        {
            MySqlCommand command = new MySqlCommand(stringQuery, connection);
            if (parameters != null)
            {
                foreach (KeyValuePair<string, object> parameter in parameters)
                {
                    command.Parameters.Add(new MySqlParameter(parameter.Key, parameter.Value));
                }
            }
            command.Prepare();
            return command.ExecuteReader();
        }

        /// <summary>
        /// Execute une requete autre que SELECT (INSERT, UPDATE, DELETE).
        /// </summary>
        /// <param name="stringQuery">Requete SQL a executer.</param>
        /// <param name="parameters">Dictionnaire des parametres de la requete.</param>
        public void ReqUpdate(string stringQuery, Dictionary<string, object> parameters = null)
        {
            MySqlCommand command = new MySqlCommand(stringQuery, connection);
            if (parameters != null)
            {
                foreach (KeyValuePair<string, object> parameter in parameters)
                {
                    command.Parameters.Add(new MySqlParameter(parameter.Key, parameter.Value));
                }
            }
            command.Prepare();
            command.ExecuteNonQuery();
        }
    }
}
