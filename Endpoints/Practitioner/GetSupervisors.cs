using Coreplus.Sample.Api.Services;

namespace Coreplus.Sample.Api.Endpoints.Practitioner;

public static class GetSupervisors
{
    public static RouteGroupBuilder MapGetSupervisorPractitioners(this RouteGroupBuilder group)
    {
        group.MapGet("/supervisors", async (PractitionerService practitionerService) =>
        {
            var practitioners = await practitionerService.GetSupervisorPractitioners();
            return Results.Ok(practitioners);
        });

        return group;
    }
    public static RouteGroupBuilder MapGetRemainingPractitioners(this RouteGroupBuilder group)
    {
        group.MapGet("/remaining", async (PractitionerService practitionerService) =>
        {
            var practitioners = await practitionerService.GetRemainingPractitioners();
            return Results.Ok(practitioners);
        });

        return group;
    }
}