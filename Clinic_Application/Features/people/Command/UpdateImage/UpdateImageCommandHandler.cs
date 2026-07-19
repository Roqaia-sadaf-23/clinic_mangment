using Clinic_Application.Common.Interfaces;
using Clinic_Application.Features.people.Command.UpdateImage;

using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Clinic_Application.Features.People.Commands.UpdateImage;

public sealed class UpdateImageCommandHandler
    : IRequestHandler<UpdateImageCommand, bool>
{
    private readonly IAppDBContext _context;

    public UpdateImageCommandHandler(
        IAppDBContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(
        UpdateImageCommand request,
        CancellationToken cancellationToken)
    {
        var person = await _context.People
            .FirstOrDefaultAsync(
                person =>
                    person.User.Id == request.UserId,
                cancellationToken
            );

        if (person is null)
            return false;

        person.ImagePath = request.ImagePath;

        await _context.SaveChangesAsync(
            cancellationToken
        );

        return true;
    }
}












































//using Clinic_Application.Common.Interfaces;
//using Clinic_Application.DTOs.Person;
//using Clinic_Application.Features.people.Command.UpdateMyImage;
//using MediatR;
//using Microsoft.EntityFrameworkCore;

//namespace Clinic_Application.Features.People.Commands.UpdateMyImage;

//public sealed class UpdateMyImageCommandHandler
//    : IRequestHandler<
//        UpdateMyImageCommand,
//        UpdateMyImageResult?
//    >
//{
//    private readonly IAppDBContext _context;
//    private readonly IImageStorage _imageStorage;

//    public UpdateMyImageCommandHandler(
//        IAppDBContext context,
//        IImageStorage imageStorage)
//    {
//        _context = context;
//        _imageStorage = imageStorage;
//    }

//    public async Task<UpdateMyImageResult?> Handle(
//        UpdateMyImageCommand request,
//        CancellationToken cancellationToken)
//    {
//        var person = await _context.People
//            .FirstOrDefaultAsync(
//                person =>
//                    person.User.Id == request.UserId,
//                cancellationToken
//            );

//        if (person is null)
//            return null;

//        var oldImagePath = person.ImagePath;

//        var newImagePath =
//            await _imageStorage.SaveAsync(
//                request.Content,
//                request.FileName,
//                request.ContentType,
//                cancellationToken
//            );

//        person.ImagePath = newImagePath;

//        try
//        {
//            await _context.SaveChangesAsync(
//                cancellationToken
//            );
//        }
//        catch
//        {
//            // إذا فشل تحديث قاعدة البيانات، احذفي الصورة الجديدة.
//            await _imageStorage.DeleteAsync(
//                newImagePath,
//                cancellationToken
//            );

//            throw;
//        }

//        if (!string.IsNullOrWhiteSpace(oldImagePath))
//        {
//            await _imageStorage.DeleteAsync(
//                oldImagePath,
//                cancellationToken
//            );
//        }

//        return new UpdateMyImageResult(
//            newImagePath
//        );
//    }
//}