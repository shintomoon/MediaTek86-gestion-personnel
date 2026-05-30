using System.Collections.Generic;
using MediaTek86.dal;
using MediaTek86.modele;

namespace MediaTek86.controller
{
    /// <summary>
    /// Contrôleur du formulaire de gestion du personnel
    /// </summary>
    public class FrmPersonnelController
    {
        /// <summary>
        /// Instance d'accès aux données
        /// </summary>
        private readonly Access access;

        /// <summary>
        /// Constructeur
        /// </summary>
        public FrmPersonnelController()
        {
            access = Access.GetInstance();
        }

        /// <summary>
        /// Récupère la liste du personnel
        /// </summary>
        /// <returns>Liste du personnel</returns>
        public List<Personnel> GetLePersonnel()
        {
            return access.GetLePersonnel();
        }

        /// <summary>
        /// Récupère la liste des services
        /// </summary>
        /// <returns>Liste des services</returns>
        public List<Service> GetLesServices()
        {
            return access.GetLesServices();
        }
    }
}