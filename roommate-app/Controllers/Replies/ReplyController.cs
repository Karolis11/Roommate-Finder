using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using roommate_app.Models;
using roommate_app.Other.Services;

namespace roommate_app.Controllers.Replies;

[ApiController]
[Route("[controller]")]
[ExcludeFromCodeCoverage]
public class ReplyController : Controller
{
    private IGenericService _genericService;
    private IReplyService _replyService;
    public ReplyController(
        IGenericService genericService,
        IReplyService replyService)
    {
        _genericService = genericService;
        _replyService = replyService;
    }

    [HttpPost]
    public async Task<JsonResult> NewReply([FromBody] ReplyRequest request)
    {
        var listing = _genericService.GetById<Listing>(request.ListingId);
        var user = _genericService.GetById<User>(request.UserId);
        var reply = new Reply() { 
            Id =  0, 
            ListingId = request.ListingId,
            Listing = listing,
            UserId = request.UserId,
            User = user,
            Message = request.Message
        };
        await _genericService.AddAsync<Reply>(reply);

        var response = new JsonResult("Created");
        response.StatusCode = 201;

        return response;
    }

    [HttpGet]
    public JsonResult GetReplies(int id)
    {
        var replies = _replyService.GetByListingId(id);

        var response = new JsonResult(replies);
        response.StatusCode = 200;

        return response;
    }
}