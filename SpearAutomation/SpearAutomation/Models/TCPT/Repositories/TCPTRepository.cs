using Microsoft.Extensions.Logging;
using SpearAutomation.Models.Logger.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpearAutomation.Models.TCPT.Repositories
{
    public class TCPTRepository : ITCPTRepository
    {
        private readonly SPEARTCPTContext _context;
        private readonly ILogger<TCPTRepository> _logger;

        public TCPTRepository(ILogger<TCPTRepository> logger, SPEARTCPTContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IEnumerable<Resource> Get()
        {
            return _context.Resource;
        }

        public Resource GetById(int id)
        {
            return _context.Resource.FirstOrDefault(x => x.ResourceId == id);
        }


        public Resource Create(Resource entity)
        {
            var resource = GetById(entity.ResourceId);

            if (resource != null)
            {
                return Update(resource);
            }

            _context.Resource.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public Resource Update(Resource resource)
        {
            var entity = GetById(resource.ResourceId);

            if (entity == null)
            {
                return Create(resource);
            }

            _context.Resource.Update(resource);
            _context.SaveChanges();

            return resource;
        }

        public IEnumerable<Resource> UpdateList(IEnumerable<Resource> resources)
        {
            var createdVehicles = new List<Resource>();
            var updatedVehicles = new List<Resource>();
            var createdPersonnel = new List<Resource>();
            var updatedPersonnel = new List<Resource>();

            foreach (var resource in resources)
            {
                var entity = GetById(resource.ResourceId);

                if (entity == null)
                {
                    if(resource.Type == Enums.ResourceType.Personnel)
                    {
                       
                        createdPersonnel.Add(resource);
                    } else
                    {
                        createdVehicles.Add(resource);
                    }
                    _context.Resource.Add(resource);
                    _context.SaveChanges();
                }
                else
                {
                    if (!(entity.Available.Equals(resource.Available) && entity.Location.Equals(resource.Location) && entity.Type.Equals(resource.Type)))
                    {
                        if (resource.Type == Enums.ResourceType.Personnel)
                        {
                            updatedPersonnel.Add(resource);
                        }
                        else
                        {
                            updatedVehicles.Add(resource);
                        }
                        entity.Available = resource.Available;
                        entity.Location = resource.Location;
                        entity.Type = resource.Type;
                        _context.Resource.Update(entity);
                        _context.SaveChanges();
                    }  
                }

            }

            if(updatedPersonnel.Count > 0)
            {
                _logger.LogInformation((int)LoggingEvent.UPDATE_ITEM, "Just updated " + NumberToWords(updatedPersonnel.Count) + " Marine's Information");
            }
            if(updatedVehicles.Count > 0)
            {
                _logger.LogInformation((int)LoggingEvent.UPDATE_ITEM, "Just updated " + NumberToWords(updatedVehicles.Count) + " Vehicle's Information");
            }
            if (createdVehicles.Count > 0)
            {
                _logger.LogInformation((int)LoggingEvent.CREATE_ITEM, "Just created " + NumberToWords(createdVehicles.Count) + " Vehicle's Information");
            }
            if (createdPersonnel.Count > 0)
            {
                _logger.LogInformation((int)LoggingEvent.CREATE_ITEM, "Just created " + NumberToWords(createdPersonnel.Count) + " Marine's Information");
            }
            return resources;
        }
        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }
    }

}
