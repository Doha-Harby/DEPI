namespace firstTryASPnet.Models
{
    public class StaffBl
    {
        List<Staff> staff;
        public StaffBl()
        { 
            staff = new List<Staff>();
            staff.Add(new Staff() { Id = 3, Name = "Mohamed", Position = "Management", Email = "mohamed@gmail.com", Phone = "01033333333", Salary = 10000 });
            staff.Add(new Staff() { Id = 5, Name = "Sara", Position = "Kitchen", Email = "sara@gmail.com", Phone = "01555555555", Salary = 10000 });
            staff.Add(new Staff() { Id = 6, Name = "ali", Position = "Housekeeping", Email = "ali@gmail.com", Phone = "01566666666", Salary = 10000 });
            staff.Add(new Staff() { Id = 1, Name = "Doha", Position = "Management", Email = "doha@gmail.com", Phone = "01111111111", Salary = 10000 });
            staff.Add(new Staff() { Id = 2, Name = "Aya", Position = "Front Office", Email = "aya@gmail.com", Phone = "01222222222", Salary = 10000 });
            staff.Add(new Staff() { Id = 4, Name = "Mona", Position = "Maintenance", Email = "mona@gmail.com", Phone = "01044444444", Salary = 10000 });
        }

        public List<Staff> GetAll()
        {
            return staff;
        }

        public Staff GetById(int id )
        {
            return staff.FirstOrDefault(e => e.Id == id);
        }
    }
}
 