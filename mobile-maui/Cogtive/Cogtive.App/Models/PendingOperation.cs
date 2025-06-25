using Cogtive.App.Enum;

namespace Cogtive.App.Models
{
    public class PendingOperation
    {
        public int Id { get; set; }
        public string EntityType { get; set; } // ex: "Machine" ou "ProductionData"
        public OperationType OperationType { get; set; }
        public string PayloadJson { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
