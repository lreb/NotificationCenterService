using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Runtime;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Microsoft.AspNetCore.Mvc;
using NotificationCenterService.Models;

namespace NotificationCenterService.Controllers
{
    /// <summary>
    /// Notifications controller
    /// </summary>
    [Route("api/[controller]")]
    public class NotificationsController : Controller
    {
        private readonly string AWSAccessKeyId = "";
        private readonly string AWSSecretAccessKey = "";


        AmazonSimpleNotificationServiceClient snsClient;
        public NotificationsController()
        {
            var credentials = new BasicAWSCredentials(AWSAccessKeyId, AWSSecretAccessKey);
            snsClient = new AmazonSimpleNotificationServiceClient(credentials, Amazon.RegionEndpoint.USEast1);
        }
        /// <summary>
        /// test values
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "id": 1,
        ///        "name": "Item1",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>
        /// <returns>test</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>    
        [HttpGet]
        public Task Get()
        {
            string phoneNumber = "+52...";
            string topicName = "DemoNetCoreLambda";

            // topic
            string topicArn = CreateSNSTopic(snsClient, topicName);
            
            // create subscribe to topic
            SubscribeToTopic(snsClient, topicArn, "sms", phoneNumber);

            PublishRequest pubRequest = new PublishRequest();
            pubRequest.Message = "My message from net core serverless hosted in aws lambda";
            pubRequest.TopicArn = topicArn;
            // add optional MessageAttributes...
            //   pubRequest.MessageAttributes["AWS.SNS.SMS.SenderID"] = 
            //      new MessageAttributeValue{ StringValue = "mySenderId", DataType = "String"};

            Task<PublishResponse> pubResponse = snsClient.PublishAsync(pubRequest);

            return pubResponse;
        }

        public static String CreateSNSTopic(AmazonSimpleNotificationServiceClient snsClient, string topicName)
        {
            //create a new SNS topic
            CreateTopicRequest createTopicRequest = new CreateTopicRequest(topicName);
            Task<CreateTopicResponse> createTopicResponse = snsClient.CreateTopicAsync(createTopicRequest);
            //get request id for CreateTopicRequest from SNS metadata		
            // Console.WriteLine("CreateTopicRequest - " + createTopicResponse.ResponseMetadata.RequestId);
            return createTopicResponse.Result.TopicArn;
        }

        static public void SubscribeToTopic(AmazonSimpleNotificationServiceClient snsClient, String topicArn,
            String protocol, String endpoint)
        {
            SubscribeRequest subscribeRequest = new SubscribeRequest(topicArn, protocol, endpoint);
            Task<SubscribeResponse> subscribeResponse = snsClient.SubscribeAsync(subscribeRequest);
            //Console.WriteLine("Subscribe request: " + subscribeResponse.ResponseMetadata.RequestId);
            //Console.WriteLine("Subscribe result: " + subscribeResponse.);
        }

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values
        [HttpPost]
        public Task Post([FromBody]Notifications notifications)
        {
            PublishRequest pubRequest = new PublishRequest();
            pubRequest.Message = notifications.Message;
            pubRequest.TopicArn = notifications.Arn;
            // add optional MessageAttributes...
            //   pubRequest.MessageAttributes["AWS.SNS.SMS.SenderID"] = 
            //      new MessageAttributeValue{ StringValue = "mySenderId", DataType = "String"};

            Task<PublishResponse> pubResponse = snsClient.PublishAsync(pubRequest);
            return pubResponse;
        }

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
