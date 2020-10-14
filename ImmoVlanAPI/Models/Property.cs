using System.Xml.Linq;

namespace ImmoVlanAPI.Models {

    public class Property {

        //Identification
        public string PropertyProId { get; set; }
        public string PropertySoftwareId { get; set; }

        //General Items
        public CommercialStatus CommercialStatus { get; set; }

        //Sections
        public Classification Classification { get; set; }
        public Location Location { get; set; }
        public GeneralInformation GeneralInformation { get; set; }
        public Description Description { get; set; }
        public FinancialDetails FinancialDetails { get; set; }

        public Property(string propertyProId, string propertySoftwareId) {
            PropertyProId = propertyProId;
            PropertySoftwareId = propertySoftwareId;
        }

        public XElement ToXElement() {
            return new XElement("property",
                new XAttribute("propertyProId", PropertyProId),
                new XAttribute("propertySoftwareId", PropertySoftwareId),
                new XAttribute("commercialStatus", CommercialStatus.ToString()),
                    Classification.ToXElement(),
                    Location.ToXElement(),
                    GeneralInformation.ToXElement(),
                    Description.ToXElement(),
                    FinancialDetails.ToXElement()
            );
        }

    }

    public enum CommercialStatus {

        SOLD,
        OPTION,
        ONLINE

    }

}
