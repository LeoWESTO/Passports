using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Passports.Data;
using Passports.Models;

namespace Passports.Controllers
{
    public class HomeController : Controller
    {
        DataContext db;
        public HomeController(DataContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View(db.Passports.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(PassportData data)
        {
            db.Passports.Add(data);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                PassportData? data = await db.Passports.FirstOrDefaultAsync(p => p.Id == id);
                if (data != null) return View(data);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(PassportData data)
        {
            db.Passports.Update(data);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                PassportData data = new PassportData { Id = id.Value };
                db.Entry(data).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
        public IActionResult DowloadExcel()
        {
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var excel = new ExcelPackage(stream))
            {
                var worksheet = excel.Workbook.Worksheets.Add("Паспорта");
                worksheet.Cells.LoadFromCollection(db.Passports, true);
                excel.Save();
            }
            stream.Position = 0;
            return File(stream, 
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Passports.xlsx");
        }
    }
}
