using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using US.AppointmentScheduling.RESTAPI.DAO;

namespace USAppointmentRESTAPI
{
    /// <summary>
    /// The AppointmentSvc is the implementation of the Appointment scheduling REST API.
    /// </summary>
    public class AppointmentSvc : IAppointment
    {
        /// <summary>
        /// The AddAppointment operation maps to HTTP PUT method for creating new appointments.
        /// </summary>
        /// <param name="appointment">The new appointment object</param>
        /// <returns>Success flag and Error message</returns>
        public ApiResponse AddAppointment(AppointmentContract appointment)
        {
            Appointment appt;
            bool success = false;
            ApiResponse response = new ApiResponse();

            try
            {
                if (!(String.IsNullOrEmpty(appointment.FirstName) ||
                          String.IsNullOrEmpty(appointment.LastName) ||
                          String.IsNullOrEmpty(appointment.StartTime) ||
                          String.IsNullOrEmpty(appointment.EndTime)))
                {

                    appt = AppointmentSvc.getAppointmentModelType(appointment);

                    if ((appt.StartTime > DateTime.Now) && (appt.StartTime <= appt.EndTime))
                    {
                        success = AppointmentSchedulingServices.AddAppointment(appt);
                    }
                    else
                    {
                        response.ErrorMessage = "Please select a date in the future.";
                    }


                }
            }
            catch (Exception e)
            {
                response.ErrorMessage = "Error creating appointment --> " + e.Message.ToString();
            }
            finally
            {
                response.Success = success.ToString();
            }

            return response;
        }

        /// <summary>
        /// The UpdateAppointment operation maps to HTTP POST method for modifying existing appointments.
        /// </summary>
        /// <param name="appointment">The updated appointment object</param>
        /// <returns>Success flag and Error message</returns>
        public ApiResponse UpdateAppointment(AppointmentContract appointment)
        {
            Appointment appt;
            bool success = false;
            ApiResponse response = new ApiResponse();

            try
            {
                if (!(String.IsNullOrEmpty(appointment.FirstName) ||
                      String.IsNullOrEmpty(appointment.LastName) ||
                      String.IsNullOrEmpty(appointment.StartTime) ||
                      String.IsNullOrEmpty(appointment.EndTime) ||
                      String.IsNullOrEmpty(appointment.ID)))
                {

                    appt = AppointmentSvc.getAppointmentModelType(appointment);

                    if ((appt.StartTime > DateTime.Now) && (appt.StartTime <= appt.EndTime))
                    {
                        if (appt.ID > 0)
                            success = AppointmentSchedulingServices.UpdateAppointment(appt);
                        else
                            response.ErrorMessage = "Missing identifier!";
                    }
                    else
                    {
                        response.ErrorMessage = "Please select a date in the future.";
                    }

                }
            }
            catch (Exception e)
            {
                response.ErrorMessage = "Error updating appointment --> " + e.Message.ToString();
            }
            finally
            {
                response.Success = success.ToString();
            }

            return response;

        }

        /// <summary>
        /// The DeleteAppointment operation maps to HTTP DELETE method for deleting appointments.
        /// </summary>
        /// <param name="id">The appointment id</param>
        /// <returns>Success flag and Error message</returns>
        public ApiResponse DeleteAppointment(string id)
        {
            int apptId;
            bool success = false;
            ApiResponse response = new ApiResponse();

            try
            {
                if (!String.IsNullOrEmpty(id))
                {
                    Int32.TryParse(id, out apptId);

                    if (apptId > 0)
                        success = AppointmentSchedulingServices.DeleteAppointment(apptId);

                }
            }
            catch (Exception e)
            {
                response.ErrorMessage = "Error creating appointment --> " + e.Message.ToString();
            }
            finally
            {
                response.Success = success.ToString();
            }

            return response;
        }

        /// <summary>
        /// The ListAppointments operation maps to HTTP GET method for fetching appointments.
        /// </summary>
        /// <param name="startTime">Start time</param>
        /// <param name="endTime">End time</param>
        /// <returns>Success flag, error message and appointment list.</returns>
        public ApiResponse ListAppointments(String startTime, String endTime)
        {
            List<AppointmentContract> results = new List<AppointmentContract>();
            List<Appointment> appts = null;
            ApiResponse response = new ApiResponse();
            bool success = false;
            DateTime startDt;
            DateTime endDt;

            try
            {   

                DateTime.TryParse(startTime, out startDt);
                DateTime.TryParse(endTime, out endDt);

                if (String.IsNullOrEmpty(startTime) && String.IsNullOrEmpty(endTime))
                {
                    appts = AppointmentSchedulingServices.GetAllAppointments();
                }
                else if ((!String.IsNullOrEmpty(startTime) && !String.IsNullOrEmpty(endTime)) &&
                        (startDt != null && endDt != null) && (startDt <= endDt)) {
                    appts = AppointmentSchedulingServices.GetAppointmentsByDateRange(startDt, endDt);
                }
                else {
                    response.ErrorMessage = "Invalid filters";
                }

                if (String.IsNullOrEmpty(response.ErrorMessage) && appts != null)
                {
                    foreach (Appointment appt in appts)
                    {
                        AppointmentContract tmpApptmnt = new AppointmentContract();

                        tmpApptmnt.ID = appt.ID.ToString();
                        tmpApptmnt.StartTime = appt.StartTime.ToString();
                        tmpApptmnt.EndTime = appt.EndTime.ToString();
                        tmpApptmnt.FirstName = appt.FirstName;
                        tmpApptmnt.LastName = appt.LastName;
                        tmpApptmnt.Comments = appt.Comments;

                        results.Add(tmpApptmnt);
                    }

                    success = true;
                }
                
            }
            catch (Exception e)
            {
                response.ErrorMessage = "Error listing appointments --> " + e.Message.ToString();
            }
            finally
            {
                response.Success = success.ToString();
                response.Appointments = (results.Count > 0) ? results : null;
            }

            return response;
        }

        /// <summary>
        /// Private method for converting AppointmentContract to an Appointment entity.
        /// </summary>
        /// <param name="appointment">The updated appointment object</param>
        /// <returns>Appointment</returns>
        private static Appointment getAppointmentModelType(AppointmentContract appointment) {

            DateTime startDt;
            DateTime endDt;
            int id;
            Appointment appt = new Appointment();

            try
            {
                DateTime.TryParse(appointment.StartTime, out startDt);
                DateTime.TryParse(appointment.EndTime, out endDt);
                Int32.TryParse(appointment.ID, out id);
                
                appt.ID = id;
                appt.FirstName = appointment.FirstName;
                appt.LastName = appointment.LastName;
                appt.StartTime = startDt;
                appt.EndTime = endDt;
                appt.Comments = appointment.Comments;
            }
            catch (Exception)
            {   
                throw;
            }

            return appt;
        }
    }
}
