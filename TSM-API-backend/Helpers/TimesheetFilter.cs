using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WebApi.Entities;

namespace WebApi.Helpers
{
    public class TimesheetFilter
    {

        public static string CreateQueryString(TimesheetObj timesheetObj)
        {
            string firstPart = "SELECT Comments, WorkedHours,ProjectName,Username,Date,Timesheet.IdTimesheet,StartTime,EndTime," +
                                "BreakTime,Locations.LocationName,Timesheet.IdLocation,TimesheetActivity.IdProject,IdTimesheetActivity FROM TimesheetActivity " +
                                  "INNER JOIN Timesheet on TimesheetActivity.IdTimesheet = Timesheet.IdTimesheet " +
                                  "INNER JOIN Projects on TimesheetActivity.IdProject = Projects.IdProject " +
                                  "INNER JOIN Users on Timesheet.IdUser = Users.IdUser "+
                                  "INNER JOIN Locations on Timesheet.IdLocation = Locations.IdLocation";

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

                List<TimesheetView> timesheetViews = new List<TimesheetView>();
                
                

                using (SqlCommand command = new SqlCommand(CreateQueryString(timesheetObj), con))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                       TimesheetActivityView timesheetActivityView= new TimesheetActivityView();

                        string Comments = reader.GetString(0);
                        TimeSpan WorkedHours = reader.GetTimeSpan(1);
                        string ProjectName = reader.GetString(2);
                        string Username = reader.GetString(3);
                        DateTime Date = reader.GetDateTime(4);
                        int IdTimesheet = reader.GetInt32(5);
                        DateTime StartTime = reader.GetDateTime(6);
                        DateTime EndTime = reader.GetDateTime(7);
                        TimeSpan BreakTime = reader.GetTimeSpan(8);
                        string Location = reader.GetString(9);
                        int IdLocation = reader.GetInt32(10);
                        int IdProject = reader.GetInt32(11);
                        int IdTimesheetActivity = reader.GetInt32(12);


                        if (!timesheetViews.Exists(t => t.IdTimesheet == IdTimesheet))
                        {
                            timesheetViews.Add(new TimesheetView()
                            {
                                IdTimesheet = IdTimesheet,
                                Username = Username,
                                Date = Date,
                                StartTime = StartTime,
                                EndTime = EndTime,
                                BreakTime = BreakTime,
                                Location = Location,
                                IdLocation = IdLocation,
                                TimesheetActivities = new List<TimesheetActivityView>()
                            }) ;
                        }

                        timesheetActivityView.IdTimesheetActivity = IdTimesheetActivity;
                        timesheetActivityView.IdTimesheet = IdTimesheet;
                        timesheetActivityView.IdProject = IdProject;
                        timesheetActivityView.ProjectName = ProjectName;
                        timesheetActivityView.WorkedHours = WorkedHours;
                        timesheetActivityView.Comments = Comments;

                        timesheetViews.Find(t => t.IdTimesheet == IdTimesheet).TimesheetActivities.Add(timesheetActivityView);
                    }
                }

                con.Close();

                return timesheetViews;

            }
        }
    }
}
