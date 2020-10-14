using System.Xml.Linq;

namespace ImmoVlanAPI.Models {

    public class Classification {

        public TransactionType TransactionType { get; set; }
        public PropertyType PropertyType { get; set; }
        public bool IsNewConstruction { get; set; }

        public Classification(TransactionType transactionType, PropertyType propertyType) {
            TransactionType = transactionType;
            PropertyType = propertyType;
        }

        public Classification(TransactionType transactionType, PropertyType propertyType, bool isNewConstruction): this(transactionType, propertyType) {
            IsNewConstruction = isNewConstruction;
        }

        public XElement ToXElement() {
            return new XElement("classification",
                new XElement("transactionType", TransactionType.ToString()),
                new XElement("propertyTypeId", PropertyType),
                new XElement("isNewConstruction", IsNewConstruction)
            );
        }

    }

}
