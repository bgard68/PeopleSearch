using Application.DTOs;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;

namespace PeopleSearch
{
    public partial class PeopleSearchForm : Form
    {
        #region Fields

        // Services for state, people, and address management
        private readonly IStateService _stateService;
        private readonly IPeopleService _peopleService;
        private readonly IAddressService _addressService;
        // Configuration for user messages
        private readonly MessagesConfig _messages;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes the form, sets up services, configures DataGridView, and wires up events.
        /// </summary>
        public PeopleSearchForm(IStateService stateService, IPeopleService peopleService, IAddressService addressService, IConfiguration configuration)
        {
            _stateService = stateService;
            _peopleService = peopleService;
            _addressService = addressService;
            InitializeComponent();
            dataGridViewResults.TabStop = false;

            // Format DataGridView cells for display
            dataGridViewResults.CellFormatting += dataGridViewResults_CellFormatting;

            // Load messages from configuration
            _messages = configuration.GetSection("Messages").Get<MessagesConfig>() ?? new MessagesConfig();

            // Disable validation on close
            this.AutoValidate = AutoValidate.Disable;

            // Wire up control events
            WireUpEvents();

            // Set up DataGridView columns and bindings
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

            // Bind columns to Person properties
            dataGridViewResults.Columns["PersonId"].DataPropertyName = "PersonId";
            dataGridViewResults.Columns["FirstName"].DataPropertyName = "FirstName";
            dataGridViewResults.Columns["MiddleName"].DataPropertyName = "MI";
            dataGridViewResults.Columns["LastName"].DataPropertyName = "LastName";
            dataGridViewResults.Columns["PhoneNumber"].DataPropertyName = "PhoneNumber";
            dataGridViewResults.Columns["CellNumber"].DataPropertyName = "CellNumber";
            dataGridViewResults.Columns["Email"].DataPropertyName = "Email";
            dataGridViewResults.Columns["StreetAddress"].DataPropertyName = "AddressDto.StreetAddress";
            dataGridViewResults.Columns["City"].DataPropertyName = "AddressDto.City";
            dataGridViewResults.Columns["ZipCode"].DataPropertyName = "AddressDto.ZipCode";
            dataGridViewResults.Columns["StateAbbr"].DataPropertyName = "AddressDto.State.StateAbbr";

            // Make columns fill available space
            foreach (DataGridViewColumn column in dataGridViewResults.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        #endregion

        #region Event Wiring

        /// <summary>
        /// Wires up control events for text changes, key presses, and button clicks.
        /// </summary>
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

        #endregion

        #region Form Events

        /// <summary>
        /// Loads initial data and handles database errors on form load.
        /// </summary>
        private void PeopleSearchForm_Load(object sender, EventArgs e)
        {
            try
            {
                LoadAllPeople();
                LoadStates();
                UpdateButtonStates();
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

        /// <summary>
        /// Ensures validation does not block form closing.
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            this.AutoValidate = AutoValidate.Disable;

            var result = MessageBox.Show(
                 _messages.ConfirmExit,
                 "Confirm Exit",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
            base.OnFormClosing(e);

        }

        #endregion

        #region DataGridView Events

        /// <summary>
        /// Handles row selection in the results grid and displays details.
        /// </summary>
        private void dataGridViewResults_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dataGridViewResults.Rows[e.RowIndex];
                var person = row.DataBoundItem as PersonDto;
                if (person != null)
                {
                    DisplayFields(person);
                }
            }
        }

        /// <summary>
        /// Customizes cell formatting for address and phone fields.
        /// </summary>
        private void dataGridViewResults_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var person = dataGridViewResults.Rows[e.RowIndex].DataBoundItem as PersonDto;
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
                // Format phone numbers as (XXX) XXX-XXXX
                if (e.Value is string phone && phone.Length == 10)
                {
                    e.Value = $"({phone.Substring(0, 3)}) {phone.Substring(3, 3)}-{phone.Substring(6, 4)}";
                    e.FormattingApplied = true;
                }
            }
        }

        /// <summary>
        /// Reserved for future use (cell content click).
        /// </summary>
        private void dataGridViewResults_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Reserved for future use
        }

