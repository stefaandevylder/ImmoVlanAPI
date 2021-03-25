using System.Xml.Linq;

namespace ImmoVlanAPI.Models {

    public class FinancialDetails : Section {

        public decimal Price { get; set; }
        public decimal? MaxPrice { get; set; }
        public PricePeriodicity? PricePeriodicity { get; set; }
        public bool? IsPricePerSquareMeter { get; set; }
        public Curreny? Curreny { get; set; }
        public int? LeaseDuration { get; set; }
        public decimal? MaintenanceCharges { get; set; }
        public int? RentalDeposit { get; set; }
        public bool? IsVatApplied { get; set; }
        public bool? IsPriceDisplayed { get; set; }
        public PriceType PriceType { get; set; }
        public decimal? MonthlyRentalIncome { get; set; }
        public float? AgencyFee { get; set; }
        public decimal? AnnualPercentageReturn { get; set; }

        public FinancialDetails(decimal price, PriceType priceType) {
            Price = price;
            PriceType = priceType;
        }

        public override XElement ToXElement() {
            return new XElement("financialDetails",
                new XElement("price", Price),
                new XElement("maxPrice", MaxPrice),
                new XElement("pricePeriodicity", (int?) PricePeriodicity),
                new XElement("isPricePerSquareMeter", IsPricePerSquareMeter),
                new XElement("currency", Curreny.ToString()),
                new XElement("leaseDuration", LeaseDuration),
                new XElement("maintenanceCharges", MaintenanceCharges),
                new XElement("rentalDeposit", RentalDeposit),
                new XElement("isVatApplied", IsVatApplied),
                new XElement("isPriceDisplayed", IsPriceDisplayed),
                new XElement("priceTypeId", (int) PriceType),
                new XElement("monthlyRentalIncome", MonthlyRentalIncome),
                new XElement("agencyFee", AgencyFee),
                new XElement("annualPercentageReturn", AnnualPercentageReturn)
            );
        }

    }

    public enum Curreny {

        EUR,
        USD,
        CHF,
        GBO,
        CAD,
        NOK

    }

    public enum PriceType {

        AskedPrice = 1,
        MakeAnOfferFrom = 2,
        ToDiscuss = 3

    }

    public enum PricePeriodicity {

        Year = 1,
        Month = 2,
        Week = 3,
        Day = 4

    }

}
