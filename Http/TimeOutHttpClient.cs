using System;
using System.Net.Http;

namespace HaFT.Utilities.Http
{
	public class TimeOutHttpClient : HttpClient
	{
		public TimeOutHttpClient(TimeSpan timeOut)
			: base(GetHandler(timeOut))
		{
		}

		private static HttpMessageHandler GetHandler(TimeSpan timeOut)
		{
			var handler = new TimeOutHandler(timeOut)
			{
				InnerHandler = new HttpClientHandler()
			};

			return handler;
		}
	}
}
