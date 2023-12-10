using Coreplus.Sample.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Coreplus.Sample.Api.Endpoints.Appointment
{
    public static class GetAppointmentsReport
    {
        [HttpGet]
        public static RouteGroupBuilder MapGetAppointmentsReport(this RouteGroupBuilder group)
        {
            group.MapGet("/appointmentReport", async (DateTime? startDate, DateTime? endDate, [FromServices] AppointmentService appointmentService) =>
            {
                var appointments = await appointmentService.GenerateReport(startDate, endDate);
                return Results.Ok(appointments);
            });

            return group;
        }
    }
}
