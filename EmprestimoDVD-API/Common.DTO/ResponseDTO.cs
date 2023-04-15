using System.Diagnostics;

namespace Common.DTO
{
    public class ResponseDTO<T>
    {
        public int Code { get; set; } = 200;
        public string Message { get; set; } = "Operação concluída!";
        public DateTime Date { get; set; } = DateTime.Now;
        public T? Object { get; set; }
        public ErrorDTO? Error { get; set; }
        public Stopwatch Elapsed { get; set; } = Stopwatch.StartNew();
        public void SetError(Exception ex)
        {
            Code = 500;
            Message = "Ocorreu um erro!";
            Error = new ErrorDTO { Message = ex.Message, InnerMessage = ex.InnerException?.Message, StackTrace = ex.StackTrace };
        }
        public void SetNotImplemented(string? cod = null)
        {
            Code = 500;
            Message = $"Método não implementado! {cod}";
        }
    }

    public class ErrorDTO
    {
        public string? Message { get; set; }
        public string? InnerMessage { get; set; }
        public string? StackTrace { get; set; }
    }
}
