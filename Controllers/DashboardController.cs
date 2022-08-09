using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Projet_Heritage.Models;
using Projet_Heritage.ViewModel;

namespace Projet_Heritage.Controllers
{
    public class DashboardController : Controller
    {
        private ApplicationDbContext _context;
        public DashboardController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Dashboard
        public ActionResult Games(DashboardGameListViewModel viewModel)
        {
            List<Game> games = _context.Games.ToList();
            SortedSet<int> realYears = new SortedSet<int>();
            foreach (var game in games)
            {
                realYears.Add(game.DatePublished.Year);
            }
            viewModel.RealizationList = realYears.OrderByDescending(x => x).ToList();
            if (viewModel.Realization == null)
            {
                viewModel.Realization = realYears.Last();
            }
            if (viewModel.Realization != 0)
            {
                games = games.Where(w => w.DatePublished.Year == viewModel.Realization).ToList();
            }
            viewModel.Games = games;
            viewModel.HandInDateTime = _context.PlatformSettings.Single(c => c.Id == 1).HandInDateTime;
            return View("GameList", viewModel);
        }

        public ActionResult Settings()
        {
            var setting = _context.PlatformSettings.First();
            return View(setting);
        }

        public ActionResult SaveSettings(PlatformSetting setting)
        {
            var oldSettings = _context.PlatformSettings.Single(c => c.Id == 1);
            oldSettings.MainDescription = setting.MainDescription;
            oldSettings.MainTitle = setting.MainTitle;
            oldSettings.MainTitleIcon = setting.MainTitleIcon;
            oldSettings.MainSubTitle = setting.MainSubTitle;
            oldSettings.HandInDateTime = setting.HandInDateTime;
            _context.SaveChanges();
            return RedirectToAction("Settings");
        }

