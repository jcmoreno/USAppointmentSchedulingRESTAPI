using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using US.AppointmentScheduling.RESTAPI.DAO;
using US.AppointmentScheduling.RESTAPI.DAO.Exceptions;

namespace US.AppointmentScheduling.RESTAPI.DAO
{
   
    public static class AppointmentSchedulingServices
    {
        public static List<Appointment> GetAllAppointments()
        {
            var dc = new AppointmentsDBEntities();
            List<Appointment> appointments = (from appt in dc.Appointments
                                              select appt).ToList<Appointment>();

            return appointments;
        }

        public static Appointment GetAppointmentsByID(int id)
        {
            var dc = new AppointmentsDBEntities();
            Appointment appointment;

            try
            {
                appointment = (from appt in dc.Appointments
                                           where appt.ID == id
                                           select appt).FirstOrDefault<Appointment>();

            }
            catch (Exception)
            {
                throw new Exception("Error fetching appointments!");
            }

            return appointment;
        }

        public static List<Appointment> GetAppointmentsByDateRange(DateTime startTime, DateTime endTime)
        {
            var dc = new AppointmentsDBEntities();
            List<Appointment> appointments;
            
            try
            {
                appointments = (from appt in dc.Appointments
                                where appt.StartTime >= startTime &&
                                      appt.EndTime <= endTime
                                select appt).ToList<Appointment>();
            }
            catch (Exception)
            {
                throw new Exception("Error fetching appointments!");
            }

            return appointments;
        }

        public static bool AddAppointment(Appointment appointment)
        {
            var dc = new AppointmentsDBEntities();
            bool success = false;

            try
            {
                if (isScheduleRangeValid(appointment.StartTime, appointment.EndTime, null)) {
                    dc.Appointments.Add(appointment);
                    success = 0 < dc.SaveChanges();
                }
                else
                {
                    throw new InvalidRangeException("Invalid range!");
                }

            }
            catch (Exception)
            {
                throw;
            }

            return success;
        }

        public static bool UpdateAppointment(Appointment appointment)
        {
            var dc = new AppointmentsDBEntities();
            bool success = false;

            try
            {
                if (isScheduleRangeValid(appointment.StartTime, appointment.EndTime, appointment.ID))
                {
                    var apptmnt = (from appt in dc.Appointments
                                   where appt.ID == appointment.ID
                                   select appt).FirstOrDefault<Appointment>();

                    if (apptmnt != null)
                    {
                        apptmnt.FirstName = appointment.FirstName;
                        apptmnt.LastName = appointment.LastName;
                        apptmnt.StartTime = appointment.StartTime;
                        apptmnt.EndTime = appointment.EndTime;
                        apptmnt.Comments = appointment.Comments;

                        success = 0 < dc.SaveChanges();
                    }
                    else
                    {
                        throw new RecordNotFoundException("Record not found!");
                    }
                }
                else
                {
                    throw new InvalidRangeException("Invalid range!");
                }

            }
            catch (Exception)
            {
                throw;
            }

            return success;
        }

        public static bool DeleteAppointment(int apptId)
        {
            var dc = new AppointmentsDBEntities();
            bool success = false;

            try
            {
                var apptmnt = (from appt in dc.Appointments
                               where appt.ID == apptId
                               select appt).FirstOrDefault<Appointment>();

                if (apptmnt != null)
                {
                    dc.Appointments.Remove(apptmnt);
                    success = 0 < dc.SaveChanges();
                }
                else
                {
                    throw new RecordNotFoundException("Record not found!");
                }

            }
            catch (Exception)
            {
                throw;
            }

            return success;
        }

        private static bool isScheduleRangeValid(Nullable<DateTime> start, Nullable<DateTime> end, Nullable<int> id)
        {
            var dc = new AppointmentsDBEntities();
            List<Appointment> appointments;

            try
            {
                if (start <= DateTime.Now || end <= DateTime.Now)
                    return false;

                if (id == null) {
                    appointments = (from appt in dc.Appointments
                                    where ((appt.StartTime >= start && appt.StartTime < end) ||
                                           (appt.EndTime > start && appt.EndTime <= end))
                                    select appt).ToList<Appointment>();
                }
                else {
                    appointments = (from appt in dc.Appointments
                                    where ((appt.StartTime >= start && appt.StartTime < end) ||
                                           (appt.EndTime > start && appt.EndTime <= end)) &&
                                           (appt.ID != id)
                                    select appt).ToList<Appointment>();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error validating appointment range!");
            }

            return appointments.Count == 0;

        }

    }
}
