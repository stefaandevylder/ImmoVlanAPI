using ImmoVlanAPI.Models;
using RestSharp;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ImmoVlanAPI {

    public class ImmoVlanClient {

        private string URL {
            get {
                return Staging ?
                    "http://api.staging.immo.vlan.be" :
                    "https://api.immo.vlan.be";
            }
        }

        private string BusinessFeedbackEmail { get; set; }
        private string TechnicalFeedbackEmail { get; set; }
        private int SoftwareId { get; set; }
        private string ProCustomerId { get; set; }
        private string SoftwarePassword { get; set; }
        private bool Staging { get; set; }

        /// <summary>
        /// Creates a client of the ImmoVlan API.
        /// You need to use this to send over the property
        /// to the API servers.
        /// </summary>
        /// <param name="businessFeedbackEmail">The Email for business feedback</param>
        /// <param name="technicalFeedbackEmail">The Email for technical things such as errors</param>
        /// <param name="softwareId">A custom software ID</param>
        /// <param name="proCustomerId">A professional customer ID (ImmoVlan creates these)</param>
        /// <param name="staging">Enable this if you need to stage</param>
        public ImmoVlanClient(string businessFeedbackEmail, string technicalFeedbackEmail,
            int softwareId, string proCustomerId, string softwarePassword, bool staging = false) {
            BusinessFeedbackEmail = businessFeedbackEmail;
            TechnicalFeedbackEmail = technicalFeedbackEmail;
            SoftwareId = softwareId;
            ProCustomerId = proCustomerId;
            SoftwarePassword = softwarePassword;
            Staging = staging;
        }

        /// <summary>
        /// Publish a new property. If the property
        /// does exist already, it gets updated.
        /// </summary>
        /// <param name="property">The property object</param>
        /// <returns>The HTTP response of our POST request</returns>
        public async Task<RestResponse> PublishProperty(Property property) {
            return await PostXML(GetXML(property));
        }

        /// <summary>
        /// Suspend a property
        /// </summary>
        /// <param name="propertySoftwareId">The property software id</param>
        /// <returns>The HTTP response of our POST request</returns>
        public async Task<RestResponse> SuspendProperty(string propertySoftwareId) {
            return await PostXML(GetXML(new Property(propertySoftwareId), ImmoVlanAction.Suspend));
        }

        /// <summary>
        /// Creates the full XML file to upload to the API.
        /// </summary>
        /// <param name="property">The property object</param>
        /// <returns>A complete XML file</returns>
        public XDocument GetXML(Property property, ImmoVlanAction action = ImmoVlanAction.Publish) {
            XDocument xml = ToBaseXML();

            if (action == ImmoVlanAction.Publish) {
                xml.Element("request").Element("action").Add(new XElement(action.ToString().ToLower(), property.ToXElement()));
                xml.Descendants().Where(a => a.IsEmpty || String.IsNullOrWhiteSpace(a.Value)).Remove();
            } else {
                xml.Element("request").Element("action").Add(new XElement(action.ToString().ToLower(), 
                    new XElement("property", new XAttribute("propertySoftwareId", property.PropertySoftwareId))));
            }

            return xml;
        }

        /// <summary>
        /// Creates an XML document especially designed
        /// for the ImmoVlan API. Information in this XML
        /// only contains the required options.
        /// The required items can be found on: http://api.immo.vlan.be/Files/XmlTransferXsd
        /// </summary>
        /// <returns>An XDocument wich can be converted to an XML file</returns>
        private XDocument ToBaseXML() {
            DateTime timeStamp = DateTime.Now;

            XDocument doc = new XDocument(
                new XElement("request",
                new XAttribute("timestamp", timeStamp.ToString("s")),
                new XAttribute("softwareId", $"{SoftwareId}"),
                    new XElement("action",
                    new XAttribute("proCustomerId", ProCustomerId),
                    new XAttribute("hashValidation", HashValidation(timeStamp)))
                )
            );

            return doc;
        }

        /// <summary>
        /// Send a post request to the ImmoVlan
        /// servers so the XML file gets posted.
        /// </summary>
        /// <param name="doc">The fial XML document</param>
        /// <returns>The HTTP response of our POST request</returns>
        private async Task<RestResponse> PostXML(XDocument doc) {
            var client = new RestClient(URL);

            var request = new RestRequest("upload", Method.Post);
            request.AlwaysMultipartFormData = true;

            request.AddParameter("businessFeedbackEmail", BusinessFeedbackEmail, ParameterType.UrlSegment);
            request.AddParameter("technicalFeedbackEmail", TechnicalFeedbackEmail, ParameterType.UrlSegment);

            MemoryStream ms = new MemoryStream();
            doc.Save(ms);

            request.AddFile("file", ms.ToArray(), "input.xml");

            return await client.ExecuteAsync(request);
        }

        /// <summary>
        /// A SHA1 hash of the following values concatenated using a
        /// pipe: Timestamp, ProCustomerId, SoftwareId, Software password.
        /// 
        /// Example: 2014-08-26T00:00:00|00245259|38|g1
        /// 1b1e1381bf6df7895a2838dbf4cbfcd406852b13
        /// </summary>
        /// <returns></returns>
        private string HashValidation(DateTime timeStamp) {
            string rawData = timeStamp.ToString("s") + "|" + ProCustomerId + "|" + SoftwareId + "|" + SoftwarePassword;

            byte[] buffer = Encoding.UTF8.GetBytes(rawData);
            SHA1CryptoServiceProvider cryptoTransformSha1 = new SHA1CryptoServiceProvider();
            string hash = BitConverter.ToString(cryptoTransformSha1.ComputeHash(buffer)).Replace("-", "");

            return hash.ToLower();
        }
    }
}
