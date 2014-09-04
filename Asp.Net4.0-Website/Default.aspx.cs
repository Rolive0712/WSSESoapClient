using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security.Tokens;
using System.ServiceModel.Security;
using System.Net;
using System.Net.Security;
using System.Text;

using Custom;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //turn off Certificate validation
        ServicePointManager.ServerCertificateValidationCallback = (object s, X509Certificate certificate, X509Chain chain,
                     SslPolicyErrors sslPolicyErrors) => true;

        SecurityBindingElement securityElement = SecurityBindingElement.CreateUserNameOverTransportBindingElement();
        securityElement.IncludeTimestamp = false;
        TextMessageEncodingBindingElement encodingElement = new TextMessageEncodingBindingElement(MessageVersion.Soap11, Encoding.UTF8);
        HttpsTransportBindingElement tranportElement = new HttpsTransportBindingElement();

        CustomBinding customBinding = new CustomBinding(securityElement, encodingElement, tranportElement);

        EndpointAddress address = new EndpointAddress("<site url wsdl link>?wsdl");
        
        cmpproxy.OrderExtWSClient orderExtWSClient = new cmpproxy.OrderExtWSClient(customBinding, address);
        orderExtWSClient.ClientCredentials.UserName.UserName = "admin";
        orderExtWSClient.ClientCredentials.UserName.Password = "admin";

        orderExtWSClient.Endpoint.Behaviors.Add(new CustomEndpointBehavior());

        var orderResult = orderExtWSClient.createOrder("<pass params>");
        var orderWithItems = orderExtWSClient.createOrderWithItems("<pass params>");

    }
}
