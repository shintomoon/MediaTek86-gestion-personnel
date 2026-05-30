using System;
using System.Collections.Generic;
using MediaTek86.bddmanager;
using MediaTek86.modele;
using MySql.Data.MySqlClient;

namespace MediaTek86.dal
{
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

        /// <summary>
        /// Retourne la liste des services
        /// </summary>
        public List<Service> GetLesServices()
        {
            List<Service> lesServices = new List<Service>();
            string req = "SELECT * FROM service ORDER BY nom;";
            try
            {
                MySqlConnection connection = bddManager.GetConnection();
                connection.Open();
                MySqlCommand command = new MySqlCommand(req, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Service service = new Service(
                        (int)reader["idservice"],
                        (string)reader["nom"]
                    );
                    lesServices.Add(service);
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return lesServices;
        }

        /// <summary>
        /// Retourne la liste du personnel
        /// </summary>
        public List<Personnel> GetLePersonnel()
        {
            List<Personnel> lePersonnel = new List<Personnel>();
            string req = "SELECT p.idpersonnel, p.nom, p.prenom, p.tel, p.mail, ";
            req += "s.idservice, s.nom AS nomservice ";
            req += "FROM personnel p JOIN service s ON p.idservice = s.idservice ";
            req += "ORDER BY p.nom, p.prenom;";
            try
            {
                MySqlConnection connection = bddManager.GetConnection();
                connection.Open();
                MySqlCommand command = new MySqlCommand(req, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Service service = new Service(
                        (int)reader["idservice"],
                        (string)reader["nomservice"]
                    );
                    Personnel personnel = new Personnel(
                        (int)reader["idpersonnel"],
                        (string)reader["nom"],
                        (string)reader["prenom"],
                        (string)reader["tel"],
                        (string)reader["mail"],
                        service
                    );
                    lePersonnel.Add(personnel);
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return lePersonnel;
        }

        /// <summary>
        /// Ajoute un personnel dans la base de données
        /// </summary>
        public void AddPersonnel(Personnel personnel)
        {
            string req = "INSERT INTO personnel(nom, prenom, tel, mail, idservice) ";
            req += "VALUES (@nom, @prenom, @tel, @mail, @idservice);";
            try
            {
                MySqlConnection connection = bddManager.GetConnection();
                connection.Open();
                MySqlCommand command = new MySqlCommand(req, connection);
                command.Parameters.AddWithValue("@nom", personnel.Nom);
                command.Parameters.AddWithValue("@prenom", personnel.Prenom);
                command.Parameters.AddWithValue("@tel", personnel.Tel);
                command.Parameters.AddWithValue("@mail", personnel.Mail);
                command.Parameters.AddWithValue("@idservice", personnel.Service.IdService);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Modifie un personnel dans la base de données
        /// </summary>
        public void UpdatePersonnel(Personnel personnel)
        {
            string req = "UPDATE personnel SET nom=@nom, prenom=@prenom, ";
            req += "tel=@tel, mail=@mail, idservice=@idservice ";
            req += "WHERE idpersonnel=@idpersonnel;";
            try
            {
                MySqlConnection connection = bddManager.GetConnection();
                connection.Open();
                MySqlCommand command = new MySqlCommand(req, connection);
                command.Parameters.AddWithValue("@nom", personnel.Nom);
                command.Parameters.AddWithValue("@prenom", personnel.Prenom);
                command.Parameters.AddWithValue("@tel", personnel.Tel);
                command.Parameters.AddWithValue("@mail", personnel.Mail);
                command.Parameters.AddWithValue("@idservice", personnel.Service.IdService);
                command.Parameters.AddWithValue("@idpersonnel", personnel.IdPersonnel);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Supprime un personnel de la base de données
        /// </summary>
        public void DeletePersonnel(Personnel personnel)
        {
            string req = "DELETE FROM personnel WHERE idpersonnel=@idpersonnel;";
            try
            {
                MySqlConnection connection = bddManager.GetConnection();
                connection.Open();
                MySqlCommand command = new MySqlCommand(req, connection);
                command.Parameters.AddWithValue("@idpersonnel", personnel.IdPersonnel);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}