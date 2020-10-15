﻿using System.Xml.Linq;

namespace ImmoVlanAPI.Models {

    public class Property {

        //Identification
        public string PropertyProId { get; set; }
        public string PropertySoftwareId { get; set; }

        //Reqiured Items
        public CommercialStatus CommercialStatus { get; set; }
        public Classification Classification { get; set; }
        public Location Location { get; set; }
        public Description Description { get; set; }
        public FinancialDetails FinancialDetails { get; set; }

        //Non-required Items
        public GeneralInformation GeneralInformation { get; set; }
        public OutdoorDescription OutdoorDescription { get; set; }
        public IndoorDescription IndoorDescription { get; set; }

        public Property(string propertyProId, string propertySoftwareId, CommercialStatus status,
            Classification classification, Location location, Description description, FinancialDetails financial,
            GeneralInformation general = null, OutdoorDescription outdoor = null, IndoorDescription indoor = null) {
            PropertyProId = propertyProId;
            PropertySoftwareId = propertySoftwareId;

            CommercialStatus = status;
            Classification = classification;
            Location = location;
            Description = description;
            FinancialDetails = financial;

            if (general == null) general = new GeneralInformation();
            if (outdoor == null) outdoor = new OutdoorDescription();
            if (indoor == null) indoor = new IndoorDescription();

            GeneralInformation = general;
            OutdoorDescription = outdoor;
            IndoorDescription = indoor;
        }

        public XElement ToXElement() {
            return new XElement("property",
                new XAttribute("propertyProId", PropertyProId),
                new XAttribute("propertySoftwareId", PropertySoftwareId),
                new XAttribute("commercialStatus", CommercialStatus.ToString()),
                    Classification.ToXElement(),
                    Location.ToXElement(),
                    Description.ToXElement(),
                    FinancialDetails.ToXElement(),
                    GeneralInformation.ToXElement()
            );
        }

    }

    public enum CommercialStatus {

        SOLD,
        OPTION,
        ONLINE

    }

}
