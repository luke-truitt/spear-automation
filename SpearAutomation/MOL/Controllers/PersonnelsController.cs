using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MOL.Models;
using MOL.Models.Services;

namespace MOL.Controllers
{
    public class PersonnelsController : Controller
    {
        private readonly IMOLService _service;

        public PersonnelsController(IMOLService service)
        {
            _service = service;
        }

        // GET: Personnels
        public async Task<IActionResult> Index()
        {
            return View(_service.Get());
        }

        // GET: Personnels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personnel = _service.GetById(id ?? 0);
            if (personnel == null)
            {
                return NotFound();
            }

            return View(personnel);
        }

        // GET: Personnels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Personnels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MarineId,DateReturning,Location,CertificationLevel")] Personnel personnel)
        {
            if (ModelState.IsValid)
            {
                _service.Create(personnel);
                return RedirectToAction(nameof(Index));
            }
            return View(personnel);
        }

        // GET: Personnels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personnel = _service.GetById(id ?? 0);
            if (personnel == null)
            {
                return NotFound();
            }
            return View(personnel);
        }

        // POST: Personnels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MarineId,DateReturning,Location,CertificationLevel")] Personnel personnel)
        {
            if (id != personnel.MarineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _service.Update(personnel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonnelExists(personnel.MarineId))
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
            return View(personnel);
        }

        // GET: Personnels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personnel = _service.GetById(id ?? 0);
            if (personnel == null)
            {
                return NotFound();
            }

            return View(personnel);
        }

        // POST: Personnels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personnel = _service.GetById(id);
            _service.Delete(personnel);
            return RedirectToAction(nameof(Index));
        }

        private bool PersonnelExists(int id)
        {
            return _service.Get().Any(e => e.MarineId == id);
        }

        [HttpPost]
        public async Task<ActionResult> UploadFile(IFormFile file)
        {
            List<Personnel> newData = new List<Personnel>();

            if (file != null)
            {
                var path = Path.Combine(
                    Directory.GetCurrentDirectory(), "wwwroot",
                    file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                //Read the contents of CSV file.
                string csvData = System.IO.File.ReadAllText(path);

                int i = 0;
                //Execute a loop over the rows.
                foreach (string row in csvData.Split('\n'))
                {
                    if (i == 0)
                    {
                        i++;
                        continue;
                    }

                    var r = row.Split(",");
                    if (!string.IsNullOrEmpty(row) && !r[0].Equals("\r"))
                    {
                        newData.Add(new Personnel()
                        {
                            MarineId = Int32.Parse(r[0]),
                            DateReturning = DateTime.Parse(r[1]),
                            Location = (Location)Int32.Parse(r[2]),
                            CertificationLevel = (CertificationLevel)Int32.Parse(r[3]),
                        });
                    }
                }
            }

            foreach (var marine in newData)
            {
                var entity = _service.GetById(marine.MarineId);

                if (entity == null)
                {
                    _service.Create(marine);
                }
                else
                {
                    entity.DateReturning = marine.DateReturning;
                    entity.Location = marine.Location;
                    entity.CertificationLevel = marine.CertificationLevel;
                    _service.Update(entity);
                }
            }

            var marines = _service.Get();
            return View("Index", marines);
        }

        [STAThread]
        public async Task<IActionResult> DownloadMarineReport()
        {
            var marines = await _service.GetAsync();
            Thread thdSyncRead = new Thread(()=> {
                fileSaving(marines);
                }
            );
            thdSyncRead.SetApartmentState(ApartmentState.STA);
            thdSyncRead.Start();

            return View("Index", marines);
        }

        public void fileSaving(List<Personnel> marines)
        {
            FileWriter writer = new FileWriter();
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "CSV File|*.csv";
            saveFileDialog1.Title = "Download Report to CSV";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                writer.WriteData(saveFileDialog1.FileName, marines);
            }

            return;
        }
    }
}
