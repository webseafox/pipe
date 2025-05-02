using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;

namespace MiraeDigital.BffMobile.Domain.SeedWork
{
    public static class ParseModel
    {
        /// <summary>
        /// Executa o mapeamento do Tipo Origem para o Tipo Destino.
        /// </summary>
        /// <typeparam name="TSource">Tipo de Origem.</typeparam>
        /// <typeparam name="TDestination">Tipo de Destino.</typeparam>
        /// <param name="origem">Tipo de Origem.</param>
        /// <returns></returns>
        public static TDestination Map<TDestination>(this object origem, bool usePropertyName = false)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();

            settings.PreserveReferencesHandling = PreserveReferencesHandling.All;

            if (usePropertyName)
            {
                settings.ContractResolver = new PropertyNameContractResolver();
            }

            var serialized = JsonConvert.SerializeObject(origem, Formatting.Indented, settings);
            return JsonConvert.DeserializeObject<TDestination>(serialized, settings);
        }
    }



    public class PropertyNameContractResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            // Let the base class create all the JsonProperties 
            // using the short names
            IList<JsonProperty> list = base.CreateProperties(type, memberSerialization);

            // Now inspect each property and replace the 
            // short name with the real property name
            foreach (JsonProperty prop in list)
            {
                prop.PropertyName = prop.UnderlyingName;
            }

            return list;
        }
    }
}
