using System.Runtime.Serialization;

namespace PruebaTecnicaPT.Models.Common
{
    public class ResponseGeneric
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public ResponseGeneric() { }

        public ResponseGeneric(bool error, string message)
        {
            Error = error;
            Message = message;
        }


    }
    public class ResponseGeneric<T> : ResponseGeneric
    {
        public T Result { get; set; }

        public ResponseGeneric() { }

        public ResponseGeneric(T result)
        {
            Error = false;
            Result = result;
        }

        public ResponseGeneric(bool error, string message) : base(error, message) { }
    }
}
