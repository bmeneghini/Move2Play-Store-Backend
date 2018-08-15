using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.Xml;
using System.ServiceModel.Description;
using System.IO;

namespace Api.Shared
{

    public class MyMessageInspector : IClientMessageInspector
    {
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {

        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            // Initialize objects
            var xmlDocument = new XmlDocument();
            var memoryStream = new MemoryStream();
            var xmlWriter = XmlWriter.Create(memoryStream);
            var xmlAttribute = xmlDocument.CreateAttribute("xmlns", "api", "http://www.w3.org/2000/xmlns/");

            xmlAttribute.Value = "http://somexmlnamespace";

            // Write the xml request message into the memory stream
            request.WriteMessage(xmlWriter);

            // Clear the xmlWriter
            xmlWriter.Flush();

            // Place the pointer in the memoryStream to the beginning 
            memoryStream.Position = 0;

            // Load the memory stream into the xmlDocument
            xmlDocument.Load(memoryStream);

            // Remove the attributes from the second node down form the top
            xmlDocument.ChildNodes[1].ChildNodes[1].Attributes.RemoveAll();

            // Add the xml namespace attribute to the first node down from the top
            xmlDocument.ChildNodes[1].Attributes.Append(xmlAttribute);

            // Reset the memoryStream object - essentially nulls it out
            memoryStream.SetLength(0);

            // ReInitialize the xmlWriter
            xmlWriter = XmlWriter.Create(memoryStream);

            // Write the modified xml request message (xmlDocument) to the memoryStream in the xmlWriter
            xmlDocument.WriteTo(xmlWriter);

            // Clear the xmlWriter
            xmlWriter.Flush();

            // Place the pointer in the memoryStream to the beginning 
            memoryStream.Position = 0;

            // Create a new xmlReader with the memoryStream that contains the xmlDocument
            var xmlReader = XmlReader.Create(memoryStream);

            // Create a new request message with the modified xmlDocument
            request = Message.CreateMessage(xmlReader, int.MaxValue, request.Version);

            return request;
        }
    }

    public class InspectorBehavior : IEndpointBehavior
    {
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {

        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.ClientMessageInspectors.Add(new MyMessageInspector());
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {

        }

        public void Validate(ServiceEndpoint endpoint)
        {

        }
    }
}