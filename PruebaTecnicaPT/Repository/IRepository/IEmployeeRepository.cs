using PruebaTecnicaPT.Models;

namespace PruebaTecnicaPT.Repository.IRepository
{
    public interface IEmployeeRepository
    {
        ICollection<Employee> GetEmployees(int pageNumber, int pageSize, string nameEmployee);
        Employee GetEmployee(int employeeId);
        bool ExistsEmployee(int employeeId);
        bool ExistsEmployee(string name);
        bool CreateEmployee(Employee employee);
        bool UpdateEmployee(Employee employee);
        bool DeleteEmployee(Employee employee);
        bool SaveChanges();
        public int GetTotalEmployees();

    }
}
