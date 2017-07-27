using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DotNet.Async.ConcurrencyCookBook.Async
{
	class ReturningCompletedTasks
	{
		/// <summary>
		/// You need to implement a synchronous method with an asynchronous signature.This
		/// situation can arise if you are inheriting from an asynchronous interface or base class
		/// but wish to implement it synchronously.This technique is particularly useful when unit
		/// testing asynchronous code, when you need a simple stub or mock for an asynchronous
		/// interface. 	
		/// If you are implementing an asynchronous interface with synchronous code, avoid any
		/// form of blocking. It is not natural for an asynchronous method to block and then return
		/// a completed task.
		/// </summary>
		interface IMyAsyncInterface
		{
			Task<int> GetValueAsync();
		}
		class MySynchronousImplementation : IMyAsyncInterface
		{
			public Task<int> GetValueAsync()
			{
				return Task.FromResult(13);
			}
		}

		internal static void Run()
		{
			var f = new Form() { Width = 600, Height = 400 };
			var b = new Button() { Text = "Run", Dock = DockStyle.Fill, Font = new Font("Consolas", 18) };

			MySynchronousImplementation mySynchronousImplementation = new MySynchronousImplementation();

			f.Controls.Add(b);

			b.Click += async delegate
			{
				var tasks = new List<Task>();

				for (int i = 0; i < 100; i++)// hit all the request init in parallel on one thread.
				{
					tasks.Add(mySynchronousImplementation.GetValueAsync()); 
				}

				await Task.WhenAll(tasks);
				Console.WriteLine("Fetch all the Results");
			};

			f.ShowDialog();
		}
	}
}
