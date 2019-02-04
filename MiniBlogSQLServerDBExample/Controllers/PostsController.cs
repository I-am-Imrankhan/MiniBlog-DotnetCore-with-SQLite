using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniBlogSQLServerDBExample.Models;

// Jag har implementerat alla CRUD funktioner här i PostController istället för att skapa en annan klass till Database Funktioner
// Jag har lagt till Updateringen funktion för övning

namespace MiniBlogSQLServerDBExample.Controllers
{
    public class PostsController : Controller
    {
        private readonly MvcPostContext context;

        public PostsController(MvcPostContext context)
        {
            this.context = context;
        }

        // GET: Posts
        // Första sidan filla på med allt från databasen
        // Index.cshtml: lägnst upp i filen finns IEnurrable Interface som är typ en lista 
        // och gör en collection av specifika typ. T.ex. post model
        public async Task<IActionResult> Index()
        {
            return View(await context.Post.ToListAsync());
        }

        // Login 
        public IActionResult Login()
        {
            return View("Login");
        }

        // Skapa nya post
        public IActionResult CreatePost()
        {
            return View("CreatePost");
        }


        //Put: Create new post method
        public async Task<IActionResult> Create(Post post)
        {
            if(!ModelState.IsValid)
            {
                return View("Index", post);
            }

            context.Post.Add(post);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        // GET: Posts/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await context.Post
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }


        // Get: Post/Edit
        // Hitta post rad med id
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var post = await context.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if( id==null) { return NotFound(); }
            var post = await context.Post.FindAsync(id);
            context.Post.RemoveRange(post);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Posta: Post/Edit (OBS! Edit metod kan återopa med hjälp av Post request
        // För att kunna undvika överposting vi ska göra binding 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Title, PostDate, AuthName, PostContent")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(post);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (post.Id.Equals( true ))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }
    }
}
