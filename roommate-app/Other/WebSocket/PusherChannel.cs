using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PusherServer;

namespace roommate_app.Other.WebSocket;
public class PusherChannel
{
    public static async Task<IActionResult> Trigger(object data, string channelName, string eventName)
    {
        var options = new PusherOptions
        {
            Cluster = "mt1",
            Encrypted = true
        };
        var pusher = new Pusher(
          "1502522",
          "ae5e63689c26a34fbdbf",
          "99bd935523c311ad8b79",
          options
        );

        var result = await pusher.TriggerAsync(
          channelName,
          eventName,
          data
        );
        return new OkObjectResult(data);
    }

    public static void OnListingFeedUpdated(object source, EventArgs e)
    {
        Trigger(new object(), "listing_feed", "feed_updated");
    }
}

