//using Clinic_Application.Mappings.Auth.DTOs;
//using Clinic_Domain.Domain.Entities;
//using ClinicManagementSystem.Domain.Entities;
//using ClinicManagementSystem.Domain.Interfaces;



//namespace Clinic_Application.Mappings.Auth.Services;
//public class AuthService
//{
//    private readonly IUserRepository _userRepository;

//    public AuthService(IUserRepository userRepository)
//    {
//        _userRepository = userRepository;
//    }

//    public async Task RegisterAsync(RegisterRequest request)
//    {
//        if (string.IsNullOrWhiteSpace(request.Email))
//            throw new Exception("Email is required");

//        var existingUser = await _userRepository.GetByEmailAsync(request.Email);

//        if (existingUser is not null)
//            throw new Exception("Email already exists");

//        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

//        var user = new UserEntity(
//            request.FullName,
//            request.Email,
//            request.PhoneNumber,
//            hashedPassword,
//            request.Role,
//            request.DateOfBirth,
//            request.Address,
//            request.Gender
//        );

//        await _userRepository.AddAsync(user);
//    }
//}