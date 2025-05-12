namespace backend.DTOs
{
    public class PartyDto
    {
        public string Id { get; set; }     // CusId or VenId
        public string Name { get; set; }   // Display name (e.g., "ABC Traders")
        public string Type { get; set; }   // "Customer" or "Vendor"
    }

}
