using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.SQS;
using Amazon.SQS.Model;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AWSLambda2
{
    public class Function
    {
        /// <summary>
        /// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
        /// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
        /// region the Lambda function is executed in.
        /// </summary>
        IAmazonSQS AmazonSQS { get; set; }
        public Function()
        {
            AmazonSQS = new AmazonSQSClient();
        }
        public Function(IAmazonSQS sqsclient)
        {
            this.AmazonSQS = sqsclient;
        }
       /* public void RecieveMessage()
        {
            var QueueName = "EventQ";
            var FileNameKey = "oKey";
            var BucketNameKey = "bKey";
            var sqs = new AmazonSQSClient();
            var queueUrl = sqs.GetQueueUrlAsync(QueueName).Result.QueueUrl;
            var receivedMsg = new ReceiveMessageRequest
            {
                QueueUrl = queueUrl
            };
            var receivedMsgResponse = sqs.ReceiveMessageAsync(receivedMsg).Result;
            var s3Client = new AmazonS3Client();
            foreach (var msg in receivedMsgResponse.Messages)
            {
                var fileName = msg.Attributes.FirstOrDefault(x => x.Key == FileNameKey).Value;
                var bucketName = msg.Attributes.FirstOrDefault(x => x.Key == BucketNameKey).Value;
                if (bucketName == null)
                {
                    Console.WriteLine("Bucket Not Found");
                    return;
                }
                if (fileName == null)
                {
                    Console.WriteLine("File Not Found");
                    return;
                }
                var request = new ListObjectsRequest
                {
                    BucketName = bucketName
                };
                var response = s3Client.ListObjectsAsync(request).Result;
                var file = response.S3Objects.Find(s => s.Key == fileName);
                if (file == null)
                {
                    Console.WriteLine("File Not Found");
                    return;
                }
                Console.WriteLine("File Attributes");
                Console.WriteLine("Name: " + file.Key);
                Console.WriteLine("Bucket: " + file.BucketName);
                Console.WriteLine("Size: " + file.Size);
                Console.WriteLine("ETag: " + file.ETag);
                Console.WriteLine("LastModified: " + file.LastModified);
                //var msgReceiptHandle = receivedMsgResponse.Messages.FirstOrDefault()?.ReceiptHandle;
                //Console.WriteLine("Deleting Messages in MarkQueue");
                //var deleteRequest = new DeleteMessageRequest
                //{
                //    QueueUrl = RQueueUrl,
                //    ReceiptHandle = msgReceiptHandle
                //};
                //sqs.DeleteMessageAsync(deleteRequest);
                //Console.WriteLine("Deleted Messages in MarkQueue");
            }
        }*/
        /// <summary>
        /// This method is called for every Lambda invocation. This method takes in an SQS event object and can be used 
        /// to respond to SQS messages.
        /// </summary>
        /// <param name="evnt"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task FunctionHandler(SQSEvent evnt, ILambdaContext context)
        {
            foreach(var message in evnt.Records)
            {
                await ProcessMessageAsync(message, context);
            }
        }

        private async Task ProcessMessageAsync(SQSEvent.SQSMessage message, ILambdaContext context)
        {
            context.Logger.LogLine($"Processed message {message.Body}");

            // TODO: Do interesting work based on the new message
            await Task.CompletedTask;
        }

       /* public void ReceiveAndDeleteMessageExample()
        {
            AmazonSQSClient client = new AmazonSQSClient();
            string queueUrl = "https://sqs.eu-west-3.amazonaws.com/231208623561/EventQ";
            //
            // Receive a single message
            //
            var receiveMessageRequest = new ReceiveMessageRequest
            {
                AttributeNames = { "SentTimestamp" },
                MaxNumberOfMessages = 1,
                MessageAttributeNames = { "All" },
                QueueUrl = queueUrl,
                VisibilityTimeout = 0,
                WaitTimeSeconds = 0
            };

            var receiveMessageResponse = client.ReceiveMessageAsync(receiveMessageRequest);

            //
            // Delete the received single message
            //
            var deleteMessageRequest = new DeleteMessageRequest
            {
                QueueUrl = queueUrl,
                ReceiptHandle = receiveMessageResponse.Result.Messages[0].ReceiptHandle
            };

            client.DeleteMessageAsync(deleteMessageRequest);
        }*/
    }
}
