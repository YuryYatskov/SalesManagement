using Microsoft.AspNetCore.Components.Authorization;
using SMApp.Data;
using SMApp.Data.Entities;
using SMApp.Extensions;
using SMApp.Models;
using SMApp.Services.Contracts;

namespace SMApp.Services;

public class AppointmentService(SalesManagementDbContext _dbContext,
    AuthenticationStateProvider _authenticationStateProvider) : IAppointmentService
{
    public async Task<List<AppointmentModel>> GetAppointments()
    {
        try
        {
            var employee = await GetLoggedOnEmployee();

            return await _dbContext.Appointments.Where(e => e.EmployeeId == employee.Id).Convert();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task AddAppointment(AppointmentModel appointmentModel)
    {
        try
        {
            var employee = await GetLoggedOnEmployee();

            appointmentModel.EmployeeId = employee.Id;

            Appointment appointment = appointmentModel.Convert();
            await _dbContext.AddAsync(appointment);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task UpdateAppointment(AppointmentModel appointmentModel)
    {
        try
        {
            Appointment? appointment = await _dbContext.Appointments.FindAsync(appointmentModel.Id);

            if (appointment != null)
            {
                appointment.Description = appointmentModel.Description;
                appointment.IsAllDay = appointmentModel.IsAllDay;
                appointment.RecurrenceId = appointmentModel.RecurrenceId;
                appointment.RecurrenceRule = appointmentModel.RecurrenceRule;
                appointment.RecurrenceException = appointmentModel.RecurrenceException;
                appointment.StartTime = appointmentModel.StartTime;
                appointment.EndTime = appointmentModel.EndTime;
                appointment.Location = appointmentModel.Location;
                appointment.Subject = appointmentModel.Subject;

                await _dbContext.SaveChangesAsync();
            }

        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task DeleteAppointment(int id)
    {
        try
        {
            Appointment? appointment = await _dbContext.Appointments.FindAsync(id);

            if (appointment != null)
            {
                _dbContext.Remove(appointment);
                await _dbContext.SaveChangesAsync();
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    private async Task<Employee> GetLoggedOnEmployee()
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        return await user.GetEmployeeObject(_dbContext);
    }
}
