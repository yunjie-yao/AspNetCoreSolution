using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using YangXuAPI.Services;

namespace YangXuAPI.Helpers
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplySort<T>(
            this IQueryable<T> source, 
            string orderBy, 
            Dictionary<string, PropertyMappingValue> mappingDictionary)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
            {
                return source;
            }

            if (source==null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (mappingDictionary==null)
            {
                throw new ArgumentNullException(nameof(mappingDictionary));
            }

            var orderByAfterSplit = orderBy.Split(",");
            // 疑问：为什么要反转？
            foreach (var orderByClause in orderByAfterSplit.Reverse())
            {
                var trimmedOrderByClause = orderByClause.Trim();
                var orderDescending = trimmedOrderByClause.EndsWith(" desc");//判断是否按倒序

                var indexOfFirstSpace = trimmedOrderByClause.IndexOf(" ", StringComparison.Ordinal);//第一个空格出现的位置
                // -1,没找到空格，说明属性名就是trim之后的，否则就是去掉trim空格之后的
                // 例：-1,orderby=name 
                // 例：!=-1,orderby=name desc
                var propertyName = indexOfFirstSpace == -1
                    ? trimmedOrderByClause
                    : trimmedOrderByClause.Remove(indexOfFirstSpace);

                if (!mappingDictionary.ContainsKey(propertyName))
                {
                    throw new ArgumentException($"没有找到key为{propertyName}的映射");
                }

                var propertyMappingValue = mappingDictionary[propertyName];
                foreach (var destinationProperty in propertyMappingValue.DestinationProperties.Reverse())
                {
                    if (propertyMappingValue.Revert)
                    {
                        orderDescending = !orderDescending;
                    }

                    source = source.OrderBy(destinationProperty 
                                            + (orderDescending ? " descending" : " ascending"));
                }
            }

            return source;
        }
    }
}