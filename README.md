[![.NET](https://github.com/stefaandevylder/ImmoVlanAPI/actions/workflows/test.yml/badge.svg)](https://github.com/stefaandevylder/ImmoVlanAPI/actions/workflows/test.yml)

# ImmoVlanAPI for Dotnet Core in C#

This is a wrapper for the immo.vlan.be API for ASP.NET Core. There is a nice documentation guide wich you can find on the ImmoVlan website. Read this before using this wrapper.

## Contributions

Have you spotted a bug or want to add a missing feature? All pull requests are welcome! Please provide a description of the bug or feature you have fixed/added. Make sure to target the latest development branch.


## 1. Installation

The easiest way to install is through the [NuGet](https://www.nuget.org/packages/ImmoVlanAPI/) package.
```
PM> Install-Package ImmoVlanAPI
```

## 2. Library limitations

The library has currently some limitations, I've created around 50% of all options. Following options have been added and are fully supported:

1. Classification
2. Location
3. Description
4. Financial Details
5. General Information (Optional)
6. Outdoor Description (Optional)
7. Indoor Description (Optional)
8. Certificates (Optional)
9. Attachments (Optional)

## 3. Documentation

The documentation will be written soon.

## Examples
### Basic Example
```cs
ImmoVlanClient client = new ImmoVlanClient("business@mail.com", "technical@mail.com", 1, "XXXX");

Property property = new Property("123", "123", CommercialStatus.ONLINE,
    new Classification(TransactionType.SALE, PropertyType.BusinessSurface),
    new Location(new Address("9250")),
    new Description("Desc dutch", "Desc french"),
    new FinancialDetails(500)
);

var publishResult = await client.PublishProperty(property).Result;
```
This only contains the most basic options, there are a lot more, the documentation will get updated soon.

### Semi-Advanced Example
```cs
Property prop = new Property("123", "123", CommercialStatus.ONLINE,
    new Classification(TransactionType.SALE, PropertyType.BusinessSurface, true),
    new Location(new Address("9250", "Neerstraat", "50")),
    new Description("Desc dutch", "Desc french", "Damn this is more"),
    new FinancialDetails(500) {
        AgencyFee = 50,
        Curreny = Curreny.CHF
    }) {
        GeneralInformation = new GeneralInformation() {
            ContactEmail = "sdhjgfj@fjhk.com"
        },
        OutdoorDescription = new OutdoorDescription() {
            HasBalcony = true
        },
        IndoorDescription = new IndoorDescription() {
            IsFurnished = true,
            Rooms = new Room[] {
                new Room(RoomType.Attic, 1, 50),
                new Room(RoomType.Bathroom, 1, 50),
            }
        },
        Certificates = new Certificates() {
            Epc = new EPC() {
                Reference = "JHSGHJKSQDH"
            },
            ElectricalInstallationCertificate = Certificate.Yes,
            ElectricalInstallationValidityDate = DateTime.Now
        },
        Attachments = new Attachments() {
            Pictures = new Picture[] { new Picture(1, "content") },
            Videos = new Video[] { new Video(1, "link") },
            Documents = new Document[] { new Document("name", 1, "content") }
        }
    };
```
