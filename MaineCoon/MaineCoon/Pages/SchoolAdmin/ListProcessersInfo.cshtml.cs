using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using MaineCoon.Models;

namespace MaineCoon.Pages.SchoolAdmin
{
    public class ListProcessersInfoModel : PageModel
    {
        private readonly MaineCoon.Data.MaineCoonContext _context;
        public ListProcessersInfoModel(MaineCoon.Data.MaineCoonContext context) {
            _context = context;
        }
        public IActionResult OnGet(string name, int id=0)
        {
            var processorsInfo = new Dictionary<int, Dictionary<string,string>>();
            var processors = new List<Processer>();
            if (id > 0) {
                processors.Add(_context.Processers.Find(id));
            }
            else if (!String.IsNullOrEmpty(name)) {
                processors = _context.Processers.Where(m =>  m.friendlyName.Contains(name) ).ToList();
            }
            else {
                processors = _context.Processers.ToList();
            }
            foreach (var item in processors) {
                var oneProcessorInfo = new Dictionary<string, string>();
                try {
                    oneProcessorInfo["name"] = item.friendlyName;
                    oneProcessorInfo["author"] = _context.User.Find(item.belongsToUserID)?.name;
                    oneProcessorInfo["Instruction"] = item.Instruction;
                    oneProcessorInfo["parameter"] = item.AlgorithmParameterJson;
                    processorsInfo[item.Id] = oneProcessorInfo;
                }
                catch (Exception e) {

                }
            }
            return Content(JsonConvert.SerializeObject(processorsInfo));

        }
    }
}