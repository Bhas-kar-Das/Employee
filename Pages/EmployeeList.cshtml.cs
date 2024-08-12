using Employee.Model;
using Employee.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Employee.Pages
{
    public class EmployeeListModel : PageModel
    {
        private readonly TimeEntryService _timeEntryService;

        public EmployeeListModel(TimeEntryService timeEntryService)
        {
            _timeEntryService = timeEntryService;
        }

        public IList<EmployeeTime> EmployeeTimes { get; set; } = new List<EmployeeTime>();

        public async Task OnGetAsync()
        {
            EmployeeTimes = await _timeEntryService.GetEmployeeTimesAsync();
        }
    }
}
