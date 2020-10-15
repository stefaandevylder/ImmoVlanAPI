using System.Xml.Linq;

namespace ImmoVlanAPI.Models {

    public class IndoorDescription {

        public decimal LivableSurface { get; set; }
        public bool IsFurnished { get; set; }
        public decimal UnderCeilingHeight { get; set; }
        public bool IsOpportunityForPro { get; set; }
        public FloorCoveringType FloorCoveringType { get; set; }
        public GlazingType GlazingType { get; set; }
        public bool HasDisabledAccess { get; set; }
        public bool HasElevator { get; set; }
        public string WindowFramingType { get; set; }
        public BlindsType BlindsType { get; set; }
        public Room[] Rooms { get; set; }
        public Connections Connections { get; set; }

        public XElement ToXElement() {
            XElement el = new XElement("indoorDescription",
                new XElement("livableSurface", LivableSurface),
                new XElement("isFurnished", IsFurnished),
                new XElement("underCeilingHeight", UnderCeilingHeight),
                new XElement("isOpportunityForPro", IsOpportunityForPro),
                new XElement("floorCoveringTypeId", FloorCoveringType),
                new XElement("glazingTypeId", GlazingType),
                new XElement("hasDisabledAccess", HasDisabledAccess),
                new XElement("hasElevator", HasElevator),
                new XElement("windowFramingType", WindowFramingType),
                new XElement("blindsTypeId", BlindsType)
            );

            if (Rooms != null) {
                if (Rooms.Length > 0) {
                    el.Element("indoorDescription").Add(
                        new XElement("rooms",
                        new XAttribute("quantity", Rooms.Length))
                    );

                    foreach (Room room in Rooms) {
                        el.Element("rooms").Add(
                            new XElement(room.RoomType.ToString(),
                            new XAttribute("quantity", room.Quantity),
                                new XElement("surface", room.Surface))
                        );
                    }
                }
            }

            if (Connections != null) {
                el.Element("indoorDescription").Add(
                    new XElement("connections",
                        new XElement("hasSatelliteTv", Connections.HasSatelliteTv),
                        new XElement("hasCableTv", Connections.HasCableTv),
                        new XElement("hasTelephone", Connections.HasTelephone),
                        new XElement("hasWifi", Connections.HasWifi),
                        new XElement("hasWaterSupply", Connections.HasWaterSupply),
                        new XElement("hasIndividualWaterSupply", Connections.HasIndividualWaterSupply),
                        new XElement("hasGasSupply", Connections.HasGasSupply),
                        new XElement("hasIndividualGasSupply", Connections.HasIndividualGasSupply),
                        new XElement("hasElectricitySupply", Connections.HasElectricitySupply),
                        new XElement("hasIndividualElectricitySupply", Connections.HasIndividualElectricitySupply)
                    )
                );
            }

            return el;
        }

    }

    public class Room {

        public RoomType RoomType { get; private set; }
        public int Quantity { get; private set; }
        public decimal Surface { get; private set; }

        public Room(RoomType roomType, int quantity, decimal surface) {
            RoomType = roomType;
            Quantity = quantity;
            Surface = surface;
        }

    }

    public class Connections {

        public bool HasSatelliteTv { get; set; }
        public bool HasCableTv { get; set; }
        public bool HasTelephone { get; set; }
        public bool HasWifi { get; set; }
        public bool HasWaterSupply { get; set; }
        public bool HasIndividualWaterSupply { get; set; }
        public bool HasGasSupply { get; set; }
        public bool HasIndividualGasSupply { get; set; }
        public bool HasElectricitySupply { get; set; }
        public bool HasIndividualElectricitySupply { get; set; }

    }

    public enum RoomType {

        Bedroom,
        LivingRoom,
        Kitchen,
        Bathroom,
        Shower,
        Toilet,
        Garage,
        InteriorParking,
        Cellar,
        DressingRoom,
        DiningRoom,
        Attic,
        Office,
        Veranda,
        Mezzanine,
        GameRoom,
        EntryHall,
        NightHall,
        WashHouse,
        Cloakroom,
        BikeStorage

    }

    public enum FloorCoveringType {

        NotSpecified = 1,
        Parquet = 2,
        Tiles = 3,
        Carpet = 4

    }

    public enum GlazingType {

        SimpleGlass = 1,
        DoubleGlass = 2,
        TripleGlass = 3

    }

    public enum BlindsType {

        BoardAndBattenShutters = 1,
        AccordionShutters = 2,
        RolldownShutters = 3,
        SlidingShutters = 4

    }

}
