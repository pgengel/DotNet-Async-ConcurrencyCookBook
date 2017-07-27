using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Async.ConcurrencyCookBook.Async
{
	class AvoidingContextForContinuations
	{
		/// <summary>
		/// When an async method resumes after an await, by default it will resume executing
		/// within the same context. This can cause performance problems if that context was a UI
		/// context and a large number of async methods are resuming on the UI context.
		/// </summary>
		/// <returns></returns>
		async Task ResumeOnContextAsync()
		{
			await Task.Delay(TimeSpan.FromSeconds(1));
			// This method resumes within the same context.
		}
		async Task ResumeWithoutContextAsync()
		{
			await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
			// This method discards its context when it resumes.
		}
	}
}
