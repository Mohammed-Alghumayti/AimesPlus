using System.Collections.Generic;

namespace SeniorProject.ViewModels
{
    public class FacultyMembersListViewModel
    {
        public string Query { get; set; }
        public List<FacultyMembersItem> FacultyMembers { get; set; }

        public class FacultyMembersItem
        {
            public int Id { get; set; }
            public int AcademicID { get; set; }
            public string Name { get; set; }           
            public string Role { get; set; }


        }


    }
}
