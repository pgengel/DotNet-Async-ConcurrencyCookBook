using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Threading.Tasks;


namespace DotNet.Async.ConcurrencyCookBook.Parallels
{
	class ParallelProcessingOfData
	{
		void RotateMatrices(IEnumerable<Matrix> matrices, float degrees)
		{
			Parallel.ForEach(matrices, matrix => matrix.Rotate(degrees));
		}

		/// <summary>
		/// There are some situations where you’ll want to stop the loop early, such as if you encounter
		/// an invalid value.
		/// </summary>
		/// <param name="matrices"></param>
		void InvertMatrices(IEnumerable<Matrix> matrices)
		{
			Parallel.ForEach(matrices, (matrix, state) =>
			{
				if (!matrix.IsInvertible)
					state.Stop();
				else
					matrix.Invert();
			});
		}

		/// <summary>
		/// A more common situation is when you want the ability to cancel a parallel loop. This
		/// is different than stopping the loop; a loop is stopped from inside the loop, and it is
		/// canceled from outside the loop. For example, a cancel button may cancel a Cancella
		/// tionTokenSource, canceling a parallel loop like this one:
		/// </summary>
		/// <param name="matrices"></param>
		/// <param name="degrees"></param>
		/// <param name="token"></param>
		void RotateMatrices(IEnumerable<Matrix> matrices, float degrees, CancellationToken token)
		{
			Parallel.ForEach(matrices,
				new ParallelOptions { CancellationToken = token },
				matrix => matrix.Rotate(degrees));
		}
	}
}
