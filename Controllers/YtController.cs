using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using YoutubeExplode;
//using YoutubeExplode.Models.MediaStreams;

namespace ytVideosDownload.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YtController : ControllerBase
    {
        [HttpPost("download")]
        public async Task<IActionResult> DownloadVideoAsync(string youtubeLink)
        {
            try
            {
                var youtube = new YoutubeClient();
                string fileName = GenerateFileName();
                string videoId = youtubeLink;
                if (youtubeLink.Contains("/"))
                {
                    string[] parts = youtubeLink.Split('/');
                    videoId = parts[parts.Length - 1];
                }


                if (youtubeLink != null)
                {
                    var streamInfoSet = await youtube.Videos.Streams.GetManifestAsync(youtubeLink);
                    var streamInfo = streamInfoSet.GetMuxedStreams();

                    // Sort by video quality and select the highest quality stream
                    var videoStream = streamInfo
                        .OrderByDescending(s => s.VideoQuality)
                        .FirstOrDefault();

                    if (videoStream != null)
                    {
                        using (var httpClient = new HttpClient())
                        {
                            // Set a timeout for the HTTP request (e.g., 5 minutes)
                          //  httpClient.Timeout = TimeSpan.FromMinutes(2);

                            // Download the video
                            var videoBytes = await httpClient.GetByteArrayAsync(videoStream.Url);

                            // Save the video to a file
                            var filePath = Path.Combine("C:\\youtube", fileName);
                            await System.IO.File.WriteAllBytesAsync(filePath, videoBytes);

                            return Ok($"Video downloaded and saved as {filePath}");
                        }
                    }
                }

                return NotFound("Video not found");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        private string GenerateFileName()
        {
            // Generate a unique file name for the downloaded video
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".mp4";
            return fileName;
        }
    }
}
