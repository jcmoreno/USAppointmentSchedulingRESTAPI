using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace USAppointmentRESTAPI
{
    [ServiceContract]
    public interface IAppointment
    {

        [WebInvoke(Method = "PUT", UriTemplate = "appointments", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        ApiResponse AddAppointment(AppointmentContract appointment);

        [WebInvoke(Method = "POST", UriTemplate = "appointments", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        ApiResponse UpdateAppointment(AppointmentContract appointment);

        [WebInvoke(Method = "DELETE", UriTemplate = "appointments/{id}", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        ApiResponse DeleteAppointment(string id);

        [WebGet(UriTemplate = "appointments?startTime={startTime}&endTime={endTime}", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        ApiResponse ListAppointments(String startTime, String endTime);
        
    }

    [DataContract]
    public class AppointmentContract
    {

        string id;
        string startTime;
        string endTime;
        string firstName;
        string lastName;
        string comments;
        
        [DataMember]
        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public string StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        [DataMember]
        public string EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        [DataMember]
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        [DataMember]
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        [DataMember]
        public string Comments
        {
            get { return comments; }
            set { comments = value; }
        }

    }

    [DataContract]
    public class ApiResponse
    {
        string errorMessage;
        string success;
        List<AppointmentContract> appointments;

        [DataMember]
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }

        [DataMember]
        public string Success
        {
            get { return success; }
            set { success = value; }
        }

        [DataMember]
        public List<AppointmentContract> Appointments
        {
            get { return appointments; }
            set { appointments = value; }
        }

    }

}
