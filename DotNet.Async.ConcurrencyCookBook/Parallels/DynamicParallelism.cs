using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DotNet.Async.ConcurrencyCookBook.Parallels
{
	class DynamicParallelism
	{

		//public void ProcessTree(Node root)
		//{
		//	var task = Task.Factory.StartNew(() => Traverse(root),
		//		CancellationToken.None,
		//		TaskCreationOptions.None,
		//		TaskScheduler.Default);
		//	task.Wait();
		//}
		//void Traverse(Node current)
		//{
		//	DoExpensiveActionOnNode(current);
		//	if (current.Left != null)
		//	{
		//		Task.Factory.StartNew(() => Traverse(current.Left),
		//			CancellationToken.None,
		//			TaskCreationOptions.AttachedToParent,
		//			TaskScheduler.Default);
		//	}
		//	if (current.Right != null)
		//	{
		//		Task.Factory.StartNew(() => Traverse(current.Right),
		//			CancellationToken.None,
		//			TaskCreationOptions.AttachedToParent,
		//			TaskScheduler.Default);
		//	}
		//}
	}
}
