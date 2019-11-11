using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaineCoon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MaineCoon.Pages.Student
{
    public class IndexModel : PageModel {
        public IndexModel(MaineCoon.Data.MaineCoonContext context) {
            _context = context;
        }
        private readonly MaineCoon.Data.MaineCoonContext _context;
        public IActionResult OnGet(string message = "") {
            ViewData["message"] = message;
            var context = _context.UniversityPrograms?.ToArray()??null;
            List<_Card> _Cards = new List<_Card>();
            foreach (var program in context) {
                var oneCard = new _Card();
                oneCard.Head = program.Id.ToString();
                oneCard.BodyTitle = program.ProgramName;
                oneCard.Body = program.ProgramIntroduction;
                oneCard.Btn0URL = "info";
                oneCard.Btn0Label = "info";
                oneCard.Btn1URL = Request.Path + "/Apply?id=" + program.Id; ;
                oneCard.Btn1Label = "ApplyNow";
                _Cards.Add(oneCard);
            }
            ViewData["CardList"] = _Cards;
            return Page();
        }
    }
}