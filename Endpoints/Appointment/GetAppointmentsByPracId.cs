using Coreplus.Sample.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Coreplus.Sample.Api.Endpoints.Appointment
{
    public static class GetAppointmentsByPracId
    {
        [HttpGet]
        public static RouteGroupBuilder MapGetAppointmentsByPracId(this RouteGroupBuilder group)
        {
            group.MapGet("/appointmentsByPracId", async (long id, DateTime? startDate, DateTime? endDate, [FromServices] AppointmentService appointmentService) =>
            {
                var appointments = await appointmentService.GetAppointmentsByPracId(id, startDate,endDate);
                return Results.Ok(appointments);
            });

            return group;
        }
    }
}
