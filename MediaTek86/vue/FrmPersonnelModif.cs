using System;
using System.Windows.Forms;
using MediaTek86.controller;
using MediaTek86.modele;

namespace MediaTek86.vue
{
    /// <summary>
    /// Formulaire d'ajout ou de modification d'un personnel
    /// </summary>
    public partial class FrmPersonnelModif : Form
    {
        /// <summary>
        /// Contrôleur du formulaire
        /// </summary>
        private readonly FrmPersonnelController controller;

        /// <summary>
        /// Personnel à modifier (null si ajout)
        /// </summary>
        private readonly Personnel personnel;

        /// <summary>
        /// Constructeur pour l'ajout
        /// </summary>
        public FrmPersonnelModif()
        {
            InitializeComponent();
            controller = new FrmPersonnelController();
            this.Text = "Ajouter un personnel";
            RemplirComboServices();
        }

        /// <summary>
        /// Constructeur pour la modification
        /// </summary>
        public FrmPersonnelModif(Personnel personnel)
        {
            InitializeComponent();
            controller = new FrmPersonnelController();
            this.personnel = personnel;
            this.Text = "Modifier un personnel";
            RemplirComboServices();
            txtNom.Text = personnel.Nom;
            txtPrenom.Text = personnel.Prenom;
            txtTel.Text = personnel.Tel;
            txtMail.Text = personnel.Mail;
            cboService.SelectedItem = personnel.Service;
        }

        /// <summary>
        /// Remplit la ComboBox avec la liste des services
        /// </summary>
        private void RemplirComboServices()
        {
            cboService.DataSource = controller.GetLesServices();
        }

        /// <summary>
        /// Clic sur Enregistrer
        /// </summary>
        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            if (!ChampsValides()) return;

            Service service = (Service)cboService.SelectedItem;

            if (personnel == null)
            {
                // Mode ajout
                Personnel nouveau = new Personnel(0, txtNom.Text, txtPrenom.Text,
                    txtTel.Text, txtMail.Text, service);
                controller.AddPersonnel(nouveau);
            }
            else
            {
                // Mode modification — demande de confirmation
                DialogResult confirm = MessageBox.Show(
                    "Confirmer l'enregistrement des modifications ?",
                    "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm != DialogResult.Yes) return;

                Personnel modifie = new Personnel(personnel.IdPersonnel,
                    txtNom.Text, txtPrenom.Text, txtTel.Text, txtMail.Text, service);
                controller.UpdatePersonnel(modifie);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Clic sur Annuler
        /// </summary>
        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Vérifie que tous les champs sont remplis
        /// </summary>
        private bool ChampsValides()
        {
            if (txtNom.Text.Equals("") || txtPrenom.Text.Equals("") ||
                txtTel.Text.Equals("") || txtMail.Text.Equals("") ||
                cboService.SelectedItem == null)
            {
                MessageBox.Show("Veuillez remplir tous les champs.",
                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }
    }
}