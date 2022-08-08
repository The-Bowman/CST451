namespace CST451.Data.DataModel.UserDataModels
{
    public class CustomerDataModel
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
        public string Password { get; set; }
        public bool Success { get; set; }

        public CustomerDataModel()
        {
        }

        public CustomerDataModel(int? iD, string name, string address, string city, string state, int? zip, string country, string email, string phone, string password)
        {
            ID = iD;
            Name = name;
            Address = address;
            City = city;
            State = state;
            Zip = zip;
            Country = country;
            Email = email;
            Phone = phone;
            Password = password;
        }
    }
}
