namespace backend.DTOs
{
    public class SubGroupDto
    {
        public  required string ParentGroupName { get; set; }  // Acn of parent
        public required string AccountType { get; set; }      // AcType of parent
        public  required string SubGroupName { get; set; }     // This will be stored as new Acn
    }

}
