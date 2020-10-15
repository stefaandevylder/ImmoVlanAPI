using ImmoVlanAPI.Models;
using Xunit;

namespace ImmoVlanAPI.Tests {

    public class SimpleTest {

        [Fact]
        public void CreateClient() {
            new ImmoVlanAPI("business@mail.com", "technical@mail.com", 1, "XXXX");
        }

        [Fact]
        public void CreateSimpleProperty() {
            Property prop = new Property("123", "123", CommercialStatus.ONLINE,
                new Classification(TransactionType.SALE, PropertyType.BusinessSurface),
                new Location(new Address("9250")),
                new Description("Desc dutch", "Desc french"),
                new FinancialDetails(500)
            );

            prop.ToXElement();
        }

        [Fact]
        public void CreateAdvancedProperty() {
            Property prop = new Property("123", "123", CommercialStatus.ONLINE,
                new Classification(TransactionType.SALE, PropertyType.BusinessSurface, true),
                new Location(new Address("9250", "Neerstraat", "50")),
                new Description("Desc dutch", "Desc french", "Damn this is more"),
                new FinancialDetails(500) {
                    AgencyFee = 50,
                    Curreny = Curreny.CHF
                },
                new GeneralInformation() {
                    ContactEmail = "sdhjgfj@fjhk.com"
                },
                new OutdoorDescription() {
                    HasBalcony = true
                },
                new IndoorDescription() {
                    IsFurnished = true,
                    Rooms = new Room[] { 
                        new Room(RoomType.Attic, 1, 50),
                        new Room(RoomType.Bathroom, 1, 50),
                    }
                }
            );

            prop.ToXElement();
        }

    }
}
