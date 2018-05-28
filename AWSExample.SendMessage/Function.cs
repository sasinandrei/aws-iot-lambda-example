using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.IotData;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AWSExample.SendMessage
{
    public class Function
    {
        private readonly string _accessKey;
        private readonly string _secretKey;
        private readonly string _serviceUrl;
        private readonly string _topic;

        public Function()
        {
            _accessKey = Environment.GetEnvironmentVariable("AccessKey");
            _secretKey = Environment.GetEnvironmentVariable("SecretKey");
            _serviceUrl = Environment.GetEnvironmentVariable("ServiceURL");
            _topic = Environment.GetEnvironmentVariable("Topic");
        }

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                var body = JsonConvert.DeserializeObject<Request>(request.Body);

                AmazonIotDataClient client = new AmazonIotDataClient(_accessKey, _secretKey, new AmazonIotDataConfig
                    {
                        RegionEndpoint = RegionEndpoint.EUWest1,
                        ServiceURL = _serviceUrl
                    });

                var response = await client.PublishAsync(new Amazon.IotData.Model.PublishRequest()
                {
                    Topic = _topic,
                    Payload = new System.IO.MemoryStream(Encoding.UTF8.GetBytes(body.Message)),
                    Qos = 0
                });

                return new APIGatewayProxyResponse()
                {
                    StatusCode = (int)response.HttpStatusCode,
                    Body = JsonConvert.SerializeObject(response.ResponseMetadata)
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new APIGatewayProxyResponse()
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Body = e.Message
                };
            }
        }

        private class Request
        {
            public string Message { get; set; }
        }
    }
}
