using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DotNet.Async.ConcurrencyCookBook.Async
{
	/// <summary>
	/// You need to (asynchronously) wait for a period of time. This can be useful when unit
	///	testing or implementing retry delays. This solution can also be useful for simple timeouts.
	/// However, if you need to implement a timeout, a CancellationToken is usually
	/// a better choice.
	/// </summary>

	class PausingForAPeriodOfTime
	{
		static async Task<T> DelayResult<T>(T result, TimeSpan delay)
		{
			await Task.Delay(delay);
			return result;
		}

		static async Task<string> DownloadStringWithRetries(string uri)
		{
			using (var client = new HttpClient())
			{
				// Retry after 1 second, then after 2 seconds, then 4.
				var nextDelay = TimeSpan.FromSeconds(1);
				for (int i = 0; i != 3; ++i)
				{
					try
					{
						return await client.GetStringAsync(uri);
					}
					catch
					{
					}
					await Task.Delay(nextDelay);
					nextDelay = nextDelay + nextDelay;
				}
				// Try one last time, allowing the error to propogate.
				return await client.GetStringAsync(uri);
			}		
		}

		static async Task<string> DownloadStringWithTimeout(string uri)
		{
			using (var client = new HttpClient())
			{
				var downloadTask = client.GetStringAsync(uri);
				var timeoutTask = Task.Delay(3000);
				var completedTask = await Task.WhenAny(downloadTask, timeoutTask);
				if (completedTask == timeoutTask)
					return null;
				return await downloadTask;
			}
		}

		internal static void Run()
		{
			var f = new Form() { Width = 600, Height = 400 };
			var b = new Button() { Text = "Run", Dock = DockStyle.Fill, Font = new Font("Consolas", 18) };
			f.Controls.Add(b);

			b.Click += async delegate
			{
				var tasks = new List<Task>();

				// DelayResult
				for (int i = 0; i < 10; i++)// hit all the request init in parallel on one thread.
				{

					int fileNum = i;
					tasks.Add(DelayResult(fileNum, TimeSpan.FromSeconds(1)));
				}

				await Task.WhenAll(tasks);
				Console.WriteLine("Done: Fetch all the files with DelayResult");

				// DownloadStringWithRetries
				for (int i = 0; i < 10; i++)// hit all the request init in parallel on one thread.
				{
					tasks.Add(DownloadStringWithRetries("UrlDownloadStringWithRetries"));
				}

				await Task.WhenAll(tasks);
				Console.WriteLine("Done: Fetch all the files with DownloadStringWithRetrie");

				// DownloadStringWithTimeout
				for (int i = 0; i < 10; i++)// hit all the request init in parallel on one thread.
				{
					tasks.Add(DownloadStringWithTimeout("URL"));
				}

				await Task.WhenAll(tasks);
				Console.WriteLine("Done: Fetch all the files with DownloadStringWithRetrie");
			};

			f.ShowDialog();
		}
	}
}
