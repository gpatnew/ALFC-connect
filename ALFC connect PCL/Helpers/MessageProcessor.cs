using ALConnect.Common;
using Amazon;
using Amazon.CognitoIdentity;
using Amazon.S3;
using Amazon.SQS;
using Amazon.SQS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ALConnect.Helpers
{
    public class MessageProcessor
    {
        #region Locals
        AmazonS3Client s3Client;
        AmazonSQSClient sqsClient;
        #endregion

        public string SQSQueue { get; set; }


        public MessageProcessor()
        {
            var credentials = new CognitoAWSCredentials(Constants.CognitoPoolId, RegionEndpoint.USWest2);
            sqsClient = new AmazonSQSClient(credentials, RegionEndpoint.USWest1);
            SQSQueue = "MyQueue";
        }

        public async Task<object> StartListener(CancellationToken cancleToken)
        {
            var  inputQueueUrl = Constants.SQSSermonQueue;
            var listener =  Task.Factory.StartNew(async () => 
            {
                ReceiveMessageRequest receiveRequest = new ReceiveMessageRequest { WaitTimeSeconds = 20, QueueUrl = inputQueueUrl, MaxNumberOfMessages=10 };
                while (!cancleToken.IsCancellationRequested)
                {
                    try
                    {
                        var response = await sqsClient.ReceiveMessageAsync(receiveRequest);
                        foreach (var message in response.Messages)
                        {
                            await ProcessMessageAsync(message, cancleToken);
                        }
                    }
                    catch (Exception e)
                    {
                        var err = e.Message;
                        //Trace.WriteLine(e);
                    }
                }
            });

            return listener;
        }

        private async Task ProcessMessageAsync(Message message, CancellationToken cancleToken)
        {
            // Get the message from the body
            string body = message.Body;
            string id = message.MessageId;
        }

    }
}
