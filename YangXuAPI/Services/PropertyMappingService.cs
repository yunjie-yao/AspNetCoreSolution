using System;
using System.Collections.Generic;
using System.Linq;
using YangXuAPI.Entities;
using YangXuAPI.Models;

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
                {"CompanyName", new PropertyMappingValue(new List<string> {"Name"})},
                {"Introduction", new PropertyMappingValue(new List<string> {"Introduction"})},
                {"Country", new PropertyMappingValue(new List<string> {"Country"})},
                {"Industry", new PropertyMappingValue(new List<string> {"Industry"})},
                {"Product", new PropertyMappingValue(new List<string> {"Product"})}
            };

        private IList<IPropertyMapping> _propertyMappings=new List<IPropertyMapping>();

        public PropertyMappingService()
        {
            _propertyMappings.Add(new PropertyMapping<EmployeeDto, Employee>(_employeePropertyMapping));
            _propertyMappings.Add(new PropertyMapping<CompanyDto, Company>(_companyPropertyMapping));
        }

        public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>()
        {
            var matchingMapping = 
                _propertyMappings.OfType<PropertyMapping<TSource, TDestination>>();
            var propertyMappings = matchingMapping.ToList();
            if (propertyMappings.Count()==1)
            {
                return propertyMappings.First().MappingDictionary;
            }

            throw new Exception($"无法找到唯一的映射关系，{typeof(TSource)},{typeof(TDestination)}");
        }

        public bool ValidMappingExistFor<TSource, TDestination>(string fields)
        {
            var propertyMapping = GetPropertyMapping<TSource, TDestination>();
            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }

            var fieldsAfterSpilt = fields.Split(",");
            foreach (var field in fieldsAfterSpilt)
            {
                var trimmedField = field.Trim();
                var indexOfFirstSpace = trimmedField.IndexOf(" ");
                var propertyName = indexOfFirstSpace == -1
                    ? trimmedField
                    : trimmedField.Remove(indexOfFirstSpace);
                if (!propertyMapping.ContainsKey(propertyName))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
