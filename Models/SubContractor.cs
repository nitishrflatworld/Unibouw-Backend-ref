namespace Unibouw.Models
{
    public class SubContractor
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }    
        public string Location { get; set; }
        public string Country { get; set; }
        public DateTime? registeredDate { get; set; }
        public string Email {  get; set; }
        public bool Status { get; set; }
        public string officeAddress { get; set; }
        public string billingAddress { get; set; }
        public List<string> Attachments { get; set; }
    }
}
