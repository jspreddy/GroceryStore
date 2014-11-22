using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroceryStore.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using GroceryStore.StringTags;

namespace GroceryStore.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationDbContext db;
        private ApplicationUserManager userManager;
        private ApplicationUser currentUser;
        public HomeController()
        {
            db = new ApplicationDbContext();
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            currentUser = userManager.FindById(User.Identity.GetUserId());
        }
        public ActionResult Index()
        {
            return View("Index",GetHomePageViewModel());
        }

        public PartialViewResult RenderCreateInventoryPartial(InventoryViewModel inventory)
        {
            return PartialView("CreateInventoryPartial", inventory);
        }

        public PartialViewResult RenderInventoryListPartial(IList<InventoryViewModel> inventoryItems)
        {
            return PartialView("InventoryListPartial", inventoryItems);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ApplicationRoles.Inventory.RW)]
        public async Task<ActionResult> CreateInventory(InventoryViewModel inventory)
        {
            if (ModelState.IsValid)
            {
                inventory.LastEdited = DateTime.Now;
                inventory.User = currentUser;
                db.Inventory.Add(inventory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            HomePageViewModel viewModel = GetHomePageViewModel();
            viewModel.CreateInventoryItem = inventory;
            return View("Index",viewModel);
        }

        #region helpers
        private HomePageViewModel GetHomePageViewModel()
        {
            HomePageViewModel homePageViewModel = new HomePageViewModel();
            homePageViewModel.InventoryList = db.Inventory.ToList();
            homePageViewModel.CreateInventoryItem = new InventoryViewModel();
            return homePageViewModel;
        }
        #endregion
    }
}