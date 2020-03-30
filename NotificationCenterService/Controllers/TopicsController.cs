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
    /// Manage all topics
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        private readonly string AWSAccessKeyId = "";
        private readonly string AWSSecretAccessKey = "";
        AmazonSimpleNotificationServiceClient snsClient;
        public TopicsController()
        {
            var credentials = new BasicAWSCredentials(AWSAccessKeyId, AWSSecretAccessKey);
            snsClient = new AmazonSimpleNotificationServiceClient(credentials, Amazon.RegionEndpoint.USEast1);
        }
        /// <summary>
        /// Creates new topic
        /// </summary>
        /// <param name="topic">topic name</param>
        /// <returns>CreateTopicResponse</returns>
        [HttpPost]
        public Task Post([FromBody] Topics topic)
        {
            //create a new SNS topic
            CreateTopicRequest createTopicRequest = new CreateTopicRequest(topic.Name); 
            Task<CreateTopicResponse> createTopicResponse = snsClient.CreateTopicAsync(createTopicRequest);
            //get request id for CreateTopicRequest from SNS metadata		
            // Console.WriteLine("CreateTopicRequest - " + createTopicResponse.ResponseMetadata.RequestId);
            return createTopicResponse;
        }
    }
}