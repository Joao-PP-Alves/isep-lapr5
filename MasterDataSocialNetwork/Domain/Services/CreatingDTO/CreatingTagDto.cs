using DDDNetCore.Domain.Users;

namespace DDDNetCore.Domain.Services.CreatingDTO
{
    public class CreatingTagDto
    {
        public Name name { get; set; }

        public CreatingTagDto(Name name)
        {
            this.name = name;
        }
    }
}