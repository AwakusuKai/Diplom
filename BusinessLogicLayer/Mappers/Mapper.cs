using BusinessLogicLayer.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Mappers
{
    public static class Mapper
    {
        public static I Convert<T, I>(T obj) where I : new()
        {
            I result = new I();
            foreach (PropertyInfo property in typeof(I).GetProperties())
            {
                PropertyInfo propertyInfo = typeof(T).GetProperty(property.Name);
                if (property.CanWrite && !IsNavigationProperty(property))
                {
                    property.SetValue(result, System.Convert.ChangeType(propertyInfo.GetValue(obj), property.PropertyType), null);
                    continue;
                }
            }
            return result;
        }

        public static IEnumerable<I> ConvertEnumerable<T, I>(IEnumerable<T> objList) where I : new()
        {
            List<I> result = new List<I>();
            foreach (T obj in objList)
            {
                I item = new I();
                foreach (PropertyInfo property in typeof(I).GetProperties())
                {
                    PropertyInfo propertyInfo = typeof(T).GetProperty(property.Name);
                    if (property.CanWrite && !IsNavigationProperty(property))
                    {

                        property.SetValue(item, System.Convert.ChangeType(propertyInfo.GetValue(obj), property.PropertyType), null);

                    }
                }
                result.Add(item);
            }

            return result;
        }

        private static bool IsNavigationProperty(PropertyInfo property)
        {
            foreach (Attribute atr in property.GetCustomAttributes())
            {
                if (atr is NavigationPropertyAttribute)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
