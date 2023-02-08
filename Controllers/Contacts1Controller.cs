using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContactBook.Data;
using ContactBook.Models;
using Microsoft.VisualBasic;

namespace ContactBook.Controllers
{
    public class Contacts1Controller : Controller
    {
        private readonly ContactBookContext _context;

        public Contacts1Controller(ContactBookContext context)
        {
            _context = context;
        }

        // GET: Contacts1
        public async Task<IActionResult> Index()
        {
              return View(await _context.Contacts.ToListAsync());
        }

        // GET: Contacts1/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }
        // POST: Contacts1/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            if (_context.Contacts == null)
            {
                return Problem("Entity set is null.");
            }
            var Phrase = from P in _context.Contacts select P;

            if (!string.IsNullOrEmpty(SearchPhrase))
            {
                Phrase = Phrase.Where(j => j.name.Contains(SearchPhrase) || j.surname.Contains(SearchPhrase) || j.phonenumber.Contains(SearchPhrase) || j.email.Contains(SearchPhrase) || j.DOB.Contains(SearchPhrase));
            }

           return View("Index", await Phrase.ToListAsync());
        }

        //POST: Contacts1/WeeklyBD
        public async Task<IActionResult> WeeklyBD()

        {
            if (_context.Contacts == null)
            {
                return Problem("Entity set is null.");
            }
            var Birthday = from B in _context.Contacts select B;
            DateTime now = DateTime.Now;
            var today = now.Date;
            DayOfWeek day = DateTime.Now.DayOfWeek;
            var Days = day - DayOfWeek.Monday;
            Birthday = Birthday.Where(j => j.DOB.Contains(now.Month+ "-" + today.AddDays(-Days).Date.Day) || j.DOB.Contains(now.Month + "-" + today.AddDays(1-Days).Date.Day) || j.DOB.Contains(now.Month + "-" + today.AddDays(2-Days).Date.Day) || j.DOB.Contains(now.Month + "-" + today.AddDays(3 - Days).Date.Day) || j.DOB.Contains(now.Month + "-" + today.AddDays(4 - Days).Date.Day) || j.DOB.Contains(now.Month + "-" + today.AddDays(5 - Days).Date.Day) || j.DOB.Contains(now.Month + "-" + today.AddDays(6 - Days).Date.Day)|| j.DOB.Contains(now.Month + "-0" + today.AddDays(-Days).Date.Day) || j.DOB.Contains(now.Month + "-0" + today.AddDays(1 - Days).Date.Day) || j.DOB.Contains(now.Month + "-0" + today.AddDays(2 - Days).Date.Day) || j.DOB.Contains(now.Month + "-0" + today.AddDays(3 - Days).Date.Day) || j.DOB.Contains(now.Month + "-0" + today.AddDays(4 - Days).Date.Day) || j.DOB.Contains(now.Month + "-0" + today.AddDays(5 - Days).Date.Day) || j.DOB.Contains(now.Month + "-0" + today.AddDays(6 - Days).Date.Day));
            return View("BirthdayWeek", await Birthday.ToListAsync());
        }

        // GET: Contacts1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Contacts == null)
            {
                return NotFound();
            }

            var contacts = await _context.Contacts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contacts == null)
            {
                return NotFound();
            }

            return View(contacts);
        }

        // GET: Contacts1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contacts1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,name,surname,phonenumber,email,DOB")] Contacts contacts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contacts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contacts);
        }

        // GET: Contacts1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Contacts == null)
            {
                return NotFound();
            }

            var contacts = await _context.Contacts.FindAsync(id);
            if (contacts == null)
            {
                return NotFound();
            }
            return View(contacts);
        }

        // POST: Contacts1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,name,surname,phonenumber,email,DOB")] Contacts contacts)
        {
            if (id != contacts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contacts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactsExists(contacts.Id))
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
            return View(contacts);
        }

        // GET: Contacts1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Contacts == null)
            {
                return NotFound();
            }

            var contacts = await _context.Contacts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contacts == null)
            {
                return NotFound();
            }

            return View(contacts);
        }

        // POST: Contacts1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Contacts == null)
            {
                return Problem("Entity set 'ContactBookContext.Contacts'  is null.");
            }
            var contacts = await _context.Contacts.FindAsync(id);
            if (contacts != null)
            {
                _context.Contacts.Remove(contacts);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactsExists(int id)
        {
          return _context.Contacts.Any(e => e.Id == id);
        }
    }
}
