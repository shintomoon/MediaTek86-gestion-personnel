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

            if (dgvPersonnel.Columns.Contains("IdPersonnel"))
                dgvPersonnel.Columns["IdPersonnel"].Visible = false;
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

        /// <summary>
        /// Clic sur Ajouter
        /// </summary>
        private void btnAjouter_Click(object sender, EventArgs e)
        {
            FrmPersonnelModif frm = new FrmPersonnelModif();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                RemplirListePersonnel();
            }
        }

        /// <summary>
        /// Clic sur Modifier
        /// </summary>
        private void btnModifier_Click(object sender, EventArgs e)
        {
            if (dgvPersonnel.CurrentRow != null)
            {
                Personnel personnel = (Personnel)dgvPersonnel.CurrentRow.DataBoundItem;
                FrmPersonnelModif frm = new FrmPersonnelModif(personnel);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    RemplirListePersonnel();
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un personnel.",
                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Clic sur Supprimer
        /// </summary>
        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            if (dgvPersonnel.CurrentRow != null)
            {
                Personnel personnel = (Personnel)dgvPersonnel.CurrentRow.DataBoundItem;
                DialogResult result = MessageBox.Show(
                    "Voulez-vous vraiment supprimer " + personnel.Nom + " " + personnel.Prenom + " ?",
                    "Confirmation de suppression",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    controller.DeletePersonnel(personnel);
                    RemplirListePersonnel();
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un personnel.",
                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Clic sur Gérer les absences
        /// </summary>
        private void btnAbsences_Click(object sender, EventArgs e)
        {
            if (dgvPersonnel.CurrentRow != null)
            {
                MessageBox.Show("Fonctionnalité bientôt disponible.",
                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un personnel.",
                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}