using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Services.Client;
using System.Data.Services.Common;

namespace ChangeTech.Models
{
    public class ActivityLogModel
    {
        public Guid ActivityLogGuid { get; set; }
        public LogTypeEnum Type { get; set; }
        public UserModel User { get; set; }
        public ProgramModel Program { get; set; }
        public SessionModel Session { get; set; }
        public PageSequenceModel PageSequence { get; set; }
        public Guid PageGuid { get; set; }
        public DateTime ActivityDateTime { get; set; }
        public string Message { get; set; }
        public string From { get; set; }
    }

    public class ActivityMonitorEmailLogModel
    {
        public Guid ActivityMonitorEmailLogGuid { get; set; }
        public LogTypeEnum Type { get; set; }
        public UserModel User { get; set; }
        public ProgramModel Program { get; set; }
        public SessionModel Session { get; set; }
        public PageSequenceModel PageSequence { get; set; }
        public Guid PageGuid { get; set; }
        public DateTime ActivityDateTime { get; set; }
        public string Message { get; set; }
        public string From { get; set; }
    }

    public class InsertLogModel
    {
        public int ActivityLogType { get; set; }
        public Guid UserGuid { get; set; }
        public Guid ProgramGuid { get; set; }
        public Guid SessionGuid { get; set; }
        public Guid PageSequenceGuid { get; set; }
        public Guid PageGuid { get; set; }
        public string Browser { get; set; }
        public string IP { get; set; }
        public string Message { get; set; }
        public string From { get; set; }
    }

    public class InsertMonitorEmailLogModel
    {
        public int ActivityLogType { get; set; }
        public Guid UserGuid { get; set; }
        public Guid ProgramGuid { get; set; }
        public Guid SessionGuid { get; set; }
        public Guid PageSequenceGuid { get; set; }
        public Guid PageGuid { get; set; }
        public string Browser { get; set; }
        public string IP { get; set; }
        public string Message { get; set; }
        public string From { get; set; }
    }

    [DataServiceKey("ProgramGuid", "ActivityLogType")]
    public class ActivityLogModelOnAzure
    {
        public int ActivityLogType { get; set; }
        public Guid ActivityLogGUID { get; set; }
        public Guid UserGuid { get; set; }
        public string Email { get; set; }
        public Guid ProgramGuid { get; set; }
        public string ProgramName { get; set; }
        public Guid SessionGuid { get; set; }
        public string SessionName { get; set; }
        public Guid PageSequenceGuid { get; set; }
        public Guid PageGuid { get; set; }
        public string Browser { get; set; }
        public string IP { get; set; }
        public string Message { get; set; }
        public string From { get; set; }
        public DateTime ActivityDateTime { get; set; }
    }

    public class ActivityLogModels : List<ActivityLogModel>
    { }

    public class ActivityLogTypeModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ActivityLogPriorityModel LogPriority { get; set; }
    }

    public class ActivityLogTypeModels : List<ActivityLogTypeModel>
    {
    }

    public class ActivityLogPriorityModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class ActivityLogPriorityModels : List<ActivityLogPriorityModel>
    {
    }
}
