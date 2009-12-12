using System;
using System.IO;
using System.Runtime.Serialization;

namespace Alsing.Serialization.Xml
{
    internal class XmlFormatter : IFormatter
    {
        #region IFormatter Members

        public SerializationBinder Binder
        {
            get { throw new Exception("The method or operation is not implemented."); }
            set { throw new Exception("The method or operation is not implemented."); }
        }

        public StreamingContext Context
        {
            get { throw new Exception("The method or operation is not implemented."); }
            set { throw new Exception("The method or operation is not implemented."); }
        }

        public object Deserialize(Stream serializationStream)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Serialize(Stream output, object graph)
        {
            var context = new SerializerEngine();
            context.Serialize(output, graph);
        }

        public ISurrogateSelector SurrogateSelector
        {
            get { throw new Exception("The method or operation is not implemented."); }
            set { throw new Exception("The method or operation is not implemented."); }
        }

        #endregion
    }
}