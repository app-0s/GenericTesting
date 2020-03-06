using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GenericsTesting.Structures;

namespace GenericsTesting
{
    class Program
    {
        static void Main(string[] args)
        {

            // Find a way to use generics to populate a Dictionary's kvp's value entry

            SimModelADictionaryCall();

        }
        // Note: We're pretending the list and array list of the Sim methods are JSON arrays holding the Keys(fields) and corresponding values
        private static void SimModelADictionaryCall()
        {
            //PopulateDictionaryClass<Models.ModelA> modelADictionaryClass = new PopulateDictionaryClass<Models.ModelA>();

            // Simulate field array
            List<string> modelAKeyList = new List<string>
            {
                "Id",
                "TestDataInt",
                "TestDataStringA",
                "TestDataDateTime"
            };

            //Simlate value array
            ArrayList modelAValueList = new ArrayList()
            {
                1,
                15,
                "ModelAString",
                DateTime.Now
            };

            // Create PopulateDictionaryClass instance
            //PopulateDictionaryClass<Models.ModelA> pdc = new PopulateDictionaryClass<Models.ModelA>();

            // PopulateDictionary Call
            //Dictionary<string, ValueTypeStruct> modelADictionary = modelADictionaryClass.PopulateDictionary()
            Dictionary<string, ValueTypeStruct> modelADictionary = PopulateDictionary(typeof(Models.ModelA), modelAKeyList, modelAValueList);

            // Method for populating the
            SimModelAPopulation(modelADictionary);
        }

        private static void SimModelAPopulation(Dictionary<string, ValueTypeStruct> maDict)
        {
            var modelA = new Models.ModelA();

            // Cycle through eache kvp in dictionary
            foreach(KeyValuePair<string, ValueTypeStruct> kvp in maDict)
            {
               // var value = null;
                // Convert the value within the ValueTypeStruct to its proper type
                if(kvp.Value.strValueType == "System.Double")
                {
                    modelA.GetType().GetProperty(kvp.Key).SetValue(modelA, Double.Parse(kvp.Value.strValue));
                } else if (kvp.Value.strValueType == "System.Int32")
                {
                    modelA.GetType().GetProperty(kvp.Key).SetValue(modelA, Int32.Parse(kvp.Value.strValue));
                } else if (kvp.Value.strValueType == "System.String")
                {
                    modelA.GetType().GetProperty(kvp.Key).SetValue(modelA, kvp.Value.strValue);
                } // Finish conversion table for datetime, timespan

               
            }
        }

        private void SimModelBDictionaryCall()
        {
            // Simulate field array
            List<string> modelBKeyList = new List<string>
            {
                "Id",
                "TestDataStringB1",
                "TestDataStringB2",
                "TestDataDouble",
                "TestTimeSpan",
                "TestDataDateTime"
            };

            //Simlate value array
            ArrayList modelAValueList = new ArrayList()
            {
                1,
                "ModelBString",
                "Loris Epsilom X Etc.",
                15.234,
                DateTime.Now.TimeOfDay,
                DateTime.Now
            };


            // PopulateDictionary Call
        }

        public static Dictionary<string, ValueTypeStruct> PopulateDictionary(Type dataObjectType, List<string> keyList, ArrayList valueList)
        {
            Dictionary<string, ValueTypeStruct> dictionary = new Dictionary<string, ValueTypeStruct>();

            // Get properties of dataObject
            List<PropertyInfo> propList = dataObjectType.GetProperties().ToList(); ;

            // Cycle through each property of propList
            for (int i = 0; i < keyList.Count; i++)
            {
                PropertyInfo prop = propList.FirstOrDefault(p => p.Name.ToLower() == keyList[i].ToLower());

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

            return dictionary;
        }

    }
}
