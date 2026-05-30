namespace MediaTek86.vue
{
    partial class FrmGestionAbsence
    {
        /// <summary>
        /// Variable necessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisees.
        /// </summary>
        /// <param name="disposing">true si les ressources managees doivent etre supprimees ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code genere par le Concepteur Windows Form

        /// <summary>
        /// Methode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette methode avec l'editeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblPersonnel = new System.Windows.Forms.Label();
            this.dgvAbsences = new System.Windows.Forms.DataGridView();
            this.btnAjouter = new System.Windows.Forms.Button();
            this.btnModifier = new System.Windows.Forms.Button();
            this.btnSupprimer = new System.Windows.Forms.Button();
            this.btnFermer = new System.Windows.Forms.Button();
            this.grpAbsence = new System.Windows.Forms.GroupBox();
            this.lblDatedebut = new System.Windows.Forms.Label();
            this.lblDatefin = new System.Windows.Forms.Label();
            this.lblMotif = new System.Windows.Forms.Label();
            this.dtpDatedebut = new System.Windows.Forms.DateTimePicker();
            this.dtpDatefin = new System.Windows.Forms.DateTimePicker();
            this.cboMotif = new System.Windows.Forms.ComboBox();
            this.btnEnregistrerAbsence = new System.Windows.Forms.Button();
            this.btnAnnulerAbsence = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAbsences)).BeginInit();
            this.grpAbsence.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPersonnel
            // 
            this.lblPersonnel.AutoSize = true;
            this.lblPersonnel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblPersonnel.Location = new System.Drawing.Point(12, 9);
            this.lblPersonnel.Name = "lblPersonnel";
            this.lblPersonnel.Size = new System.Drawing.Size(78, 21);
            this.lblPersonnel.TabIndex = 0;
            this.lblPersonnel.Text = "Absences";
            // 
            // dgvAbsences
            // 
            this.dgvAbsences.AllowUserToAddRows = false;
            this.dgvAbsences.AllowUserToDeleteRows = false;
            this.dgvAbsences.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAbsences.Location = new System.Drawing.Point(12, 45);
            this.dgvAbsences.MultiSelect = false;
            this.dgvAbsences.Name = "dgvAbsences";
            this.dgvAbsences.ReadOnly = true;
            this.dgvAbsences.RowHeadersVisible = false;
            this.dgvAbsences.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAbsences.Size = new System.Drawing.Size(420, 300);
            this.dgvAbsences.TabIndex = 1;
            // 
            // btnAjouter
            // 
            this.btnAjouter.Location = new System.Drawing.Point(450, 45);
            this.btnAjouter.Name = "btnAjouter";
            this.btnAjouter.Size = new System.Drawing.Size(120, 30);
            this.btnAjouter.TabIndex = 2;
            this.btnAjouter.Text = "Ajouter";
            this.btnAjouter.UseVisualStyleBackColor = true;
            this.btnAjouter.Click += new System.EventHandler(this.BtnAjouter_Click);
            // 
            // btnModifier
            // 
            this.btnModifier.Location = new System.Drawing.Point(450, 85);
            this.btnModifier.Name = "btnModifier";
            this.btnModifier.Size = new System.Drawing.Size(120, 30);
            this.btnModifier.TabIndex = 3;
            this.btnModifier.Text = "Modifier";
            this.btnModifier.UseVisualStyleBackColor = true;
            this.btnModifier.Click += new System.EventHandler(this.BtnModifier_Click);
            // 
            // btnSupprimer
            // 
            this.btnSupprimer.Location = new System.Drawing.Point(450, 125);
            this.btnSupprimer.Name = "btnSupprimer";
            this.btnSupprimer.Size = new System.Drawing.Size(120, 30);
            this.btnSupprimer.TabIndex = 4;
            this.btnSupprimer.Text = "Supprimer";
            this.btnSupprimer.UseVisualStyleBackColor = true;
            this.btnSupprimer.Click += new System.EventHandler(this.BtnSupprimer_Click);
            // 
            // btnFermer
            // 
            this.btnFermer.Location = new System.Drawing.Point(450, 315);
            this.btnFermer.Name = "btnFermer";
            this.btnFermer.Size = new System.Drawing.Size(120, 30);
            this.btnFermer.TabIndex = 5;
            this.btnFermer.Text = "Fermer";
            this.btnFermer.UseVisualStyleBackColor = true;
            this.btnFermer.Click += new System.EventHandler(this.BtnFermer_Click);
            // 
            // lblDatedebut
            // 
            this.lblDatedebut.AutoSize = true;
            this.lblDatedebut.Location = new System.Drawing.Point(20, 35);
            this.lblDatedebut.Name = "lblDatedebut";
            this.lblDatedebut.Size = new System.Drawing.Size(76, 13);
            this.lblDatedebut.TabIndex = 0;
            this.lblDatedebut.Text = "Date de debut :";
            // 
            // lblDatefin
            // 
            this.lblDatefin.AutoSize = true;
            this.lblDatefin.Location = new System.Drawing.Point(20, 75);
            this.lblDatefin.Name = "lblDatefin";
            this.lblDatefin.Size = new System.Drawing.Size(64, 13);
            this.lblDatefin.TabIndex = 2;
            this.lblDatefin.Text = "Date de fin :";
            // 
            // lblMotif
            // 
            this.lblMotif.AutoSize = true;
            this.lblMotif.Location = new System.Drawing.Point(20, 115);
            this.lblMotif.Name = "lblMotif";
            this.lblMotif.Size = new System.Drawing.Size(39, 13);
            this.lblMotif.TabIndex = 4;
            this.lblMotif.Text = "Motif :";
            // 
            // dtpDatedebut
            // 
            this.dtpDatedebut.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDatedebut.CustomFormat = "dd/MM/yyyy";
            this.dtpDatedebut.Location = new System.Drawing.Point(120, 31);
            this.dtpDatedebut.Name = "dtpDatedebut";
            this.dtpDatedebut.Size = new System.Drawing.Size(150, 20);
            this.dtpDatedebut.TabIndex = 1;
            // 
            // dtpDatefin
            // 
            this.dtpDatefin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDatefin.CustomFormat = "dd/MM/yyyy";
            this.dtpDatefin.Location = new System.Drawing.Point(120, 71);
            this.dtpDatefin.Name = "dtpDatefin";
            this.dtpDatefin.Size = new System.Drawing.Size(150, 20);
            this.dtpDatefin.TabIndex = 3;
            // 
            // cboMotif
            // 
            this.cboMotif.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMotif.FormattingEnabled = true;
            this.cboMotif.Location = new System.Drawing.Point(120, 111);
            this.cboMotif.Name = "cboMotif";
            this.cboMotif.Size = new System.Drawing.Size(150, 21);
            this.cboMotif.TabIndex = 5;
            // 
            // btnEnregistrerAbsence
            // 
            this.btnEnregistrerAbsence.Location = new System.Drawing.Point(40, 155);
            this.btnEnregistrerAbsence.Name = "btnEnregistrerAbsence";
            this.btnEnregistrerAbsence.Size = new System.Drawing.Size(100, 30);
            this.btnEnregistrerAbsence.TabIndex = 6;
            this.btnEnregistrerAbsence.Text = "Enregistrer";
            this.btnEnregistrerAbsence.UseVisualStyleBackColor = true;
            this.btnEnregistrerAbsence.Click += new System.EventHandler(this.BtnEnregistrerAbsence_Click);
            // 
            // btnAnnulerAbsence
            // 
            this.btnAnnulerAbsence.Location = new System.Drawing.Point(160, 155);
            this.btnAnnulerAbsence.Name = "btnAnnulerAbsence";
            this.btnAnnulerAbsence.Size = new System.Drawing.Size(100, 30);
            this.btnAnnulerAbsence.TabIndex = 7;
            this.btnAnnulerAbsence.Text = "Annuler";
            this.btnAnnulerAbsence.UseVisualStyleBackColor = true;
            this.btnAnnulerAbsence.Click += new System.EventHandler(this.BtnAnnulerAbsence_Click);
            // 
            // grpAbsence
            // 
            this.grpAbsence.Controls.Add(this.lblDatedebut);
            this.grpAbsence.Controls.Add(this.lblDatefin);
            this.grpAbsence.Controls.Add(this.lblMotif);
            this.grpAbsence.Controls.Add(this.dtpDatedebut);
            this.grpAbsence.Controls.Add(this.dtpDatefin);
            this.grpAbsence.Controls.Add(this.cboMotif);
            this.grpAbsence.Controls.Add(this.btnEnregistrerAbsence);
            this.grpAbsence.Controls.Add(this.btnAnnulerAbsence);
            this.grpAbsence.Location = new System.Drawing.Point(90, 90);
            this.grpAbsence.Name = "grpAbsence";
            this.grpAbsence.Size = new System.Drawing.Size(300, 210);
            this.grpAbsence.TabIndex = 6;
            this.grpAbsence.TabStop = false;
            this.grpAbsence.Text = "Fiche absence";
            // 
            // FrmGestionAbsence
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 365);
            this.Controls.Add(this.grpAbsence);
            this.Controls.Add(this.btnFermer);
            this.Controls.Add(this.btnSupprimer);
            this.Controls.Add(this.btnModifier);
            this.Controls.Add(this.btnAjouter);
            this.Controls.Add(this.dgvAbsences);
            this.Controls.Add(this.lblPersonnel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmGestionAbsence";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MediaTek86 - Gestion des absences";
            ((System.ComponentModel.ISupportInitialize)(this.dgvAbsences)).EndInit();
            this.grpAbsence.ResumeLayout(false);
            this.grpAbsence.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblPersonnel;
        private System.Windows.Forms.DataGridView dgvAbsences;
        private System.Windows.Forms.Button btnAjouter;
        private System.Windows.Forms.Button btnModifier;
        private System.Windows.Forms.Button btnSupprimer;
        private System.Windows.Forms.Button btnFermer;
        private System.Windows.Forms.GroupBox grpAbsence;
        private System.Windows.Forms.Label lblDatedebut;
        private System.Windows.Forms.Label lblDatefin;
        private System.Windows.Forms.Label lblMotif;
        private System.Windows.Forms.DateTimePicker dtpDatedebut;
        private System.Windows.Forms.DateTimePicker dtpDatefin;
        private System.Windows.Forms.ComboBox cboMotif;
        private System.Windows.Forms.Button btnEnregistrerAbsence;
        private System.Windows.Forms.Button btnAnnulerAbsence;
    }
}
