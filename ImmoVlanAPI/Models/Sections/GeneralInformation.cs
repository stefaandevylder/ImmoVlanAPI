using System;
using System.Xml.Linq;

namespace ImmoVlanAPI.Models {

    public class GeneralInformation : Section {

        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string PropertyUrl { get; set; }
        public string ContactLanguage { get; set; }
        public string VisitingHours { get; set; }
        public Condition? Condition { get; set; }
        public Availability? Availability { get; set; }
        public DateTime? AvailableFrom { get; set; }
        public bool? IsFirstOccupation { get; set; }
        public bool? IsCurrentlyRented { get; set; }
        public int? BuildYear { get; set; }
        public decimal? CadastralIncome { get; set; }
        public bool? ArePetsAllowed { get; set; }
        public bool? IsWoodConstruction { get; set; }
        public bool? IsListedBuilding { get; set; }

        public override XElement ToXElement() {
            return new XElement("generalInformation",
                new XElement("contactEmail", ContactEmail),
                new XElement("contactPhone", ContactPhone),
                new XElement("propertyUrl", PropertyUrl),
                new XElement("contactLanguage", ContactLanguage),
                new XElement("visitingHours", VisitingHours),
                new XElement("conditionId", (int?) Condition),
                new XElement("availabilityId", (int?) Availability),
                new XElement("availableFrom", AvailableFrom != null ? AvailableFrom.Value.ToString("s") : null),
                new XElement("isFirstOccupation", IsFirstOccupation),
                new XElement("isCurrentlyRented", IsCurrentlyRented),
                new XElement("buildYear", BuildYear),
                new XElement("cadastralIncome", CadastralIncome),
                new XElement("arePetsAllowed", ArePetsAllowed),
                new XElement("isWoodConstruction", IsWoodConstruction),
                new XElement("isListedBuilding", IsListedBuilding)
            );
        }

    }

    public enum Condition {

        New = 0,
        ExcellentCondition = 1,
        Renovated = 2,
        GoodCondition = 3,
        ToRefresh = 4,
        ToRestore = 5,
        ToRenovate = 6,
        ToDemolish = 7,
        UnderConstruction = 8

    }

    public enum Availability {

        Immediately = 1,
        ToAgree = 2,
        AvailableFrom = 3,
        ByDeed = 4

    }

}
