using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.DTOs.Person;

public sealed record UpdateImageRequest(
    string ImagePath
);