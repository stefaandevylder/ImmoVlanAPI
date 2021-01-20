using ImmoVlanAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ImmoVlanAPI {

    public class ImmoVlanAPI {

        private string URL {
            get {
                return Staging ?
                    "http://api.staging.immo.vlan.be/upload" :
                    "http://api.immo.vlan.be/upload";
            }
        }

        private HttpClient Http = new HttpClient();

        private string BusinessFeedbackEmail { get; set; }
        private string TechnicalFeedbackEmail { get; set; }
        private int SoftwareId { get; set; }
        private string ProCustomerId { get; set; }
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
        public ImmoVlanAPI(string businessFeedbackEmail, string technicalFeedbackEmail, 
            int softwareId, string proCustomerId, bool staging = false) {
            if (softwareId < 1 || softwareId > 100) {
                throw new ArgumentException("SoftwareId must be between 1 and 100.");
            }

            BusinessFeedbackEmail = businessFeedbackEmail;
            TechnicalFeedbackEmail = technicalFeedbackEmail;
            SoftwareId = softwareId;
            ProCustomerId = proCustomerId;
            Staging = staging;
        }

        /// <summary>
        /// Publish a new property. If the property
        /// does exist already, it gets updated.
        /// </summary>
        /// <param name="property">The property object</param>
        /// <returns>The HTTP response of our POST request</returns>
        public async Task<HttpResponseMessage> PublishProperty(Property property) {
            return await PostXML(GetXML(property));
        }

        /// <summary>
        /// Creates the full XML file to upload to the API.
        /// </summary>
        /// <param name="property">The property object</param>
        /// <returns>A complete XML file</returns>
        public XDocument GetXML(Property property) {
            XDocument xml = ToBaseXML();

            xml.Element("request").Element("action").Add(new XElement("publish", property.ToXElement()));
            xml.Descendants().Where(a => a.IsEmpty || String.IsNullOrWhiteSpace(a.Value)).Remove();

            return xml;
        }

        /// <summary>
        /// Creates an XML document especially designed
        /// for the ImmoVlan API.Information in this XML
        /// only contains the required options.
        /// The required items can be found on: http://api.immo.vlan.be/Files/XmlTransferXsd
        /// </summary>
        /// <returns>An XDocument wich can be converted to an XML file</returns>
        private XDocument ToBaseXML() {
            XDocument doc = new XDocument(
                new XElement("request",
                new XAttribute("timestamp", DateTime.Now.ToString().Replace(" ", "T")),
                new XAttribute("softwareId", $"{SoftwareId}"),
                    new XElement("action",
                    new XAttribute("proCustomerId", ProCustomerId),
                    new XAttribute("hashValidation", Guid.NewGuid().ToString("N")))
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
        private async Task<HttpResponseMessage> PostXML(XDocument doc) {
            var content = new MultipartFormDataContent();

            var values = new[] {
                new KeyValuePair<string, string>("businessFeedbackEmail", BusinessFeedbackEmail),
                new KeyValuePair<string, string>("technicalFeedbackEmail", TechnicalFeedbackEmail),
            };

            foreach (var keyValuePair in values) {
                content.Add(new StringContent(keyValuePair.Value), keyValuePair.Key);
            }

            MemoryStream ms = new MemoryStream();
            doc.Save(ms);

            var fileContent = new ByteArrayContent(ms.ToArray());
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("file") {
                FileName = "Input.xml"
            };
            content.Add(fileContent);

            return await Http.PostAsync(URL, content);
        }
    }
}
