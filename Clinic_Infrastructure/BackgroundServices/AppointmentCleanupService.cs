
using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Clinic_Application.Features.Appointments.Services;

namespace Clinic_Infrastructure.BackgroundServices
{
    
    public class AppointmentCleanupService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public AppointmentCleanupService(
            IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope =
                    _scopeFactory.CreateScope();

                var mediator =
                    scope.ServiceProvider
                        .GetRequiredService<IMediator>();

                await mediator.Send(
                    new CancelExpiredAppointmentsCommand(),
                    stoppingToken);

                await Task.Delay(
                    TimeSpan.FromHours(1),
                    stoppingToken);
            }
        }
    }
}
