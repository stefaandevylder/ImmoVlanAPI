using System.Xml.Linq;

namespace ImmoVlanAPI.Models {

    public class Description : Section {

        public string Dutch { get; set; }
        public string French { get; set; }
        public string English { get; set; }

        public Description(string dutch, string french, string english = null) {
            Dutch = dutch;
            French = french;
            English = english;
        }

        public override XElement ToXElement() {
            return new XElement("freeDescription",
                new XElement("dutch", Dutch),
                new XElement("french", French),
                new XElement("english", English)
            );
        }

    }

}
