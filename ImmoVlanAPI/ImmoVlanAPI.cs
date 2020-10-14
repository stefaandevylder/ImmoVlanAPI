﻿using ImmoVlanAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ImmoVlanAPI {

    public class ImmoVlanAPI {

        public string URL {
            get {
                return Staging ?
                    "http://api.staging.immo.vlan.be/upload" :
                    "http://api.immo.vlan.be/upload";
            }
        }

        public HttpClient Http = new HttpClient();

        public string BusinessFeedbackEmail { get; private set; }
        public string TechnicalFeedbackEmail { get; private set; }
        public int SoftwareId { get; private set; }
        public string ProCustomerId { get; private set; }
        public bool Staging { get; private set; }

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

        /**
         * Publish a new property. If the property
         * does exist already, it gets updated.
         */
        public async Task PublishProperty(Property property) {
            XDocument xml = ToBaseXML();
            xml.Root.Element("action").Add(new XElement("publish", property.ToXElement()));

            await PostXML(xml);
        }

        /**
         * Creates an XML document especially designed
         * for the ImmoVlan API. Information in this XML
         * only contains the required options.
         * The required items can be found on: http://api.immo.vlan.be/Files/XmlTransferXsd
         */
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

        /**
         * Send a post request to the ImmoVlan
         * servers so the XML file gets posted.
         */
        public async Task PostXML(XDocument doc) {
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

            await Http.PostAsync(URL, content);
        }
    }
}