        public ActionResult Comments()
        {
            var reviews = _context.Reviews.ToList();
            List<ReviewGameViewModel> view = new List<ReviewGameViewModel>();
            foreach (var review in reviews)
            {
                view.Add(new ReviewGameViewModel()
                {
                    Review = review,
                    Game = _context.Games.SingleOrDefault(c => c.Id == review.GameId)
                });
            }
            return View("ReviewList", view);
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        bool hasSpecialChar(string input)
        {
            string specialChar = @"\|!#$%&/()=?»«@£§€{};'<>,";
            foreach (var item in specialChar)
            {
                if (input.Contains(item)) return true;
            }

            return false;
        }
        bool CheckFileType(string fileName)
        {
            switch (fileName.ToLower())
            {
                case ".jpg":
                    return true;
                case ".jpeg":
                    return true;
                case ".png":
                    return true;
                default:
                    return false;
            }
        }

        public ActionResult EditGame(GameFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var gameInDb = _context.Games.SingleOrDefault(c => c.Id == viewModel.Game.Id);
                if (gameInDb == null)
                    return HttpNotFound();
                viewModel.HasError = false;
                viewModel.ErrorString = "";
                // if (viewModel.File == null || viewModel.BigFile == null)
                // {
                //     viewModel.HasError = true;
                //     viewModel.ErrorString = "Les fichiers d'images ne sont pas inclus.";
                //     return View("GameEdit", viewModel);
                // }
                if (!viewModel.Game.Link.StartsWith("https://") || !viewModel.Game.SolutionLink.StartsWith("https://"))
                {
                    viewModel.HasError = true;
                    viewModel.ErrorString = "Les liens ne commencent pas par https://";
                    return View("GameEdit", viewModel);
                }
                if (!viewModel.Game.GuideLink.IsNullOrWhiteSpace() && !viewModel.Game.GuideLink.StartsWith("https://"))
                {
                    viewModel.HasError = true;
                    viewModel.ErrorString = "Lien vers mode d'emploi ne commence pas par https://";
                    return View("GameEdit", viewModel);
                }
                if (!viewModel.Game.FormLink.IsNullOrWhiteSpace() && !viewModel.Game.FormLink.StartsWith("https://"))
                {
                    viewModel.HasError = true;
                    viewModel.ErrorString = "Lien vers le sondage ne commence pas par https://";
                    return View("GameEdit", viewModel);
                }
                string module = viewModel.Game.Modules;
                if(module.EndsWith(", "))
                    viewModel.Game.Modules = module.Remove(module.Length - 2);

                var rootPath = Server.MapPath("~");

                if (viewModel.ResourceFile != null)
                {
                    var fileName = RandomString(10);
                    var fileExtension = Path.GetExtension(viewModel.ResourceFile.FileName);
                    if (fileExtension != ".zip" || hasSpecialChar(viewModel.ResourceFile.FileName))
                    {
                        viewModel.HasError = true;
                        viewModel.ErrorString = "Le fichier n'est pas un zip ou contient des caractères spéciaux.";
                        return View("GameEdit", viewModel);
                    }

                    var imagePath = Server.MapPath("/UploadedMedia/") + fileName + fileExtension;
                    viewModel.ResourceFile.SaveAs(imagePath);
                    viewModel.Game.resourcePath = imagePath.Replace(rootPath, "\\").Replace("\\", "/");
                    if(gameInDb.resourcePath != null) 
                        System.IO.File.Delete(rootPath + gameInDb.resourcePath.Replace("~", "").Replace("/", "\\"));
                }
                if (viewModel.File != null)
                {
                    var fileName = RandomString(10);
                    var fileExtension = Path.GetExtension(viewModel.File.FileName);
                    if (!CheckFileType(fileExtension) || hasSpecialChar(viewModel.File.FileName))
                    {
                        viewModel.HasError = true;
                        viewModel.ErrorString = "La vignette est invalide ou contient des caractères spéciaux.";
                        return View("GameEdit", viewModel);
                    }
                    var imagePath = Server.MapPath("/UploadedMedia/") + fileName + fileExtension;
                    viewModel.File.SaveAs(imagePath);
                    viewModel.Game.imagePath = imagePath.Replace(rootPath, "\\").Replace("\\", "/");
                    if (gameInDb.imagePath != null)
                        System.IO.File.Delete(rootPath + gameInDb.imagePath.Replace("~", "").Replace("/", "\\"));
                }
                if (viewModel.BigFile != null)
                {
                    var fileName = RandomString(10);
                    var fileExtension = Path.GetExtension(viewModel.BigFile.FileName);
                    if (!CheckFileType(fileExtension) || hasSpecialChar(viewModel.BigFile.FileName))
                    {
                        viewModel.HasError = true;
                        viewModel.ErrorString = "L'affiche est invalide ou contient des caractères spéciaux.";
                        return View("GameEdit", viewModel);
                    }
                    var imagePath = Server.MapPath("/UploadedMedia/") + fileName + fileExtension;
                    viewModel.BigFile.SaveAs(imagePath);
                    viewModel.Game.largeImagePath = imagePath.Replace(rootPath, "\\").Replace("\\", "/");
                    if (gameInDb.largeImagePath != null)
                        System.IO.File.Delete(rootPath + gameInDb.largeImagePath.Replace("~", "").Replace("/", "\\"));
                }

                gameInDb.Name = viewModel.Game.Name;
                gameInDb.Theme = viewModel.Game.Theme;
                gameInDb.Description = viewModel.Game.Description;
                gameInDb.Modules = viewModel.Game.Modules;
                gameInDb.TeamMembers = viewModel.Game.TeamMembers;
                gameInDb.Group = viewModel.Game.Group;
                gameInDb.yearContent = viewModel.Game.yearContent;
                gameInDb.Link = viewModel.Game.Link;
                gameInDb.FormLink = viewModel.Game.FormLink;
                gameInDb.SolutionLink = viewModel.Game.SolutionLink;
                gameInDb.GuideLink = viewModel.Game.GuideLink;
                if (viewModel.Game.imagePath != null)
                    gameInDb.imagePath = viewModel.Game.imagePath;
                if (viewModel.Game.resourcePath != null)
                    gameInDb.resourcePath = viewModel.Game.resourcePath;
                if (viewModel.Game.largeImagePath != null)
                    gameInDb.largeImagePath = viewModel.Game.largeImagePath;
                _context.SaveChanges();
                return RedirectToAction("Games", "Dashboard");
            }
            else
            {
                return View("GameEdit", viewModel);
            }
        }

        public ActionResult GameEdit(int id = 0)
        {
            if (id == 0)
            {
                return HttpNotFound();
            }
            Game game = _context.Games.SingleOrDefault(c => c.Id == id);
            string key = _context.SerialKeys.SingleOrDefault(k => k.GameID == id)?.Key;
            if (key == null)
                return HttpNotFound();
            if (game == null)
                return HttpNotFound();
            GameFormViewModel viewModel = new GameFormViewModel()
            {
                Game = game,
                Key = key
            };
            return View("GameEdit", viewModel);
        }

        public ActionResult Keys(KeyListViewModel viewModel)
        {
            var keys = _context.SerialKeys.ToList();
            var games = _context.Games.ToList();
            SortedSet<int> realYears = new SortedSet<int>();
            foreach (var key in keys)
            {
                realYears.Add(key.DateCreated.Year);
            }
            viewModel.RealizationList = realYears.OrderByDescending(x => x).ToList();
            if (viewModel.Realization == null)
            {
                viewModel.Realization = realYears.Last();
            }
            if (viewModel.Realization != 0)
            {
                keys = keys.Where(w => w.DateCreated.Year == viewModel.Realization).ToList();
            }
            foreach (var key in keys)
            {
                viewModel.keyGamePair.Add(key, games.SingleOrDefault(c => c.Id == key.GameID));
            }
            return View("KeyList", viewModel);
        }

    }
}