using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.AuthFeature.Commands;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.AuthFeature.Handlers.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, OperationResult<string>>
    {
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginCommandHandler(
            IMapper mapper,
            IPasswordHasher<User> passwordHasher,
            IUserRepository userRepository,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<OperationResult<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.LoginDto.Email);

            if (user == null)
            {
                return OperationResult<string>.Failure("Invalid email or password.");
            }

            // Verifiera lösenord
            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.LoginDto.Password);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                return OperationResult<string>.Failure("Invalid email or password.");
            }

            // Generera JWT-token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return OperationResult<string>.Success(token);
        }
    }
}
