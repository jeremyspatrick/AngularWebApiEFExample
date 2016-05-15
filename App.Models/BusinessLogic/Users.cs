using SingleApp.Models.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SingleApp.Models
{
    public static class Users
    {
        public static User GetByFirstName(string firstName)
        {
            var context = new SingleAppContextCustom();
            var user = (from u in context.Users
                        //where u.FirstName == firstName
                        select u).FirstOrDefault();

            return user;

        }

        public static User GetUserById(int id)
        {
            AdoHelper.ConnectionString = "Data Source=HOME-PC;Initial Catalog=SingleApp;Integrated Security=True;MultipleActiveResultSets=True";
            User user = null;
            using (var adoHelper = new AdoHelper()) {

                DataSet ds = adoHelper.ExecDataSetProc("Select_UserById", adoHelper.Parameter("@user_id", id, ParameterDirection.Input));
                DataTable dt = ds.Tables[0];

                foreach (DataRow row in dt.Rows)
                {
                    user = new User();
                    int userId = 0;
                    int.TryParse(row["UserId"].ToString(), out userId);
                    user.UserId = userId;

                   // user.FirstName = row["FirstName"].ToString();
                   // user.LastName = row["FirstName"].ToString();
                }
                
            }

            
            

            return user;
        }
    }
}