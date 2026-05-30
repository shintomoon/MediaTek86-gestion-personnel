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
        public List<Personnel> GetLePersonnel()
        {
            return access.GetLePersonnel();
        }

        /// <summary>
        /// Récupère la liste des services
        /// </summary>
        public List<Service> GetLesServices()
        {
            return access.GetLesServices();
        }

        /// <summary>
        /// Demande l'ajout d'un personnel
        /// </summary>
        public void AddPersonnel(Personnel personnel)
        {
            access.AddPersonnel(personnel);
        }

        /// <summary>
        /// Demande la modification d'un personnel
        /// </summary>
        public void UpdatePersonnel(Personnel personnel)
        {
            access.UpdatePersonnel(personnel);
        }

        /// <summary>
        /// Demande la suppression d'un personnel
        /// </summary>
        public void DeletePersonnel(Personnel personnel)
        {
            access.DeletePersonnel(personnel);
        }
    }
}