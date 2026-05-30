using System;
using System.Collections.Generic;
using MediaTek86.bddmanager;
using MySql.Data.MySqlClient;

namespace MediaTek86.dal
{
    /// <summary>
    /// Package contenant les classes d'accès aux données (DAL).
    /// </summary>
    internal class NamespaceDoc
    {
    }

    /// <summary>
    /// Classe d'accès aux données
    /// </summary>
    public class Access
    {
        /// <summary>
        /// Chaîne de connexion à la base de données
        /// </summary>
        private static readonly string stringConnect =
            "server=localhost;user id=mediatek86_user;" +
            "password=Mediatek2024!;database=mediatek86";

        /// <summary>
        /// Instance de BddManager
        /// </summary>
        private readonly BddManager bddManager;

        /// <summary>
        /// Instance unique de la classe Access
        /// </summary>
        private static Access instance = null;

        /// <summary>
        /// Constructeur privé
        /// </summary>
        private Access()
        {
            bddManager = BddManager.GetInstance(stringConnect);
        }

        /// <summary>
        /// Retourne l'instance unique de la classe
        /// </summary>
        /// <returns>Instance de Access</returns>
        public static Access GetInstance()
        {
            if (instance == null)
            {
                instance = new Access();
            }
            return instance;
        }

        /// <summary>
        /// Contrôle l'authentification du responsable
        /// </summary>
        /// <param name="login">Login saisi</param>
        /// <param name="pwd">Mot de passe saisi (sera hashé)</param>
        /// <returns>true si authentification réussie, false sinon</returns>
        public bool ControleAuthentification(string login, string pwd)
        {
            string req = "SELECT * FROM responsable ";
            req += "WHERE login=@login AND pwd=SHA2(@pwd, 256);";

            try
            {
                MySqlConnection connection = bddManager.GetConnection();
                connection.Open();
                MySqlCommand command = new MySqlCommand(req, connection);
                command.Parameters.AddWithValue("@login", login);
                command.Parameters.AddWithValue("@pwd", pwd);
                MySqlDataReader reader = command.ExecuteReader();
                bool authentifie = reader.HasRows;
                connection.Close();
                return authentifie;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}