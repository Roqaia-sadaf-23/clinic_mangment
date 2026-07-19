using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Common.Interfaces
{
    //namespace Clinic_Application.Common.Interfaces;

    public interface IImageStorage
    {
        Task<string> SaveAsync(
            Stream content,
            string fileName,
            string contentType,
            CancellationToken cancellationToken
        );

        Task DeleteAsync(
            string imagePath,
            CancellationToken cancellationToken
        );
    }
}