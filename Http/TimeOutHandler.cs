using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HaFT.Utilities.Http
{
	public class TimeOutHandler : DelegatingHandler
	{
		private readonly TimeSpan _timeOut;

		public TimeOutHandler(TimeSpan timeOut) => _timeOut = timeOut;

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage req, CancellationToken ct)
		{
			using (var ctTimeOut = CancellationTokenSource.CreateLinkedTokenSource(ct))
			{
				ctTimeOut.CancelAfter(_timeOut);

				try
				{
					return await base.SendAsync(req, ctTimeOut.Token);
				}
				catch (OperationCanceledException) when (!ct.IsCancellationRequested)
				{
					throw new TimeoutException();
				}
			}
		}
	}
}
