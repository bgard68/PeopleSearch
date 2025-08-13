namespace PeopleSearch

{
    partial class Form1
    {
        private System.Windows.Forms.MaskedTextBox maskedTextBoxPhoneNumber;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxCellNumber;

        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            labelFirstName = new Label();
            labelMI = new Label();
            labelLastName = new Label();
            labelPhoneNumber = new Label();
            labelCellNumber = new Label();
            labelEmail = new Label();
            textBoxFirstName = new TextBox();
            textBoxMI = new TextBox();
            textBoxLastName = new TextBox();
            textBoxPhoneNumber = new TextBox();
            textBoxCellNumber = new TextBox();
            textBoxEmail = new TextBox();
            buttonSearch = new Button();
            dataGridViewResults = new DataGridView();
            errorProvider = new ErrorProvider(components);
            maskedTextBoxPhoneNumber = new MaskedTextBox();
            maskedTextBoxCellNumber = new MaskedTextBox();
            buttonClear = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewResults).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // labelFirstName
            // 
            labelFirstName.AutoSize = true;
            labelFirstName.Location = new Point(30, 30);
            labelFirstName.Name = "labelFirstName";
            labelFirstName.Size = new Size(89, 21);
            labelFirstName.TabIndex = 0;
            labelFirstName.Text = "First Name:";
            // 
            // labelMI
            // 
            labelMI.AutoSize = true;
            labelMI.Location = new Point(409, 30);
            labelMI.Name = "labelMI";
            labelMI.Size = new Size(31, 21);
            labelMI.TabIndex = 1;
            labelMI.Text = "MI:";
            // 
            // labelLastName
            // 
            labelLastName.AutoSize = true;
            labelLastName.Location = new Point(30, 70);
            labelLastName.Name = "labelLastName";
            labelLastName.Size = new Size(87, 21);
            labelLastName.TabIndex = 2;
            labelLastName.Text = "Last Name:";
            // 
            // labelPhoneNumber
            // 
            labelPhoneNumber.AutoSize = true;
            labelPhoneNumber.Location = new Point(330, 70);
            labelPhoneNumber.Name = "labelPhoneNumber";
            labelPhoneNumber.Size = new Size(119, 21);
            labelPhoneNumber.TabIndex = 3;
            labelPhoneNumber.Text = "Phone Number:";
            // 
            // labelCellNumber
            // 
            labelCellNumber.AutoSize = true;
            labelCellNumber.Location = new Point(30, 110);
            labelCellNumber.Name = "labelCellNumber";
            labelCellNumber.Size = new Size(101, 21);
            labelCellNumber.TabIndex = 4;
            labelCellNumber.Text = "Cell Number:";
            // 
            // labelEmail
            // 
            labelEmail.AutoSize = true;
            labelEmail.Location = new Point(330, 110);
            labelEmail.Name = "labelEmail";
            labelEmail.Size = new Size(51, 21);
            labelEmail.TabIndex = 5;
            labelEmail.Text = "Email:";
            // 
            // textBoxFirstName
            // 
            textBoxFirstName.Location = new Point(130, 27);
            textBoxFirstName.Name = "textBoxFirstName";
            textBoxFirstName.Size = new Size(180, 29);
            textBoxFirstName.TabIndex = 0;
            textBoxFirstName.Validating += textBox_Validating;
            // 
            // textBoxMI
            // 
            textBoxMI.Location = new Point(455, 30);
            textBoxMI.Name = "textBoxMI";
            textBoxMI.Size = new Size(180, 29);
            textBoxMI.TabIndex = 1;
            textBoxMI.Validating += textBox_Validating;
            // 
            // textBoxLastName
            // 
            textBoxLastName.Location = new Point(130, 67);
            textBoxLastName.Name = "textBoxLastName";
            textBoxLastName.Size = new Size(180, 29);
            textBoxLastName.TabIndex = 2;
            textBoxLastName.Validating += textBox_Validating;
            // 
            // textBoxPhoneNumber
            // 
            textBoxPhoneNumber.Location = new Point(0, 0);
            textBoxPhoneNumber.Name = "textBoxPhoneNumber";
            textBoxPhoneNumber.Size = new Size(100, 29);
            textBoxPhoneNumber.TabIndex = 0;
            // 
            // textBoxCellNumber
            // 
            textBoxCellNumber.Location = new Point(0, 0);
            textBoxCellNumber.Name = "textBoxCellNumber";
            textBoxCellNumber.Size = new Size(100, 29);
            textBoxCellNumber.TabIndex = 0;
            // 
            // textBoxEmail
            // 
            textBoxEmail.Location = new Point(455, 102);
            textBoxEmail.Name = "textBoxEmail";
            textBoxEmail.Size = new Size(180, 29);
            textBoxEmail.TabIndex = 5;
            textBoxEmail.TextChanged += TextBoxEmail_TextChanged;
            textBoxEmail.Validating += textBox_Validating;
            // 
            // buttonSearch
            // 
            buttonSearch.Location = new Point(716, 30);
            buttonSearch.Name = "buttonSearch";
            buttonSearch.Size = new Size(120, 32);
            buttonSearch.TabIndex = 6;
            buttonSearch.Text = "Search";
            buttonSearch.UseVisualStyleBackColor = true;
            buttonSearch.Click += buttonSearch_Click;
            // 
            // dataGridViewResults
            // 
            dataGridViewResults.AllowUserToAddRows = false;
            dataGridViewResults.AllowUserToDeleteRows = false;
            dataGridViewResults.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewResults.Location = new Point(30, 160);
            dataGridViewResults.Name = "dataGridViewResults";
            dataGridViewResults.ReadOnly = true;
            dataGridViewResults.RowTemplate.Height = 29;
            dataGridViewResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewResults.Size = new Size(806, 260);
            dataGridViewResults.TabIndex = 7;
            dataGridViewResults.CellClick += dataGridViewResults_CellClick;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // maskedTextBoxPhoneNumber
            // 
            maskedTextBoxPhoneNumber.Location = new Point(455, 67);
            maskedTextBoxPhoneNumber.Mask = "(999) 000-0000";
            maskedTextBoxPhoneNumber.Name = "maskedTextBoxPhoneNumber";
            maskedTextBoxPhoneNumber.Size = new Size(180, 29);
            maskedTextBoxPhoneNumber.TabIndex = 3;
            // 
            // maskedTextBoxCellNumber
            // 
            maskedTextBoxCellNumber.Location = new Point(130, 107);
            maskedTextBoxCellNumber.Mask = "(999) 999-9999";
            maskedTextBoxCellNumber.Name = "maskedTextBoxCellNumber";
            maskedTextBoxCellNumber.Size = new Size(180, 29);
            maskedTextBoxCellNumber.TabIndex = 4;
            // 
            // buttonClear
            // 
            buttonClear.Location = new Point(716, 68);
            buttonClear.Name = "buttonClear";
            buttonClear.Size = new Size(120, 34);
            buttonClear.TabIndex = 8;
            buttonClear.Text = "Refresh";
            buttonClear.UseVisualStyleBackColor = true;
            buttonClear.Click += buttonClear_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(866, 450);
            Controls.Add(buttonClear);
            Controls.Add(labelFirstName);
            Controls.Add(textBoxFirstName);
            Controls.Add(labelMI);
            Controls.Add(textBoxMI);
            Controls.Add(labelLastName);
            Controls.Add(textBoxLastName);
            Controls.Add(labelPhoneNumber);
            Controls.Add(maskedTextBoxPhoneNumber);
            Controls.Add(labelCellNumber);
            Controls.Add(maskedTextBoxCellNumber);
            Controls.Add(labelEmail);
            Controls.Add(textBoxEmail);
            Controls.Add(buttonSearch);
            Controls.Add(dataGridViewResults);
            Name = "Form1";
            Text = "Person Search";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewResults).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label labelFirstName;
        private System.Windows.Forms.Label labelMI;
        private System.Windows.Forms.Label labelLastName;
        private System.Windows.Forms.TextBox textBoxFirstName;
        private System.Windows.Forms.TextBox textBoxMI;
        private System.Windows.Forms.TextBox textBoxLastName;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.DataGridView dataGridViewResults;
        private System.Windows.Forms.ErrorProvider errorProvider;
        // Add fields for new controls
        private TextBox textBoxPhoneNumber;
        private TextBox textBoxCellNumber;
        private TextBox textBoxEmail;
        private Label labelPhoneNumber;
        private Label labelCellNumber;
        private Label labelEmail;
        private Button buttonClear;
    }

}
#endregion