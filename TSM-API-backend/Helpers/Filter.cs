using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Helpers
{
    public class Filter
    {
        public static IList<TimesheetView> GetFilteredTimesheet(TimesheetObj timesheetObj)
        {
            using (SqlConnection con = new SqlConnection("Server=DESKTOP-RPNBQ1M;Integrated Security=true;Database=TSM;"))
            {
                con.Open();
                IList<TimesheetView> timesheetViews = new List<TimesheetView>();
                string select = "SELECT Comments, WorkedHours,ProjectName,Username,Date FROM TimesheetActivity ";
                string mainJoin = "INNER JOIN Timesheet on TimesheetActivity.IdTimesheet=Timesheet.IdTimesheet " +
                                  "INNER JOIN Projects on TimesheetActivity.IdProject = Projects.IdProject " +
                                  "INNER JOIN Users on Timesheet.IdUser = Users.IdUser ";
                string projManagersJoin = "INNER JOIN ProjectManagers on Projects.IdProject = ProjectManagers.IdProject ";
                string dateExists = " Timesheet.Date="+"'"+timesheetObj.Date+"' ";
                string projectExists = " TimesheetActivity.IdProject=" + timesheetObj.IdProject.ToString();
                string userExists = " Users.IdUser=" + timesheetObj.IdUser.ToString();
                string teamExists = " Users.IdTeam=" + timesheetObj.IdTeam.ToString();
                string managerExists = " ProjectManagers.IdUser=" + timesheetObj.IdManager.ToString();

                int v = 0;
                string query = select + mainJoin;

                if(timesheetObj.IdManager!=-1)
                {
                    query += projManagersJoin;
                }

                query += "WHERE ";

                if (timesheetObj.Date!="nullDate")
                {
                    v=1;             
                    query += dateExists;
                }

                if (timesheetObj.IdUser!=-1)
                {
                    if(v!=0)
                    {
                        query += " AND";
                    }
                    query += userExists;
                    v = 1;
                }

                if(timesheetObj.IdProject!=-1)
                {
                    if (v != 0)
                    {
                        query += " AND";
                    }
                    query += projectExists;
                    v = 1;
                }

                if(timesheetObj.IdTeam!=-1)
                {
                    if (v != 0)
                    {
                        query += " AND";
                    }
                    query += teamExists;
                    v = 1;
                }

                if(timesheetObj.IdManager!=-1)
                {
                    if (v != 0)
                    {
                        query += " AND";
                    }
                    query += managerExists;
                }

                using (SqlCommand command = new SqlCommand(query, con))
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
