using System.Collections.Generic;
using System.Net;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AwsDotnetCsharp;

public class Handler
{
    public APIGatewayProxyResponse Notification(APIGatewayProxyRequest request, ILambdaContext context)
    {
        var dict = new Dictionary<string, string>
        {
            {
                "Hello", "World"
            }
        };
        var response = CreateResponse(dict);

        return response;
    }

    APIGatewayProxyResponse CreateResponse(IDictionary<string, string> result)
    {
        var statusCode = (result != null) ? (int)HttpStatusCode.OK : (int)HttpStatusCode.InternalServerError;

        var body = (result != null) ? JsonConvert.SerializeObject(result) : string.Empty;

        var response = new APIGatewayProxyResponse
        {
            StatusCode = statusCode,
            Body = body,
            Headers = new Dictionary<string, string>
            {
                {
                    "Content-Type", "application/json"
                },
                {
                    "Access-Control-Allow-Origin", "*"
                }
            }
        };

        return response;
    }
}
