using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GCSS.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using GCSS.Models.Services;

namespace GCSS.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly IGCSSService _service;

        public VehiclesController(IGCSSService service)
        {
            _service = service;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            return View(_service.Get());
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            } else
            {
                var vehicle = _service.GetById(id ?? 0);
                if (vehicle == null)
                {
                    return NotFound();
                }

                return View(vehicle);
            }            
        }

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Tam,DateAvailable,Location,VehicleType")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                _service.Create(vehicle);
                return RedirectToAction(nameof(Index));
            }
            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            } else
            {
                var vehicle = _service.GetById(id ?? 0);
                if (vehicle == null)
                {
                    return NotFound();
                }
                return View(vehicle);
            }
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Tam,DateAvailable,Location,VehicleType")] Vehicle vehicle)
        {
            if (id != vehicle.Tam)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _service.Update(vehicle);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.Tam))
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
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = _service.GetById(id??0);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicle = _service.GetById(id);
            _service.Delete(vehicle);
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(int id)
        {
            return _service.Get().Any(x => x.Tam == id);
        }
        [HttpPost]
        public async Task<ActionResult> UploadFile(IFormFile file)
        {
            List<Vehicle> newData = new List<Vehicle>();

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
                    if (!string.IsNullOrEmpty(row))
                    {
                        newData.Add(new Vehicle()
                        {
                            Tam = Int32.Parse(r[0]),
                            DateAvailable = DateTime.Parse(r[1]),
                            Location = (Location) Int32.Parse(r[2]),
                            VehicleType = (VehicleType) Int32.Parse(r[3]),
                        });
                    }
                }
            }

            foreach (var vehicle in newData)
            {
                var entity = _service.GetById(vehicle.Tam);

                if (entity == null)
                {
                    _service.Create(vehicle);
                }
                else
                {
                    entity.DateAvailable = vehicle.DateAvailable;
                    entity.Location = vehicle.Location;
                    entity.VehicleType = vehicle.VehicleType;
                    _service.Update(entity);
                }
            }

            var vehicles = _service.Get();
            return View("Index", vehicles);
        }

        [STAThread]
        public async Task<IActionResult> DownloadMPR()
        {
            var vehicles = await _service.GetAsync();
            Thread thdSyncRead = new Thread(()=>
            {
                fileSaving(vehicles);
            });
            thdSyncRead.SetApartmentState(ApartmentState.STA);
            thdSyncRead.Start();

            return View("Index", vehicles);
        }

        public void fileSaving(List<Vehicle> vehicles)
        {
            FileWriter writer = new FileWriter();
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "CSV File|*.csv";
            saveFileDialog1.Title = "Download MPR to CSV";
            saveFileDialog1.ShowDialog();

            if(saveFileDialog1.FileName != "")
            {
                writer.WriteData(saveFileDialog1.FileName, vehicles);
            }

            return;
        }
    }
}
