using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfdemo0403.classes
{
    public abstract class AppUser
    {
        public int Id { get; set; }
        public string Fio { get; set; }
        public string Login { get; set; }
        public abstract string RoleName { get; }
        public abstract bool CanEditData { get; } 
    }

    public class AdminUser : AppUser
    {
        public override string RoleName => "Администратор";
        public override bool CanEditData => true;
    }

    public class ManagerUser : AppUser
    {
        public override string RoleName => "Менеджер";
        public override bool CanEditData => true;
    }

    public class ClientUser : AppUser
    {
        public override string RoleName => "Клиент";
        public override bool CanEditData => false;
    }

    public class GuestUser : AppUser
    {
        public override string RoleName => "Гость";
        public override bool CanEditData => false;

        public GuestUser()
        {
            Fio = "Неизвестно";
        }
    }

    
    public static class AppSession
    {
        public static AppUser CurrentUser { get; set; }
        public static void Clear()
        {
            CurrentUser = null;
        }
    }

}
