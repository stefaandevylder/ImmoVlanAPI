using System.Xml.Linq;

namespace ImmoVlanAPI.Models {

    public class Classification {

        public TransactionType TransactionType { get; set; }
        public PropertyType PropertyType { get; set; }
        public bool IsNewConstruction { get; set; }

        public XElement ToXElement() {
            return new XElement("classification",
                new XElement("transactionType", TransactionType.ToString()),
                new XElement("propertyTypeId", PropertyType),
                new XElement("isNewConstruction", IsNewConstruction)
            );
        }

    }

}
