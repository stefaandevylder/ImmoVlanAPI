using ImmoVlanAPI.Models;
using Xunit;

namespace ImmoVlanAPI.Tests {

    public class SimpleTest {

        [Fact]
        public void CreateClient() {
            new ImmoVlanAPI("business@mail.com", "technical@mail.com", 1, "XXXX");
        }

        [Fact]
        public void CreateProperty() {
            Property prop = new Property("123", "123", CommercialStatus.ONLINE,
                new Classification(TransactionType.SALE, PropertyType.BusinessSurface),
                new Location(new Address("9250")),
                new Description("Desc dutch", "Desc french"),
                new FinancialDetails(500)
            );

            prop.ToXElement();
        }

    }
}
