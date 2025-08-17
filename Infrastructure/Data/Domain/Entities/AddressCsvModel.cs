// No, AddressCsvModel does not need an interface.
// Reasoning
// •	AddressCsvModel is a simple data transfer object (DTO) used for mapping CSV data to properties.
// •	Interfaces are typically used for abstraction, dependency injection, or when multiple implementations are needed.
// •	For DTOs or models used only for data import/export, an interface is unnecessary and adds complexity without benefit.
// •	DTOs and models used for external data exchange (like CSV import/export) are best placed in the infrastructure layer or
//      a dedicated application/data layer. 

public class AddressCsvModel
{
    public string StreetAddress { get; set; }
    public string City { get; set; }
    public string StateAbbr { get; set; }
    public string ZipCode { get; set; }
}