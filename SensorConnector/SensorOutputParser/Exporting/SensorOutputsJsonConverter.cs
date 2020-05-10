using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace SensorOutputParser.Exporting
{
    /// <summary>
    /// Overrides default JsonConverter and creates custom Json from the sensor output.
    /// </summary>
    public class SensorOutputsJsonConverter : JsonConverter
    {
        private readonly Type[] _types;

        public SensorOutputsJsonConverter(params Type[] types)
        {
            _types = types;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JToken jsJToken = JToken.FromObject(value);

            if (jsJToken.Type != JTokenType.Object)
            {
                jsJToken.WriteTo(writer);
            }
            else
            {
                JObject composedJObject = (JObject)jsJToken;

                var parsedDataParent = composedJObject.Properties().Where(p => p.Name.Equals(nameof(SensorOutputForExport.ParsedData)))
                    .ToList();

                var parsedDataTokens = parsedDataParent.Children().Values();

                var newParsedDataJObject = new JObject();

                foreach (var parsedDataToken in parsedDataTokens)
                {
                    var fieldName = parsedDataToken.Value<string>(nameof(SensorFieldValue.FieldName));

                    var fieldType = parsedDataToken.Children().Values().Last().Type;

                    switch (fieldType)
                    {
                        case JTokenType.Integer:
                            {
                                var fieldValue = parsedDataToken.Value<int>(nameof(SensorFieldValue.FieldValue));
                                newParsedDataJObject.Add(fieldName, (int)fieldValue);

                                break;
                            }

                        case JTokenType.Float:
                            {
                                var fieldValue = parsedDataToken.Value<double>(nameof(SensorFieldValue.FieldValue));
                                newParsedDataJObject.Add(fieldName, (double)fieldValue);

                                break;
                            }

                        case JTokenType.Boolean:
                            {
                                var fieldValue = parsedDataToken.Value<bool>(nameof(SensorFieldValue.FieldValue));
                                newParsedDataJObject.Add(fieldName, (bool)fieldValue);

                                break;
                            }

                        default:
                            {
                                var fieldValue = parsedDataToken.Value<string>(nameof(SensorFieldValue.FieldValue));
                                newParsedDataJObject.Add(fieldName, (string)fieldValue);

                                break;
                            }
                    }
                }

                composedJObject.Remove(nameof(SensorOutputForExport.ParsedData));

                composedJObject.Add("ParsedData", newParsedDataJObject);

                composedJObject.WriteTo(writer);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary because CanRead is false. The type will skip the converter.");
        }

        public override bool CanRead => false;

        public override bool CanConvert(Type objectType)
        {
            return _types.Any(t => t == objectType);
        }
    }
}

