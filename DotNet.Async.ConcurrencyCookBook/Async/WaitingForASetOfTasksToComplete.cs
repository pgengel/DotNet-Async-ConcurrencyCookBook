using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Async.ConcurrencyCookBook.Async
{
	class WaitingForASetOfTasksToComplete
	{
		/// <summary>
		/// You have several tasks and need to wait for them all to complete.
		/// If any of the tasks throws an exception, then Task.WhenAll will fault its returned task
		/// with that exception. If multiple tasks throw an exception, then all of those exceptions
		/// are placed on the Task returned by Task.WhenAll. However, when that task is awaited,
		/// only one of them will be thrown.
		/// </summary>
		/// <param name="urls"></param>
		/// <returns></returns>
		static async Task<string> DownloadAllAsync(IEnumerable<string> urls)
		{
			var httpClient = new HttpClient();
			// Define what we're going to do for each URL.
			var downloads = urls.Select(url => httpClient.GetStringAsync(url));
			// Note that no tasks have actually started yet
			// because the sequence is not evaluated.
			// Start all URLs downloading simultaneously.
			Task<string>[] downloadTasks = downloads.ToArray();
			// Now the tasks have all started.
			// Asynchronously wait for all downloads to complete.
			string[] htmlPages = await Task.WhenAll(downloadTasks);
			return string.Concat(htmlPages);
		}

		static async Task ThrowNotImplementedExceptionAsync()
		{
			throw new NotImplementedException();
		}
		static async Task ThrowInvalidOperationExceptionAsync()
		{
			throw new InvalidOperationException();
		}
		static async Task ObserveOneExceptionAsync()
		{
			var task1 = ThrowNotImplementedExceptionAsync();
			var task2 = ThrowInvalidOperationExceptionAsync();
			try
			{
				await Task.WhenAll(task1, task2);
			}
			catch (Exception ex)
			{
				// "ex" is either NotImplementedException or InvalidOperationException.
			}
		}

		static async Task ObserveAllExceptionsAsync()
		{
			var task1 = ThrowNotImplementedExceptionAsync();
			var task2 = ThrowInvalidOperationExceptionAsync();
			Task allTasks = Task.WhenAll(task1, task2);
			try
			{
				await allTasks;
			}
			catch
			{
				AggregateException allExceptions = allTasks.Exception;
					//...
			}
		}
	}
}
