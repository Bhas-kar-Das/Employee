using Employee.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Employee.Services
{
	public class TimeEntryService
	{
		private readonly HttpClient _httpClient;
		private readonly string _apiUrl = "https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code=vO17RnE8vuzXzPJo5eaLLjXjmRW07law99QTD90zat9FfOQJKKUcgQ==";

		public TimeEntryService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<List<EmployeeTime>> GetEmployeeTimesAsync()
		{
			var response = await _httpClient.GetStringAsync(_apiUrl);
			var timeEntries = JsonConvert.DeserializeObject<List<TimeEntry>>(response);

			var employeeTimes = new List<EmployeeTime>();
			foreach (var group in timeEntries.GroupBy(te => te.EmployeeName))
			{
				var totalHours = group.Sum(te => (te.EndTimeUtc - te.StarTimeUtc).TotalHours);
				employeeTimes.Add(new EmployeeTime
				{
					Name = group.Key,
					TotalHours = totalHours
				});
			}

			return employeeTimes.OrderByDescending(e => e.TotalHours).ToList();
		}
	}
}
