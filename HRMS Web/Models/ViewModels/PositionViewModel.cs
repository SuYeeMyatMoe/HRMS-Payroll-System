namespace HRMS_Web.Models.ViewModels
{
    public class PositionViewModel
    {
        public string Id { get; set; }//for delete and update cases
        public string Name { get; set; }
        public  string Description { get; set; }
        public int Level { get; set; }
    }
}
