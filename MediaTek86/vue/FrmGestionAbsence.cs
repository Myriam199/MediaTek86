using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MediaTek86.controleur;
using MediaTek86.modele;

namespace MediaTek86.vue
{
    /// <summary>
    /// Fenetre de gestion des absences d'un personnel.
    /// Affiche les absences (de la plus recente a la plus ancienne) et permet
    /// de les ajouter, modifier et supprimer en evitant les chevauchements.
    /// </summary>
    public partial class FrmGestionAbsence : Form
    {
        /// <summary>
        /// Controleur de l'application.
        /// </summary>
        private readonly Controle controle;

        /// <summary>
        /// Personnel dont on gere les absences.
        /// </summary>
        private readonly Personnel personnel;

        /// <summary>
        /// Liste des absences affichees.
        /// </summary>
        private List<Absence> lesAbsences;

        /// <summary>
        /// Indique si on est en cours de modification (true) ou d'ajout (false).
        /// </summary>
        private bool enModification = false;

        /// <summary>
        /// Date de debut d'origine de l'absence en cours de modification
        /// (cle primaire, necessaire pour la mise a jour).
        /// </summary>
        private DateTime ancienneDatedebut;

        /// <summary>
        /// Constructeur : initialise la fenetre pour un personnel donne.
        /// </summary>
        /// <param name="controle">Le controleur de l'application.</param>
        /// <param name="personnel">Le personnel concerne.</param>
        public FrmGestionAbsence(Controle controle, Personnel personnel)
        {
            InitializeComponent();
            this.controle = controle;
            this.personnel = personnel;
            lblPersonnel.Text = "Absences de " + personnel.Nom + " " + personnel.Prenom;
            RemplirCboMotif();
            RemplirListeAbsences();
            GroupeAbsenceEnabled(false);
        }

