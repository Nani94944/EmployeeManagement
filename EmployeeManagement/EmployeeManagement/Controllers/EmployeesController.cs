using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeDbContext _context;
        private const int PageSize = 5;

        public EmployeesController ( EmployeeDbContext context )
        {
            _context=context;
        }

        public async Task<IActionResult> Index ( int page = 1 )
        {
            var totalItems = await _context.Employees.CountAsync ();
            var employees = await _context.Employees
                .OrderBy ( e => e.Id )
                .Skip ( (page-1)*PageSize )
                .Take ( PageSize )
                .ToListAsync ();

            ViewBag.CurrentPage=page;
            ViewBag.TotalPages=(int)Math.Ceiling ( (double)totalItems/PageSize );
            return View ( employees );
        }

        public IActionResult Create ( )
        {
            return View ();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create ( Employee employee )
        {
            if (ModelState.IsValid)
            {
                _context.Add ( employee );
                await _context.SaveChangesAsync ();
                TempData["SuccessMessage"]="Employee added successfully!";
                return RedirectToAction ( nameof ( Index ) );
            }
            return View ( employee );
        }

        public async Task<IActionResult> Edit ( int? id )
        {
            if (id==null) return NotFound ();
            var employee = await _context.Employees.FindAsync ( id );
            if (employee==null) return NotFound ();
            return View ( employee );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit ( int id , Employee employee )
        {
            if (id!=employee.Id) return NotFound ();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update ( employee );
                    await _context.SaveChangesAsync ();
                    TempData["SuccessMessage"]="Employee updated successfully!";
                    return RedirectToAction ( nameof ( Index ) );
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Employees.Any ( e => e.Id==id ))
                    {
                        return NotFound ();
                    }
                    throw;
                }
            }
            return View ( employee );
        }

        [HttpPost]
        public async Task<IActionResult> Delete ( int id )
        {
            var employee = await _context.Employees.FindAsync ( id );
            if (employee==null)
            {
                return Json ( new { success = false , message = "Employee not found" } );
            }

            _context.Employees.Remove ( employee );
            await _context.SaveChangesAsync ();
            return Json ( new { success = true , message = "Employee deleted successfully!" } );
        }
    }
}