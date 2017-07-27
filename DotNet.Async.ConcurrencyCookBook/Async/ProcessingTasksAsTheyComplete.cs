using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Async.ConcurrencyCookBook.Async
{
	class ProcessingTasksAsTheyComplete
	{
		/// <summary>
		/// You have a collection of tasks to await, and you want to do some processing on each
		/// task after it completes. However, you want to do the processing for each one as soon as
		/// it completes, not waiting for any of the other tasks.
		/// </summary>
		/// <param name="val"></param>
		/// <returns></returns>
		static async Task<int> DelayAndReturnAsync(int val)
		{
			await Task.Delay(TimeSpan.FromSeconds(val));
			return val;
		}
		// This method now prints "1", "2", and "3".
		static async Task ProcessTasksAsync()
		{
			// Create a sequence of tasks.
			Task<int> taskA = DelayAndReturnAsync(2);
			Task<int> taskB = DelayAndReturnAsync(3);
			Task<int> taskC = DelayAndReturnAsync(1);
			var tasks = new[] { taskA, taskB, taskC };
			var processingTasks = tasks.Select(async t =>
			{
				var result = await t;
				Trace.WriteLine(result);
			}).ToArray();
			// Await all processing to complete
			await Task.WhenAll(processingTasks);// This method now prints "1", "2", and "3", and not 2,3,1.
		}
	}
}
