using MISA.WEB02.Core.Entities;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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
                
                dynamic entity = new ExpandoObject();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var propName = ToPascalCase(reader.GetName(i));
                    var propValue = reader.GetValue(i);
                    if (typeof(T).IsPrimitive || typeof(T).Name == "String")
                    {
                        entity = propValue;
                        continue;
                    }
                    else if(propValue!=null) ((IDictionary<string, object>)entity).Add(propName, propValue);
                }

                var entityData = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(entity));
                listItem.Add(entityData);
                
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
        public static string ToPascalCase(string str)
        {
            string result = Regex.Replace(str, "_[a-z]", delegate (Match m) {
                return m.ToString().TrimStart('_').ToUpper();
            });

            result = char.ToUpper(result[0]) + result.Substring(1);

            return result;
        }
    }
}
