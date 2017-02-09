using System;
using System.Threading.Tasks;
using Amazon.CognitoIdentity;

using Amazon.S3;
using ALConnect.Common;
using Amazon.S3.Model;
using Newtonsoft.Json;
using System.IO;
using ALConnect.Models;
using System.Collections.Generic;

namespace ALConnect.Helpers
{
    public class AWSHelper
    {
        private static CognitoAWSCredentials cognitoCredentials;
        private static AmazonS3Client s3Client;
        public static CognitoAWSCredentials Credentials
        {
            get
            {
                if (cognitoCredentials == null)
                {
                    cognitoCredentials = new CognitoAWSCredentials(Constants.CognitoPoolId, Constants.REGION);
                }
                return cognitoCredentials;
            }
        }

        public static IAmazonS3 S3Client
        {
            get
            {
                if (s3Client == null)
                {
                    s3Client = new AmazonS3Client(Credentials, Constants.REGION);
                }
                return s3Client;
            }
        }


        public AWSHelper()
        {
            cognitoCredentials = new CognitoAWSCredentials(Constants.CognitoPoolId, Constants.REGION);
            var id = cognitoCredentials.GetIdentityId();
        }

        public async Task<bool> BucketExist()
        {
            try
            {
                var response = await S3Client.ListObjectsAsync(new ListObjectsRequest()
                {
                    BucketName = Constants.Bucket.ToLowerInvariant(),
                    Prefix = "notifications",
                    MaxKeys = 10
                }).ConfigureAwait(false);
                return true;
            }
            catch (AmazonS3Exception e)
            {
                if ((e.StatusCode.Equals(Constants.BUCKET_REDIRECT_STATUS_CODE)) || e.StatusCode.Equals(Constants.BUCKET_ACCESS_FORBIDDEN_STATUS_CODE))
                {
                    //bucket exists if there is a redirect errror or forbidden error
                    return true;
                }
                else if (e.StatusCode.Equals(Constants.NO_SUCH_BUCKET_STATUS_CODE))
                {
                    return false;
                }
                else
                {
                    throw e;
                }
            }
            catch(Exception ee)
            {
                throw ee;
                
            }
        }

        public async Task<List<AWSNotification>> LoadNotifications()
        {
            var client = S3Client;
            var notes = new List<AWSNotification>();
            var response = await client.ListObjectsAsync(new ListObjectsRequest()
            {
                BucketName = Constants.Bucket.ToLowerInvariant(),
                Prefix = "notifications",
                MaxKeys = 10
            });

            foreach(S3Object s3Object in response.S3Objects)
            {
                try
                        {
                var res = await client.GetObjectAsync(new GetObjectRequest()
                {
                    BucketName = Constants.Bucket.ToLowerInvariant(),
                    Key = s3Object.Key
                });

                
                using (var stream = res.ResponseStream)
                {
                    
                            var reader = new StreamReader(stream);
                            var notification = reader.ReadToEnd();
                            var note = ProcessNotification(notification);
                            if(note != null)
                                {notes.Add(note);}
                        

                        
                } 
            
                }
                        catch (Exception e)
            {
                var vMsg = e.Message;

            }
            }
            return notes;
        }

        private AWSNotification ProcessNotification(string notification)
        {
            if (string.IsNullOrEmpty(notification))
                return null;
            
            return JsonConvert.DeserializeObject<AWSNotification>(notification);
        }
    }
}
