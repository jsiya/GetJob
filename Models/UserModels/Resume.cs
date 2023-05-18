namespace GetJob.Models.UserModels;
public class Resume
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public string Profession { get; set; }
    public string UserName { get; set; }
    public string LinkedIn { get; set; }
    public List<string> Education { get; set; }
    public List<string> Skills { get; set; }
    public int ViewCount { get; set; }
    public bool Showable { get; set; }
    public bool IsActive { get; set; }
    public List<KeyValuePair<string, LanguageLevelInfo>> Languages { get; set; }
    public List<string> Certificates { get; set; }
    //Experience ilk pair sirket ve vezifeni, ikinci pair baslama ve bitme tarixi
    public List<KeyValuePair<KeyValuePair<string, string>, KeyValuePair<string, string>>> Experiences { get; set; }
    public Resume() { }
    public Resume(Guid employeeId, string profession, string userName, string linkedIn, List<string> education, List<string> skills, List<KeyValuePair<string, LanguageLevelInfo>> languages, List<string> certificates, List<KeyValuePair<KeyValuePair<string, string>, KeyValuePair<string, string>>> experiences)
    {
        Id = Guid.NewGuid();
        EmployeeId = employeeId;
        Profession = profession;
        UserName = userName;
        LinkedIn = linkedIn;
        Education = education;
        Skills = skills;
        Languages = languages;
        Certificates = certificates;
        Experiences = experiences;
        ViewCount = 0;
        Showable = false;
        IsActive = false;
    }

    //toString
    
    
}
