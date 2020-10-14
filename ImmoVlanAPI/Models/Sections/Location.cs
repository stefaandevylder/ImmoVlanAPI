using System.Xml.Linq;

namespace ImmoVlanAPI.Models {

    public class Location {

        public Address Address { get; set; }
        public Coordinates Coordinates { get; set; }
        public bool IsAddressDisplayed { get; set; }
        public Environment Environment { get; set; }

        public XElement ToXElement() {
            return new XElement("location",
                new XElement("address",
                    new XElement("street", Address.Street),
                    new XElement("streetNumber", Address.StreetNumber),
                    new XElement("postBox", Address.PostBox),
                    new XElement("zipCode", Address.ZipCode),
                    new XElement("city", Address.City),
                    new XElement("province", Address.Province),
                    new XElement("country", Address.Country)
                ),
                new XElement("coordinates",
                    new XElement("latitude", Coordinates.Latitude),
                    new XElement("longitude", Coordinates.Longitude)
                ),
                new XElement("isAddressDisplayed", IsAddressDisplayed),
                new XElement("environmentId", Environment)
            );
        }

    }

    public class Address {

        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string PostBox { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }

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
