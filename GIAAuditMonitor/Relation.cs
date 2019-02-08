using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace GIAAuditMonitor
{
    public class Relation
    {
        public string OctopusRelease;
        public string TeamCityBuild;
        public string GitHubBranch;
        public string JIRATicket;

        public static Relation GetInformation(string contents)
        {
            //data mine contents and put things into the object
            Relation relation = new Relation();

            int checkingLocation = contents.IndexOf("Checking package cache for package");
            int endCheckingLocation = contents.IndexOf("\"", checkingLocation);
            string tmp = contents.Substring(checkingLocation, endCheckingLocation - checkingLocation);
            int lastDash = tmp.LastIndexOf("-");
            tmp = tmp.Substring(lastDash + 1);
            relation.TeamCityBuild = tmp;

            checkingLocation = contents.IndexOf("Deploy WebACE Web release ");
            endCheckingLocation = contents.IndexOf(" ", checkingLocation + "Deploy WebACE Web release ".Length);
            tmp = contents.Substring(checkingLocation + "Deploy WebACE Web release ".Length, endCheckingLocation - checkingLocation - "Deploy WebACE Web release ".Length);
            if (checkingLocation > -1)
            {
                relation.OctopusRelease = tmp;
            }

            return relation;
        }

        public static string GetBranchName(string teamcityResponse)
        {
            int checkingLocation = teamcityResponse.IndexOf("branchName=");
            int endCheckingLocation = teamcityResponse.IndexOf("\"", checkingLocation + "branchName=".Length + 1);
            
            if (checkingLocation > -1)
            {
                string tmp = teamcityResponse.Substring(checkingLocation + "branchName=".Length + 1, endCheckingLocation - checkingLocation - ("branchName=".Length + 1));
                return tmp;
            }

            return "";
        }
    }

}