using System.Threading.Tasks;
using API.Interfaces;
using API.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.Entities;
using System.Collections.Generic;
using API.DTOs;
using API.Helpers;

namespace API.Controllers
{
    [Authorize]
    public class LikesController : BaseApiController
    {
        private readonly IUnitOfWork  _unitOfWork;
        public LikesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("{username}")]
        public async Task<ActionResult> AddLike(string username)
        {
            var sourceUserId = User.GetUserId();
            var likedUser = await _unitOfWork.userRepository.GetUserByUsernameAsync(username);
            var sourceUser = await _unitOfWork.likesRepository.GetUserWithLikes(sourceUserId);

            if (likedUser == null)
            {
                return NotFound("User not found.");
            }

            if (sourceUserId == likedUser.Id)
            {
                return BadRequest("You cannot like with yourself!");
            }

            var userLike = await _unitOfWork.likesRepository.GetUserLike(sourceUserId, likedUser.Id);

            if (userLike != null)
            {
                return BadRequest("You have already liked this user.");
            }

            userLike = new UserLike
            {
                SourceUserId = sourceUserId,
                LikedUserId = likedUser.Id
            };

            sourceUser.LikedUsers.Add(userLike);

            if (await _unitOfWork.Complete()) return Ok();

            return BadRequest("Failed to like user");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LikeDto>>> GetUserLikes([FromQuery] LikesParams likesParams)
        {
            likesParams.UserId = User.GetUserId();
            var users = await _unitOfWork.likesRepository.GetUserLikes(likesParams);

            Response.AddPaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

            return Ok(users);
        }
    }
}