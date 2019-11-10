using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using MaineCoon.Models;
using System.Security.Claims;

namespace MaineCoon.Pages.Developer
{
    public class IndexModel : PageModel {
        public IndexModel(MaineCoon.Data.MaineCoonContext context) {
            _context = context;
        }
        private readonly MaineCoon.Data.MaineCoonContext _context;
        public IActionResult OnGet(string message="")
        {
            ViewData["message"] = message;
            if (this.User.Identity.IsAuthenticated) {
                //logined
                var currentUserId = Convert.ToInt32(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value ?? "-1");
                var context = _context.Processers.Where(procer => procer.belongsToUserID ==currentUserId);
                List<_Card> _Cards = new List<_Card>();
                foreach (var oneProcesser in context) {
                    var oneCard = new _Card();
                    oneCard.Head = oneProcesser.Id.ToString();
                    oneCard.BodyTitle = oneProcesser.friendlyName;
                    oneCard.Body = string.Format("This processor has been called {0} times", oneProcesser.count);
                    oneCard.Btn0URL = Request.Path + "/Edit?id=" + oneProcesser.Id;
                    oneCard.Btn0Label = "Edit";
                    oneCard.Btn1URL = Request.Path + "/Delete?id=" + oneProcesser.Id;
                    oneCard.Btn1Label = "Delete";
                    _Cards.Add(oneCard);
                }
                ViewData["CardList"] = _Cards;
                return Page();
            }
            else {
                return RedirectToPage("/Index");
            }
        }
    }
}