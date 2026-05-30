using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MediaTek86.controller;
using MediaTek86.modele;

namespace MediaTek86.vue
{
    /// <summary>
    /// Formulaire de gestion du personnel
    /// </summary>
    public partial class FrmPersonnel : Form
    {
        /// <summary>
        /// Contrôleur du formulaire
        /// </summary>
        private readonly FrmPersonnelController controller;

        /// <summary>
        /// Liste du personnel pour le binding
        /// </summary>
        private BindingSource bdgPersonnel = new BindingSource();

        /// <summary>
        /// Constructeur
        /// </summary>
        public FrmPersonnel()
        {
            InitializeComponent();
            controller = new FrmPersonnelController();
            RemplirListePersonnel();
        }

        /// <summary>
        /// Remplit le DataGridView avec la liste du personnel
        /// </summary>
        private void RemplirListePersonnel()
        {
            List<Personnel> lePersonnel = controller.GetLePersonnel();
            bdgPersonnel.DataSource = lePersonnel;
            dgvPersonnel.DataSource = bdgPersonnel;

            // Masquer la colonne IdPersonnel
            if (dgvPersonnel.Columns.Contains("IdPersonnel"))
                dgvPersonnel.Columns["IdPersonnel"].Visible = false;

            // Renommer les colonnes
            if (dgvPersonnel.Columns.Contains("Nom"))
                dgvPersonnel.Columns["Nom"].HeaderText = "Nom";
            if (dgvPersonnel.Columns.Contains("Prenom"))
                dgvPersonnel.Columns["Prenom"].HeaderText = "Prénom";
            if (dgvPersonnel.Columns.Contains("Tel"))
                dgvPersonnel.Columns["Tel"].HeaderText = "Téléphone";
            if (dgvPersonnel.Columns.Contains("Mail"))
                dgvPersonnel.Columns["Mail"].HeaderText = "Mail";
            if (dgvPersonnel.Columns.Contains("Service"))
                dgvPersonnel.Columns["Service"].HeaderText = "Service";
        }
    }
}