using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Windows;
using Recetario.Services.Interfaces;

namespace Recetario.Services.LocalData
{
    public class LocalDataService : ILocalDataService
    {
        public IEnumerable<T> Load<T>(string file)
        {
            var sri = Application.GetResourceStream(new Uri(file, UriKind.Relative));
            var types = new List<Type> {typeof (T)};
            var deserializer = new DataContractJsonSerializer(typeof(IEnumerable<T>), types);

            return (IEnumerable<T>)deserializer.ReadObject(sri.Stream);
        }
    }
}