        /// <summary>
        /// Recupere et affiche la liste des absences du personnel.
        /// </summary>
        private void RemplirListeAbsences()
        {
            lesAbsences = controle.GetLesAbsences(personnel.Idpersonnel);
            BindingSource bs = new BindingSource { DataSource = lesAbsences };
            dgvAbsences.DataSource = bs;
            if (dgvAbsences.Columns.Count > 0)
            {
                dgvAbsences.Columns["Idpersonnel"].Visible = false;
                dgvAbsences.Columns["Idmotif"].Visible = false;
                dgvAbsences.Columns["Datedebut"].HeaderText = "Date de debut";
                dgvAbsences.Columns["Datefin"].HeaderText = "Date de fin";
                dgvAbsences.Columns["Motif"].HeaderText = "Motif";
            }
            dgvAbsences.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        /// <summary>
        /// Recupere et affiche la liste des motifs dans la liste deroulante.
        /// </summary>
        private void RemplirCboMotif()
        {
            List<Motif> lesMotifs = controle.GetLesMotifs();
            BindingSource bs = new BindingSource { DataSource = lesMotifs };
            cboMotif.DataSource = bs;
            cboMotif.ValueMember = "Idmotif";
            cboMotif.DisplayMember = "Libelle";
        }

        /// <summary>
        /// Retourne l'absence selectionnee dans le DataGridView.
        /// </summary>
        /// <returns>L'absence selectionnee ou null.</returns>
        private Absence GetAbsenceSelectionnee()
        {
            if (dgvAbsences.CurrentRow != null)
            {
                return (Absence)dgvAbsences.CurrentRow.DataBoundItem;
            }
            return null;
        }

        /// <summary>
        /// Active ou desactive la zone de saisie d'une absence et inversement
        /// la liste et les boutons de commande.
        /// </summary>
        /// <param name="actif">true pour activer la saisie.</param>
        private void GroupeAbsenceEnabled(bool actif)
        {
            grpAbsence.Enabled = actif;
            grpAbsence.Visible = actif;
            dgvAbsences.Enabled = !actif;
            btnAjouter.Enabled = !actif;
            btnModifier.Enabled = !actif;
            btnSupprimer.Enabled = !actif;
            btnFermer.Enabled = !actif;
        }

        /// <summary>
        /// Verifie qu'une absence ne chevauche pas une absence deja existante.
        /// Deux periodes se chevauchent si debut1 &lt;= fin2 ET debut2 &lt;= fin1.
        /// L'absence en cours de modification est exclue de la comparaison.
        /// </summary>
        /// <param name="datedebut">Date de debut a tester.</param>
        /// <param name="datefin">Date de fin a tester.</param>
        /// <returns>True s'il y a chevauchement, false sinon.</returns>
        private bool ExisteChevauchement(DateTime datedebut, DateTime datefin)
        {
            foreach (Absence absence in lesAbsences)
            {
                // On ignore l'absence en cours de modification
                if (enModification && absence.Datedebut == ancienneDatedebut)
                {
                    continue;
                }
                if (datedebut <= absence.Datefin && absence.Datedebut <= datefin)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Clic sur "Ajouter" : prepare la saisie d'une nouvelle absence.
        /// </summary>
        private void BtnAjouter_Click(object sender, EventArgs e)
        {
            enModification = false;
            grpAbsence.Text = "Ajouter une absence";
            dtpDatedebut.Value = DateTime.Today;
            dtpDatefin.Value = DateTime.Today;
            if (cboMotif.Items.Count > 0)
            {
                cboMotif.SelectedIndex = 0;
            }
            GroupeAbsenceEnabled(true);
        }

        /// <summary>
        /// Clic sur "Modifier" : pre-remplit la saisie avec l'absence selectionnee.
        /// </summary>
        private void BtnModifier_Click(object sender, EventArgs e)
        {
            Absence absence = GetAbsenceSelectionnee();
            if (absence == null)
            {
                MessageBox.Show("Veuillez selectionner une absence.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            enModification = true;
            ancienneDatedebut = absence.Datedebut;
            grpAbsence.Text = "Modifier une absence";
            dtpDatedebut.Value = absence.Datedebut;
            dtpDatefin.Value = absence.Datefin;
            cboMotif.SelectedValue = absence.Idmotif;
            GroupeAbsenceEnabled(true);
        }

        /// <summary>
        /// Clic sur "Enregistrer" : controle la saisie (dates et chevauchement)
        /// puis ajoute ou modifie l'absence selon le contexte.
        /// </summary>
        private void BtnEnregistrerAbsence_Click(object sender, EventArgs e)
        {
            if (cboMotif.SelectedItem == null)
            {
                MessageBox.Show("Tous les champs doivent etre remplis.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DateTime datedebut = dtpDatedebut.Value.Date;
            DateTime datefin = dtpDatefin.Value.Date;
            if (datefin < datedebut)
            {
                MessageBox.Show("La date de fin ne peut pas etre anterieure a la date de debut.",
                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (ExisteChevauchement(datedebut, datefin))
            {
                MessageBox.Show("Une absence est deja programmee sur ce creneau.",
                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Motif motif = (Motif)cboMotif.SelectedItem;
            Absence nouvelleAbsence = new Absence(personnel.Idpersonnel, datedebut,
                datefin, motif.Idmotif, motif.Libelle);
            if (enModification)
            {
                DialogResult confirmation = MessageBox.Show(
                    "Confirmez-vous l'enregistrement des modifications ?", "Confirmation",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmation == DialogResult.No)
                {
                    return;
                }
                controle.ModifierAbsence(nouvelleAbsence, ancienneDatedebut);
            }
            else
            {
                controle.AjouterAbsence(nouvelleAbsence);
            }
            GroupeAbsenceEnabled(false);
            RemplirListeAbsences();
        }

        /// <summary>
        /// Clic sur "Annuler" dans la fiche : abandonne la saisie en cours.
        /// </summary>
        private void BtnAnnulerAbsence_Click(object sender, EventArgs e)
        {
            GroupeAbsenceEnabled(false);
        }

        /// <summary>
        /// Clic sur "Supprimer" : demande confirmation puis supprime l'absence.
        /// </summary>
        private void BtnSupprimer_Click(object sender, EventArgs e)
        {
            Absence absence = GetAbsenceSelectionnee();
            if (absence == null)
            {
                MessageBox.Show("Veuillez selectionner une absence.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult confirmation = MessageBox.Show(
                "Voulez-vous vraiment supprimer cette absence ?",
                "Confirmation de suppression",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmation == DialogResult.Yes)
            {
                controle.SupprimerAbsence(absence);
                RemplirListeAbsences();
            }
        }

        /// <summary>
        /// Clic sur "Fermer" : ferme la fenetre des absences.
        /// </summary>
        private void BtnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
