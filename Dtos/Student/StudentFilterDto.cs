using ClassManagement2.Dtos.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClassManagement2.Dtos.Student
{
    public class StudentFilterDto : FilterDto
    {
        [FromQuery(Name = "startAge")]
        public int? StartAge { get; set; }

        [FromQuery(Name = "endAge")]
        public int? EndAge { get; set; }
    }
}
