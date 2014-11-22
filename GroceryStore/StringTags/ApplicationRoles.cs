using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

namespace GroceryStore.StringTags
{
    public static class ApplicationRoles
    {
        public static class User
        {
            //read
            public const string R = "user_r";
            //write / create / update
            public const string RW = "user_rw";
            //delete
            public const string RWD = "user_rwd";
        }

        public static class Inventory
        {
            //read
            public const string R = "inven_r";
            //write / create / update
            public const string RW = "inven_rw";
            //delete
            public const string RWD = "inven_rwd";
        }

        public static string[] GetAllRoles()
        {
            string[] roles = { 
                             User.R,
                             User.RW,
                             User.RWD,
                             Inventory.R,
                             Inventory.RW,
                             Inventory.RWD
                             };

            return roles;
        }
    }
}