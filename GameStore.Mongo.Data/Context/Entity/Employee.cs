namespace GameStore.Mongo.Data.Context.Entity;

public class Employee: BaseEntity
{
    public required int EmployeeID { get; set; }
    public required string LastName { get; set; }
    public required string FirstName { get; set; }
    public required string Title { get; set; }
    public required string TitleOfCourtesy { get; set; }
    public required DateTime BirthDate { get; set; }
    public required DateTime HireDate { get; set; }
    public required string Address { get; set; }
    public required string City { get; set; }
    public required string Region { get; set; }
    public object PostalCode { get; set; }
    public required string Country { get; set; }
    public required string HomePhone { get; set; }
    public required string Extension { get; set; }
    public required string Photo { get; set; }
    public required string Notes { get; set; }
    public required string ReportsTo { get; set; }
    public required string PhotoPath { get; set; }
}
