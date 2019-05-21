using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Eimt.Application.Interfaces.Models.EmailModels
{
    public class Attacment
    {
        public string FileName { get; set; }
        private object content;
        public Attacment(string fileName, object content)
        {
            this.content = content;
            this.FileName = fileName;
        }
        public bool IsStreamable {
            get
            {
                var type = content.GetType();
                return type.IsSerializable || Attribute.IsDefined(type, typeof(SerializableAttribute));
            }
        }
        public byte[] Stream { get
            {
                using (var stream = new MemoryStream())
                {
                    var bf = new BinaryFormatter();
                    bf.Serialize(stream, content);
                    return stream.ToArray();
                }
            }
        }
   }
}
