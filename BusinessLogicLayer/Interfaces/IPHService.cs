using BusinessLogicLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Interfaces
{
    public interface IPHService
    {
        void CreatePH(PublishingHouseDTO phDTO);
        void UpdatePH(PublishingHouseDTO phDTO);
        void DeletePH(int id);
        IEnumerable<PublishingHouseDTO> GetPHs();
        PublishingHouseDTO GetPH(int? id);
    }
}
