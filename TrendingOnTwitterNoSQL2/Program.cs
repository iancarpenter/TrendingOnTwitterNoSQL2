using System;
using Raven.Client.Document;
using Tweetinvi;

namespace TrendingOnTwitterNoSQL2
{
    class Program
    {
        static void Main(string[] args)
        {
            // replace CONSUMER_KEY, CONSUMER_SECRET etc. with your own twitter api creditials here
            Auth.SetUserCredentials("CONSUMER_KEY", "CONSUMER_SECRET", "ACCESS_TOKEN", "ACCESS_TOKEN_SECRET");

            var stream = Stream.CreateFilteredStream();
            stream.AddTrack("CanadianGP");

            var documentStore = DocumentStoreHolder.Store;

            using (BulkInsertOperation bulkInsert = documentStore.BulkInsert())
            {

                stream.MatchingTweetReceived += (sender, theTweet) =>
                {
                    Console.WriteLine(theTweet.Tweet.FullText);

                    var tm = new TwitterModel
                    {
                        Id = theTweet.Tweet.Id,
                        Tweet = theTweet.Tweet.FullText
                    };

                    bulkInsert.Store(tm);

                };
                stream.StartStreamMatchingAllConditions();
            }

        }
    }
}