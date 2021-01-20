using System;
using System.Xml.Linq;

namespace ImmoVlanAPI.Models {

    public class Certificates : Section {

        public EPC Epc { get; set; }

        public Certificate GasoilTankCertificate { get; set; }
        public DateTime? GasoilTankValidityDate { get; set; }

        public Certificate ElectricalInstallationCertificate { get; set; }
        public DateTime? ElectricalInstallationValidityDate { get; set; }

        public override XElement ToXElement() {
            XElement el = new XElement("certificates",
                new XElement("gasoilTankCertificateId", GasoilTankCertificate),
                new XElement("gasoilTankValidityDate", GasoilTankValidityDate),
                new XElement("electricalInstallationCertificateId", ElectricalInstallationCertificate),
                new XElement("electricalInstallationValidityDate", ElectricalInstallationValidityDate));

            if (Epc != null) {
                el.Add(
                    new XElement("epc",
                        new XElement("reference", Epc.Reference),
                        new XElement("energyConsumption", Epc.EnergyConsumption),
                        new XElement("specificYearEnergyConsumption", Epc.SpecificYearEnergyConsumption),
                        new XElement("yearCo2Consumption", Epc.YearCo2Consumption),
                        new XElement("validityDate", Epc.ValidityDate)
                    ));
            }

            return el;
        }

    }

    public class EPC {

        public string Reference { get; set; }
        public int EnergyConsumption { get; set; }
        public int SpecificYearEnergyConsumption { get; set; }
        public int YearCo2Consumption { get; set; }
        public DateTime? ValidityDate { get; set; }

    }

    public enum Certificate {

        Yes = 1,
        NotComply = 2,
        NoCertificate = 3,
        NotApplicable = 4

    }
}
