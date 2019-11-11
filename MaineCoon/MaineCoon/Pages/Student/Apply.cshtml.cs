using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MaineCoon.Pages.Student
{
    public class ApplyModel : PageModel {
        private readonly MaineCoon.Data.MaineCoonContext _context;
        public ApplyModel(MaineCoon.Data.MaineCoonContext context) {
            _context = context;
        }
        public JToken parameters { get; set; }
        public IActionResult OnGet(int id)
        {
            if (id <= 0) {
                return BadRequest();
            }
            var program = _context.UniversityPrograms.Find(id);
            if (program == null) {
                return BadRequest();
            }
            var programJson = JsonConvert.DeserializeObject<JObject>(program.ProgramJson);
            parameters = programJson["parameter"];

            return Page();
        }
    }
}