using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection.Emit;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Projet_Heritage.Models;
using HttpDeleteAttribute = System.Web.Http.HttpDeleteAttribute;

namespace Projet_Heritage.Controllers.api
{
    public class GlobalController : ApiController
    {
        private ApplicationDbContext _context;
        public GlobalController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [System.Web.Http.Route("api/global/key/freekeys")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult FreeKeysCount()
        {
            return Ok(_context.SerialKeys.Where(c=>c.Activated == false).ToList().Count);
        }

        [System.Web.Http.Route("api/global/key/{id}")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult GenerateKey(int id)
        {
            for (int i = 0; i < id; i++)
            {
                string randomKey = RandomString(5);
                if (_context.SerialKeys.SingleOrDefault(c => c.Key == randomKey) == null)
                {
                    var key = new SerialKey
                    {
                        Key = randomKey,
                        Activated = false,
                        GameID = null
                    };
                    _context.SerialKeys.Add(key);
                }
                else
                { 
                    var key = new SerialKey
                    {
                        Key = RandomString(5),
                        Activated = false,
                        GameID = null
                    };
                    _context.SerialKeys.Add(key);
                }

            }
            _context.SaveChanges();
            return Ok();
        }

        [System.Web.Http.Route("api/global/key/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteKey(int id)
        {
            var serverPath = System.Web.Hosting.HostingEnvironment.MapPath("~");
            if (id == -1)
            {
                var keys = _context.SerialKeys.Where(c => c.GameID == null);
                foreach (var key in keys)
                {
                    _context.SerialKeys.Remove(key);
                }
                _context.SaveChanges();
                return Ok();
            }
            else if (id == -2)
            {
                var keys = _context.SerialKeys.ToList();
                foreach (var key in keys)
                {
                    var game = _context.Games.SingleOrDefault(c => c.Id == key.GameID);
                    var reviews = _context.Reviews.Where(c => c.GameId == key.GameID).ToList();
                    if (key != null)
                        _context.SerialKeys.Remove(key);
                    if (game != null)
                    {
                        File.Delete(serverPath + game.imagePath.Replace("~", "").Replace("/", "\\"));
                        if (game.resourcePath != null)
                            File.Delete(serverPath + game.resourcePath.Replace("~", "").Replace("/", "\\"));
                        File.Delete(serverPath + game.largeImagePath.Replace("~", "").Replace("/", "\\"));
                        _context.Games.Remove(game);
                    }
                    foreach (var review in reviews.Where(review => review != null))
                    {
                        _context.Reviews.Remove(review);
                    }
                }
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                var key = _context.SerialKeys.SingleOrDefault(c => c.Id == id);
                var game = _context.Games.SingleOrDefault(c => c.Id == key.GameID);
                var reviews = _context.Reviews.Where(c => c.GameId == key.GameID).ToList();
                if (key != null)
                    _context.SerialKeys.Remove(key);
                if (game != null)
                {
                    File.Delete(serverPath + game.imagePath.Replace("~", "").Replace("/", "\\"));
                    if (game.resourcePath != null)
                        File.Delete(serverPath + game.resourcePath.Replace("~", "").Replace("/", "\\"));
                    File.Delete(serverPath + game.largeImagePath.Replace("~", "").Replace("/", "\\"));
                    _context.Games.Remove(game);
                }

                foreach (var review in reviews.Where(review => review != null))
                {
                    _context.Reviews.Remove(review);
                }
                _context.SaveChanges();
                return Ok();
            }
        }

        [System.Web.Http.Route("api/global/review/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteReview(int id)
        {
            var review = _context.Reviews.SingleOrDefault(c => c.Id == id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                _context.SaveChanges();
            }
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteGame(int id)
        {
            var game = _context.Games.SingleOrDefault(c => c.Id == id);
            var key = _context.SerialKeys.SingleOrDefault(c => c.GameID == id);
            var reviews = _context.Reviews.Where(c => c.GameId == id).ToList();
            var serverPath = System.Web.Hosting.HostingEnvironment.MapPath("~");
            if (game != null)
            {
                File.Delete(serverPath + game.imagePath.Replace("~", "").Replace("/","\\"));
                if(game.resourcePath != null)
                    File.Delete(serverPath + game.resourcePath.Replace("~", "").Replace("/", "\\"));
                File.Delete(serverPath + game.largeImagePath.Replace("~", "").Replace("/", "\\"));
                _context.Games.Remove(game);
            }
            if (key != null)
                _context.SerialKeys.Remove(key);
            foreach (var review in reviews.Where(review => review != null))
            {
                _context.Reviews.Remove(review);
            }
            _context.SaveChanges();
            return Ok();
        }
    }
}