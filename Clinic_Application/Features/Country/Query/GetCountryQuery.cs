using Clinic_Application.DTOs.Country;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Country.Query
{
    public sealed class GetCountryQuery : IRequest<List<CountryDTO>>;
    
}
