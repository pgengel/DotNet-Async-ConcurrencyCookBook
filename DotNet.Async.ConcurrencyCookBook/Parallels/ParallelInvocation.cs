using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DotNet.Async.ConcurrencyCookBook.Parallels
{
	class ParallelInvocation
	{
		/// <summary>
		/// You have a number of methods to call in parallel, and these methods are (mostly) independent
		/// of each other.
		/// </summary>
		/// <param name="array"></param>
		/// <param name="begin"></param>
		/// <param name="end"></param>
		static void ProcessPartialArray(double[] array, int begin, int end)
		{
			// CPU-intensive processing...
		}
		static void ProcessArray(double[] array)
		{
			Parallel.Invoke(
				() => ProcessPartialArray(array, 0, array.Length / 2),
				() => ProcessPartialArray(array, array.Length / 2, array.Length)
			);
		}

		static void DoAction20Times(Action action)
		{
			Action[] actions = Enumerable.Repeat(action, 20).ToArray();
			Parallel.Invoke(actions);
		}

		static void DoAction20Times(Action action, CancellationToken token)
		{
			Action[] actions = Enumerable.Repeat(action, 20).ToArray();
			Parallel.Invoke(new ParallelOptions { CancellationToken = token }, actions);
		}
	}
}
