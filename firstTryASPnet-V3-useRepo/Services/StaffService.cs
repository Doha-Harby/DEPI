using firstTryASPnet.Models;
using firstTryASPnet.Repositories;
using System.Collections.Generic;

namespace firstTryASPnet.Services
{
    public class StaffService
    {
        private readonly StaffRepository _staffRepository;

        public StaffService(StaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public List<Staff> GetAllStaff()
        {
            return _staffRepository.GetAll();
        }

        public Staff GetStaffById(int id)
        {
            return _staffRepository.GetById(id);
        }

        public void AddStaff(Staff staff)
        {
            // مثال بسيط على منطق البزنس:
            if (string.IsNullOrEmpty(staff.Name))
                throw new Exception("Staff name cannot be empty.");

            _staffRepository.Add(staff);
        }
    }
}
