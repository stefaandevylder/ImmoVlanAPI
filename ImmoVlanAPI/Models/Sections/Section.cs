using System.Xml.Linq;

namespace ImmoVlanAPI.Models {

    public abstract class Section {

        /// <summary>
        /// Creates an XML document object from this class.
        /// </summary>
        /// <returns>The XElement of this object</returns>
        public abstract XElement ToXElement();

    }
}
