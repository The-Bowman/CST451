namespace CST451.Data.DataModel.UserDataModels
{
    public class EmployeeDataModel
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int? Zip { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool? IsAdmin { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public bool Success { get; set; }

        public EmployeeDataModel()
        {
        }

        public EmployeeDataModel(int? employeeID, string name, string address, string city, string state, int? zip, string country, string email, string phone, bool? isAdmin, string username, string password)
        {
            ID = employeeID;
            Name = name;
            Address = address;
            City = city;
            State = state;
            Zip = zip;
            Country = country;
            Email = email;
            Phone = phone;
            IsAdmin = isAdmin;
            Username = username;
            Password = password;
        }
    }
}
