using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Helpers
{
    public class TimesheetFilter
    {
        public static string CreateQueryString(TimesheetObj timesheetObj)
        {
            string firstPart = "SELECT Comments, WorkedHours,ProjectName,Username,Date FROM TimesheetActivity " +
                                  "INNER JOIN Timesheet on TimesheetActivity.IdTimesheet = Timesheet.IdTimesheet " +
                                  "INNER JOIN Projects on TimesheetActivity.IdProject = Projects.IdProject " +
                                  "INNER JOIN Users on Timesheet.IdUser = Users.IdUser ";

            string secondPart = "";


            if (timesheetObj.IdManager != -1)
            {
                firstPart += "INNER JOIN ProjectManagers on Projects.IdProject = ProjectManagers.IdProject WHERE ";
                secondPart += " AND ProjectManagers.IdUser=" + timesheetObj.IdManager.ToString();
            }
            else
            {
                firstPart += " WHERE ";
            }

            if (timesheetObj.Date != "nullDate")
            {
                secondPart += " AND  Timesheet.Date=" + "'" + timesheetObj.Date + "' ";
            }

            if (timesheetObj.IdUser != -1)
            {
                secondPart += " AND Users.IdUser=" + timesheetObj.IdUser.ToString();
            }

            if (timesheetObj.IdProject != -1)
            {
                secondPart += " AND TimesheetActivity.IdProject=" + timesheetObj.IdProject.ToString();
            }

            if (timesheetObj.IdTeam != -1)
            {
                secondPart += " AND Users.IdTeam=" + timesheetObj.IdTeam.ToString();
            }

            

            return firstPart + secondPart.Substring(5);
        }
        public static IList<TimesheetView> GetFilteredTimesheet(TimesheetObj timesheetObj)
        {
            using (SqlConnection con = new SqlConnection("Server=DESKTOP-RPNBQ1M;Integrated Security=true;Database=TSM;"))
            {
                con.Open();
                IList<TimesheetView> timesheetViews = new List<TimesheetView>();

                using (SqlCommand command = new SqlCommand(CreateQueryString(timesheetObj), con))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string Comments = reader.GetString(0);
                        TimeSpan WorkedHours = reader.GetTimeSpan(1);
                        string ProjectName = reader.GetString(2);
                        string Username = reader.GetString(3);
                        DateTime Date = reader.GetDateTime(4);
                        timesheetViews.Add(new TimesheetView() { Comments = Comments, WorkedHours = WorkedHours, Project = ProjectName,Username=Username, Date=Date});
                    }
                }

                con.Close();

                return timesheetViews;

            }
        }
    }
}
