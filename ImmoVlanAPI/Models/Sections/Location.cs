using System.Xml.Linq;

namespace ImmoVlanAPI.Models {

    public class Location : Section {

        public Address Address { get; set; }
        public Coordinates Coordinates { get; set; }
        public bool IsAddressDisplayed { get; set; }
        public Environment? Environment { get; set; }

        public Location(Address address, Coordinates coordinates = null) {
            Address = address;
            Coordinates = coordinates;
        }

        public Location(Address address, Coordinates coordinates, bool isAddressDisplayed, Environment environment): this(address, coordinates) {
            IsAddressDisplayed = isAddressDisplayed;
            Environment = environment;
        }

        public override XElement ToXElement() {
            XElement el = new XElement("location",
                new XElement("address",
                    new XElement("street", Address.Street),
                    new XElement("streetNumber", Address.StreetNumber),
                    new XElement("postBox", Address.PostBox),
                    new XElement("zipCode", Address.ZipCode),
                    new XElement("city", Address.City),
                    new XElement("province", Address.Province),
                    new XElement("country", Address.Country)
                ),
                new XElement("isAddressDisplayed", IsAddressDisplayed),
                new XElement("environmentId", (int?) Environment)
            );

            if (Coordinates != null) {
                el.Element("location").Add(
                    new XElement("coordinates", 
                        new XElement("latitude", Coordinates.Latitude),
                        new XElement("longitude", Coordinates.Longitude)
                    )
                );
            }

            return el;
        }

    }

    public class Address {

        public string ZipCode { get; set; }

        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string PostBox { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }

        public Address(string zipCode, string street = null, string streetNumber = null, 
            string postBox = null, string city = null, string province = null, string country = null) {
            ZipCode = zipCode;

            Street = street;
            StreetNumber = streetNumber;
            PostBox = postBox;
            City = city;
            Province = province;
            Country = country;
        }
    }

    public class Coordinates {

        public decimal Latitude { get; private set; }
        public decimal Longitude { get; private set; }

        public Coordinates(decimal latitude, decimal longitude) {
            Latitude = latitude;
            Longitude = longitude;
        }

    }

    public enum Environment {

        City = 0,
        Sea = 1,
        Mountains = 2,
        Countryside = 3,
        Other = 4

    }

}
