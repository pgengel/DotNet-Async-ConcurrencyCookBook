using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;


namespace DotNet.Async.ConcurrencyCookBook.Async
{
	class ReportingProgress
	{
		/// <summary>
		/// You need to respond to progress while an asynchronous operation is executing.
		/// </summary>
		/// <param name="progress"></param>
		/// <returns></returns>
		static async Task MyMethodAsync(IProgress<double> progress = null)
		{
			double percentComplete = 0;
			bool done = false;
			while (!done)
			{
				/// ...
				if (progress != null)
					progress.Report(percentComplete);
			}
		}

		static async Task CallMyMethodAsync()
		{
			var progress = new Progress<double>();
			progress.ProgressChanged += (sender, args) =>
			{
				///...
			};
			await MyMethodAsync(progress);
		}

		internal static void Run()
		{
			var f = new Form() { Width = 600, Height = 400 };
			var b = new Button() { Text = "Run", Dock = DockStyle.Fill, Font = new Font("Consolas", 18) };
			f.Controls.Add(b);

			b.Click += async delegate
			{
				var tasks = new List<Task>();

				for (int i = 0; i < 100; i++)// hit all the request init in parallel on one thread.
				{

					int fileNum = i;

					//tasks.Add(Library.FetchFileAsyncBad(fileNum));
					//tasks.Add(Library.FetchFile(fileNum)); //deadlock created
				}

				await Task.WhenAll(tasks);
				Console.WriteLine("Fetch all the files");
			};

			f.ShowDialog();
		}
	}
}
