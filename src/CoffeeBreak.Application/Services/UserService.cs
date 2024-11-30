using CoffeeBreak.Application.Common.Interfaces;
using CoffeeBreak.Domain.Entities;
using CoffeeBreak.Application.Common.Exceptions;
using CoffeeBreak.Application.DTOs.Request.User;
using CoffeeBreak.Application.Common.Enums;
using CoffeeBreak.Application.Common.Interfaces.Email;
using CoffeeBreak.Application.Common.Interfaces.Services;
using CoffeeBreak.Application.Common.Interfaces.Repositories;

namespace CoffeeBreak.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IVerificationCodeRepository _verificationCodeRepository;
        private readonly IEmailService _emailService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IAuthorizationService _authorizationService;

        public UserService(IUserRepository userRepository, IVerificationCodeRepository verificationCodeRepository, IEmailService emailService, IJwtTokenGenerator jwtTokenGenerator, IAuthorizationService authorizationService)
        {
            _userRepository = userRepository;
            _verificationCodeRepository = verificationCodeRepository;
            _emailService = emailService;
            _jwtTokenGenerator = jwtTokenGenerator;
            _authorizationService = authorizationService;
        }

        public async Task Register(CreateUserDTO userDTO, Role role)
        {
            User? user = await _userRepository.GetByEmail(userDTO.Email);
            if (user != null)
            {
                throw new BaseException($"Email \"{userDTO.Email}\" já está em uso!", System.Net.HttpStatusCode.BadGateway);
            }

            user = new User 
            { 
                Name = userDTO.Name,
                Email = userDTO.Email,
                Role = (byte)role,
                Verified = false,
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
            };

            user.Password = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);

            user.VerificationCodes.Add(_verificationCodeRepository.Create());

            _userRepository.Insert(user, true);

            var EmailMessage = new CoffeeBreak.Application.DTOs.Request.Email.Email
            {
                RecipientEmail = userDTO.Email,
                Subject = "Seu código de uso único",
                Message = $"Seu código é: {user.VerificationCodes.FirstOrDefault()?.Code}.{Environment.NewLine}Se você não solicitou este código, poderá ignorar com segurança este email. Outra pessoa pode ter digitado seu endereço de email por engano."
            };

            _emailService.SendEmail(EmailMessage);
        }

        public async Task<User> ValidateAccount(string email, string sCode)
        {
            var user = await _userRepository.GetByEmail(email);
            if (user is null)
            {
                throw new BaseException("Usuário não encontrado.", System.Net.HttpStatusCode.NotFound);
            }

            if (user.Verified)
            {
                throw new BaseException("Usuário já verificádo", System.Net.HttpStatusCode.BadRequest);
            }

            var code = await _verificationCodeRepository.GetByUserIdByCode(user.Id, sCode);

            if (code is null)
            {
                throw new BaseException("Código inválido.", System.Net.HttpStatusCode.NotFound);
            }

            _verificationCodeRepository.DeleteWhere(x => x.UserId == user.Id);

            await _userRepository.Approve(user);

            return user;
        }
        public async Task<string> Login(string email, string password)
        {
            var user = await _userRepository.GetByEmail(email);
            if (user is null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                throw new BaseException("Email ou senha inválidos.", System.Net.HttpStatusCode.NotFound);
            }

            if (!user.Verified)
            {
                throw new BaseException("Conta não verificada.", System.Net.HttpStatusCode.Unauthorized);
            }

            var token = _jwtTokenGenerator.GenerateJwtToken(user.Id, user.Name, user.Email, Enum.Parse<Role>(user.Role.ToString()));

            return token;
        }

        public async Task<string> LoginCode(string email, string sCode)
        {
            var user = await _userRepository.GetByEmail(email);
            if (user is null)
            {
                throw new BaseException("Usuário não encontrado.", System.Net.HttpStatusCode.NotFound);
            }

            var code = await _verificationCodeRepository.GetByUserIdByCode(user.Id, sCode);

            if (code is null)
            {
                throw new BaseException("Código inválido.", System.Net.HttpStatusCode.NotFound);
            }

            _verificationCodeRepository.DeleteWhere(x => x.UserId == user.Id);

            await _userRepository.Approve(user);

            var token = _jwtTokenGenerator.GenerateJwtToken(user.Id, user.Name, user.Email, Enum.Parse<Role>(user.Role.ToString()));

            return token;
        }

        public async Task SendCode(string email)
        {
            var user = await _userRepository.GetByEmail(email);
            if (user is null)
            {
                throw new BaseException("Usuário não encontrado.", System.Net.HttpStatusCode.NotFound);
            }

            var code = _verificationCodeRepository.Create();
            code.UserId = user.Id;

            _verificationCodeRepository.Insert(code, true);

            var EmailMessage = new CoffeeBreak.Application.DTOs.Request.Email.Email
            {
                RecipientEmail = email,
                Subject = "Seu código de uso único",
                Message = $"Seu código é: {code.Code}.{Environment.NewLine}Se você não solicitou este código, poderá ignorar com segurança este email. Outra pessoa pode ter digitado seu endereço de email por engano."
            };

            _emailService.SendEmail(EmailMessage);
        }

        public async Task ChangePassword(string jwtToken, string sCode, string NewPassword)
        {
            var decodedToken = _authorizationService.DecodeToken(jwtToken);

            string email = string.Empty;

            if (!decodedToken.TryGetValue("email", out email))
            {
                throw new BaseException($"Invalid token: {jwtToken}", System.Net.HttpStatusCode.BadRequest);
            };

            if (String.IsNullOrWhiteSpace(email))
            {
                throw new BaseException($"Email não informado", System.Net.HttpStatusCode.BadRequest);
            }

            var user = await _userRepository.GetByEmail(email);
            if (user is null)
            {
                throw new BaseException("Usuário não encontrado.", System.Net.HttpStatusCode.NotFound);
            }

            var code = await _verificationCodeRepository.GetByUserIdByCode(user.Id, sCode);
            if (code is null)
            {
                throw new BaseException("Código inválido.", System.Net.HttpStatusCode.NotFound);
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(NewPassword);
            _userRepository.Commit();

            _verificationCodeRepository.DeleteWhere(x => x.UserId == user.Id);
        }

        public List<User> GetDeliverieMans()
        {
            return _userRepository.GetDeliveryMans();
        }
    }
}
