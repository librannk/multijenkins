
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System.Collections.Generic;
using Template.API.AccessBlob;

namespace Template.API.Infrastructure.Filters
{
    /// <summary>
    /// ValidateJson is an internal class
    /// </summary>
    internal class ValidateJson
    {
        /// <summary>
        /// IsValid is a method to validate model with schema validation
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        public bool IsValid(object obj, out IList<ValidationError> errors)
        {
            //instance of validation error to capture schema failed validation
            errors = new List<ValidationError>();
            //fetching file name
            var fileName = obj.GetType().Name;

            //getting all blob files    
            Dictionary<string, string> files = AccessBlobFiles.dictBlobFiles;

            bool Isvalid = false;
            JSchema schema = new JSchema();

            //checking whether the filename exist in blob files and file is not empty
            if (files.ContainsKey(fileName) && !string.IsNullOrEmpty(files[fileName]))
            {
                //if exist then get content of that file
                schema = JSchema.Parse(files[fileName]);

                //serialization of class object
                string json = JsonConvert.SerializeObject(obj);

                //parsing of object into json
                JObject modelData = JObject.Parse(json);

                //validating with json schema and capturing errors in out parameter in case of errors
                Isvalid = modelData.IsValid(schema, out errors);
            }
            return Isvalid;
        }
    }
}
