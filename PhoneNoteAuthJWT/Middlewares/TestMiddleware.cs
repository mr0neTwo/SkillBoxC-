namespace PhoneNoteAuthJWT.Middlewares;

public class TestMiddleware
{
	private readonly RequestDelegate _next;

	public TestMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		Console.WriteLine("Incoming Request Headers:");
		
		foreach (var header in context.Request.Headers)
		{
			Console.WriteLine($"{header.Key}: {header.Value}");
		}

		await _next(context);
	}
}
