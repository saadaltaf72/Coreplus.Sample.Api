using Coreplus.Sample.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Coreplus.Sample.Api.Endpoints.Appointment
{
    public static class GetAppointments
    {
        [HttpGet]
        public static RouteGroupBuilder MapGetAllAppointments(this RouteGroupBuilder group)
        {
            group.MapGet("/appointments", async (AppointmentService appointmentService) =>
            {
                var appointments = await appointmentService.GetAppointments();
                return Results.Ok(appointments);
            });

            return group;
        }
    }
}
