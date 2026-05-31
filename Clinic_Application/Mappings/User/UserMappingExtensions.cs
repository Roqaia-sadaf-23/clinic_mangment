using Clinic_Application.DTOs.User;
using Clinic_Domain.Entities;
using System.Net;


namespace Clinic_Application.Mappings.UserMapping;


public static class UserMappingExtensions
{
    public static UserDTO ToDTO(this User user)
    {
        if (user == null) return null!;

        return new UserDTO
        {
            Id = user.Id,          
            PersonId = user.PersonId,

            FirstName = user.Person.FirstName,
            LastName = user.Person.LastName,
            NationalityNo = user.Person.NationalityNo,
            Email = user.Email,
            UserName = user.UserName,
            PhoneNumber = user.Person.PhoneNumber,

            RoleName = user.UserRoles
                .FirstOrDefault()?
                .Role?
                .RoleName ?? string.Empty,

            IsActive = user.IsActive,
            Age = (int)user.Person.Age,
            Address = user.Person.Address,
            Gender = user.Person.Gender,
            NationalityCountryID = user.Person.NationalityCountryId,
            ImagePath = user.Person.ImagePath,
            Note = user.Person.Note
        };
    }

    public static User ToEntity(this CreateUserDTO user, string passwordHash)
    {
        return User.CreateUser(
            user.PersonId,
            user.Email,
            user.UserName,
            passwordHash,
            user.IsActive
        );
    }

    public static void UpdateEntity(this UpdateUserDTO dto, User user)
    {

        user.UpdateUser(
            dto.Email,
            dto.IsActive,
            DateTime.Now
        );


         
    }

 

        //    user.FullName = dto.FullName;
        //    user.Email = dto.Email;
        //    user.PhoneNumber = dto.PhoneNumber;
        //    user.Role = dto.Role;
        //    user.DateOfBirth = dto.DateOfBirth;
        //    user.Address = dto.Address;
        //    user.Gender = dto.Gender;
        }
    










//    public static class UserDTOExtition
//    {
//        public static UserDTO ToDTO(this UserEntity user)
//        {
//         return new UserDTO
//        {
//            Id = user.Id,
//            FullName = user.FullName,
//            Email = user.Email,
//            PhoneNumber = user.PhoneNumber,
//            Role = user.Role,
//            DateOfBirth = user.DateOfBirth,
//            Address = user.Address,
//            Gender = user.Gender,
//            CreatedAt = user.CreatedAt
//        };
//    }

//        public static UserEntity ToEntity(this UserDTO user)
//        {
//        return new UserEntity
//        {

//            Id = user.Id,
//            FullName = user.FullName,
//            Email = user.Email,
//            PhoneNumber = user.PhoneNumber,
//            Role = user.Role,
//            DateOfBirth = user.DateOfBirth,
//            Address = user.Address,
//            Gender = user.Gender,
//            CreatedAt = user.CreatedAt

//        };
//        }

//    }

//}

