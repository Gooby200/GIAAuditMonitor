using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class FolderHandler {
    public static void SetupDirectory() {
        if (!Directory.Exists(Manager.AUDIT_LOG_DIRECTORY)) {
            Directory.CreateDirectory(Path.GetDirectoryName(Manager.AUDIT_LOG_DIRECTORY));
        }
    }

    //read through any file modified today
    public static void ProcessRelations() {
        string todayAuditDirectory = Manager.AUDIT_LOG_DIRECTORY + DateTime.Now.ToString("yyyy-MM-dd") + @"\";
        //make sure we have an audit directory with today's date before we search
        if (!Directory.Exists(todayAuditDirectory)) {
            Directory.CreateDirectory(todayAuditDirectory);
        }

        DirectoryInfo dir = new DirectoryInfo(Manager.OCTOPUS_LOGS_DIRECTORY);
        List<FileInfo> octopusFiles = dir.GetFiles().Where(file => file.LastWriteTime.Date == DateTime.Now.Date).ToList();
        List<Relation> relations = new List<Relation>();
        foreach (var file in octopusFiles) {
            //check if this is a file that contains teamcity and octopus info
            //if it is, mine the file for info using the Relation object to capture the data
        }

        //go through the audit files we have for today
        //see which ones we are missing from the audit folder and lets get that list
        dir = new DirectoryInfo(todayAuditDirectory);
        List<FileInfo> auditFiles = dir.GetFiles().ToList();
        List<Relation> needToAudit = relations.Where(relation => !auditFiles.Select(file => file.FullName).ToList().Contains(relation.OctopusRelease)).ToList();

        //now that we have the files we need to pull info for, lets find their associated github branch info
        
        //use that list of relations and go fetch the github info and jira info
    }
}
