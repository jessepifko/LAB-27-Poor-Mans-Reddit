using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static LAB_27___POOR_MAN_S_REDDIT.Models.RedditPost;
using System.Text.Json;

namespace LAB_27___POOR_MAN_S_REDDIT.Models
{
    public class RedditDal
    {
        //API calls break in two spots commonly.
        //1) Setting up request - url
        //2) Deserialization/Serialization

        public string GetAPIString(string subreddit)
        {
            string url = $"https://www.reddit.com/r/{subreddit}/.json";

            HttpWebRequest request = WebRequest.CreateHttp(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            //Convert the response into something usable
            StreamReader sr = new StreamReader(response.GetResponseStream());
            string output = sr.ReadToEnd();
            return output;
        }

        
        public List<RedditPost> GetPost()
        {
                string output = GetAPIString("aww");
                Rootobject aww = System.Text.Json.JsonSerializer.Deserialize<Rootobject>(output);
                //List<Child> s = aww.data.children[0].data.ToList();
                List<RedditPost> rp = new List<RedditPost>();
                for (int i = 0; i < aww.data.children.Length; i++)
                {
                    RedditPost post = new RedditPost();
                    post.title = aww.data.children[i].data.title;
                    post.thumbnail = aww.data.children[i].data.thumbnail;
                    post.url = aww.data.children[i].data.url;
                    rp.Add(post);
                }

                //List<RedditPost> rp = JsonSerializer.Deserialize<List<RedditPost>>(JsonSerializer.Serialize<List<Child>>(s));
                return rp;
        }
    }
}
