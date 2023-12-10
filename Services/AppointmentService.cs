using System.Text.Json;
using Coreplus.Sample.Api.Types;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Coreplus.Sample.Api.Services;

public record AppointmentDto(long id, string date, string client_name, string appointment_type, int duration, int revenue, int practitioner_id, string practitioner_name);
public record ReportResultDto(string PractitionerName, int TotalDuration,int TotalRevenue);

public class AppointmentService
{
    public async Task<IEnumerable<AppointmentDto>> GetAppointments()
    {
        using var fileStreamApp = File.OpenRead(@"./Data/appointments.json");
        var dataApp = await JsonSerializer.DeserializeAsync<Appointment[]>(fileStreamApp);
        if (dataApp == null)
        {
            throw new Exception("Data read error in Appointments");
        }

        using var fileStreamPrac = File.OpenRead(@"./Data/practitioners.json");
        var dataPrac = await JsonSerializer.DeserializeAsync<Practitioner[]>(fileStreamPrac);
        if (dataPrac == null)
        {
            throw new Exception("Data read error in Practitioners");
        }



        return dataPrac.Join(
             dataApp,
             practitioner => practitioner.id,
             appointment => appointment.practitioner_id,
             (practitioner, appointment) => new
             {
                 Practitioner = practitioner,
                 Appointment = appointment
             })
             .Select(app => new AppointmentDto(
                 app.Appointment.id,
                 app.Appointment.date, 
                 app.Appointment.client_name,
                 app.Appointment.appointment_type,
                 app.Appointment.duration,
                 app.Appointment.revenue,
                 app.Appointment.practitioner_id,
                 app.Practitioner.name
             ));
    }

    public async Task<IEnumerable<ReportResultDto>> GenerateReport(DateTime? startDate, DateTime? endDate)
    {
        var appointments = await GetAppointments();

        var filteredAppointments = appointments;

        if (startDate.HasValue)
        {
            filteredAppointments = filteredAppointments
                .Where(app => DateTime.Parse(app.date) >= startDate.Value);
        }

        if (endDate.HasValue)
        {
            filteredAppointments = filteredAppointments
                .Where(app => DateTime.Parse(app.date) <= endDate.Value);
        }

        var report = filteredAppointments
        .GroupBy(app => app.practitioner_name)
        .Select(group => new ReportResultDto(
            PractitionerName: group.Key,
            TotalDuration: group.Sum(app => app.duration),
            TotalRevenue: group.Sum(app => app.revenue)
        ))
        .ToList();


        return report;
    }

}