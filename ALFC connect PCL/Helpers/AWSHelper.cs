using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.CognitoIdentity;
using Amazon.SQS;
using Amazon.SQS.Model;
using ALConnect.Common;
using System.Threading;

namespace ALConnect.Helpers
{
    class AWSHelper
    {
        CognitoAWSCredentials credentials;

        public AWSHelper()
        {
//            CognitoCachingCredentialsProvider credentialsProvider = new CognitoCachingCredentialsProvider(
//    getApplicationContext(),
//    "us-west-2:7024f61e-9663-4dbb-8786-b96e8a65c8b1", // Identity Pool ID
//    Regions.US_WEST_2 // Region
//);
             credentials = new CognitoAWSCredentials(Constants.CognitoPoolId, RegionEndpoint.USWest2);
            var id = credentials.GetIdentityId();
        }

        public async Task<List<string>> SQSReader()
        {// 
            List<string> messagesList = new List<string>();
            var sqsClient = new AmazonSQSClient(credentials, RegionEndpoint.USWest1);
            //var response = new ReceiveMessageResponse();
            var token = new CancellationToken();
            var queUrl = sqsClient.GetQueueUrlAsync("MyQueue", token);
            ReceiveMessageRequest request = new  ReceiveMessageRequest(Constants.SQSSermonQueue);
            //request.WaitTimeSeconds = 20;
            request.MaxNumberOfMessages = 10;
            while (!token.IsCancellationRequested)
            { 
                var responseT =  sqsClient.ReceiveMessageAsync(request, token);
                responseT.Start();
                var response = responseT.Result;
                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    foreach (var msg in response.Messages)
                    {
                        messagesList.Add(msg.Body);
                    }
                }

            }
            return messagesList;
        }
    }
}
