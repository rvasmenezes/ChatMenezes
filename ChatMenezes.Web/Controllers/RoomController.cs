using ChatMenezes.Domain.Aggregates.MessageAgg.Interfaces;
using ChatMenezes.Domain.Aggregates.RoomAgg.Interfaces;
using ChatMenezes.Web.Models.ViewModels.RoomViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ChatMenezes.Web.Controllers
{
    [Authorize]
    public class RoomController : BaseController
    {
        public readonly IRoomServices _roomServices;
        public readonly IMessageServices _messageServices;

        public RoomController(
            IRoomServices roomServices,
            IMessageServices messageServices)
        {
            _roomServices = roomServices;
            _messageServices = messageServices;
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> List()
            => View(await _roomServices.GetList());

        [HttpGet]
        [Route("[controller]/Create")]
        public IActionResult Create()
            => View(new RoomCreateViewModel());

        [HttpPost]
        [Route("[controller]/Create")]
        public async Task<IActionResult> Create(RoomCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _roomServices.Add(viewModel.Name);

                if (!response.ValidationResult.IsValid)
                    return BadRequest(response.ValidationResult.Errors.FirstOrDefault().ErrorMessage);

                return Ok("/Room");
            }
            
            return BadRequest("Fill in the fields correctly!");
        }

        [HttpGet]
        [Route("[controller]/Chat/{roomId}")]
        public async Task<IActionResult> Chat(int roomId)
        {
            var viewModel = new ChatViewModel
            {
                Room = await _roomServices.GetById(roomId)
            };

            return View(viewModel);
        }
    }
}
