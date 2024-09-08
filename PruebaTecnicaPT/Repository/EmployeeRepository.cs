using PruebaTecnicaPT.Data;
using PruebaTecnicaPT.Models;
using PruebaTecnicaPT.Repository.IRepository;

namespace PruebaTecnicaPT.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool CreateEmployee(Employee employee)
        {
            employee.Created = DateTime.Now;
            _context.Employees.Add(employee);
            return SaveChanges();
        }

        public bool DeleteEmployee(Employee employee)
        {
            _context.Remove(employee);
            return SaveChanges();
        }

        public bool ExistsEmployee(int employeeId)
        {
           return _context.Employees.Any(employee => employee.EmployeeId == employeeId);

        }

        public bool ExistsEmployee(string name)
        {
           bool value = _context.Employees.Any(employee => employee.FirstName == name);
           return value;
        }

        public Employee GetEmployee(int employeeId)
        {
            Employee employee = _context.Employees.FirstOrDefault(employee => employee.EmployeeId == employeeId);
            return employee;
        }

        public ICollection<Employee> GetEmployees(int pageNumber, int pageSize, string nameEmployee)
        {
            IQueryable<Employee> query = _context.Employees;
            if (!string.IsNullOrEmpty(nameEmployee))
            {
                query = query.Where(e => e.FirstName == nameEmployee || e.LastName == nameEmployee);
            }
            return query.OrderBy(employee => employee.FirstName)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public int GetTotalEmployees()
        {
            return _context.Employees.Count();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateEmployee(Employee employee)
        {
            employee.Modify = DateTime.Now;
            _context.Employees.Update(employee);
            return SaveChanges();
        }
    }
}
