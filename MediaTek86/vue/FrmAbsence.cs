using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MediaTek86.controller;
using MediaTek86.modele;

namespace MediaTek86.vue
{
    /// <summary>
    /// Formulaire de gestion des absences d'un personnel
    /// </summary>
    public partial class FrmAbsence : Form
    {
        /// <summary>
        /// Contrôleur du formulaire
        /// </summary>
        private readonly FrmAbsenceController controller;

        /// <summary>
        /// Personnel concerné
        /// </summary>
        private readonly Personnel personnel;

        /// <summary>
        /// Liste des absences pour le binding
        /// </summary>
        private BindingSource bdgAbsences = new BindingSource();

        /// <summary>
        /// Constructeur
        /// </summary>
        public FrmAbsence(Personnel personnel)
        {
            InitializeComponent();
            controller = new FrmAbsenceController();
            this.personnel = personnel;
            lblNomPersonnel.Text = "Absences de : " + personnel.Nom + " " + personnel.Prenom;
            RemplirListeAbsences();
        }

        /// <summary>
        /// Remplit le DataGridView avec les absences du personnel
        /// </summary>
        private void RemplirListeAbsences()
        {
            List<Absence> lesAbsences = controller.GetLesAbsences(personnel);
            bdgAbsences.DataSource = lesAbsences;
            dgvAbsences.DataSource = bdgAbsences;

            if (dgvAbsences.Columns.Contains("Personnel"))
                dgvAbsences.Columns["Personnel"].Visible = false;
            if (dgvAbsences.Columns.Contains("DateDebut"))
                dgvAbsences.Columns["DateDebut"].HeaderText = "Date de début";
            if (dgvAbsences.Columns.Contains("DateFin"))
                dgvAbsences.Columns["DateFin"].HeaderText = "Date de fin";
            if (dgvAbsences.Columns.Contains("Motif"))
                dgvAbsences.Columns["Motif"].HeaderText = "Motif";
        }

        /// <summary>
        /// Clic sur Ajouter
        /// </summary>
        private void btnAjouter_Click(object sender, EventArgs e)
        {
            FrmAbsenceModif frm = new FrmAbsenceModif(personnel);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                RemplirListeAbsences();
            }
        }

        /// <summary>
        /// Clic sur Modifier
        /// </summary>
        private void btnModifier_Click(object sender, EventArgs e)
        {
            if (dgvAbsences.CurrentRow != null)
            {
                Absence absence = (Absence)dgvAbsences.CurrentRow.DataBoundItem;
                FrmAbsenceModif frm = new FrmAbsenceModif(personnel, absence);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    RemplirListeAbsences();
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une absence.",
                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Clic sur Supprimer
        /// </summary>
        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            if (dgvAbsences.CurrentRow != null)
            {
                Absence absence = (Absence)dgvAbsences.CurrentRow.DataBoundItem;
                DialogResult result = MessageBox.Show(
                    "Voulez-vous vraiment supprimer cette absence ?",
                    "Confirmation de suppression",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    controller.DeleteAbsence(absence);
                    RemplirListeAbsences();
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une absence.",
                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}