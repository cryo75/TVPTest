using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WindowsFormsApp2
{
    static class Extensions
    {
        private static bool IsNullableEnum(this Type t)
        {
            Type u = Nullable.GetUnderlyingType(t);
            return (u != null) && u.IsEnum;
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> items)
        {
            var dataTable = new DataTable(typeof(T).Name);

            //Get all the properties not marked with Ignore attribute
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                      .Where(x => x.GetCustomAttributes(typeof(XmlIgnoreAttribute), false).Length == 0).ToList();

            //Set column names as property names
            foreach (var property in properties)
            {
                if (!property.PropertyType.IsEnum && !property.PropertyType.IsNullableEnum())
                {
                    var type = property.PropertyType;

                    //Check if type is Nullable like int?
                    if (Nullable.GetUnderlyingType(type) != null)
                        type = Nullable.GetUnderlyingType(type);

                    dataTable.Columns.Add(property.Name, type);
                }
                else dataTable.Columns.Add(property.Name, typeof(int));
            }

            //Insert property values to datatable rows
            foreach (T item in items)
            {
                var values = new object[properties.Count];
                for (int i = 0; i < properties.Count; i++)
                {
                    values[i] = properties[i].GetValue(item, null);
                }

                dataTable.Rows.Add(values);
            }

            //Return filled datatable
            return dataTable;
        }
    }
}
