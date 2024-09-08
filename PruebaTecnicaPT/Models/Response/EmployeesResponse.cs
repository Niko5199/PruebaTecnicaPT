using PruebaTecnicaPT.Models.Dtos;

namespace PruebaTecnicaPT.Models.Response
{
    public class EmployeesResponse
    {
        public class EmployesListRespons
        {
            public int PageNumber { get; set; }
            public int PageSize { get; set; }
            public int TotalPages { get; set; }
            public int TotalEmployees { get; set; }
            public List<EmployeeDto> Employees { get; set; }
        }
    }
}
