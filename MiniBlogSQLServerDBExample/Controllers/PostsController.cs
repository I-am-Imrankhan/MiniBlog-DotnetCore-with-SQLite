using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniBlogSQLServerDBExample.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public async Task<IActionResult> Index()
        {
            return View(await context.Post.ToListAsync());
        }


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

        // Posta: Delete funktion (Post/Delete)
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await context.Post.FindAsync(id);
            context.Post.Remove(movie);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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

        // Posta: Post/Edit (OBS! Edit metod kan återopa med hjälp av Post request
        // För att kunna undvika överposting vi ska göra binding 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID, Title, PostDate, AuthName, PostContent")] Post post)
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

                    //if (!PostExisted(post.Id))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }
    }
}
