using System.Net;


namespace DeskFlow.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {

        // RequestDelegate é pra chamar o próximo middleware na pipeline
        private readonly RequestDelegate _next;
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // Este método é executado automaticamente em cada requisição que entra na API
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                await TratarExcecaoAsync(context, ex);
            }
        }

        private static Task TratarExcecaoAsync(HttpContext context, Exception exception)
        {
            // Status 500
            var statusCode = HttpStatusCode.InternalServerError;
            var mensagem = "Ocorreu um erro interno inesperado no servidor.";

            if (exception is KeyNotFoundException)
            {
                statusCode = HttpStatusCode.NotFound;
                mensagem = exception.Message;
            }
            else if (exception is ArgumentException)
            {
                statusCode = HttpStatusCode.BadRequest;
                mensagem = exception.Message;
            }
            else if (exception is InvalidOperationException)
            {
                statusCode = HttpStatusCode.Conflict;
                mensagem = exception.Message;
            }

            // Retorna a resposta de erro em JSON
            context.Response.StatusCode = (int)statusCode;

            var respostaErro = new
            {
                status = context.Response.StatusCode,
                mensagem
            };

            return context.Response.WriteAsJsonAsync(respostaErro);
        }
    }
}