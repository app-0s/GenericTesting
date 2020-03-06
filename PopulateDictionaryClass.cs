using GenericsTesting.Structures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GenericsTesting
{
    // Generics may not be the route to take with this, as we want the actual value to differ, and not be the object type declared
    // during instantiation.
    public class PopulateDictionaryClass<T>
    {
        public Dictionary<string, ValueTypeStruct> PopulateDictionary(T dataObject, List<string> keyList, ArrayList valueList)
        {
            Dictionary<string, ValueTypeStruct> dictionary = new Dictionary<string, ValueTypeStruct>();

            // Get properties of dataObject
            List<PropertyInfo> propList = typeof(T).GetProperties().ToList();

            // Cycle through each property of propList
            for (int i = 0; i < keyList.Count; i++){
                PropertyInfo prop = propList.FirstOrDefault(p => p.Name == keyList[i]);

                if (prop != null)
                {
                    Type propType = prop.PropertyType;
                    string keyName = prop.Name;

                    ValueTypeStruct vts = new ValueTypeStruct();
                    vts.strValueType = prop.PropertyType.FullName;
                    vts.strValue = valueList[i].ToString();

                    dictionary.Add(keyName, vts);
                }

                
            }

            foreach(PropertyInfo prop in propList)
            {
                Type propType = prop.PropertyType;
                string propName = prop.Name;

                ValueTypeStruct vts = new ValueTypeStruct();
                //vts.strValue = 
            }

            return dictionary;
        }
    }
}
// Another way to handle this: Store value as a struct containing a value property, and a data type property. This way, you can convert the value
// when it is before it is inserted into the model from the dictionary