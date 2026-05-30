using MediaTek86.dal;

namespace MediaTek86.controller
{
    /// <summary>
    /// Package contenant les contrôleurs de l'application.
    /// </summary>
    internal class NamespaceDoc
    {
    }

    /// <summary>
    /// Contrôleur du formulaire de connexion
    /// </summary>
    public class FrmConnexionController
    {
        /// <summary>
        /// Instance d'accès aux données
        /// </summary>
        private readonly Access access;

        /// <summary>
        /// Constructeur
        /// </summary>
        public FrmConnexionController()
        {
            access = Access.GetInstance();
        }

        /// <summary>
        /// Demande à la DAL de contrôler l'authentification
        /// </summary>
        /// <param name="login">Login saisi</param>
        /// <param name="pwd">Mot de passe saisi</param>
        /// <returns>true si authentification réussie</returns>
        public bool ControleAuthentification(string login, string pwd)
        {
            return access.ControleAuthentification(login, pwd);
        }
    }
}