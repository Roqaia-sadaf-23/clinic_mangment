namespace Clinic_Domain.Entities;

public class Person
{
    protected Person()
    {
    }
    protected Person(string firstName, string lastName,
        string nationalityNo, int? phoneNumber, int? age,
        string? address, byte? gender, 
        int nationalityCountryId, string? imagePath, string? note)
    {

        FirstName = firstName;
        LastName = lastName;
        NationalityNo = nationalityNo;
        PhoneNumber = phoneNumber;
        Age = age;
        Address = address;
        Gender = gender;
        NationalityCountryId = nationalityCountryId;
        ImagePath = imagePath;
        Note = note;

    }

    public int ID { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string NationalityNo { get; set; } = null!;

    public int? PhoneNumber { get; set; }
    public int? Age { get; set; }
    public string? Address { get; set; }

    public byte? Gender { get; set; }
    //1=Male 2=Female
    public int NationalityCountryId { get; set; }
    //   public Country Country { get; set; }
    public string? ImagePath { get; set; }

    public string? Note { get; set; }

    //  public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    public Doctor Doctor { get; set; }
    public virtual Country NationalityCountry { get; set; } = null!;

    //  public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();

    public virtual User? User { get; set; }

    public Patient Patient { get; set; }




    public static Person Create(string firstName, string lastName
        , string nationalityno, int? phoneNumber, int? age, string address,
        byte? gender, int nationalityCountryId, string? imagePath, string? note)
    {
        return new Person(firstName, lastName, nationalityno, phoneNumber, age, address, gender, nationalityCountryId, imagePath, note);


    }


    public void Update(string firstName, string lastName
        , string nationalityno, int? phoneNumber, int? age, string address,
        byte? gender, int nationalityCountryId, string? imagePath, string? note)
    {

        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("firstName is required.");
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("lastName is required.");
        if (string.IsNullOrWhiteSpace(nationalityno))
            throw new ArgumentException("nationalityno is required.");
        if (nationalityCountryId <= 0)
            throw new ArgumentException("nationalityCountryId must be greater than 0.");
        if (age <= 0) throw new ArgumentException("age must be greater than 0.");


        FirstName = firstName;
        LastName = lastName;
        NationalityNo = nationalityno;
        PhoneNumber = phoneNumber;
        Age = age;
        Address = address;
        Gender = gender;
        NationalityCountryId = nationalityCountryId;
        ImagePath = imagePath;
        Note = note;
    }

}