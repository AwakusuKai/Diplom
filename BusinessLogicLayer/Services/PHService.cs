using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Mappers;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Services
{
    public class PHService : IPHService
    {
        IRepository<PublishingHouse> PHRepository { get; set; }
        public PHService(IRepository<PublishingHouse> phRepository)
        {
            PHRepository = phRepository;
        }

        public void CreatePH(PublishingHouseDTO publishingHouseDTO)
        {
            PublishingHouse publishingHouse = Mapper.Convert<PublishingHouseDTO, PublishingHouse>(publishingHouseDTO);
            PHRepository.Create(publishingHouse);
        }
        public IEnumerable<PublishingHouseDTO> GetPHs()
        {
            List<PublishingHouseDTO> publishingHouseDTOs = new List<PublishingHouseDTO>();
            foreach (PublishingHouse publishingHouse in PHRepository.GetAll())
            {
                PublishingHouseDTO publishingHouseDTO = Mapper.Convert<PublishingHouse, PublishingHouseDTO>(publishingHouse);
                publishingHouseDTOs.Add(publishingHouseDTO);
            }

            return publishingHouseDTOs;
        }

        public void UpdatePH(PublishingHouseDTO publishingHouseDTO)
        {
            PublishingHouse publishingHouse = Mapper.Convert<PublishingHouseDTO, PublishingHouse>(publishingHouseDTO);
            PHRepository.Update(publishingHouse);
        }
        public PublishingHouseDTO GetPH(int? id)
        {
            var publishingHouse = PHRepository.GetById(id.Value);
            if (publishingHouse != null)
            {
                PublishingHouseDTO publishingHouseDTO = Mapper.Convert<PublishingHouse, PublishingHouseDTO>(publishingHouse);
                return publishingHouseDTO;
            }
            return null;
        }

        public void DeletePH(int id)
        {
            PHRepository.Delete(id);
        }
    }
}