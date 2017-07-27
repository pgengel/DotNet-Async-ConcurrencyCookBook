using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Async.ConcurrencyCookBook.Async
{
	class WaitingForAnyTaskToComplete
	{
		/// <summary>
		/// This method takes a sequence of tasks and returns a
		/// task that completes when any of the tasks complete. The result of the returned task is
		/// the task that completed.
		/// 
		/// The task returned by Task.WhenAny never completes in a faulted or canceled state. It
		///	always results in the first Task to complete; if that task completed with an exception,
		///	then the exception is not propogated to the task returned by Task.WhenAny. For this
		///	reason, you should usually await the task after it has completed.
		///	When the first task completes, consider whether to cancel the remaining tasks. If the
		///	other tasks are not canceled but are also never awaited, then they are abandoned. Abandoned
		///	tasks will run to completion, and their results will be ignored. Any exceptions
		///	from those abandoned tasks will also be ignored.
		///	It is possible to use Task.WhenAny to implement timeouts (e.g., using Task.Delay as one
		///	of the tasks), but it’s not recommended. It’s more natural to express timeouts with cancellation,
		///	and cancellation has the added benefit that it can actually cancel the operation(
		///	s) if they time out.
		///	Another antipattern for Task.WhenAny is handling tasks as they complete. At first it
		///	seems like a reasonable approach to keep a list of tasks and remove each task from the
		///	list as it completes. The problem with this approach is that it executes in O(N^2) time,
		/// </summary>
		/// <param name="urlA"></param>
		/// <param name="urlB"></param>
		/// <returns></returns>
		// Returns the length of data at the first URL to respond.
		private static async Task<int> FirstRespondingUrlAsync(string urlA, string urlB)
		{
			var httpClient = new HttpClient();
			// Start both downloads concurrently.
			Task<byte[]> downloadTaskA = httpClient.GetByteArrayAsync(urlA);
			Task<byte[]> downloadTaskB = httpClient.GetByteArrayAsync(urlB);
			// Wait for either of the tasks to complete.
			Task<byte[]> completedTask =
				await Task.WhenAny(downloadTaskA, downloadTaskB);
			// Return the length of the data retrieved from that URL.
			byte[] data = await completedTask;
			return data.Length;
		}
	}
}
