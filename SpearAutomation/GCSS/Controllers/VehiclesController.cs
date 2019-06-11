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

namespace GCSS.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly SPEARGCSSContext _context;

        public VehiclesController(SPEARGCSSContext context)
        {
            _context = context;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vehicle.ToListAsync());
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .FirstOrDefaultAsync(m => m.Tam == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
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
                _context.Add(vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
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
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
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

            var vehicle = await _context.Vehicle
                .FirstOrDefaultAsync(m => m.Tam == id);
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
            var vehicle = await _context.Vehicle.FindAsync(id);
            _context.Vehicle.Remove(vehicle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(int id)
        {
            return _context.Vehicle.Any(e => e.Tam == id);
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
                var entity = _context.Vehicle.FirstOrDefault(x => x.Tam == vehicle.Tam);

                if (entity == null)
                {
                    _context.Database.OpenConnection();
                    _context.Vehicle.Add(vehicle);
                    _context.SaveChanges();
                }
                else
                {
                    entity.DateAvailable = vehicle.DateAvailable;
                    entity.Location = vehicle.Location;
                    entity.VehicleType = vehicle.VehicleType;
                    _context.Vehicle.Update(entity);
                    _context.Database.OpenConnection();
                    _context.SaveChanges();
                }
            }

            var vehicles = _context.Vehicle;
            return View("Index", vehicles);
        }

        [STAThread]
        public async Task<IActionResult> DownloadMPR()
        {
            Thread thdSyncRead = new Thread(new ThreadStart(fileSaving));
            thdSyncRead.SetApartmentState(ApartmentState.STA);
            thdSyncRead.Start();
            var vehicles = await _context.Vehicle.ToListAsync();
            return View("Index", vehicles);
        }

        public async void fileSaving()
        {
            var vehicles = await _context.Vehicle.ToListAsync();

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
