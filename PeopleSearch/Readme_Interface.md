# Interface Usage Analysis

## Overview

This solution uses interfaces to abstract service and repository logic, promoting maintainability, testability, and flexibility. The main data models (`Person`, `Address`, `State`) and their DTOs are simple property containers and do not encapsulate business logic. Interfaces are applied where polymorphism, dependency injection, and mocking are beneficial.

---

## Should `Person.cs`, `Address.cs`, `State.cs` Use Interfaces?

### Analysis

- **DTOs and Domain Models**  
  - `PersonDto`, `AddressDto`, `StateDto`, and `State` are simple classes with properties.
  - They are used for data transfer and do not contain business logic or behavior.

- **Service Interfaces**  
  - `IPeopleService`, `IAddressService`, and `IStateService` abstract the business logic and data access.
  - These interfaces enable dependency injection and unit testing.

- **Repository Interfaces**  
  - Repository interfaces (e.g., `IAddressRepository`, `IPerson`) are used for data access abstraction.

### Recommendation

- **Do not use interfaces for simple DTOs or domain models** unless you require polymorphism or multiple implementations.
- **Use interfaces for services and repositories** to enable abstraction, testability, and maintainability.

---

## Referenced Types

### DTOs
 public class AddressDto 
 { 
   public int AddressId { get; set; }
   public string StreetAddress { get; set; } 
   public string City { get; set; } 
   public int StateId { get; set; } 
   public string ZipCode { get; set; } public 
   StateDto State { get; set; }
 }

 public class PersonDto
 { 
   public int PersonId { get; set; } 
   public string FirstName { get; set; } 
   public string MI { get; set; } 
   public string LastName { get; set; } 
   public string PhoneNumber { get; set; } 
   public string? CellNumber { get; set; } 
   public string Email { get; set; } 
   public int AddressId { get; set; } 
   public AddressDto Address { get; set; }
  }

 public class StateDto
 { 
   public int StateId { get; set; } 
   public string StateName { get; set; } 
   public string StateAbbr { get; set; }
 }

public class State { public int StateId { get; set; } public string StateName { get; set; } public string StateAbbr { get; set; } }

### Service Interfaces

`IPeopleService`, `IAddressService`, and `IStateService` are examples of service interfaces that define the contract for the corresponding services. Implementing these interfaces in the services allows for easy substitution and testing.

public interface IAddressService { IEnumerable<AddressDto> SearchAddress(string streetName, string cityName, int stateId, string zipCode); AddressDto? GetAddressById(int id); IEnumerable<AddressDto> GetAllAddresses(); (bool Success, string Message) AddAddress(AddressDto address); void UpdateAddress(AddressDto address); void DeleteAddress(int id); }
public interface IPeopleService { IEnumerable<PersonDto> Search(string firstName, string mi, string lastName); PersonDto? GetById(int id); IEnumerable<PersonDto> GetAllPeople(); (bool Success, string Message) AddPerson(PersonDto person); void UpdatePerson(PersonDto person); void DeletePerson(int id); }
public interface IStateService { IEnumerable<State> SearchStates(string stateAbbr, string stateName); State? GetStateById(int id); IEnumerable<State> GetStates(); }

### Configuration

public class MessagesConfig { public string NoSearchCriteria { get; set; } public string NoMatchingRecords { get; set; } public string FirstNameRequired { get; set; } public string MiddleNameInvalid { get; set; } public string LastNameRequired { get; set; } public string EmailInvalid { get; set; } public string PersonDeletedSuccess { get; set; } public string PersonDeletedError { get; set; } public string DuplicateRecord { get; set; } public string AddressError { get; set; } public string ValidateError { get; set; } }

---

## Summary

- **Use interfaces for services and repositories.**
- **Do not use interfaces for simple DTOs or domain models unless polymorphism is required.**
- This approach keeps the codebase clean, maintainable, and aligned with .NET best practices.

