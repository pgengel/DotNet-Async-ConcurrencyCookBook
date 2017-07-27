using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Async.ConcurrencyCookBook.Parallels
{
	class ParallelLINQ
	{
		/// <summary>
		/// The Parallel class is good for many scenarios, but PLINQ code is simpler when doing
		/// aggregation or transforming one sequence to another. Bear in mind that the Paral
		/// lel class is more friendly to other processes on the system than PLINQ; this is especially
		/// a consideration if the parallel processing is done on a server machine.
		/// PLINQ provides parallel versions of a wide variety of operators, including filtering
		/// (Where), projection (Select), and a variety of aggregations, such as Sum, Average, and
		/// the more generic Aggregate. In general, anything you can do with regular LINQ you
		/// can do in parallel with PLINQ. This makes PLINQ a great choice if you have existing
		/// LINQ code that would benefit from running in parallel.
		/// </summary>
		/// <param name="values"></param>
		/// <returns></returns>
		static IEnumerable<int> AnyOrderMultiplyBy2(IEnumerable<int> values)
		{
			return values.AsParallel().Select(item => item * 2);
		}

		static IEnumerable<int> PreserveOrderMultiplyBy2(IEnumerable<int> values)
		{
			return values.AsParallel().AsOrdered().Select(item => item * 2);
		}

		static int ParallelSum(IEnumerable<int> values)
		{
			return values.AsParallel().Sum();
		}
	}
}
