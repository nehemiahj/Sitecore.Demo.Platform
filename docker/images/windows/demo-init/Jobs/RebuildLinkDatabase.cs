﻿using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sitecore.Demo.Init.Jobs
{
	using Microsoft.Extensions.Logging;

	class RebuildLinkDatabase : TaskBase
	{
		public static async Task Run()
		{
			await Start(typeof(RebuildLinkDatabase).Name);
			var hostCM = Environment.GetEnvironmentVariable("HOST_CM");
			Log.LogInformation($"RebuildLinkDatabase() started {hostCM}");

			using var client = new HttpClient { BaseAddress = new Uri(hostCM) };
			using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"/Utilities/RebuildLinkDatabase.aspx"))
			{
				using (var response = await client.SendAsync(request))
				{
					var contents = await response.Content.ReadAsStringAsync();
					Log.LogInformation($"{response.StatusCode} {contents}");
					Log.LogInformation("RebuildLinkDatabase() complete");
				}
			}
		}
	}
}
