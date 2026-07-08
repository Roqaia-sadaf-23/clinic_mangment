using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.Country;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Country.Query
{
    public class GetCountryQueryHandler(IAppDBContext context) : IRequestHandler<GetCountryQuery, List<CountryDTO>>
    {
        public async Task<List<CountryDTO>> Handle(GetCountryQuery request, CancellationToken cancellationToken)
        {
var countries = context.Countries.AsNoTracking().Select(c => new CountryDTO
{
    Id = c.Id,
    CountryName = c.CountryName
}).ToList();
            return await Task.FromResult(countries);

        }
    }
}
