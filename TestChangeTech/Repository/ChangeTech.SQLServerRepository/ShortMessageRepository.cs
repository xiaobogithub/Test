using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;
using System.Data;

namespace ChangeTech.SQLServerRepository
{
    public class ShortMessageRepository : RepositoryBase, IShortMessageRepository
    {
        #region IShortMessageRepository Members

        public void AddShortMessageRepository(ShortMessageQueue message)
        {
            using (DynamicObjectContext context = new DynamicObjectContext(DefaultEntitiesConnectionString, DefaultEntitiesContainerName))
            {
                try
                {
                    InsertEntity(message, context);
                }
                catch (UpdateException ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public void AddMessage(ShortMessage message)
        {
            InsertEntity(message);
        }

        public IQueryable<ShortMessageQueue> GetShortMessageShouldSend()
        {
            using (DynamicObjectContext context = new DynamicObjectContext(DefaultEntitiesConnectionString, DefaultEntitiesContainerName))
            {
                try
                {
                    return GetEntities<ShortMessageQueue>(context).Where(s => s.IsSent.HasValue == false || s.IsSent.Value == false);
                }
                catch (UpdateException ex)
                {
                    Console.WriteLine(ex.ToString());
                    throw;
                }
            }
        }

        public List<ShortMessageQueue> GetShortMessageListShouldSend(DateTime time)
        {
            using (DynamicObjectContext context = new DynamicObjectContext(DefaultEntitiesConnectionString, DefaultEntitiesContainerName))
            {
                try
                {
                    DateTime beginDate = time.Date;
                    DateTime endDate = time.Date.AddDays(1);
                    IQueryable<ShortMessageQueue>entityList = GetEntities<ShortMessageQueue>(context).Where(s => s.IsSent.HasValue == false || s.IsSent.Value == false);
                    IQueryable<ShortMessageQueue> smsList = entityList.Where(sm => sm.Date.HasValue && sm.Time.HasValue && (sm.Date.Value >= beginDate && sm.Date.Value<endDate) && ((time.Hour * 60) + time.Minute) >= sm.Time.Value).Where(sm => sm.Program.ProgramUser.Where(pu => pu.User.UserGUID == sm.UserGUID && (!pu.IsSMSToEmail.HasValue || pu.IsSMSToEmail.HasValue && pu.IsSMSToEmail.Value == false)).Count() > 0);
                    return smsList.ToList();
                }
                catch (UpdateException ex)
                {
                    Console.WriteLine(ex.ToString());
                    throw;
                }
            }
        }

        public List<ShortMessageQueue> GetEmailListShouldSend(DateTime time)
        {
            using (DynamicObjectContext context = new DynamicObjectContext(DefaultEntitiesConnectionString, DefaultEntitiesContainerName))
            {
                try
                {
                    DateTime beginDate = time.Date;
                    DateTime endDate = time.Date.AddDays(1);
                    IQueryable<ShortMessageQueue> entityList = GetEntities<ShortMessageQueue>(context).Where(s => s.IsSent.HasValue == false || s.IsSent.Value == false);
                    IQueryable<ShortMessageQueue> emailList = entityList.Where(sm => sm.Date.HasValue && sm.Time.HasValue && (sm.Date.Value >= beginDate && sm.Date.Value < endDate) && ((time.Hour * 60) + time.Minute) >= sm.Time.Value).Where(sm => sm.Program.ProgramUser.Where(pu => pu.User.UserGUID == sm.UserGUID && (pu.IsSMSToEmail.HasValue && pu.IsSMSToEmail.Value == true)).Count() > 0);
                    return emailList.ToList();
                }
                catch (UpdateException ex)
                {
                    Console.WriteLine(ex.ToString());
                    throw;
                }
            }
        }

        public ShortMessage GetMessageByProgramAndType(Guid programGuid, string type)
        {
            return GetEntities<ShortMessage>(s => s.Program.ProgramGUID == programGuid && s.Type == type).FirstOrDefault();
        }

        public void Update(ShortMessage message)
        {
            UpdateEntity(message);
        }

        public void UpdateMessageQueue(ShortMessageQueue queue)
        {
            using (DynamicObjectContext context = new DynamicObjectContext(DefaultEntitiesConnectionString, DefaultEntitiesContainerName))
            {
                try
                {
                    UpdateEntity(queue, context);
                }
                catch (UpdateException ex)
                {
                    Console.WriteLine(ex.ToString());
                    throw;
                }
            }
        }

        public IQueryable<ShortMessageQueue> GetShortMessageQueueByProgram(Guid programGuid)
        {
            return GetEntities<ShortMessageQueue>(s =>s.ProgramGUID == programGuid);
        }

        public void AddSMSRecord(SMSRecord record)
        {
            InsertEntity(record);
        }

        public List<ProgramDailySMS> GetProgramDailySMSList(Guid programGuid)
        {
            return GetEntities<ProgramDailySMS>(p => p.Session.Program.ProgramGUID == programGuid && (p.IsDeleted.HasValue ? p.IsDeleted == false : true) && (p.Session.IsDeleted.HasValue ? p.Session.IsDeleted == false : true) && (p.Session.Program.IsDeleted.HasValue ? p.Session.Program.IsDeleted == false : true)).ToList();
        }

        public void UpdateProgramDailySMS(ProgramDailySMS dailySMSEntity)
        {
            UpdateEntity(dailySMSEntity);
        }

        public ProgramDailySMS GetProgramDailySMS(Guid programDailySMSGuid)
        {
            return GetEntities<ProgramDailySMS>(p => p.ProgramDailySMSGUID == programDailySMSGuid && (p.IsDeleted.HasValue ? p.IsDeleted == false : true)).FirstOrDefault();
        }

        public ProgramDailySMS GetProgramDailySMSBySessionGuid(Guid sessionGuid)
        {
            return GetEntities<ProgramDailySMS>(p => p.SessionGUID == sessionGuid && (p.IsDeleted.HasValue ? p.IsDeleted == false : true) && (p.Session.IsDeleted.HasValue ? p.Session.IsDeleted == false : true) && (p.Session.Program.IsDeleted.HasValue ? p.Session.Program.IsDeleted == false : true)).FirstOrDefault();
        }

        public void AddProgramDailySMS(ProgramDailySMS dailySMS)
        {
            InsertEntity(dailySMS);
        }



        #endregion
    }
}
