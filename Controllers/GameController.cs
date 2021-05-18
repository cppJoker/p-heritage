using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Projet_Heritage.Models;
using Projet_Heritage.ViewModel;

namespace Projet_Heritage.Controllers
{
    [AllowAnonymous]
    public class GameController : Controller
    {
        private ApplicationDbContext _context;
        public GameController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Game
        public ActionResult New()
        {
            if (_context.SerialKeys.Where(c => c.Activated == false).ToList().Count == 0) 
                return HttpNotFound();
            var viewModel = new GameFormViewModel
            {
                Game = new Game
                {
                    DatePublished = DateTime.Now,
                    Stars = 5
                },
            };
            return View(viewModel);
        }
        public ActionResult Index(int id = 0, bool hasError = false)
        {
            var game = _context.Games.SingleOrDefault(c => c.Id == id);
            if (game == null)
                return RedirectToAction("List");
            var viewModel = new GamePageViewModel()
            {
                Game = game,
                Reviews = _context.Reviews.Where(c=>c.GameId == game.Id).ToList(),
                Comment = new Review()
                {
                    Published = DateTime.Now
                },
                Key = "",
                HadError = hasError
            };
            
            return View("Game", viewModel);
        }

        public ActionResult SendComment(GamePageViewModel viewModel)
        {
            var comment = viewModel.Comment;
            if (comment.Content.IsNullOrWhiteSpace())
                return RedirectToAction("Index", new {id = viewModel.Game.Id});
            comment.Published = DateTime.Now;

            //Trouver trouver si le code existe. Si oui, extraire la cle
            var serialKeyInDb = _context.SerialKeys.SingleOrDefault(c => c.Key == viewModel.Key);
            if(serialKeyInDb == null)
                return RedirectToAction("Index", new { id = viewModel.Game.Id, hasError = true });

            //Selectionner tout les reviews qui corresponde avec la cle
            var reviewsLinkedWithKey = _context.Reviews.Where(c=> c.SerialKeyId == serialKeyInDb.Id).ToList();

            //Voir si le game ID est deja existant avec la cle
            var checkerSelfGame = reviewsLinkedWithKey.FirstOrDefault(c => c.GameId == serialKeyInDb.GameID);
            if (checkerSelfGame != null)
                return RedirectToAction("Index", new { id = viewModel.Game.Id, hasError = true });

            var checkerGame = reviewsLinkedWithKey.SingleOrDefault(c=>c.SerialKeyId == serialKeyInDb.Id && c.GameId == viewModel.Game.Id);
            if (checkerGame != null)
                return RedirectToAction("Index", new { id = viewModel.Game.Id, hasError = true });

            comment.GameId = viewModel.Game.Id;
            comment.SerialKeyId = serialKeyInDb.Id;

            var authorGame = _context.Games.Single(c => c.Id == serialKeyInDb.GameID);
            comment.Authors = authorGame.TeamMembers;
            if(comment.GameId == authorGame.Id)
                return RedirectToAction("Index", new { id = viewModel.Game.Id, hasError = true });
            _context.Reviews.Add(comment);
            _context.SaveChanges();
            return RedirectToAction("Index",new {id = viewModel.Game.Id});
        }
        private static Random rng = new Random();

   
        public ActionResult List(GameListViewModel viewModel)
        {
            List<Game> games = _context.Games.ToList();
            games = games.OrderBy(x => random.Next()).ToList();
            string matiereText = "";
            string yearText = "";
            switch (viewModel.ModuleCont)
             {
                 case GameListViewModel.ModuleContent.French:
                     matiereText = "Français";
                 break;
                 case GameListViewModel.ModuleContent.Math:
                     matiereText = "Mathématiques";
                     break;
                case GameListViewModel.ModuleContent.Physics:
                    matiereText = "Physique";
                    break;
                case GameListViewModel.ModuleContent.Chemistry:
                     matiereText = "Chimie";
                    break;
                case GameListViewModel.ModuleContent.ELA:
                     matiereText = "ELA";
                    break;
                case GameListViewModel.ModuleContent.History:
                     matiereText = "Monde contemporin";
                    break;
                case GameListViewModel.ModuleContent.Finance:
                     matiereText = "Éducation financière";
                    break;
                default:
                     matiereText = "";
                    break;
             }
            switch (viewModel.YearCont)
            {
                case GameListViewModel.YearContent.Both:
                    yearText = "Secondaire 4 et 5";
                    break;
                case GameListViewModel.YearContent.Four:
                    yearText = "Secondaire 4";
                    break;
                case GameListViewModel.YearContent.Five:
                    yearText = "Secondaire 5";
                    break;
            }
            games = games.Where(w => w.Stars != 0).ToList();
            SortedSet<int> realYears = new SortedSet<int>();
            foreach (var game in games)
            {
                realYears.Add(game.DatePublished.Year);
            }
            viewModel.RealizationList = realYears.OrderByDescending(x=>x).ToList();
            if (viewModel.Realization == null)
            {
                viewModel.Realization = realYears.Last();
            }
            if (viewModel.Realization != 0)
            {
                games = games.Where(w => w.DatePublished.Year == viewModel.Realization).ToList();
            }
            if (!string.IsNullOrEmpty(matiereText))
                games = games.Where(w => w.Modules.Contains(matiereText)).ToList();
            if (yearText != "Secondaire 4 et 5")
                games = games.Where(w => w.yearContent.Equals(yearText)).ToList();
            if (viewModel.Group != 0)
                games = games.Where(w => w.Group.Equals(viewModel.Group)).ToList();
            if (!string.IsNullOrEmpty(viewModel.Research))
                games = games.Where(w => w.Name.Contains(viewModel.Research)).ToList();
            games = games.OrderByDescending(w => w.Stars).ToList();
            viewModel.Games = games;
            return View(viewModel);

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(GameFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.HasError = false;
                viewModel.ErrorString = "";
                if (viewModel.File == null || viewModel.BigFile == null)
                {
                    viewModel.HasError = true;
                    viewModel.ErrorString = "Les fichiers d'images ne sont pas inclus.";
                    return View("New", viewModel);
                }

                if (!viewModel.Game.Link.StartsWith("https://") || !viewModel.Game.SolutionLink.StartsWith("https://"))
                {
                    viewModel.HasError = true;
                    viewModel.ErrorString = "Les liens ne commencent pas par https://";
                    return View("New", viewModel);
                }
                if (!viewModel.Game.GuideLink.IsNullOrWhiteSpace() && !viewModel.Game.GuideLink.StartsWith("https://"))
                {
                    viewModel.HasError = true;
                    viewModel.ErrorString = "Lien vers mode d'emploi ne commence pas par https://";
                    return View("New", viewModel);
                }
                if (!viewModel.Game.FormLink.IsNullOrWhiteSpace() && !viewModel.Game.FormLink.StartsWith("https://"))
                {
                    viewModel.HasError = true;
                    viewModel.ErrorString = "Lien vers le sondage ne commence pas par https://";
                    return View("New", viewModel);
                }
                var rootPath = Server.MapPath("~");
                var imgFileName = RandomString(10);
                var imgFileExtension = Path.GetExtension(viewModel.File.FileName);
                var largeFileName = RandomString(10);
                var largeFileExtension = Path.GetExtension(viewModel.BigFile.FileName);
                if (!CheckFileType(imgFileExtension) || hasSpecialChar(viewModel.File.FileName))
                {
                    viewModel.HasError = true;
                    viewModel.ErrorString = "La vignette est invalide ou contient des caractères spéciaux.";
                    return View("New", viewModel);
                }
                if (!CheckFileType(largeFileExtension) || hasSpecialChar(viewModel.BigFile.FileName))
                {
                    viewModel.HasError = true;
                    viewModel.ErrorString = "L'affiche est invalide ou contient des caractères spéciaux.";
                    return View("New", viewModel);
                }
                var serialKey = _context.SerialKeys.SingleOrDefault(c => c.Key == viewModel.Key.Trim());
                if (serialKey == null || serialKey.Activated)
                {
                    viewModel.Key = "";
                    viewModel.HasError = true;
                    viewModel.ErrorString = "Votre code d'équipe n'est pas valide";
                    return View("New", viewModel);
                }
                serialKey.Activated = true;
                viewModel.Game.DatePublished = DateTime.Now;
                string module = viewModel.Game.Modules;
                viewModel.Game.Modules = module.Remove(module.Length - 2);
                viewModel.Game.Stars = 5;
                _context.Games.Add(viewModel.Game);
                serialKey.GameID = viewModel.Game.Id;

                if (viewModel.ResourceFile != null)
                {
                    var fileName2 = RandomString(10);
                    var fileExtension2 = Path.GetExtension(viewModel.ResourceFile.FileName);
                    if (fileExtension2 != ".zip" || hasSpecialChar(viewModel.ResourceFile.FileName))
                    {
                        viewModel.HasError = true;
                        viewModel.ErrorString = "Le fichier n'est pas un zip ou contient des caractères spéciaux.";
                        return View("New", viewModel);
                    }

                    var imagePath2 = Server.MapPath("/UploadedMedia/") + fileName2 + fileExtension2;
                    viewModel.ResourceFile.SaveAs(imagePath2);
                    viewModel.Game.resourcePath = imagePath2.Replace(rootPath, "\\").Replace("\\", "/");
                }
                var imagePath = Server.MapPath("/UploadedMedia/") + imgFileName + imgFileExtension;
                viewModel.File.SaveAs(imagePath);
                viewModel.Game.imagePath = imagePath.Replace(rootPath, "\\").Replace("\\", "/");

                imagePath = Server.MapPath("/UploadedMedia/") + largeFileName + largeFileExtension;
                viewModel.BigFile.SaveAs(imagePath);
                viewModel.Game.largeImagePath = imagePath.Replace(rootPath, "\\").Replace("\\", "/");

                _context.SaveChanges();
               return RedirectToAction("List", "Game");
            }
            else
            {
                return View("New", viewModel);
            }
        }
    }
}