        #endregion

        #region Button Events

        /// <summary>
        /// Searches for people based on input criteria and displays results.
        /// </summary>
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
                dataGridViewResults.DataSource = new BindingList<PersonDto>(allPeople);
                return;
            }

            var results = _peopleService.Search(firstName, mi, lastName).ToList();

            if (results.Count == 0)
            {
                MessageBox.Show(_messages.NoMatchingRecords, "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            dataGridViewResults.DataSource = new BindingList<PersonDto>(results);
        }

        /// <summary>
        /// Clears input fields, error messages, and reloads all people.
        /// </summary>
        private void ButtonClear_Click(object? sender, EventArgs e)
        {
            ClearInputFields();
            ClearErrorMessages();
            LoadAllPeople();
        }

        /// <summary>
        /// Adds a new person record after validation and address assignment.
        /// </summary>
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
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

            // Check for duplicate person
            var existingPeople = _peopleService.Search(firstName, mi, lastName)
                .Where(p => p.PhoneNumber == phoneNumber || p.CellNumber == cellNumber || p.Email == email)
                .ToList();

            if (existingPeople.Any())
            {
                MessageBox.Show(_messages.DuplicateRecord, "Duplicate Record", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get or create address for the person
            var addressToAssign = GetOrCreateAddress(streetAddress, city, stateId, zipCode);
            if (addressToAssign == null)
            {
                MessageBox.Show(_messages.AddressError, "Address Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Create and add the person
            var newPerson = new PersonDto
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

        /// <summary>
        /// Deletes a person record matching the input fields.
        /// </summary>
        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            string firstName = textBoxFirstName.Text.Trim();
            string mi = textBoxMI.Text.Trim();
            string lastName = textBoxLastName.Text.Trim();

            int personId = 0;

            // Find matching person
            var match = _peopleService
                .Search(firstName, mi, lastName)
                .FirstOrDefault(p =>
                    p.FirstName == firstName &&
                    p.MI == mi &&
                    p.LastName == lastName);

            if (match != null)
            {
                // Ask for confirmation before deleting
                var confirmResult = MessageBox.Show(
                    "Are you sure you want to delete this person?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Yes)
                {
                    personId = match.PersonId;
                    _peopleService.DeletePerson(personId);
                    MessageBox.Show(_messages.PersonDeletedSuccess, "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputFields();
                }
            }
            else
            {
                MessageBox.Show(_messages.PersonDeletedError, "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            LoadAllPeople();

            // Display first record after refresh
            var allPeople = _peopleService.GetAllPeople().ToList();
            if (allPeople.Count > 0)
            {
                DisplayFields(allPeople[0]);
            }
        }

        /// <summary>
        /// Placeholder for edit functionality.
        /// </summary>
        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            // MessageBox.Show("Edit functionality is not yet implemented.", "Edit", MessageBoxButtons.OK, MessageBoxIcon.Information);


            // 1. Check if a record is selected
            if (dataGridViewResults.CurrentRow == null || dataGridViewResults.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show(_messages.NoMatchingRecords, "Edit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var person = (PersonDto)dataGridViewResults.CurrentRow.DataBoundItem;

            // 2. Validate required fields
            string firstName = textBoxFirstName.Text.Trim();
            string lastName = textBoxLastName.Text.Trim();
            string streetAddress = textBoxStreetAddress.Text.Trim();
            string city = textBoxCity.Text.Trim();
            int stateId = comboBoxState.SelectedValue is int id ? id : 0;
            string zipCode = textBoxZipCode.Text.Trim();
            string email = textBoxEmail.Text.Trim();
            string phoneNumber = maskedTextBoxPhoneNumber.Text.Trim();

            if (string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(streetAddress) ||
                string.IsNullOrWhiteSpace(city) ||
                stateId == 0 ||
                string.IsNullOrWhiteSpace(zipCode) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(phoneNumber))
            {
                MessageBox.Show(_messages.ValidateError, "Edit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Check address existence
            var existingAddresses = _addressService.SearchAddress(streetAddress, city, stateId, zipCode).ToList();
            HomeAddressDto addressToUse;

            if (existingAddresses.Any())
            {
                addressToUse = existingAddresses.First();
                // Check if other people use this address
                var peopleWithAddress = _peopleService.GetAllPeople().Where(p => p.AddressId == addressToUse.AddressId && p.PersonId != person.PersonId).ToList();

                if (!peopleWithAddress.Any())
                {
                    // No other people use it, update the address
                    addressToUse.StreetAddress = streetAddress;
                    addressToUse.City = city;
                    addressToUse.StateId = stateId;
                    addressToUse.ZipCode = zipCode;
                    _addressService.UpdateAddress(addressToUse);
                }
                else
                {
                    // Other people use it, check for a match
                    var match = existingAddresses.FirstOrDefault(a =>
                        a.StreetAddress == streetAddress &&
                        a.City == city &&
                        a.StateId == stateId &&
                        a.ZipCode == zipCode);

                    if (match == null)
                    {
                        // No match found, add new address
                        var newAddress = new HomeAddressDto
                        {
                            StreetAddress = streetAddress,
                            City = city,
                            StateId = stateId,
                            ZipCode = zipCode
                        };
                        var result = _addressService.AddAddress(newAddress);
                        if (!result.Success)
                        {
                            MessageBox.Show(_messages.AddressError, "Edit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        addressToUse = _addressService.SearchAddress(streetAddress, city, stateId, zipCode).FirstOrDefault();
                    }
                    else
                    {
                        // Match found, update address id in person
                        addressToUse = match;
                    }
                }
            }
            else
            {
                // Address does not exist, add it
                var newAddress = new HomeAddressDto
                {
                    StreetAddress = streetAddress,
                    City = city,
                    StateId = stateId,
                    ZipCode = zipCode
                };
                var result = _addressService.AddAddress(newAddress);
                if (!result.Success)
                {
                    MessageBox.Show(_messages.AddressError, "Edit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                addressToUse = _addressService.SearchAddress(streetAddress, city, stateId, zipCode).FirstOrDefault();
            }

            // Update person record
            person.FirstName = firstName;
            person.LastName = lastName;
            person.Email = email;
            person.PhoneNumber = phoneNumber;
            person.AddressId = addressToUse.AddressId;
            person.Address = addressToUse;

            _peopleService.UpdatePerson(person);

            MessageBox.Show("Person updated successfully.", "Edit", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadAllPeople();

        }

        #endregion

        #region TextBox & ComboBox Events

        /// <summary>
        /// Handles text changes in input fields and updates button states.
        /// </summary>
        private void TextBox_TextChanged(object? sender, EventArgs e)
        {
            UpdateButtonStates();
            var textBox = sender as TextBox;
            if (string.IsNullOrEmpty(textBox?.Text))
            {
                // PlaceholderText will automatically be shown
            }
        }

        /// <summary>
        /// Reserved for future use (email text changed).
        /// </summary>
        private void TextBoxEmail_TextChanged(object sender, EventArgs e)
        {
            // Reserved for future use
        }

        /// <summary>
        /// Restricts middle initial to a single letter.
        /// </summary>
        private void TextBoxMI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
            if (textBoxMI.Text.Length >= 1 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Restricts zip code input to 5 digits.
        /// </summary>
        private void TextBoxZipCode_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            if (textBoxZipCode.Text.Length >= 5 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Reserved for future use (state selection changed).
        /// </summary>
        private void comboBoxState_SelectedIndexChanged(object? sender, EventArgs e)
        {
            // Reserved for future use
            UpdateButtonStates();
        }


        /// <summary>
        /// Reserved for future use (label click).
        /// </summary>
        private void label1_Click(object sender, EventArgs e)
        {
            // Reserved for future use
        }

        #endregion

        #region Validation

        /// <summary>
        /// Validates input fields and sets error messages.
        /// </summary>
        private void textBox_Validating(object? sender, CancelEventArgs e)
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
        }

        /// <summary>
        /// Checks if an email address is valid.
        /// </summary>
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

        #endregion

        #region UI Helpers

        /// <summary>
        /// Clears all input fields on the form.
        /// </summary>
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
            comboBoxState.SelectedIndex = -1;
            dataGridViewResults.ClearSelection();
            UpdateButtonStates();
        }

        /// <summary>
        /// Clears all error messages from the form.
        /// </summary>
        private void ClearErrorMessages()
        {
            foreach (var tb in new[] { textBoxFirstName, textBoxMI, textBoxLastName, textBoxEmail })
                errorProvider.SetError(tb, string.Empty);
        }

        /// <summary>
        /// Enables or disables Add/Edit buttons based on field completion.
        /// </summary>
        private void UpdateButtonStates()
        {


            var requiredTextBoxes = new[]
  {
        textBoxFirstName,
        textBoxLastName,
        textBoxStreetAddress,
        textBoxCity,
        textBoxZipCode,
        textBoxEmail
    };

            bool allFieldsFilled = true;

            foreach (var tb in requiredTextBoxes)
            {
                string placeholder = tb.Tag as string ?? string.Empty;
                if (string.IsNullOrWhiteSpace(tb.Text) || tb.Text == placeholder)
                {
                    allFieldsFilled = false;
                    break;
                }
            }

            // Phone number: check mask is filled
            if (!maskedTextBoxPhoneNumber.MaskFull)
                allFieldsFilled = false;

            // State: check selection
            if (comboBoxState.SelectedItem == null)
                allFieldsFilled = false;

            // bool recordSelected = dataGridViewResults.CurrentRow != null && dataGridViewResults.CurrentRow.DataBoundItem != null;

            //        bool allFieldsFilled =
            //!string.IsNullOrWhiteSpace(textBoxFirstName.Text) &&
            //textBoxFirstName.Text != textBoxFirstName.PlaceholderText &&
            //!string.IsNullOrWhiteSpace(textBoxLastName.Text) &&
            //textBoxLastName.Text != textBoxLastName.PlaceholderText &&
            //!string.IsNullOrWhiteSpace(textBoxStreetAddress.Text) &&
            //textBoxStreetAddress.Text != textBoxStreetAddress.PlaceholderText &&
            //!string.IsNullOrWhiteSpace(textBoxCity.Text) &&
            //textBoxCity.Text != textBoxCity.PlaceholderText &&
            //!string.IsNullOrWhiteSpace(textBoxZipCode.Text) &&
            //textBoxZipCode.Text != textBoxZipCode.PlaceholderText &&
            //!string.IsNullOrWhiteSpace(textBoxEmail.Text) &&
            //textBoxEmail.Text != textBoxEmail.PlaceholderText &&
            //!string.IsNullOrWhiteSpace(maskedTextBoxPhoneNumber.Text) &&
            //comboBoxState.SelectedItem != null;

            //bool allFieldsFilled =
            //    !string.IsNullOrWhiteSpace(textBoxFirstName.Text) &&
            //    !string.IsNullOrWhiteSpace(textBoxLastName.Text) &&
            //    !string.IsNullOrWhiteSpace(textBoxEmail.Text) &&
            //    !string.IsNullOrWhiteSpace(maskedTextBoxPhoneNumber.Text) &&
            //    !string.IsNullOrWhiteSpace(textBoxStreetAddress.Text) &&
            //    !string.IsNullOrWhiteSpace(textBoxCity.Text) &&
            //    comboBoxState.SelectedItem != null &&
            //    !string.IsNullOrWhiteSpace(textBoxZipCode.Text);

            ButtonAdd.Enabled = allFieldsFilled;
            ButtonDelete.Enabled = allFieldsFilled;
            ButtonEdit.Enabled = allFieldsFilled;
            UpdateFieldBorders();
        }


        private void UpdateFieldBorders()
        {
            // First Name
            textBoxFirstName.BackColor = string.IsNullOrWhiteSpace(textBoxFirstName.Text)
                ? Color.MistyRose : SystemColors.Window;

            // Last Name
            textBoxLastName.BackColor = string.IsNullOrWhiteSpace(textBoxLastName.Text)
                ? Color.MistyRose : SystemColors.Window;

            // Street Address
            textBoxStreetAddress.BackColor = string.IsNullOrWhiteSpace(textBoxStreetAddress.Text)
                ? Color.MistyRose : SystemColors.Window;

            // City
            textBoxCity.BackColor = string.IsNullOrWhiteSpace(textBoxCity.Text)
                ? Color.MistyRose : SystemColors.Window;


            // State (ComboBox) - robust check
            var selectedState = comboBoxState.SelectedItem as StateDto;
            if (selectedState != null && selectedState.StateId > 0)
                comboBoxState.BackColor = SystemColors.Window;
            else
                comboBoxState.BackColor = Color.MistyRose;

            // Zip
            textBoxZipCode.BackColor = string.IsNullOrWhiteSpace(textBoxZipCode.Text)
                ? Color.MistyRose : SystemColors.Window;


            // Email
            textBoxEmail.BackColor = string.IsNullOrWhiteSpace(textBoxEmail.Text)
                ? Color.MistyRose : SystemColors.Window;

            // Phone (masked)
            maskedTextBoxPhoneNumber.BackColor = !maskedTextBoxPhoneNumber.MaskFull
                ? Color.MistyRose : SystemColors.Window;

                  }

        /// <summary>
        /// Populates form fields with the selected person's data.
        /// </summary>
        private void DisplayFields(PersonDto person)
        {
            textBoxFirstName.Text = person.FirstName;
            textBoxMI.Text = person.MI;
            textBoxLastName.Text = person.LastName;
            maskedTextBoxPhoneNumber.Text = person.PhoneNumber;
            maskedTextBoxCellNumber.Text = person.CellNumber;
            textBoxEmail.Text = person.Email;

            HomeAddressDto address = person.Address;
            if (address == null && person.AddressId != 0)
            {
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
                        var item = comboBoxState.Items[i] as StateDto;
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

        #endregion

        #region Data Operations

        /// <summary>
        /// Loads all people from the service and binds to the DataGridView.
        /// </summary>
        private void LoadAllPeople()
        {
            var allPeople = _peopleService.GetAllPeople().ToList();
            if (allPeople.Count == 0)
            {
                MessageBox.Show(_messages.NoMatchingRecords, "No Records loaded.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Ensure address is loaded for each person
                foreach (var person in allPeople)
                {
                    if (person.Address == null && person.AddressId != 0)
                    {
                        var address = _addressService.GetAddressById(person.AddressId);
                        if (address != null)
                        {
                            person.Address = address;
                        }
                    }
                }
                // Debug output for loaded people
                var allPeople1 = _peopleService.GetAllPeople().ToList();
                foreach (var person in allPeople1)
                {
                    Debug.WriteLine($"{person.PersonId}: {person.Address?.StreetAddress}, {person.Address?.State?.StateAbbr}");
                }
            }
            dataGridViewResults.DataSource = new BindingList<PersonDto>(allPeople);
        }

        /// <summary>
        /// Loads all states and binds to the state ComboBox.
        /// </summary>
        private void LoadStates()
        {
            var states = _stateService.GetStates()
                .OrderBy(s => s.StateName)
                .Select(s => new StateDto
                {
                    StateId = s.StateId,
                    StateName = s.StateName,
                    StateAbbr = s.StateAbbr
                })
                .ToList();

            comboBoxState.DataSource = states;
            comboBoxState.DisplayMember = "StateAbbr";
            comboBoxState.ValueMember = "StateId";
            comboBoxState.SelectedIndex = -1;
        }

        /// <summary>
        /// Finds an existing address or creates a new one.
        /// </summary>
        private HomeAddressDto? GetOrCreateAddress(string streetAddress, string city, int stateId, string zipCode)
        {
            var existingAddresses = _addressService.SearchAddress(streetAddress, city, stateId, zipCode).ToList();
            if (existingAddresses.Any())
            {
                return existingAddresses.First();
            }

            var newAddress = new HomeAddressDto
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

        #endregion

        #region Utility Methods

        /// <summary>
        /// Displays a database error message.
        /// </summary>
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
        }

        #endregion
    }
}