﻿using CIS174Library.Models;
using CIS174Library.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CIS174Library.Controllers
{
    public class LibraryController : Controller
    {
        private ILibraryRepository libraryRepository;

        //public MovieController(MovieContext ctx)
        public LibraryController(ILibraryRepository repository)
        {
            //context = ctx
            this.libraryRepository = repository;
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("Edit", new Book());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var movie = this.libraryRepository.Find(id);
            return View(movie);
        }

        [HttpPost]
        public IActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                if (book.BookId == 0)
                {
                    this.libraryRepository.InsertBook(book);
                }
                else
                {
                    this.libraryRepository.UpdateBook(book);
                }
                this.libraryRepository.Save();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Action = (book.BookId == 0) ? "Add" : "Edit";
                return View(book);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var movie = this.libraryRepository.Find(id);
            return View(movie);
        }

        [HttpPost]
        public IActionResult Delete(Book book)
        {
            //context.Movies.Remove(movie);
            //context.SaveChanges();
            this.libraryRepository.DeleteBook(book);
            return RedirectToAction("Index", "Home");
        }
    }
}
