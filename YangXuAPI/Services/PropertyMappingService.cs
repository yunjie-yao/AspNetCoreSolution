using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YangXuAPI.Services
{
    public class PropertyMappingService:IPropertyMappingService
    {
        private readonly Dictionary<string, PropertyMappingValue> _employeePropertyMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                {"Id", new PropertyMappingValue(new List<string> {"Id"})},
                {"CompanyId", new PropertyMappingValue(new List<string> {"CompanyId"})},
                {"Name", new PropertyMappingValue(new List<string> {"FirstName", "LastName"})},
                {"EmployeeNo", new PropertyMappingValue(new List<string> {"EmployeeNo"})},
                {"GenderDisplay", new PropertyMappingValue(new List<string> {"Gender"})},
                {"Age", new PropertyMappingValue(new List<string> {"DateOfBirth"}, true)}
            };
        private readonly Dictionary<string, PropertyMappingValue> _companyPropertyMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                {"Id", new PropertyMappingValue(new List<string> {"Id"})},
                {"CompanyId", new PropertyMappingValue(new List<string> {"CompanyId"})},
                {"Name", new PropertyMappingValue(new List<string> {"FirstName", "LastName"})},
                {"EmployeeNo", new PropertyMappingValue(new List<string> {"EmployeeNo"})},
                {"GenderDisplay", new PropertyMappingValue(new List<string> {"Gender"})},
                {"Age", new PropertyMappingValue(new List<string> {"DateOfBirth"}, true)}
            };

        public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>()
        {

        }
        {

        }
    }
}
