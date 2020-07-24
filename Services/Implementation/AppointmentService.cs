using Accommodation.DAL.Interface;
using Accommodation.Models;
using Accommodation.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Accommodation.Services.Implementation
{
    public class AppointmentService : IAppointmentService
    {
        private IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public bool CheckAppoinment(Appointment appointment)
        {
            var appoimtments = _appointmentRepository.GetAppointments().ToList();
            bool result = false;
            foreach(var iten in appoimtments)
            {
                if (iten.ManagerId == appointment.ManagerId)
                {
                    if(iten.TimeSlotID==appointment.TimeSlotID && iten.ADate == appointment.ADate)
                    {
                        result= true;
                    }
                }
            }
            return result;

        }

        public bool Delete(Appointment appointment)
        {
            return _appointmentRepository.Delete(appointment);
        }

        public IEnumerable<Appointment> Find(Func<Appointment, bool> prdicate)
        {
            return _appointmentRepository.Find(prdicate);
        }

        public List<Appointment> GetAppointments()
        {
            return _appointmentRepository.GetAppointments().ToList();
        }

        public Appointment GetAppointments(int id)
        {
            return _appointmentRepository.GetAppointments(id);
        }

        public bool Insert(Appointment appointment)
        {
            return _appointmentRepository.Insert(appointment);
        }

        public bool Update(Appointment appointment)
        {
            return _appointmentRepository.Update(appointment);
        }
    }
}