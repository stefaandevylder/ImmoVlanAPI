using System;
using System.Xml.Linq;

namespace ImmoVlanAPI.Models {

    public class Attachments : Section {

        public Picture[] Pictures { get; set; }
        public Video[] Videos { get; set; }
        public Document[] Documents { get; set; }

        public Attachments(Picture[] pictures = null, Video[] videos = null, Document[] documents = null) {
            if (pictures != null && pictures.Length > 32) {
                throw new ArgumentException("Maximum 32 images!");
            }

            if (videos != null && videos.Length > 5) {
                throw new ArgumentException("Maximum 5 videos!");
            }

            if (documents != null && documents.Length > 5) {
                throw new ArgumentException("Maximum 5 documents!");
            }

            Pictures = pictures;
            Videos = videos;
            Documents = documents;
        }

        public override XElement ToXElement() {
            XElement el = new XElement("attachments");

            if (Pictures != null) {
                foreach (Picture picture in Pictures) {
                    el.Add(
                        new XElement("picture", 
                            new XElement("order", picture.Order),
                            new XElement("content", picture.Base64Content))
                    );
                }
            }

            if (Videos != null) {
                foreach (Video video in Videos) {
                    el.Add(
                        new XElement("video",
                            new XElement("order", video.Order),
                            new XElement("link", video.Link))
                    );
                }
            }

            if (Documents != null) {
                foreach (Document doc in Documents) {
                    el.Add(
                        new XElement("document",
                            new XElement("name", doc.Name),
                            new XElement("order", doc.Order),
                            new XElement("content", doc.Base64Content))
                    );
                }
            }

            return el;
        }

    }

    public class Picture {

        public int Order { get; set; }
        public string Base64Content { get; set; }

        /// <summary>
        /// Creates an image.
        /// </summary>
        /// <param name="order">The order the image should be displayed in</param>
        /// <param name="base64Content">The content of the image in base 64 string</param>
        public Picture(int order, string base64Content) {
            if (order < 1 || order > 32) {
                throw new ArgumentException("Order must be between 1 and 32.");
            }

            Order = order;
            Base64Content = base64Content;
        }

    }

    public class Video {

        public int Order { get; set; }
        public string Link { get; set; }

        /// <summary>
        /// Creates a video.
        /// </summary>
        /// <param name="order">The order the video should be displayed in</param>
        /// <param name="link">The link to the video</param>
        public Video(int order, string link) {
            if (order < 1 || order > 5) {
                throw new ArgumentException("Order must be between 1 and 5.");
            }

            Order = order;
            Link = link;
        }

    }

    public class Document {

        public string Name { get; set; }
        public int Order { get; set; }
        public string Base64Content { get; set; }

        /// <summary>
        /// Creates a document.
        /// </summary>
        /// <param name="name">The name of the document</param>
        /// <param name="order">The order the documents should be displayed in</param>
        /// <param name="base64Content">The content of the document in base 64 string</param>
        public Document(string name, int order, string base64Content) {
            if (order < 1 || order > 5) {
                throw new ArgumentException("Order must be between 1 and 5.");
            }

            Name = name;
            Order = order;
            Base64Content = base64Content;
        }

    }
}
