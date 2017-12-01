using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using System.Text;


namespace BittrexRxSharp.Helpers
{
    public static class Utilities
    {
        //http://www.c-sharpcorner.com/UploadFile/07c1e7/getting-property-values-of-an-object-dynamically-in-C-Sharp/
        public static string GenerateQueryString(dynamic obj)
        {
            if (obj == null)
                return "";
            var result = new List<string>();

            Type objectType = obj.GetType();
            PropertyInfo[] objectProperties = objectType.GetProperties();

            foreach (PropertyInfo prop in objectProperties)
            {
                var propValue = obj.GetType().GetProperty(prop.Name).GetValue(obj, null).ToString();
                result.Add(prop.Name + "=" + propValue);
            }

            //HttpUtility.UrlEncode()
            return ("?" + (string.Join("&", result)));
        }

        public static string GenerateQueryString(IDictionary<string, object> obj)
        {
            if (obj == null)
                return "";
            var result = new List<string>();
            foreach (var property in obj)
            {
                result.Add(property.Key + "=" + property.Value.ToString());
            }
            //HttpUtility.UrlEncode()
            return ("?" + (string.Join("&", result)));
        }

        //https://stackoverflow.com/questions/24480685/merging-anonymous-objects-into-a-single-anonymous-object
        public static IDictionary<string, object> MergeObjects(params object[] objects)
        {
            dynamic expandObj = new ExpandoObject();

            var merged = expandObj as IDictionary<string, object>;

            foreach (object obj in objects)
                if (obj != null)
                    foreach (PropertyInfo property in obj.GetType().GetProperties())
                        merged[property.Name] = property.GetValue(obj);

            return merged;
        }

    }
}
