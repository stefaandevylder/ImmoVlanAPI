using ImmoVlanAPI.Models;
using System;
using Xunit;
using Xunit.Abstractions;

namespace ImmoVlanAPI.Tests {

    public class SimpleTest {

        private readonly ITestOutputHelper _output;

        private readonly ImmoVlanAPI _api;

        public SimpleTest(ITestOutputHelper output) {
            _output = output;

            _api = new ImmoVlanAPI("business@mail.com", "technical@mail.com", 1, "XXXX", true);
        }

        [Fact]
        public void CreateSimpleProperty() {
            Property prop = new Property("123", "123", CommercialStatus.ONLINE,
                new Classification(TransactionType.SALE, PropertyType.BusinessSurface),
                new Location(new Address("9250")),
                new Description("Desc dutch", "Desc french"),
                new FinancialDetails(500)
            );

            _output.WriteLine(_api.GetXML(prop).ToString());
            Assert.NotNull(prop.ToXElement());
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
                },
                new Attachments() {
                    Pictures = new Picture[] { new Picture(1, "content") },
                    Videos = new Video[] { new Video(1, "link") },
                    Documents = new Document[] { new Document("name", 1, "content") }
                }
            );

            _output.WriteLine(_api.GetXML(prop).ToString());
            Assert.NotNull(prop.ToXElement());
        }

/*        [Fact]
        public async void SendPropertyToAPI() {
            Property prop = new Property("123", "123", CommercialStatus.ONLINE,
                new Classification(TransactionType.SALE, PropertyType.BusinessSurface),
                new Location(new Address("9250")),
                new Description("Desc dutch", "Desc french"),
                new FinancialDetails(500)
            );

            var result = await _api.PublishProperty(prop);

            Assert.True(result.IsSuccessStatusCode);
        }*/
    }
}
