using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;

namespace PeopleSearch
{
    public partial class Form1 : Form
    {
        private readonly IPeopleService _peopleService;
        private readonly MessagesConfig _messages;

        // Add this event handler method to your Form1 class to fix CS0103
        private void textBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == textBoxFirstName)
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    errorProvider.SetError(textBox, _messages.FirstNameRequired);
                    e.Cancel = true;
                }
                else
                {
                    errorProvider.SetError(textBox, string.Empty);
                }
            }
            else if (textBox == textBoxMI)
            {
                // Middle name: allow empty or 1 character only
                if (!string.IsNullOrEmpty(textBox.Text) && textBox.Text.Length > 1)
                {
                    errorProvider.SetError(textBox, _messages.MiddleNameInvalid);
                    e.Cancel = true;
                }
                else
                {
                    errorProvider.SetError(textBox, string.Empty);
                }
            }
            else if (textBox == textBoxLastName)
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    errorProvider.SetError(textBox, _messages.LastNameRequired);
                    e.Cancel = true;
                }
                else
                {
                    errorProvider.SetError(textBox, string.Empty);
                }
            }
            else if (textBox == textBoxEmail)
            {
                if (!string.IsNullOrWhiteSpace(textBox.Text) && !IsValidEmail(textBox.Text))
                {
                    errorProvider.SetError(textBox, _messages.EmailInvalid);
                    e.Cancel = true;
                }
                else
                {
                    errorProvider.SetError(textBox, string.Empty);
                }
            }
            // Add similar blocks for other textboxes if needed
        }
        // Add this method to handle the buttonSearch.Click event
        private void buttonSearch_Click(object sender, EventArgs e)
        {

            string firstName = textBoxFirstName.Text.Trim();
            string mi = textBoxMI.Text.Trim();
            string lastName = textBoxLastName.Text.Trim();

            if (string.IsNullOrWhiteSpace(firstName) &&
                string.IsNullOrWhiteSpace(mi) &&
                string.IsNullOrWhiteSpace(lastName))
            {
                MessageBox.Show(_messages.NoSearchCriteria, "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);

                var allPeople = _peopleService.GetAllPeople().ToList();
                dataGridViewResults.DataSource = new BindingList<Person>(allPeople);
                return;
            }

            var results = _peopleService.Search(firstName, mi, lastName).ToList();

            if (results.Count == 0)
            {
                MessageBox.Show(_messages.NoMatchingRecords, "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            dataGridViewResults.DataSource = new BindingList<Person>(results);
        }
        // Add this method to handle the CellClick event for dataGridViewResults
        private void dataGridViewResults_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure a valid row is selected
            {
                var row = dataGridViewResults.Rows[e.RowIndex];
                var person = row.DataBoundItem as Person;
                if (person != null)
                {
                    DisplayFields(person);
                }
            }
        }
        public Form1(IPeopleService peopleService, IConfiguration configuration)
        {
            _peopleService = peopleService;
            InitializeComponent();

            // Load messages from configuration
            _messages = configuration.GetSection("Messages").Get<MessagesConfig>() ?? new MessagesConfig();

            // Disable validation so closing is never blocked
            this.AutoValidate = AutoValidate.Disable;

            // Attach the Validating event handler to the text boxes
            textBoxFirstName.Validating += textBox_Validating;
            textBoxMI.Validating += textBox_Validating;
            textBoxLastName.Validating += textBox_Validating;

            // Attach the Clear button event handler
            buttonClear.Click += buttonClear_Click;

            // Set up DataGridView columns
            dataGridViewResults.AutoGenerateColumns = false;
            dataGridViewResults.Columns.Add("PersonId", "Person Id");
            dataGridViewResults.Columns.Add("FirstName", "First Name");
            dataGridViewResults.Columns.Add("MiddleName", "MI");
            dataGridViewResults.Columns.Add("LastName", "Last Name");

            // Set column data properties
            dataGridViewResults.Columns["PersonId"].DataPropertyName = "PersonId";
            dataGridViewResults.Columns["FirstName"].DataPropertyName = "FirstName";
            dataGridViewResults.Columns["MiddleName"].DataPropertyName = "MI";
            dataGridViewResults.Columns["LastName"].DataPropertyName = "LastName";

            //// Set custom column widths for better balance
            //dataGridViewResults.Columns["PersonId"].Width = 80;
            //dataGridViewResults.Columns["FirstName"].Width = 120;
            //dataGridViewResults.Columns["MiddleName"].Width = 50;
            //dataGridViewResults.Columns["LastName"].Width = 120;

            // Optionally, set AutoSizeMode to None to use fixed widths
            foreach (DataGridViewColumn column in dataGridViewResults.Columns)
            {
                // comment this out if you are using custom column widths
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // Use Fill to balance the columns
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                LoadAllPeople();

                //// Populate fields with the first record if available
                //if (peopleList.Count > 0)
                //{
                //    var person = peopleList[0];
                //    textBoxFirstName.Text = person.FirstName;
                //    textBoxMI.Text = person.MI;
                //    textBoxLastName.Text = person.LastName;
                //    maskedTextBoxPhoneNumber.Text = person.PhoneNumber;
                //    maskedTextBoxCellNumber.Text = person.CellNumber;
                //    textBoxEmail.Text = person.Email;
                //}
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show(
                    "Unable to connect to the database. " +
                    "Please check your connection settings. " +
                    "Also, check to see that your database has been created." +
                    " or contact support.",
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                // Optionally log ex.Message for diagnostics
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "An unexpected error occurred: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        private void TextBoxEmail_TextChanged(object sender, EventArgs e)
        {

        }


        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Disable validation so closing is never blocked
            this.AutoValidate = AutoValidate.Disable;
            base.OnFormClosing(e);
        }

        // Update the buttonClear_Click method signature to explicitly allow 'sender' to be nullable
        private void buttonClear_Click(object? sender, EventArgs e)
        {
            ClearInputFields();
            ClearErrorMessages();
            LoadAllPeople();
        }

        private void ClearInputFields()
        {
            textBoxFirstName.Text = string.Empty;
            textBoxMI.Text = string.Empty;
            textBoxLastName.Text = string.Empty;
            maskedTextBoxPhoneNumber.Text = string.Empty;
            maskedTextBoxCellNumber.Text = string.Empty;
            textBoxEmail.Text = string.Empty;
        }

        private void ClearErrorMessages()
        {
            errorProvider.SetError(textBoxFirstName, string.Empty);
            errorProvider.SetError(textBoxMI, string.Empty);
            errorProvider.SetError(textBoxLastName, string.Empty);
            errorProvider.SetError(textBoxEmail, string.Empty);
        }

        private void LoadAllPeople()
        {
            var allPeople = _peopleService.GetAllPeople().ToList();
            dataGridViewResults.DataSource = new BindingList<Person>(allPeople);
        }

        private void DisplayFields(Person person)
        {
            textBoxFirstName.Text = person.FirstName;
            textBoxMI.Text = person.MI;
            textBoxLastName.Text = person.LastName;
            maskedTextBoxPhoneNumber.Text = person.PhoneNumber;
            maskedTextBoxCellNumber.Text = person.CellNumber;
            textBoxEmail.Text = person.Email;
        }

    }
}
