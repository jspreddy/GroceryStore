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
            public static string R = "user_r";
            //write / create / update
            public static string W = "user_w";
            //delete
            public static string D = "user_d";
        }

        public static class Inventory
        {
            //read
            public static string R = "inven_r";
            //write / create / update
            public static string W = "inven_w";
            //delete
            public static string D = "inven_d";
        }

        public static string[] GetAllRoles()
        {
            string[] roles = { 
                             User.R,
                             User.W,
                             User.D,
                             Inventory.R,
                             Inventory.W,
                             Inventory.D
                             };

            return roles;
        }
    }
}