using Coreplus.Sample.Api.Endpoints.Appointment;

namespace Coreplus.Sample.Api.Endpoints.Practitioner;

public static class MapEndpoints
{
    public static RouteGroupBuilder MapPractitionerEndpoints(this RouteGroupBuilder group)
    {
        group.MapGetAllPractitioners();
        group.MapGetRemainingPractitioners();
        group.MapGetSupervisorPractitioners();
        group.MapGetAllAppointments();
        group.MapGetAppointmentsReport();
        group.MapGetAppointmentsByPracId();
        return group;
    }
}