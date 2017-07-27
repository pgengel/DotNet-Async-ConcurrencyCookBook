using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Async.ConcurrencyCookBook.Async
{

	class HandlingExceptionsFromAsyncTaskMethods
	{
		/// <summary>
		/// Exceptions can be caught by a simple try/catch, just like you would for synchronous
		/// </summary>
		/// <returns></returns>
		static async Task ThrowExceptionAsync()
		{
			await Task.Delay(TimeSpan.FromSeconds(1));
			throw new InvalidOperationException("Test");
		}

		static async Task TestAsync()
		{
			try
			{
				await ThrowExceptionAsync();
			}
			catch (InvalidOperationException)
			{
			}
		}

		/// <summary>
		/// Exceptions raised from async Task methods are placed on the returned Task. They are
		/// only raised when the returned Task is awaited:
		/// </summary>
		/// <returns></returns>
		static async Task TestAsyncTask()
		{
			// The exception is thrown by the method and placed on the task.
			Task task = ThrowExceptionAsync();
			try
			{
				// The exception is reraised here, where the task is awaited.
				await task;
			}
			catch (InvalidOperationException)
			{
				// The exception is correctly caught here.
			}
		}
	}
}
