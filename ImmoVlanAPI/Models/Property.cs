using System.Xml.Linq;

namespace ImmoVlanAPI.Models {

    public class Property : Section {

        //Identification
        private string PropertyProId { get; set; }
        private string PropertySoftwareId { get; set; }

        //Reqiured Private Items & Sections
        private CommercialStatus CommercialStatus { get; set; }
        private Classification Classification { get; set; }
        private Location Location { get; set; }
        private Description Description { get; set; }
        private FinancialDetails FinancialDetails { get; set; }

        //Non-required Public Sections
        public GeneralInformation GeneralInformation { get; set; }
        public OutdoorDescription OutdoorDescription { get; set; }
        public IndoorDescription IndoorDescription { get; set; }
        public Certificates Certificates { get; set; }
        public Attachments Attachments { get; set; }

        public Property(string propertyProId, string propertySoftwareId, CommercialStatus status,
            Classification classification, Location location, Description description, FinancialDetails financial) {
            PropertyProId = propertyProId;
            PropertySoftwareId = propertySoftwareId;

            CommercialStatus = status;
            Classification = classification;
            Location = location;
            Description = description;
            FinancialDetails = financial;
        }

        public override XElement ToXElement() {
            XElement el = new XElement("property",
                new XAttribute("propertyProId", PropertyProId),
                new XAttribute("propertySoftwareId", PropertySoftwareId),
                new XAttribute("commercialStatus", CommercialStatus.ToString()),
                    Classification.ToXElement(),
                    Location.ToXElement(),
                    Description.ToXElement(),
                    FinancialDetails.ToXElement()
            );

            if (GeneralInformation != null) el.Add(GeneralInformation.ToXElement());
            if (OutdoorDescription != null) el.Add(OutdoorDescription.ToXElement());
            if (IndoorDescription != null) el.Add(IndoorDescription.ToXElement());
            if (Certificates != null) el.Add(Certificates.ToXElement());
            if (Attachments != null) el.Add(Attachments.ToXElement());

            return el;
        }

    }

    public enum CommercialStatus {

        SOLD,
        OPTION,
        ONLINE

    }

}
