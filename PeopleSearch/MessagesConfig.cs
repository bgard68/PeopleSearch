public class MessagesConfig
{
    public string NoSearchCriteria { get; set; } = "No search criteria entered. Displaying all records.";
    public string NoMatchingRecords { get; set; } = "No matching records found.";
    public string FirstNameRequired { get; set; } = "First name is required.";
    public string MiddleNameInvalid { get; set; } = "Middle name must be empty or a single letter.";
    public string LastNameRequired { get; set; } = "Last name is required.";
    public string EmailInvalid { get; set; } = "Please enter a valid email address.";
}