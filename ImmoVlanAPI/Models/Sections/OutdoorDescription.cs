using System.Xml.Linq;

namespace ImmoVlanAPI.Models {

    public class OutdoorDescription : Section {

        public int? NrOfFrontages { get; set; }
        public int? FrontageWidth { get; set; }
        public Orientation? Orientation { get; set; }
        public int? NrOfFloors { get; set; }
        public int? FloorNumber { get; set; }
        public int? NrOfHousings { get; set; }
        public int? NrOfOutdoorParkings { get; set; }
        public bool? HasBalcony { get; set; }
        public Ground Ground { get; set; }
        public int? GardenSurface { get; set; }
        public int? TerraceSurface { get; set; }
        public int? CourtyardSurface { get; set; }

        public override XElement ToXElement() {
            XElement el = new XElement("outdoorDescription",
                new XElement("nrOfFrontages", NrOfFrontages),
                new XElement("frontageWidth", FrontageWidth),
                new XElement("orientationId", (int?) Orientation),
                new XElement("nrOfFloors", NrOfFloors),
                new XElement("floorNumber", FloorNumber),
                new XElement("nrOfHousings", NrOfHousings),
                new XElement("nrOfOutdoorParkings", NrOfOutdoorParkings),
                new XElement("hasBalcony", HasBalcony),
                new XElement("gardenSurface", GardenSurface),
                new XElement("terraceSurface", TerraceSurface),
                new XElement("courtyardSurface", CourtyardSurface)
            );

            if (Ground != null) {
                el.Element("outdoorDescription").Add(
                    new XElement("ground",
                        new XElement("totalBuiltSurface", Ground.TotalBuiltSurface),
                        new XElement("surface", Ground.Surface),
                        new XElement("depth", Ground.Depth),
                        new XElement("frontWidth", Ground.FrontWidth)
                    )
                );
            }

            return el;
        }

    }

    public class Ground {

        public decimal? TotalBuiltSurface { get; set; }
        public decimal? Surface { get; set; }
        public decimal? Depth { get; set; }
        public decimal? FrontWidth { get; set; }

    }

    public enum Orientation {

        NotSpecified = 0,
        North = 1,
        South = 2,
        West = 3,
        East = 4,
        NorthEast = 5,
        NorthWest = 6,
        SouthEast = 7,
        SouthWest = 8

    }

}
