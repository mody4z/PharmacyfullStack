namespace Pharmacy.Application.DTOs
{
    public class InOutDto
    {
        public int Id { get; set; }
        public int MedicineId { get; set; }
        public string? MedicineName { get; set; }
        public int? EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public int? ClientId { get; set; }
        public string? ClientName { get; set; }
        public string TransactionType { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? Notes { get; set; }
    }

    public class CreateInOutDto
    {
        public int MedicineId { get; set; }
        public int? EmployeeId { get; set; }
        public int? ClientId { get; set; }
        public string TransactionType { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string? Notes { get; set; }
    }
}
