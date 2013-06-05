using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Xml.Serialization;

namespace DHL.SchemaGenerated
{
    class Program
    {
        private static string FILE_PATH = @"D:\serialized_request.txt";
        private static void Main(string[] args)
        {
            var bookPickup = CreateNewBookPickup();
            SendRequest(bookPickup);
        }

        private static void SendRequest(BookPickupRequestEA bookPickup)
        {
            // Create a request for the URL. 
            WebRequest request = WebRequest.Create("http://xmlpitest-ea.dhl.com/XMLShippingServlet");

            // If required by the server, set the credentials.
           // request.Credentials = CredentialCache.DefaultCredentials;

            // Wrap the request stream with a text-based writer
            request.Method = "POST";        // Post method
            request.ContentType = "text/xml";

            var stream = request.GetRequestStream();
            StreamWriter writer = new StreamWriter(stream);

            // Write the XML text into the stream
            var soapWriter = new XmlSerializer(typeof(BookPickupRequestEA));

            //add namespaces and/or prefixes ( e.g " <req:BookPickupRequestEA xmlns:req="http://www.dhl.com"> ... </req:BookPickupRequestEA>"
            var ns = new XmlSerializerNamespaces();
            ns.Add("req", "http://www.dhl.com");
            soapWriter.Serialize(writer, bookPickup, ns);
            writer.Close();

            // Get the response.
            WebResponse response = request.GetResponse();

            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();

            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);

            // Read the content.
            string responseFromServer = reader.ReadToEnd();

            // Display the content.
            Console.WriteLine(responseFromServer);

            // Clean up the streams and the response.
            reader.Close();
            response.Close();
        }

       

        private static BookPickupRequestEA CreateNewBookPickup()
        {
            var bookPickup = new BookPickupRequestEA();
            bookPickup.Pickup = new Pickup()
            {
                PickupDate = DateTime.Now.AddDays(5),
                ReadyByTime = "10:20",
                CloseTime = "17:20"
            };
            bookPickup.PickupContact = new Contact()
            {
                PersonName = "sadasd",
                Phone = "2141231231"
            };
            bookPickup.Place = new Place()
            {
                LocationType = PlaceLocationType.B,
                CompanyName = "afadasd",
                Address1 = "adsdas",
                Address2 = "dasdasd",
                Address3 = "dadsadas",
                City = "adasd",
                CountryCode = "GB",
                DivisionName = "dasdasd",
                PackageLocation = "dasdasd",
                PostalCode = "W12 7TQ",
                StateCode = "UK"

            };
            bookPickup.Request = new Request()
            {
                ServiceHeader = new ServiceHeader()
                {
                    SiteID = "abdcefg", //Valid Site ID
                    Password = "abdcefg$$$", //Valid Password 
                    MessageReference = "1234567890123456789012345678901", //Message Reference - used for tracking meesages
                    MessageTime = DateTime.Now
                }
            };
            bookPickup.Requestor = new Requestor()
            {
                AccountNumber = "123456789",  //Valid account number
                AccountType = RequestorAccountType.D,
                CompanyName = "Company Name",  
                RequestorContact = new RequestorContact()
                {
                    PersonName = "Person Name",
                    Phone = "0123456789"
                }
            };

            return bookPickup;
        }

        #region FOR_DEBUGGING

        private static string GetTextFromXmlStream(Stream xmlStream)
        {
            StreamReader reader = new StreamReader(xmlStream);
            string ret = reader.ReadToEnd();
            reader.Close();
            return ret;
        }

        private static void SerializeEntity(BookPickupRequestEA bookPickup)
        {
            using (FileStream serializationStream = new
                                                      FileStream(FILE_PATH,
                                                                 FileMode.Create, FileAccess.Write))
            {

                var soapWriter = new XmlSerializer(typeof(BookPickupRequestEA));
                var ns = new XmlSerializerNamespaces();
                ns.Add("req", "http://www.dhl.com");
                soapWriter.Serialize(serializationStream, bookPickup, ns);

            }
        }

        #endregion 
    }


}
