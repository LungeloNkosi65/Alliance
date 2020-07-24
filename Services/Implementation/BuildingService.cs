using Accommodation.DAL.Interface;
using Accommodation.Models;
using Accommodation.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Accommodation.Services.Implementation
{
    public class BuildingService : IBuildingService
    {
        private IBuildingRepository _buildingRepository;

        public BuildingService(IBuildingRepository buildingRepository)
        {
           _buildingRepository = buildingRepository;
        }
        public bool Delete(Building building)
        {
            return _buildingRepository.Delete(building);
        }

        public IEnumerable<Building> Find(Func<Building, bool> prdicate)
        {
            return _buildingRepository.Find(prdicate);
        }

        public List<Building> GetBuildings()
        {
            return _buildingRepository.GetBuildings().ToList();
        }

        public Building GetBuildings(int id)
        {
            return _buildingRepository.GetBuildings(id);
        }

        public bool Insert(Building building)
        {
            return _buildingRepository.Insert(building);
        }

        public bool Update(Building building)
        {
            return _buildingRepository.Update(building);
        }
    }
}