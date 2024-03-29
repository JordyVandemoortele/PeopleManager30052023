﻿using Microsoft.EntityFrameworkCore;
using PeopleManager.Models;
using PeopleManager.Core;

namespace PeopleManager.Services
{
    public class VehicleService
    {
        private readonly PeopleManagerDbContext _dbContext;

        public VehicleService(PeopleManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Find
        public IList<Vehicle> Find()
        {
            return _dbContext.Vehicles
                .Include(p => p.ResponsiblePerson)
                .ToList();
        }

        //Get by id
        public Vehicle? Get(int id)
        {
            return _dbContext.Vehicles.Find(id);
        }

        //Create
        public Vehicle? Create(Vehicle vehicle)
        {
            _dbContext.Add(vehicle);
            _dbContext.SaveChanges();

            return vehicle;
        }

        //Update
        public Vehicle? Update(int id, Vehicle vehicle)
        {
            var dbVehicle = _dbContext.Vehicles.Find(id);
            if (dbVehicle is null)
            {
                return null;
            }

            dbVehicle.LicensePlate = vehicle.LicensePlate;
            dbVehicle.ResponsiblePerson = vehicle.ResponsiblePerson;
            dbVehicle.ResponsiblePersonId = vehicle.ResponsiblePersonId;
            dbVehicle.Brand = vehicle.Brand;
            dbVehicle.Type = vehicle.Type;

            _dbContext.SaveChanges();

            return dbVehicle;
        }

        //Delete
        public void Delete(int id)
        {
            var vehicle = new Vehicle
            {
                Id = id,
                LicensePlate = string.Empty,
                ResponsiblePersonId = 0,
                Brand = string.Empty,
                ResponsiblePerson = null,
                Type = string.Empty
            };
            _dbContext.Vehicles.Attach(vehicle);

            _dbContext.Vehicles.Remove(vehicle);

            _dbContext.SaveChanges();
        }
    }
}
