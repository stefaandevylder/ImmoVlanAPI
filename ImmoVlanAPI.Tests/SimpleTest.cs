using ImmoVlanAPI.Models;
using System;
using Xunit;
using Xunit.Abstractions;

namespace ImmoVlanAPI.Tests {

    public class SimpleTest {

        private readonly ITestOutputHelper _output;

        private readonly ImmoVlanClient _api;
        private readonly Property _simpleProperty;
        private readonly Property _advancedProperty;

        public SimpleTest(ITestOutputHelper output) {
            _output = output;

            _api = new ImmoVlanClient("XXX", "XXX", 1, "XXX", "XXX", true);

            _simpleProperty = new Property("123", "123", CommercialStatus.ONLINE,
                new Classification(TransactionType.SALE, PropertyType.BusinessSurface),
                new Location(new Address("9250")),
                new Description("Desc dutch", "Desc french"),
                new FinancialDetails(500, PriceType.AskedPrice)
            );

            _advancedProperty = new Property("123", "123", CommercialStatus.ONLINE,
                new Classification(TransactionType.SALE, PropertyType.BusinessSurface, true),
                new Location(new Address("9250", "Neerstraat", "50")),
                new Description("Desc dutch", "Desc french", "Damn this is more"),
                new FinancialDetails(500, PriceType.AskedPrice) {
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
                    IsFurnished = true
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
        }

        [Fact]
        public void CreateSimpleProperty() {
            _output.WriteLine(_api.GetXML(_simpleProperty).ToString());
            Assert.NotNull(_simpleProperty.ToXElement());
        }

        [Fact]
        public void CreateAdvancedProperty() {
            _output.WriteLine(_api.GetXML(_advancedProperty).ToString());
            Assert.NotNull(_advancedProperty.ToXElement());
        }

        /*
        [Fact]
        public async void SendSimplePropertyToAPI() {
            var result = await _api.PublishProperty(_simpleProperty);
            _output.WriteLine(String.Join(", ", result.Request.Parameters.Select(p => p.Name + " & " + p.Value)));
            _output.WriteLine(result.Content);

            Assert.True(result.IsSuccessful);
        }*/

        /*
        [Fact]
        public async void SendAdvancedPropertyToAPI() {
            var result = await _api.PublishProperty(_advancedProperty);
            _output.WriteLine(String.Join(", ", result.Request.Parameters.Select(p => p.Name + " & " + p.Value)));
            _output.WriteLine(result.Content);

            Assert.True(result.IsSuccessful);
        }*/
    }
}
