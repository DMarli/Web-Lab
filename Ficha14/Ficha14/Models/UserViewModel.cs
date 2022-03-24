namespace Ficha14.Models
{
    public class UserViewModel
    {
        //igual ao User, menos a password, porque não a queremos enviar para a view
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
    }
} 