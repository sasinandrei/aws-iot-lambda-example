using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace AWSExample.SendMessage.Test
{
    [TestClass]
    public class SendMessage
    {
        [TestMethod]
        public async Task PublishToIoT()
        {
            Environment.SetEnvironmentVariable("AccessKey", "xxxxxxxx");
            Environment.SetEnvironmentVariable("SecretKey", "xxxxxxxxxxxxx");
            Environment.SetEnvironmentVariable("ServiceURL", "xxxxxxxxxxxx");
            Environment.SetEnvironmentVariable("Topic", "/test/iot-pubsub-demo");

            var func = new Function();
            var result = await func.FunctionHandler(new Amazon.Lambda.APIGatewayEvents.APIGatewayProxyRequest()
            {
                Body = "{ \"Message\": \"Test\" }"
            }, null);
        }
    }
}
