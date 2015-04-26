using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using US.AppointmentScheduling.RESTAPI.DAO;
using System.Collections.Generic;
using US.AppointmentScheduling.RESTAPI.DAO.Exceptions;

namespace US.AppointmentScheduling.RESTAPI.Tests
{
    [TestClass]
    public class USAppointmentSchedulingTests
    {
        [TestMethod]
        public void TestGetAllAppointments()
        {
            List<Appointment> apptList;

            try
            {
                apptList = AppointmentSchedulingServices.GetAllAppointments();

                Assert.IsNotNull(apptList, "Appointment list not null.");
                Assert.IsTrue(apptList.Count > 0, "One or more elements found this year.");

            }
            catch (AssertFailedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception thrown --> " + ex.ToString());
            }

        }

        [TestMethod]
        public void TestCreateNewAppointment()
        {
            Appointment appt = new Appointment();

            try
            {
                appt.StartTime = DateTime.Now.AddDays(1);
                appt.EndTime = DateTime.Now.AddDays(1).AddMinutes(15);
                appt.FirstName = "John";
                appt.LastName = "Doe";
                appt.Comments = "Appointment Created";

                bool result = AppointmentSchedulingServices.AddAppointment(appt);

                Assert.IsTrue(result);              

            }
            catch (AssertFailedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception thrown --> " + ex.ToString());
            }

        }

        [TestMethod]
        public void TestCreateNewAppointmentInThePast()
        {
            Appointment appt = new Appointment();

            try
            {
                appt.StartTime = DateTime.Now.AddDays(-1);
                appt.EndTime = DateTime.Now.AddDays(-1).AddMinutes(15);
                appt.FirstName = "John";
                appt.LastName = "Doe";
                appt.Comments = "Appointment Created";

                bool result = AppointmentSchedulingServices.AddAppointment(appt);

            }
            catch (InvalidRangeException)
            {
                Assert.IsTrue(true, "Cannot create appointments in the past.");
            }
            catch (AssertFailedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception thrown --> " + ex.ToString());
            }

        }

        [TestMethod]
        public void TestGetAppointmentsByRange()
        {
            List<Appointment> apptList;

            try
            {
                apptList = AppointmentSchedulingServices.GetAppointmentsByDateRange(new DateTime(2015, 01, 01), new DateTime(2015, 12, 31));

                Assert.IsNotNull(apptList, "Appointment list not null.");
                Assert.IsTrue(apptList.Count > 0, "One or more elements found this year.");

            }
            catch (AssertFailedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception thrown --> " + ex.ToString());
            }

        }

        [TestMethod]
        public void TestUpdateLastAppointment()
        {
            Appointment appt = new Appointment();

            try
            {

                List<Appointment> apptList = AppointmentSchedulingServices.GetAllAppointments();

                if (apptList != null && apptList.Count > 0)
                {
                    appt = apptList[apptList.Count-1];

                    appt.Comments = "Appointment updated";
                    appt.StartTime = DateTime.Now.AddDays(1);
                    appt.EndTime = DateTime.Now.AddDays(1).AddMinutes(30);

                    bool result = AppointmentSchedulingServices.UpdateAppointment(appt);

                    Assert.IsTrue(result, "Appointment updated.");
                }
                else
                {
                    Assert.Fail("Appointment List was Empty");
                }
                 
            }
            catch (AssertFailedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception thrown --> " + ex.ToString());
            }

        }

        [TestMethod]
        public void TestExactOverlapOnAddAppointment()
        {
            Appointment appt = new Appointment();

            try
            {

                List<Appointment> apptList = AppointmentSchedulingServices.GetAllAppointments();

                if (apptList != null && apptList.Count > 0)
                {
                    appt = apptList[apptList.Count - 1];
                    bool result = AppointmentSchedulingServices.AddAppointment(appt);
                }
                else
                {
                    Assert.Fail("Appointment List was Empty");
                }


            }
            catch (AssertFailedException)
            {
                throw;
            }
            catch (InvalidRangeException)
            {
                Assert.IsTrue(true, "Invalid range expected");
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception thrown --> " + ex.ToString());
            }

        }

        [TestMethod]
        public void TestLowerBoundOverlapOnAddAppointment()
        {
            Appointment appt = new Appointment();

            try
            {

                List<Appointment> apptList = AppointmentSchedulingServices.GetAllAppointments();

                if (apptList != null && apptList.Count > 0)
                {
                    appt = apptList[apptList.Count - 1];
                    appt.StartTime = appt.StartTime.Value.AddMinutes(-15);
                    appt.EndTime = appt.EndTime.AddMinutes(-15);
                    bool result = AppointmentSchedulingServices.AddAppointment(appt);
                }
                else
                {
                    Assert.Fail("Appointment List was Empty");
                }


            }
            catch (AssertFailedException)
            {
                throw;
            }
            catch (InvalidRangeException)
            {
                Assert.IsTrue(true, "Invalid range expected.");
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception thrown --> " + ex.ToString());
            }

        }

        [TestMethod]
        public void TestUpperOverlapOnAddAppointment()
        {
            Appointment appt = new Appointment();

            try
            {

                List<Appointment> apptList = AppointmentSchedulingServices.GetAllAppointments();

                if (apptList != null && apptList.Count > 0)
                {
                    appt = apptList[apptList.Count - 1];
                    appt.StartTime = appt.StartTime.Value.AddMinutes(15);
                    appt.EndTime = appt.EndTime.AddMinutes(15);
                    bool result = AppointmentSchedulingServices.AddAppointment(appt);
                }
                else
                {
                    Assert.Fail("Appointment List was Empty");
                }


            }
            catch (AssertFailedException)
            {
                throw;
            }
            catch (InvalidRangeException)
            {
                Assert.IsTrue(true, "Invalid range expected.");
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception thrown --> " + ex.ToString());
            }

        }

        [TestMethod]
        public void TestDeleteLastAppointment()
        {
            Appointment appt = new Appointment();

            try
            {

                List<Appointment> apptList = AppointmentSchedulingServices.GetAllAppointments();

                if (apptList != null && apptList.Count > 0)
                {
                    appt = apptList[apptList.Count - 1];

                    bool result = AppointmentSchedulingServices.DeleteAppointment(appt.ID);

                    Assert.IsTrue(result, "Appointment deleted.");
                }
                else
                {
                    Assert.Fail("Appointment List was Empty");
                }


            }
            catch (AssertFailedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception thrown --> " + ex.ToString());
            }

        }

    }
}
