# ImmoVlanAPI for Dotnet Core in C#

This is a wrapper for the immo.vlan.be API for ASP.NET Core.


# Documentation

The documentation will be written soon.

## Basic Example
```cs
ImmoVlanAPI client = ImmoVlanAPI("business@mail.com", "technical@mail.com", 1, "XXXX");

Property property = new Property("123", "123", CommercialStatus.ONLINE,
    new Classification(TransactionType.SALE, PropertyType.BusinessSurface),
    new Location(new Address("9250")),
    new Description("Desc dutch", "Desc french"),
    new FinancialDetails(500)
);

var publishResult = await client.PublishProperty(property).Result;
```
This only contains the most basic options, there are a lot more, the documentation will get updated soon.

## Semi-Advanced Example
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
