using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Runtime;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificationCenterService.Models;

namespace NotificationCenterService.Controllers
{
    /// <summary>
    /// Manage all subscribers
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribersController : ControllerBase
    {
        private readonly string AWSAccessKeyId = "";
        private readonly string AWSSecretAccessKey = "";
        AmazonSimpleNotificationServiceClient snsClient;
        public SubscribersController()
        {
            var credentials = new BasicAWSCredentials(AWSAccessKeyId, AWSSecretAccessKey);
            snsClient = new AmazonSimpleNotificationServiceClient(credentials, Amazon.RegionEndpoint.USEast1);
        }
        /// <summary>
        /// Create new subscriber
        /// </summary>
        /// <param name="subscriber"></param>
        /// <returns></returns>
        [HttpPost]
        public Task Post([FromBody] Subscriber subscriber)
        {
            SubscribeRequest subscribeRequest = new SubscribeRequest(subscriber.TopicArn, subscriber.Protocol, subscriber.EndPoint);
            Task<SubscribeResponse> subscribeResponse = snsClient.SubscribeAsync(subscribeRequest);
            return subscribeResponse;
        }
    }
}