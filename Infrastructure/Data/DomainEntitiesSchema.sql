-- •	All relationships and navigation properties are represented with foreign keys.
-- •	The many-to-many relationship between City and ZipCode is handled by the CityZipCode table.
-- •	The schema is normalized and matches your domain model.

-- State Table
CREATE TABLE State (
    StateId INT PRIMARY KEY IDENTITY(1,1),
    StateName NVARCHAR(100) NOT NULL UNIQUE,
    StateCode NVARCHAR(10) NOT NULL UNIQUE
);

-- CityName Table
CREATE TABLE CityName (
    CityNameId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL UNIQUE
);

-- City Table
CREATE TABLE City (
    CityId INT PRIMARY KEY IDENTITY(1,1),
    CityNameId INT NOT NULL,
    StateId INT NOT NULL,
    CONSTRAINT FK_City_CityName FOREIGN KEY (CityNameId) REFERENCES CityName(CityNameId),
    CONSTRAINT FK_City_State FOREIGN KEY (StateId) REFERENCES State(StateId),
    CONSTRAINT UQ_City_CityNameId_StateId UNIQUE (CityNameId, StateId)
);

-- ZipCode Table
CREATE TABLE ZipCode (
    ZipCodeId INT PRIMARY KEY IDENTITY(1,1),
    Zip NVARCHAR(20) NOT NULL UNIQUE
);

-- CityZipCode Table (Many-to-Many: City <-> ZipCode)
CREATE TABLE CityZipCode (
    CityId INT NOT NULL,
    ZipCodeId INT NOT NULL,
    CONSTRAINT PK_CityZipCode PRIMARY KEY (CityId, ZipCodeId),
    CONSTRAINT FK_CityZipCode_City FOREIGN KEY (CityId) REFERENCES City(CityId),
    CONSTRAINT FK_CityZipCode_ZipCode FOREIGN KEY (ZipCodeId) REFERENCES ZipCode(ZipCodeId)
);

-- Address Table
CREATE TABLE Address (
    AddressId INT PRIMARY KEY IDENTITY(1,1),
    StreetAddress NVARCHAR(255) NOT NULL,
    CityId INT NOT NULL,
    ZipCodeId INT NOT NULL,
    CONSTRAINT FK_Address_City FOREIGN KEY (CityId) REFERENCES City(CityId),
    CONSTRAINT FK_Address_ZipCode FOREIGN KEY (ZipCodeId) REFERENCES ZipCode(ZipCodeId)
);

-- Person Table
CREATE TABLE Person (
    PersonId INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(100) NOT NULL,
    MI NVARCHAR(10),
    LastName NVARCHAR(100) NOT NULL,
    PhoneNumber NVARCHAR(20),
    CellNumber NVARCHAR(20),
    Email NVARCHAR(255) NOT NULL UNIQUE,
    AddressId INT,
    CONSTRAINT FK_Person_Address FOREIGN KEY (AddressId) REFERENCES Address(AddressId)
);