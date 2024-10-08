﻿using RealEst.Core.Constants;
using RealEst.Core.DTOs;

namespace RealEst.Core.Models
{
    public class Defect : IBaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DefectTypes DefectType { get; set; }
        public Organisation Organisation { get; init; }

        public Defect()
        {
            
        }

        public Defect(int id, string name, string description, DefectTypes defectType)
        {
            Id = id;
            Name = name;
            Description = description;
            DefectType = defectType;
        }

        public Defect(DefectInputDto defectDTO, Organisation organisation)
            : this(0, defectDTO.Name, defectDTO.Description, defectDTO.DefectType)
        {
            Organisation = organisation;
        }
    }
}
