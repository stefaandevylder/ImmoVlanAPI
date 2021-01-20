﻿using System.Xml.Linq;

namespace ImmoVlanAPI.Models {

    public class Property : Section {

        //Identification
        public string PropertyProId { get; set; }
        public string PropertySoftwareId { get; set; }

        //Reqiured Items & Sections
        public CommercialStatus CommercialStatus { get; set; }
        public Classification Classification { get; set; }
        public Location Location { get; set; }
        public Description Description { get; set; }
        public FinancialDetails FinancialDetails { get; set; }

        //Non-required Sections
        public GeneralInformation GeneralInformation { get; set; }
        public OutdoorDescription OutdoorDescription { get; set; }
        public IndoorDescription IndoorDescription { get; set; }
        public Attachments Attachments { get; set; }

        public Property(string propertyProId, string propertySoftwareId, CommercialStatus status,
            Classification classification, Location location, Description description, FinancialDetails financial,
            GeneralInformation general = null, OutdoorDescription outdoor = null, IndoorDescription indoor = null,
            Attachments attachments = null) {
            PropertyProId = propertyProId;
            PropertySoftwareId = propertySoftwareId;

            CommercialStatus = status;
            Classification = classification;
            Location = location;
            Description = description;
            FinancialDetails = financial;

            GeneralInformation = general;
            OutdoorDescription = outdoor;
            IndoorDescription = indoor;
            Attachments = attachments;
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
