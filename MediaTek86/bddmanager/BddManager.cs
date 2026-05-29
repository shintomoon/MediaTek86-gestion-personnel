using System;
using MySql.Data.MySqlClient;

namespace MediaTek86.bddmanager
{
    /// <summary>
    /// Package contenant la classe technique de connexion à la base de données.
    /// </summary>
    internal class NamespaceDoc
    {
    }

    /// <summary>
    /// Classe singleton de connexion à la base de données
    /// </summary>
    public class BddManager
    {
        /// <summary>
        /// Instance unique de la classe
        /// </summary>
        private static BddManager instance = null;

        /// <summary>
        /// Connexion à la base de données
        /// </summary>
        private MySqlConnection connection = null;

        /// <summary>
        /// Constructeur privé
        /// </summary>
        /// <param name="stringConnect">Chaîne de connexion</param>
        private BddManager(string stringConnect)
        {
            connection = new MySqlConnection(stringConnect);
        }

        /// <summary>
        /// Retourne l'instance unique de la classe
        /// </summary>
        /// <param name="stringConnect">Chaîne de connexion</param>
        /// <returns>Instance de BddManager</returns>
        public static BddManager GetInstance(string stringConnect)
        {
            if (instance == null)
            {
                instance = new BddManager(stringConnect);
            }
            return instance;
        }

        /// <summary>
        /// Retourne la connexion à la base de données
        /// </summary>
        /// <returns>Connexion MySQL</returns>
        public MySqlConnection GetConnection()
        {
            return connection;
        }
    }
}