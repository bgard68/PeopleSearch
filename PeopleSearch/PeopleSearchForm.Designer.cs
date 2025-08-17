namespace PeopleSearch

{
    partial class PeopleSearchForm
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
            labelPhoneNumber = new Label();
            labelCellNumber = new Label();
            textBoxFirstName = new TextBox();
            textBoxMI = new TextBox();
            textBoxLastName = new TextBox();
            textBoxPhoneNumber = new TextBox();
            textBoxCellNumber = new TextBox();
            textBoxEmail = new TextBox();
            ButtonSearch = new Button();
            dataGridViewResults = new DataGridView();
            errorProvider = new ErrorProvider(components);
            maskedTextBoxPhoneNumber = new MaskedTextBox();
            maskedTextBoxCellNumber = new MaskedTextBox();
            ButtonClear = new Button();
            textBoxStreetAddress = new TextBox();
            textBoxCity = new TextBox();
            comboBoxState = new ComboBox();
            textBoxZipCode = new TextBox();
            lblState = new Label();
            ButtonAdd = new Button();
            ButtonDelete = new Button();
            ButtonEdit = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewResults).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // labelPhoneNumber
            // 
            labelPhoneNumber.AutoSize = true;
            labelPhoneNumber.Location = new Point(317, 96);
            labelPhoneNumber.Name = "labelPhoneNumber";
            labelPhoneNumber.Size = new Size(119, 21);
            labelPhoneNumber.TabIndex = 0;
            labelPhoneNumber.Text = "Phone Number:";
            // 
            // labelCellNumber
            // 
            labelCellNumber.AutoSize = true;
            labelCellNumber.Location = new Point(585, 96);
            labelCellNumber.Name = "labelCellNumber";
            labelCellNumber.Size = new Size(101, 21);
            labelCellNumber.TabIndex = 0;
            labelCellNumber.Text = "Cell Number:";
            // 
            // textBoxFirstName
            // 
            textBoxFirstName.Location = new Point(41, 23);
            textBoxFirstName.Name = "textBoxFirstName";
            textBoxFirstName.PlaceholderText = "First Name";
            textBoxFirstName.Size = new Size(395, 29);
            textBoxFirstName.TabIndex = 1;
            textBoxFirstName.TextChanged += TextBox_TextChanged;
            textBoxFirstName.Validating += textBox_Validating;
            // 
            // textBoxMI
            // 
            textBoxMI.Location = new Point(468, 23);
            textBoxMI.Name = "textBoxMI";
            textBoxMI.PlaceholderText = "MI";
            textBoxMI.Size = new Size(42, 29);
            textBoxMI.TabIndex = 2;
            textBoxMI.TextChanged += TextBox_TextChanged;
            textBoxMI.Validating += textBox_Validating;
            // 
            // textBoxLastName
            // 
            textBoxLastName.Location = new Point(527, 23);
            textBoxLastName.Name = "textBoxLastName";
            textBoxLastName.PlaceholderText = "Last Name";
            textBoxLastName.Size = new Size(357, 29);
            textBoxLastName.TabIndex = 3;
            textBoxLastName.TextChanged += TextBox_TextChanged;
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
            textBoxEmail.Location = new Point(41, 93);
            textBoxEmail.Name = "textBoxEmail";
            textBoxEmail.PlaceholderText = "Email";
            textBoxEmail.Size = new Size(255, 29);
            textBoxEmail.TabIndex = 8;
            textBoxEmail.TextChanged += TextBox_TextChanged;
            textBoxEmail.Validating += textBox_Validating;
            // 
            // ButtonSearch
            // 
            ButtonSearch.Location = new Point(1001, 12);
            ButtonSearch.Name = "ButtonSearch";
            ButtonSearch.Size = new Size(120, 32);
            ButtonSearch.TabIndex = 11;
            ButtonSearch.Text = "Search";
            ButtonSearch.UseVisualStyleBackColor = true;
            ButtonSearch.Click += ButtonSearch_Click;
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
            dataGridViewResults.Size = new Size(1261, 562);
            dataGridViewResults.TabIndex = 0;
            dataGridViewResults.TabStop = false;
            dataGridViewResults.CellClick += dataGridViewResults_CellClick;
            dataGridViewResults.CellContentClick += dataGridViewResults_CellContentClick;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // maskedTextBoxPhoneNumber
            // 
            maskedTextBoxPhoneNumber.Location = new Point(442, 93);
            maskedTextBoxPhoneNumber.Mask = "(999) 000-0000";
            maskedTextBoxPhoneNumber.Name = "maskedTextBoxPhoneNumber";
            maskedTextBoxPhoneNumber.Size = new Size(137, 29);
            maskedTextBoxPhoneNumber.TabIndex = 9;
            // 
            // maskedTextBoxCellNumber
            // 
            maskedTextBoxCellNumber.Location = new Point(692, 93);
            maskedTextBoxCellNumber.Mask = "(999) 999-9999";
            maskedTextBoxCellNumber.Name = "maskedTextBoxCellNumber";
            maskedTextBoxCellNumber.Size = new Size(132, 29);
            maskedTextBoxCellNumber.TabIndex = 10;
            // 
            // ButtonClear
            // 
            ButtonClear.Location = new Point(1001, 48);
            ButtonClear.Name = "ButtonClear";
            ButtonClear.Size = new Size(120, 34);
            ButtonClear.TabIndex = 12;
            ButtonClear.Text = "Refresh";
            ButtonClear.UseVisualStyleBackColor = true;
            ButtonClear.Click += ButtonClear_Click;
            // 
            // textBoxStreetAddress
            // 
            textBoxStreetAddress.Location = new Point(41, 58);
            textBoxStreetAddress.Name = "textBoxStreetAddress";
            textBoxStreetAddress.PlaceholderText = "Street Address";
            textBoxStreetAddress.Size = new Size(395, 29);
            textBoxStreetAddress.TabIndex = 4;
            textBoxStreetAddress.TextChanged += TextBox_TextChanged;
            // 
            // textBoxCity
            // 
            textBoxCity.Location = new Point(468, 58);
            textBoxCity.Name = "textBoxCity";
            textBoxCity.PlaceholderText = "City";
            textBoxCity.Size = new Size(212, 29);
            textBoxCity.TabIndex = 5;
            textBoxCity.TextChanged += TextBox_TextChanged;
            // 
            // comboBoxState
            // 
            comboBoxState.Location = new Point(763, 58);
            comboBoxState.Name = "comboBoxState";
            comboBoxState.Size = new Size(54, 29);
            comboBoxState.TabIndex = 6;
            // 
            // textBoxZipCode
            // 
            textBoxZipCode.Location = new Point(841, 58);
            textBoxZipCode.Name = "textBoxZipCode";
            textBoxZipCode.PlaceholderText = "Zip Code";
            textBoxZipCode.Size = new Size(100, 29);
            textBoxZipCode.TabIndex = 7;
            textBoxZipCode.TextChanged += TextBox_TextChanged;
            textBoxZipCode.KeyPress += TextBoxZipCode_KeyPress;
            // 
            // lblState
            // 
            lblState.AutoSize = true;
            lblState.Location = new Point(710, 61);
            lblState.Name = "lblState";
            lblState.Size = new Size(47, 21);
            lblState.TabIndex = 0;
            lblState.Text = "State:";
            lblState.Click += label1_Click;
            // 
            // ButtonAdd
            // 
            ButtonAdd.Location = new Point(1152, 55);
            ButtonAdd.Name = "ButtonAdd";
            ButtonAdd.Size = new Size(120, 41);
            ButtonAdd.TabIndex = 14;
            ButtonAdd.Text = "Add";
            ButtonAdd.UseVisualStyleBackColor = true;
            ButtonAdd.Click += ButtonAdd_Click;
            // 
            // ButtonDelete
            // 
            ButtonDelete.Location = new Point(1152, 102);
            ButtonDelete.Name = "ButtonDelete";
            ButtonDelete.Size = new Size(120, 41);
            ButtonDelete.TabIndex = 15;
            ButtonDelete.Text = "Delete";
            ButtonDelete.UseVisualStyleBackColor = true;
            ButtonDelete.Click += ButtonDelete_Click;
            // 
            // ButtonEdit
            // 
            ButtonEdit.Location = new Point(1152, 8);
            ButtonEdit.Name = "ButtonEdit";
            ButtonEdit.Size = new Size(120, 41);
            ButtonEdit.TabIndex = 13;
            ButtonEdit.Text = "Save";
            ButtonEdit.UseVisualStyleBackColor = true;
            ButtonEdit.Click += ButtonEdit_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1321, 752);
            Controls.Add(ButtonEdit);
            Controls.Add(ButtonDelete);
            Controls.Add(ButtonAdd);
            Controls.Add(lblState);
            Controls.Add(ButtonClear);
            Controls.Add(textBoxFirstName);
            Controls.Add(textBoxMI);
            Controls.Add(textBoxLastName);
            Controls.Add(labelPhoneNumber);
            Controls.Add(maskedTextBoxPhoneNumber);
            Controls.Add(labelCellNumber);
            Controls.Add(maskedTextBoxCellNumber);
            Controls.Add(textBoxStreetAddress);
            Controls.Add(textBoxCity);
            Controls.Add(comboBoxState);
            Controls.Add(textBoxZipCode);
            Controls.Add(textBoxEmail);
            Controls.Add(ButtonSearch);
            Controls.Add(dataGridViewResults);
            Name = "PeopleSearchForm";
            Text = "Person Search";
            Load += PeopleSearchForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewResults).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
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
        private Button ButtonClear;
        private TextBox textBoxStreetAddress;
        private TextBox textBoxCity;
        private ComboBox comboBoxState;
        private TextBox textBoxZipCode;
        private Label lblState;
        private Button ButtonDelete;
        private Button ButtonAdd;
        private Button ButtonEdit;
        private Button ButtonSearch;
    }

}
#endregion