using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Google.Apis.Services;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using System.IO;
using System.Threading;
using Google.Apis.YouTube.v3.Data;
using Google.Apis.Upload;
//using Google.YouTube;

namespace eMall
{
    public class YoutubeAPI
    {
        //public static YouTubeService yservice = Auth();

        //private static YouTubeService Auth()
        //{
        //    UserCredential creds;

        //    using (var stream = new FileStream("youtube_client_certificate.json", FileMode.Open, FileAccess.Read))
        //    {
        //        creds = GoogleWebAuthorizationBroker.AuthorizeAsync(
        //            GoogleClientSecrets.Load(stream).Secrets,
        //            new[] { YouTubeService.Scope.YoutubeUpload },
        //            "user",
        //            System.Threading.CancellationToken.None,
        //            new FileDataStore("YoutubeAPI")
        //            ).Result;
        //    }

        //    var service = new YouTubeService(new BaseClientService.Initializer()
        //    {
        //        ApplicationName = "YoutubeAPI",
        //        HttpClientInitializer = creds
        //    });

        //    return service;
        //}

        public string uploadYoutubeVideo()
        {
            try
            {
                var CertPath = @"D://mydownloads/youtube_client_certificate1.json";
                UserCredential credential;
                using (var stream = new FileStream(CertPath, FileMode.Open, FileAccess.Read))
                {
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        new[] { YouTubeService.Scope.YoutubeUpload },
                        "ebaa4oman@gmail.com",
                        CancellationToken.None
                    ).Result;
                }

                // This OAuth 2.0 access scope allows an application to upload files to the
                // authenticated user's YouTube channel, but doesn't allow other types of access.

                var youtubeService = new YouTubeService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "EbaaApplication"
                });

                var video = new Video();
                video.Snippet = new VideoSnippet();
                video.Snippet.Title = "Ebaa Test - Default Video Title";
                video.Snippet.Description = "Ebaa Test - Default Video Description";
                video.Snippet.Tags = new string[] { "Ebaa tag1", "Ebaa tag2" };
                video.Snippet.CategoryId = "22"; // See https://developers.google.com/youtube/v3/docs/videoCategories/list
                video.Status = new VideoStatus();
                video.Status.PrivacyStatus = "private"; // or "private" or "public"
                var filePath = @"D://mydownloads/ebaatest_1_1280x720_1mb.mp4"; // Replace with path to actual movie file.

