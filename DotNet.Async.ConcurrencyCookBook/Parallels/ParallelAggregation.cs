using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Async.ConcurrencyCookBook.Parallels
{
	class ParallelAggregation
	{
		static int ParallelSum(IEnumerable<int> values)
		{
			return values.AsParallel().Sum();
		}
	}
}
