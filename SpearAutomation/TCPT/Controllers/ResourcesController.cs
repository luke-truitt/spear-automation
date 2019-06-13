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
using TCPT.Models;
using TCPT.Models.Services;

namespace TCPT.Controllers
{
    public class ResourcesController : Controller
    {
        private readonly ITCPTService _service;

        public ResourcesController(ITCPTService service)
        {
            _service = service;
        }

        // GET: Resources
        public async Task<IActionResult> Index()
        {
            return View(_service.Get());
        }

        // GET: Resources/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resource = _service.GetById(id ?? 0);
            if (resource == null)
            {
                return NotFound();
            }

            return View(resource);
        }

        // GET: Resources/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Resources/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResourceId,Type,Available,Location,VehicleType,CertificationLevel")] Resource resource)
        {
            if (ModelState.IsValid)
            {
                _service.Create(resource);
                return RedirectToAction(nameof(Index));
            }
            return View(resource);
        }

        // GET: Resources/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resource = _service.GetById(id ?? 0);
            if (resource == null)
            {
                return NotFound();
            }
            return View(resource);
        }

        // POST: Resources/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ResourceId,Type,Available,Location,VehicleType,CertificationLevel")] Resource resource)
        {
            if (id != resource.ResourceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _service.Update(resource);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResourceExists(resource.ResourceId))
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
            return View(resource);
        }

        // GET: Resources/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resource = _service.GetById(id ?? 0);
            if (resource == null)
            {
                return NotFound();
            }

            return View(resource);
        }

        // POST: Resources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resource = _service.GetById(id);
            _service.Delete(resource);
            return RedirectToAction(nameof(Index));
        }

        private bool ResourceExists(int id)
        {
            return _service.Get().Any(x => id == x.ResourceId);
        }
        [HttpPost]
        public async Task<ActionResult> UploadFile(IFormFile file)
        {
            List<Resource> newData = new List<Resource>();

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
                        var resource = new Resource();
                        resource.ResourceId = Int32.Parse(r[0]);
                        resource.Type = (ResourceType)Int32.Parse(r[1]);
                        resource.Available = Boolean.Parse(r[2]);
                        resource.Location = (Location)Int32.Parse(r[3]);
                        if (!r[4].Equals(""))
                        {
                            resource.VehicleType = (VehicleType)Int32.Parse(r[4]);
                        }
                        if (!r[5].Equals("\r"))
                        {
                            resource.CertificationLevel = (CertificationLevel)Int32.Parse(r[5]);
                        }

                        newData.Add(resource);
                    }
                }
            }

            foreach (var resource in newData)
            {
                var entity = _service.GetById(resource.ResourceId);

                if (entity == null)
                {
                    _service.Create(resource);
                }
                else
                {
                    entity.Type = resource.Type;
                    entity.Available = resource.Available;
                    entity.Location = resource.Location;
                    entity.VehicleType = resource.VehicleType;
                    entity.CertificationLevel = resource.CertificationLevel;
                    _service.Update(entity);
                }
            }

            var vehicles = _service.Get();
            return View("Index", vehicles);
        }

        [STAThread]
        public async Task<IActionResult> DownloadDispatchReport()
        {
            var resources = await _service.GetAsync();
            Thread thdSyncRead = new Thread(()=> {fileSaving(resources); });
            thdSyncRead.SetApartmentState(ApartmentState.STA);
            thdSyncRead.Start();

            return View("Index", resources);
        }

        public void fileSaving(List<Resource> resources)
        {
            FileWriter writer = new FileWriter();
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "CSV File|*.csv";
            saveFileDialog1.Title = "Download Dispatch Report to CSV";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                writer.WriteData(saveFileDialog1.FileName, resources);
            }

            return;
        }
    }
}
