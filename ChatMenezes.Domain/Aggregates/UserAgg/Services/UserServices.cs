using ChatMenezes.Domain.Aggregates.UserAgg.Entities;
using ChatMenezes.Domain.Aggregates.UserAgg.Interfaces;
using ChatMenezes.Domain.Aggregates.UserAgg.Requests;
using ChatMenezes.Domain.Aggregates.UserAgg.Responses;
using ChatMenezes.Domain.Common.Dtos;
using ChatMenezes.Domain.Common.Validators;
using ChatMenezes.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChatMenezes.Domain.Aggregates.UserAgg.Services
{
    public class UserServices : IUserServices
    {
        protected readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserServices(
            IUnitOfWork unitOfWork,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<User?> GetById(string id)
            => await _unitOfWork.UsuarioRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<ResponseCreateDto<LoginResponse>> DoLogin(LoginRequest request)
        {
            var response = new ResponseCreateDto<LoginResponse>();
            var loginResponse = new LoginResponse();

            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, request.RememberMe, false);

            if (!result.Succeeded)
            {
                response.AddWarningValidation(ConstantMessages.EMAIL_PASSWAORD_INVALID);
                return response;
            }

            var user = await _unitOfWork.UsuarioRepository.GetAll().Where(x => x.Email == request.Email).FirstAsync();

            loginResponse.Rota = ConstantMessages.ROUTE_ROOM;
            loginResponse.UserId = user.Id;
            loginResponse.UserEmail = user.Email;
            loginResponse.UserName = user.Name;

            response.Entity = loginResponse;

            return response;
        }

        public async Task<ResponseCreateDto<LoginResponse>> Create(CreateUserRequest request)
        {
            var response = new ResponseCreateDto<LoginResponse>();

            var usuario = new User(request.Email!, request.Name!);

            var result = await _userManager.CreateAsync(usuario, request.Password!);

            if (!result.Succeeded)
                response.AddWarningValidation(string.Join("<br>", result.Errors.Select(x => x.Description)));

            return response;
        }
    }
}
