using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace API.Helpers
{
	public class JsonActionResult : ContentResult
	{
		public JsonActionResult(object response) 
			: this(JsonSerializeHelper.Serialize(response))
		{
		}

		public JsonActionResult(string response)
			: this()
		{
			Content = response;
		}

		protected JsonActionResult()
		{
			ContentType = "application/json";
		}

		public static ActionResult Ok(object content) => new JsonActionResult(content);
		public static ActionResult NotFound(object content) => HttpCodeResponse(content, HttpStatusCode.NotFound);
		public static ActionResult BadRequest(object content) => HttpCodeResponse(content, HttpStatusCode.BadRequest); 
		public static ActionResult Unauthorized(object content) => HttpCodeResponse(content, HttpStatusCode.Unauthorized);
		public static ActionResult Forbidden(object content) => HttpCodeResponse(content, HttpStatusCode.Forbidden);

		private static ActionResult HttpCodeResponse(object content, HttpStatusCode code) => new JsonActionResult(content) { StatusCode = (int) code };
	}
	
	public static class JsonSerializeHelper
	{
		private static readonly JsonSerializerOptions SerializerSettings = new JsonSerializerOptions
		{
			IgnoreNullValues = true,
			IncludeFields = true,
		};

		public static string Serialize(object o)
		{
			return JsonSerializer.Serialize(o, SerializerSettings);
		}
	}
}