using System.Runtime.Serialization;

namespace csharp_lista_indirizzi
{
    [Serializable]
    internal class ExceptionOfType : Exception
    {
        public ExceptionOfType()
        {
        }

        public ExceptionOfType(string? message) : base(message)
        {
        }

        public ExceptionOfType(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ExceptionOfType(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}