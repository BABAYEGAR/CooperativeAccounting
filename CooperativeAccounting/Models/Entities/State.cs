using System.ComponentModel.DataAnnotations;

namespace CooperativeAccounting.Models.Entities
{
    public class State
    {
        [Key]
        public int StateId { get; set; }
        public string Name { get; set; }
    }
}