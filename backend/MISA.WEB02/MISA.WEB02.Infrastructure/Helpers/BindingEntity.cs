using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB02.Infrastructure.Helpers
{
    public class BindingEntity
    {
        public static IEnumerable<T> BindingData<T>(NpgsqlDataReader reader)
        {
            var listItem = new List<T>();
            while (reader.Read())
            {
                //Department department = new Department();
                //int i = 0;
                //dynamic obj = new ExpandoObject();
                var item = (T)Activator.CreateInstance(typeof(T));
                foreach (PropertyInfo prop in item.GetType().GetProperties())
                {


                    var type = prop.PropertyType;
                    var value = reader[ToSnakeCase(prop.Name)];
                    if (type.Name == "Guid")
                    {
                        value = new Guid(value.ToString());
                    }
                    var a = reader[ToSnakeCase(prop.Name)];
                    prop.GetType();
                    prop.SetValue(item, value, null);
                    //department.GetType().GetProperty(prop.Name) = reader[ToSnakeCase(prop.Name)];



                }
                listItem.Add(item);
                
            }
            return listItem;
        }
        public static string ToSnakeCase(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }
            if (text.Length < 2)
            {
                return text;
            }
            var sb = new StringBuilder();
            sb.Append(char.ToLowerInvariant(text[0]));
            for (int i = 1; i < text.Length; ++i)
            {
                char c = text[i];
                if (char.IsUpper(c))
                {
                    sb.Append('_');
                    sb.Append(char.ToLowerInvariant(c));
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}
