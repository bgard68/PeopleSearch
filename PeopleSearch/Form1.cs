using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Windows.Forms;

namespace PeopleSearch
{
    public partial class Form1 : Form
    {
        private readonly IStateService _stateService;
        private readonly IPeopleService _peopleService;
        private readonly IAddressService _addressService; // Add this field to your form
        private readonly MessagesConfig _messages;

        // Change the method signature to allow for nullable sender
        private void textBox_Validating(object? sender, System.ComponentModel.CancelEventArgs e)
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
        private void ButtonSearch_Click(object sender, EventArgs e)
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
        public Form1(IStateService stateService, IPeopleService peopleService, IAddressService addressService, IConfiguration configuration)
        {
            _stateService = stateService;
            _peopleService = peopleService;
            _addressService = addressService;
            InitializeComponent();

            // In your Form1 constructor or after InitializeComponent()
            dataGridViewResults.CellFormatting += dataGridViewResults_CellFormatting;

            // Load messages from configuration
            _messages = configuration.GetSection("Messages").Get<MessagesConfig>() ?? new MessagesConfig();

            // Disable validation so closing is never blocked
            this.AutoValidate = AutoValidate.Disable;

            WireUpEvents();
        
            // Set up DataGridView columns
            dataGridViewResults.AutoGenerateColumns = false;
            dataGridViewResults.Columns.Clear();

            dataGridViewResults.Columns.Add("PersonId", "PersonId");
            dataGridViewResults.Columns.Add("FirstName", "FirstName");
            dataGridViewResults.Columns.Add("MiddleName", "MI");
            dataGridViewResults.Columns.Add("LastName", "LastName");
            dataGridViewResults.Columns.Add("PhoneNumber", "PhoneNumber");
            dataGridViewResults.Columns.Add("CellNumber", "CellNumber");
            dataGridViewResults.Columns.Add("Email", "Email");
            dataGridViewResults.Columns.Add("StreetAddress", "StreetAddress");
            dataGridViewResults.Columns.Add("City", "City");
            dataGridViewResults.Columns.Add("ZipCode", "ZipCode");
            dataGridViewResults.Columns.Add("StateAbbr", "State");

            // Set column data properties
            dataGridViewResults.Columns["PersonId"].DataPropertyName = "PersonId";
            dataGridViewResults.Columns["FirstName"].DataPropertyName = "FirstName";
            dataGridViewResults.Columns["MiddleName"].DataPropertyName = "MI";
            dataGridViewResults.Columns["LastName"].DataPropertyName = "LastName";
            dataGridViewResults.Columns["PhoneNumber"].DataPropertyName = "PhoneNumber";
            dataGridViewResults.Columns["CellNumber"].DataPropertyName = "CellNumber";
            dataGridViewResults.Columns["Email"].DataPropertyName = "Email";
            dataGridViewResults.Columns["StreetAddress"].DataPropertyName = "Address.StreetAddress";
            dataGridViewResults.Columns["City"].DataPropertyName = "Address.City";
            dataGridViewResults.Columns["ZipCode"].DataPropertyName = "Address.ZipCode";
            dataGridViewResults.Columns["StateAbbr"].DataPropertyName = "Address.State.StateAbbr"; // or StateAbbr



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
            {                // Load initial data
                LoadAllPeople();
                LoadStates();

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
                ShowDatabaseError(ex);
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

        // Add this method to your Form1 class
        private void dataGridViewResults_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var person = dataGridViewResults.Rows[e.RowIndex].DataBoundItem as Person;
            if (person == null) return;

            var columnName = dataGridViewResults.Columns[e.ColumnIndex].Name;

            if (columnName == "StreetAddress")
            {
                e.Value = person.Address?.StreetAddress ?? "";
                e.FormattingApplied = true;
            }
            else if (columnName == "City")
            {
                e.Value = person.Address?.City ?? "";
                e.FormattingApplied = true;
            }
            else if (columnName == "ZipCode")
            {
                e.Value = person.Address?.ZipCode ?? "";
                e.FormattingApplied = true;
            }
            else if (columnName == "StateAbbr")
            {
                e.Value = person.Address?.State?.StateAbbr ?? "";
                e.FormattingApplied = true;
            }
            else if (columnName == "PhoneNumber" || columnName == "CellNumber")
            {
                if (e.Value is string phone && phone.Length == 10)
                {
                    e.Value = $"({phone.Substring(0, 3)}) {phone.Substring(3, 3)}-{phone.Substring(6, 4)}";
                    e.FormattingApplied = true;
                }
            }
        }

        private void TextBoxEmail_TextChanged(object sender, EventArgs e)
        {

        }


        // Change the method signature to allow for nullable sender
        private void TextBox_TextChanged(object? sender, EventArgs e)
        {
            UpdateButtonStates();
            var textBox = sender as TextBox;
            if (string.IsNullOrEmpty(textBox?.Text))
            {
                // PlaceholderText will automatically be shown
                // No further action needed
            }
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
        private void ButtonClear_Click(object? sender, EventArgs e)
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
            textBoxPhoneNumber.Text = string.Empty;
            textBoxCellNumber.Text = string.Empty;
            textBoxStreetAddress.Text = string.Empty;
            textBoxCity.Text = string.Empty;
            textBoxZipCode.Text = string.Empty;
            comboBoxState.SelectedIndex = -1; // Clear the selected state


        }

        private void ClearErrorMessages()
        {
            foreach (var tb in new[] { textBoxFirstName, textBoxMI, textBoxLastName, textBoxEmail })
                errorProvider.SetError(tb, string.Empty);
        }

        private void LoadAllPeople()
        {
            var allPeople = _peopleService.GetAllPeople().ToList();
            if (allPeople.Count == 0)
            {
                MessageBox.Show(_messages.NoMatchingRecords, "No Records loaded.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                foreach (var person in allPeople)
                {
                    if (person.Address == null && person.AddressId != 0)
                    {
                        var address = _addressService.GetAddressById(person.AddressId);
                        if (address != null)
                        {
                            person.Address = address;
                        }
                        else
                        {
                            // Optionally log or handle missing address
                        }
                    }
                }
                var allPeople1 = _peopleService.GetAllPeople().ToList();
                foreach (var person in allPeople1)
                {
                    Debug.WriteLine($"{person.PersonId}: {person.Address?.StreetAddress}, {person.Address?.State?.StateAbbr}");
                }

            }
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

            Address address = person.Address;
            if (address == null && person.AddressId != 0)
            {
                // Retrieve address from the address service if not loaded
                address = _addressService.GetAddressById(person.AddressId);
            }

            if (address != null)
            {
                textBoxStreetAddress.Text = address.StreetAddress;
                textBoxCity.Text = address.City;
                textBoxZipCode.Text = address.ZipCode;

                var state = _stateService.GetStateById(address.StateId);
                if (state != null)
                {
                    for (int i = 0; i < comboBoxState.Items.Count; i++)
                    {
                        var item = comboBoxState.Items[i] as State;
                        if (item != null && item.StateId == state.StateId)
                        {
                            comboBoxState.SelectedIndex = i;
                            break;
                        }
                    }
                }
                else
                {
                    comboBoxState.SelectedIndex = -1;
                }
            }
            else
            {
                textBoxStreetAddress.Text = string.Empty;
                textBoxCity.Text = string.Empty;
                textBoxZipCode.Text = string.Empty;
                comboBoxState.SelectedIndex = -1;
            }
        }

        private void dataGridViewResults_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void LoadStates()
        {
            var states = _stateService.GetStates()
                .OrderBy(s => s.StateName)
                .ToList();

            comboBoxState.DataSource = states;
            comboBoxState.DisplayMember = "StateAbbr"; // or "StateName" if you want to show state name
            comboBoxState.ValueMember = "StateId";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        // Fix: Change the method signature to match the KeyPressEventHandler delegate (non-nullable sender)
        private void TextBoxMI_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only one character, and only A-Z or a-z
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
            // Prevent more than one character
            if (textBoxMI.Text.Length >= 1 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Change the method signature to allow for nullable sender
        private void TextBoxZipCode_KeyPress(object? sender, KeyPressEventArgs e)
        {
            // Allow only digits and control keys
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            // Prevent more than 5 digits
            if (textBoxZipCode.Text.Length >= 5 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ShowDatabaseError(Exception ex)
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

        private void WireUpEvents()
        {

            textBoxFirstName.TextChanged += TextBox_TextChanged;
            textBoxMI.TextChanged += TextBox_TextChanged;
            textBoxLastName.TextChanged += TextBox_TextChanged;
            textBoxEmail.TextChanged += TextBox_TextChanged;
            textBoxStreetAddress.TextChanged += TextBox_TextChanged;
            textBoxCity.TextChanged += TextBox_TextChanged;
            textBoxZipCode.TextChanged += TextBox_TextChanged;
            maskedTextBoxPhoneNumber.TextChanged += TextBox_TextChanged;
            comboBoxState.SelectedIndexChanged += comboBoxState_SelectedIndexChanged;

            textBoxMI.KeyPress += TextBoxMI_KeyPress;
            textBoxZipCode.KeyPress += TextBoxZipCode_KeyPress;
            ButtonClear.Click += ButtonClear_Click;
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            // Gather input values
            string firstName = textBoxFirstName.Text.Trim();
            string mi = textBoxMI.Text.Trim();
            string lastName = textBoxLastName.Text.Trim();
            string phoneNumber = maskedTextBoxPhoneNumber.Text.Trim();
            string cellNumber = maskedTextBoxCellNumber.Text.Trim();
            string email = textBoxEmail.Text.Trim();
            string streetAddress = textBoxStreetAddress.Text.Trim();
            string city = textBoxCity.Text.Trim();
            int stateId = (comboBoxState.SelectedValue is int id) ? id : 0;
            string zipCode = textBoxZipCode.Text.Trim();

            // Check for existing person
            var existingPeople = _peopleService.Search(firstName, mi, lastName)
                .Where(p => p.PhoneNumber == phoneNumber || p.CellNumber == cellNumber || p.Email == email)
                .ToList();

            if (existingPeople.Any())
            {
                MessageBox.Show(_messages.DuplicateRecord, "Duplicate Record", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get or create address
            var addressToAssign = GetOrCreateAddress(streetAddress, city, stateId, zipCode);
            if (addressToAssign == null)
            {
                MessageBox.Show(_messages.AddressError, "Address Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Create and add the person
            var newPerson = new Person
            {
                FirstName = firstName,
                MI = mi,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                CellNumber = cellNumber,
                Email = email,
                AddressId = addressToAssign.AddressId,
                Address = addressToAssign
            };

            var result = _peopleService.AddPerson(newPerson);
            if (!result.Success)
            {
                MessageBox.Show(result.Message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Person added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadAllPeople();
            ClearInputFields();
        }

        // Helper method for address retrieval/creation
        private Address? GetOrCreateAddress(string streetAddress, string city, int stateId, string zipCode)
        {
            var existingAddresses = _addressService.SearchAddress(streetAddress, city, stateId, zipCode).ToList();
            if (existingAddresses.Any())
            {
                return existingAddresses.First();
            }

            var newAddress = new Address
            {
                StreetAddress = streetAddress,
                City = city,
                StateId = stateId,
                ZipCode = zipCode
            };
            var addressResult = _addressService.AddAddress(newAddress);
            if (!addressResult.Success)
            {
                return null;
            }
            return _addressService.SearchAddress(streetAddress, city, stateId, zipCode).FirstOrDefault();
        }

        private void UpdateButtonStates()
        {
            bool allFieldsFilled =
                !string.IsNullOrWhiteSpace(textBoxFirstName.Text) &&
                !string.IsNullOrWhiteSpace(textBoxLastName.Text) &&
                !string.IsNullOrWhiteSpace(textBoxEmail.Text) &&
                !string.IsNullOrWhiteSpace(maskedTextBoxPhoneNumber.Text) &&
                !string.IsNullOrWhiteSpace(textBoxStreetAddress.Text) &&
                !string.IsNullOrWhiteSpace(textBoxCity.Text) &&
                comboBoxState.SelectedItem != null &&
                !string.IsNullOrWhiteSpace(textBoxZipCode.Text);

            // Optionally, add further validation (e.g., email format, phone number length, zip code length)
            ButtonAdd.Enabled = allFieldsFilled;
            ButtonEdit.Enabled = allFieldsFilled;
        }
        // Add this method to handle the SelectedIndexChanged event for comboBoxState
        private void comboBoxState_SelectedIndexChanged(object? sender, EventArgs e)
        {
            // You can add logic here if you want to respond to state selection changes.
            // For now, this can be left empty or used to update other UI elements as needed.
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {

            // Get input values from textboxes
            string firstName = textBoxFirstName.Text.Trim();
            string mi = textBoxMI.Text.Trim();
            string lastName = textBoxLastName.Text.Trim();

            int personId = 0;

            // Try to find a person in the service that matches the input fields
            var match = _peopleService
                .Search(firstName, mi, lastName)
                .FirstOrDefault(p =>
                    p.FirstName == firstName &&
                    p.MI == mi &&
                    p.LastName == lastName);

            if (match != null)
            {
                personId = match.PersonId;
                _peopleService.DeletePerson(personId);
                MessageBox.Show(_messages.PersonDeletedSuccess, "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputFields();
            }
            else
            {
                MessageBox.Show(_messages.PersonDeletedError, "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            LoadAllPeople();
            // Load the text fields with the first record after refresh
            var allPeople = _peopleService.GetAllPeople().ToList();
            if (allPeople.Count > 0)
            {
                DisplayFields(allPeople[0]);
            }

        }

        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Edit functionality is not yet implemented.", "Edit", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
