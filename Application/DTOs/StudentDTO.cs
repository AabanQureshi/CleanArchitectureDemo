using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public sealed record StudentDTO
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required DateOnly DateOfBirth { get; set; }

        public required List<Guid> CourseIds { get; set; } = new List<Guid>();

    }
}
