namespace KeelteKoolV2.Models.Lecturers;

public class LecturerCreateUpdateViewModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Qualifications { get; set; } = string.Empty;
    public string? UserID { get; set; }
}
