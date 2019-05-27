using System.Collections;
using System.IO;
using System.Reflection;
using System.Text;

namespace Eimt.Application.Services.Impl
{
    public class ExelService : IDocumentService
    {
        public byte[] ToDocument(IList objs)
        {
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            SetHeader(sw, objs[0]);
            foreach (var obj in objs)
            {
                var props = obj.GetType().GetProperties();
                WriteProps(sw, obj, props);
            }
            return Encoding.ASCII.GetBytes(sw.ToString());
        }
        private void NestedList(StringWriter sw, IList objs)
        {
            bool first = true;
            foreach (var obj in objs)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    sw.Write(" - ");

                }
                sw.Write(obj.ToString());
            }
            sw.Write(", ");
        }
        private void WriteProps(StringWriter sw, object obj, PropertyInfo[] propertyInfos)
        {
            foreach (var prop in propertyInfos)
            {
                var val = obj.GetType().GetProperty(prop.Name).GetValue(obj, null);
                if (val is IList lst)
                {
                    NestedList(sw, lst);
                }
                else
                {
                    sw.Write(val.ToString());
                    sw.Write(", ");
                }
            }
            sw.WriteLine();
        }
        private void SetHeader(StringWriter sw, object obj)
        {
            var props = obj.GetType().GetProperties();
            foreach (var prop in props)
            {
                sw.Write(prop.Name);
                sw.Write(", ");
            }
            sw.WriteLine();
        }
    }
}