                using (var fileStream = new FileStream(filePath, FileMode.Open))
                {
                    var videosInsertRequest = youtubeService.Videos.Insert(video, "snippet,status", fileStream, "video/*");
                    //videosInsertRequest.ProgressChanged += videosInsertRequest_ProgressChanged;
                    //videosInsertRequest.ResponseReceived += videosInsertRequest_ResponseReceived;

                    videosInsertRequest.Upload();
                }
                return "success";
            }
            catch (Exception ex)
            {   
                throw ex;
            }


        }

        void videosInsertRequest_ProgressChanged(Google.Apis.Upload.IUploadProgress progress)
        {
            switch (progress.Status)
            {
                case UploadStatus.Uploading:
                    Console.WriteLine("{0} bytes sent.", progress.BytesSent);
                    break;

                case UploadStatus.Failed:
                    Console.WriteLine("An error prevented the upload from completing.\n{0}", progress.Exception);
                    break;
            }
        }

        void videosInsertRequest_ResponseReceived(Video video)
        {
            Console.WriteLine("Video id '{0}' was successfully uploaded.", video.Id);
        }

        public string uploadYoutubeVideo(string filepath, string title, string description, List<string> tags, string uniqueID, string youtubeCertFilePath)
        {
            string lineNumber = "1";
            try
            {
                var CertPath = youtubeCertFilePath; // @"D://mydownloads/youtube_client_certificate1.json";
                lineNumber = youtubeCertFilePath;
                UserCredential credential;
                using (var stream = new FileStream(CertPath, FileMode.Open, FileAccess.Read))
                {
                    string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                    //credPath = Path.Combine(credPath, ".credentials");
                    credPath = @"G:\PleskVhosts\ebaa.co\dev.ebaa.co\school_Youtube_Cert\";

                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        new[] { YouTubeService.Scope.YoutubeUpload },
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)
                    ).Result;
                    lineNumber = "cert validation complted : " + credPath;
                }
                
                // This OAuth 2.0 access scope allows an application to upload files to the
                // authenticated user's YouTube channel, but doesn't allow other types of access.

                var youtubeService = new YouTubeService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "EbaaApplication"
                });

                var video = new Video();
                video.Snippet = new VideoSnippet();
                video.Snippet.Title = title;
                video.Snippet.Description = description;
                video.Snippet.Tags = tags;
                video.Id = uniqueID;
                //video.Snippet.CategoryId = "22"; // See https://developers.google.com/youtube/v3/docs/videoCategories/list
                video.Status = new VideoStatus();
                video.Status.PrivacyStatus = "private"; // or "private" or "public"
                //var filePath = @"D://mydownloads/ebaatest_1_1280x720_1mb.mp4"; // Replace with path to actual movie file.
                var filePath = filepath; // Replace with path to actual movie file.

                lineNumber = "Before upload video " + filepath;
                using (var fileStream = new FileStream(filePath, FileMode.Open))
                {
                    var videosInsertRequest = youtubeService.Videos.Insert(video, "snippet,status", fileStream, "video/*");
                    //videosInsertRequest.ProgressChanged += videosInsertRequest_ProgressChanged;
                    //videosInsertRequest.ResponseReceived += videosInsertRequest_ResponseReceived;

                    var result = videosInsertRequest.Upload();
                    lineNumber = "After Upload " + result;
                }

                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message + ":LINE:" + lineNumber + "::::" + ex.InnerException;
            }
        }

        //public string uploadYoutubeVideo(string filepath, string title, string description, List<string> tags, string uniqueID, string youtubeCertFilePath)
        //{
        //    string lineNumber = "1";
        //    try
        //    {                
        //        var CertPath = youtubeCertFilePath; // @"D://mydownloads/youtube_client_certificate1.json";
        //        lineNumber = youtubeCertFilePath;
        //        UserCredential credential;
        //        using (var stream = new FileStream(System.Web.HttpContext.Current.Server.MapPath(CertPath), FileMode.Open, FileAccess.Read))
        //        {
        //            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
        //                GoogleClientSecrets.Load(stream).Secrets,
        //                new[] { YouTubeService.Scope.YoutubeUpload },
        //                "ebaauser",                        
        //                CancellationToken.None
        //            ).Result;
        //        }
        //        lineNumber = "cert validation complted : " + youtubeCertFilePath ;
        //        // This OAuth 2.0 access scope allows an application to upload files to the
        //        // authenticated user's YouTube channel, but doesn't allow other types of access.

        //        var youtubeService = new YouTubeService(new BaseClientService.Initializer()
        //        {
        //            HttpClientInitializer = credential,
        //            ApplicationName = "EbaaApplication"
        //        });

        //        var video = new Video();
        //        video.Snippet = new VideoSnippet();
        //        video.Snippet.Title = title;
        //        video.Snippet.Description = description;
        //        video.Snippet.Tags = tags;
        //        video.Id = uniqueID;
        //        //video.Snippet.CategoryId = "22"; // See https://developers.google.com/youtube/v3/docs/videoCategories/list
        //        video.Status = new VideoStatus();
        //        video.Status.PrivacyStatus = "private"; // or "private" or "public"
        //        //var filePath = @"D://mydownloads/ebaatest_1_1280x720_1mb.mp4"; // Replace with path to actual movie file.
        //        var filePath = filepath; // Replace with path to actual movie file.

        //        lineNumber = "Before upload video " + filepath;
        //        using (var fileStream = new FileStream(filePath, FileMode.Open))
        //        {
        //            var videosInsertRequest = youtubeService.Videos.Insert(video, "snippet,status", fileStream, "video/*");
        //            //videosInsertRequest.ProgressChanged += videosInsertRequest_ProgressChanged;
        //            //videosInsertRequest.ResponseReceived += videosInsertRequest_ResponseReceived;

        //            var result = videosInsertRequest.Upload();
        //            lineNumber = "After Upload " + result;
        //        }

        //        return "success";
        //    }
        //    catch (Exception ex)
        //    {   
        //        return ex.Message + ":LINE:" + lineNumber + "::::" +  ex.InnerException ;
        //    }
        //}
    }
}