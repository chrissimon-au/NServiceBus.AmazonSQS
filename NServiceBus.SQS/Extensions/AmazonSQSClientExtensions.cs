﻿namespace NServiceBus.SQS
{
	using Amazon.SQS;
	using Amazon.SQS.Model;
	using System.Collections.Generic;
	using System.Linq;

    internal static class AmazonSQSClientExtensions
    {
        public static Message DequeueMessage(this IAmazonSQS sqs, string queueUrl)
        {
            var receiveMessageRequest = new ReceiveMessageRequest
            {
                WaitTimeSeconds = 20,
                QueueUrl = queueUrl,
                MaxNumberOfMessages = 1,
                MessageAttributeNames = new List<string> { "All" }
            };

            var receiveMessageResponse = sqs.ReceiveMessage(receiveMessageRequest);
            return receiveMessageResponse.Messages.FirstOrDefault();
        }
    }
}
