USAppointmentSchedulingRESTAPI
==============================

INTRO
------------------------------
The following repository contains a simple REST API for scheduling appointments that's built in .Net.

.Net was selected for the Simplicity and array of components that it provides in order to support a 
simple and portable solution.

Artifacts included
------------------- 
 - USAppointmentRESTAPI - REST API
 - USAppointmentRESTAPI.DAO - Data access object built on Microsoft's Entity Model Framework.
 - AppointmentsDB.sdf - LocalCompact database. This type of artifact was selected i order to maximize portability.
 - ApptSchedulingAPI-soapui-project - This is a SOAP UI project that contains all the requests to each API methods together with a few tests.

This API SUPPORTS the following HTTP Methods
--------------------------------------------
 - PUT
 - POST
 - GET
 - DELETE

API Documentation
------------------

PUT: This method is used for creating appointments on the database
------------------------------------------------------------------
```bash
METHOD: PUT
URL: /AppointmentSvc.svc/appointments
DATA: 
{
	"Comments": null,
	"EndTime": "4/20/2015 2:30:00 PM",
	"FirstName": "John",
	"LastName": "Doe",
	"StartTime": "4/20/2015 2:00:00 PM"
}
```

GET: This method is used for fetching appointments ALL or by data range
-----------------------------------------------------------------------
```bash
METHOD: GET
URL: /AppointmentSvc.svc/appointments (ALL)
URL: /AppointmentSvc.svc/appointments?startTime={startTime}&endTime={endTime} (Date Range)
```

POST: This method is used for updating appointments
---------------------------------------------------
```bash
METHOD: PUT
URL: /AppointmentSvc.svc/appointments
DATA: 
{
	"ID": 169
	"Comments": null,
	"EndTime": "4/20/2015 2:30:00 PM",
	"FirstName": "John",
	"LastName": "Doe",
	"StartTime": "4/20/2015 2:00:00 PM"
}
```

DELETE: This method is used for removing appointments 
------------------------------------------------------
```bash
METHOD: DELETE
URL: /AppointmentSvc.svc/appointments/{id}
```

TODO
-----
- More specialized tests on SOAP-UI and .Net unit tests.
- More comments
- Use custom exceptions to enhance error reporting
