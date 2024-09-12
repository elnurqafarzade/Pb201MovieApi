namespace PB201Initial.DTOs.StudentDtos
{
    public record StudentCreateDto(string fullName, double grade, int groupId, bool isDeleted);
    }
