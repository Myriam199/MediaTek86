using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MediaTek86.controleur;
using MediaTek86.modele;

namespace MediaTek86.vue
{
    /// <summary>
    /// Fenetre principale de gestion du personnel.
    /// Affiche la liste des personnels et permet de les ajouter, modifier,
    /// supprimer et d'acceder a la gestion de leurs absences.
    /// </summary>
    public partial class FrmGestionPersonnel : Form
    {
        /// <summary>
        /// Controleur de l'application.
        /// </summary>
        private readonly Controle controle;

        /// <summary>
        /// Liste des personnels affiches.
        /// </summary>
        private List<Personnel> lesPersonnels;

        /// <summary>
        /// Indique si on est en cours de modification (true) ou d'ajout (false).
        /// </summary>
        private bool enModification = false;

        /// <summary>
        /// Constructeur : initialise la fenetre, le controleur et charge les donnees.
        /// </summary>
        /// <param name="controle">Le controleur de l'application.</param>
        public FrmGestionPersonnel(Controle controle)
        {
            InitializeComponent();
            this.controle = controle;
            RemplirCboService();
            RemplirListePersonnels();
            GroupePersonnelEnabled(false);
        }

        /// <summary>
        /// Recupere et affiche la liste des personnels dans le DataGridView.
        /// </summary>
        private void RemplirListePersonnels()
        {
            lesPersonnels = controle.GetLesPersonnels();
            BindingSource bs = new BindingSource { DataSource = lesPersonnels };
            dgvPersonnel.DataSource = bs;
            // Masquage des colonnes techniques
            if (dgvPersonnel.Columns.Count > 0)
            {
                dgvPersonnel.Columns["Idpersonnel"].Visible = false;
                dgvPersonnel.Columns["Idservice"].Visible = false;
                dgvPersonnel.Columns["Nom"].HeaderText = "Nom";
                dgvPersonnel.Columns["Prenom"].HeaderText = "Prenom";
                dgvPersonnel.Columns["Tel"].HeaderText = "Telephone";
                dgvPersonnel.Columns["Mail"].HeaderText = "Mail";
                dgvPersonnel.Columns["Service"].HeaderText = "Service";
            }
            dgvPersonnel.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        /// <summary>
        /// Recupere et affiche la liste des services dans la liste deroulante.
        /// </summary>
        private void RemplirCboService()
        {
            List<Service> lesServices = controle.GetLesServices();
            BindingSource bs = new BindingSource { DataSource = lesServices };
            cboService.DataSource = bs;
            cboService.ValueMember = "Idservice";
            cboService.DisplayMember = "Nom";
        }

        /// <summary>
        /// Retourne le personnel selectionne dans le DataGridView.
        /// </summary>
        /// <returns>Le personnel selectionne ou null.</returns>
        private Personnel GetPersonnelSelectionne()
        {
            if (dgvPersonnel.CurrentRow != null)
            {
                return (Personnel)dgvPersonnel.CurrentRow.DataBoundItem;
            }
            return null;
        }

        /// <summary>
        /// Active ou desactive la zone de saisie d'un personnel et inversement
        /// la liste et les boutons de commande.
        /// </summary>
        /// <param name="actif">true pour activer la saisie.</param>
        private void GroupePersonnelEnabled(bool actif)
        {
            grpPersonnel.Enabled = actif;
            grpPersonnel.Visible = actif;
            dgvPersonnel.Enabled = !actif;
            btnAjouter.Enabled = !actif;
            btnModifier.Enabled = !actif;
            btnSupprimer.Enabled = !actif;
            btnGererAbsences.Enabled = !actif;
        }

        /// <summary>
        /// Vide les zones de saisie de la fiche personnel.
        /// </summary>
        private void ViderSaisiePersonnel()
        {
            txtNom.Text = "";
            txtPrenom.Text = "";
            txtTel.Text = "";
            txtMail.Text = "";
            if (cboService.Items.Count > 0)
            {
                cboService.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Clic sur "Ajouter" : prepare la saisie d'un nouveau personnel.
        /// </summary>
        private void BtnAjouter_Click(object sender, EventArgs e)
        {
            enModification = false;
            grpPersonnel.Text = "Ajouter un personnel";
            ViderSaisiePersonnel();
            GroupePersonnelEnabled(true);
            txtNom.Focus();
        }

        /// <summary>
        /// Clic sur "Modifier" : pre-remplit la saisie avec le personnel selectionne.
        /// </summary>
        private void BtnModifier_Click(object sender, EventArgs e)
        {
            Personnel personnel = GetPersonnelSelectionne();
            if (personnel == null)
            {
                MessageBox.Show("Veuillez selectionner un personnel.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            enModification = true;
            grpPersonnel.Text = "Modifier un personnel";
            txtNom.Text = personnel.Nom;
            txtPrenom.Text = personnel.Prenom;
            txtTel.Text = personnel.Tel;
            txtMail.Text = personnel.Mail;
            cboService.SelectedValue = personnel.Idservice;
            GroupePersonnelEnabled(true);
            txtNom.Focus();
        }

        /// <summary>
        /// Clic sur "Enregistrer" : controle la saisie puis ajoute ou modifie
        /// le personnel selon le contexte.
        /// </summary>
        private void BtnEnregistrerPersonnel_Click(object sender, EventArgs e)
        {
            if (txtNom.Text.Trim() == "" || txtPrenom.Text.Trim() == ""
                || txtTel.Text.Trim() == "" || txtMail.Text.Trim() == ""
                || cboService.SelectedItem == null)
            {
                MessageBox.Show("Tous les champs doivent etre remplis.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Service service = (Service)cboService.SelectedItem;
            if (enModification)
            {
                Personnel personnel = GetPersonnelSelectionne();
                DialogResult confirmation = MessageBox.Show(
                    "Confirmez-vous l'enregistrement des modifications ?", "Confirmation",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmation == DialogResult.No)
                {
                    return;
                }
                Personnel personnelModifie = new Personnel(personnel.Idpersonnel,
                    txtNom.Text.Trim(), txtPrenom.Text.Trim(), txtTel.Text.Trim(),
                    txtMail.Text.Trim(), service.Idservice, service.Nom);
                controle.ModifierPersonnel(personnelModifie);
            }
            else
            {
                Personnel nouveauPersonnel = new Personnel(0, txtNom.Text.Trim(),
                    txtPrenom.Text.Trim(), txtTel.Text.Trim(), txtMail.Text.Trim(),
                    service.Idservice, service.Nom);
                controle.AjouterPersonnel(nouveauPersonnel);
            }
            GroupePersonnelEnabled(false);
            RemplirListePersonnels();
        }

        /// <summary>
        /// Clic sur "Annuler" dans la fiche : abandonne la saisie en cours.
        /// </summary>
        private void BtnAnnulerPersonnel_Click(object sender, EventArgs e)
        {
            GroupePersonnelEnabled(false);
        }

        /// <summary>
        /// Clic sur "Supprimer" : demande confirmation puis supprime le personnel.
        /// </summary>
        private void BtnSupprimer_Click(object sender, EventArgs e)
        {
            Personnel personnel = GetPersonnelSelectionne();
            if (personnel == null)
            {
                MessageBox.Show("Veuillez selectionner un personnel.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult confirmation = MessageBox.Show(
                "Voulez-vous vraiment supprimer le personnel " + personnel.Nom + " "
                + personnel.Prenom + " ?\n(Ses absences seront egalement supprimees.)",
                "Confirmation de suppression",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmation == DialogResult.Yes)
            {
                controle.SupprimerPersonnel(personnel);
                RemplirListePersonnels();
            }
        }

        /// <summary>
        /// Clic sur "Gerer les absences" : ouvre la fenetre des absences du
        /// personnel selectionne (cas d'utilisation "afficher les absences").
        /// </summary>
        private void BtnGererAbsences_Click(object sender, EventArgs e)
        {
            Personnel personnel = GetPersonnelSelectionne();
            if (personnel == null)
            {
                MessageBox.Show("Veuillez selectionner un personnel.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            FrmGestionAbsence frmGestionAbsence = new FrmGestionAbsence(controle, personnel);
            frmGestionAbsence.ShowDialog();
        }

        /// <summary>
        /// Fermeture de l'application a la fermeture de la fenetre principale.
        /// </summary>
        private void FrmGestionPersonnel_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
