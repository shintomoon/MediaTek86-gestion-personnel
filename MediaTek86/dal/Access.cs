using System;
using MediaTek86.bddmanager;

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
    }
}