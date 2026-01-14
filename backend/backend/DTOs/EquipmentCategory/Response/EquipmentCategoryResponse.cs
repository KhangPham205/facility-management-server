using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.DTOs.Criteria.Response;
using backend.DTOs.Equipment.Response;

namespace backend.DTOs.EquipmentCategory.Response
{
    public class EquipmentCategoryResponse
    {
        public string? EquipmentCategoryId { get; set; }

        public string? EquipmentCategoryName { get; set; }

        public string? Description { get; set; }

        public ICollection<CriteriaResponse>? Criterias { get; set; }
        public ICollection<EquipmentResponse>? Equipments { get; set; }
    }
}
