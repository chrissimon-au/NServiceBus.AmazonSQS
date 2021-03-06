﻿using System;
using System.Threading;

namespace NServiceBus.AmazonSQS.IntegrationTests
{
	static class SqsContextExtensions
	{
		public static bool MyQueueExists(this SqsTestContext context)
		{
			var queueExists = false;
			var tryCount = 0;

			while (!queueExists && tryCount < 10)
			{
				++tryCount;
				
				var listQueuesResponse = context.SqsClient.ListQueues(context.ConnectionConfiguration.QueueNamePrefix);
				foreach (var q in listQueuesResponse.QueueUrls)
				{
					if (q.Contains(context.Address.Queue))
					{
						queueExists = true;
					}
				}

				if (!queueExists)
					Thread.Sleep(TimeSpan.FromMilliseconds(500));
			}

			return queueExists;
		}
	}
}
