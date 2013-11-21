using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ChangeTech.Contracts;
using ChangeTech.IDataRepository;
using Ethos.DependencyInjection;
using ChangeTech.Entities;
using ChangeTech.Models;
using System.Data.Objects.DataClasses;

namespace ChangeTech.Services
{
    class ProgramScheduleService : ServiceBase, IProgramScheduleService
    {
        #region CloneProgram related
        public ProgramSchedule CloneProgramSchedule(ProgramSchedule schedule)
        {
            try
            {
                ProgramSchedule clonedSchedule = new ProgramSchedule
                {
                    Week = schedule.Week,
                    WeekDay = schedule.WeekDay,
                    ParentProgramScheduleGUID = schedule.ID,
                    DefaultGUID = schedule.DefaultGUID
                };

                return clonedSchedule;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public ProgramSchedule SetDefaultGuidForProgramSchedule(ProgramSchedule needSetDefaultEntity, CloneProgramParameterModel cloneParameterModel)
        {
            ProgramSchedule newEntity = new ProgramSchedule();
            
            newEntity = needSetDefaultEntity;
            switch (cloneParameterModel.source)
            {
                case DefaultGuidSourceEnum.FromDefaultGuid:
                    //newEntity.DefaultGUID = needSetDefaultEntity.DefaultGUID;//This is default value, don't need assign again.
                    break;
                case DefaultGuidSourceEnum.FromMatchDefaultGuidFunction:
                    bool isMatchDefaultGuidSuccessful = false;//whether default guid can be matched, if not ,set null.
                    int fromProgramScheduleID = newEntity.ParentProgramScheduleGUID == null ? int.MinValue : (int)newEntity.ParentProgramScheduleGUID;
                    ProgramSchedule fromProgramScheduleEntity = Resolve<IProgramScheduleRepository>().GetProgramScheduleByProgramScheduleGuid(fromProgramScheduleID);

                    if (fromProgramScheduleEntity != null)
                    {
                        if (!fromProgramScheduleEntity.ProgramReference.IsLoaded) fromProgramScheduleEntity.ProgramReference.Load();
                        Program fromProgramInDefaultLanguage = new Program();
                        if (fromProgramScheduleEntity.Program.DefaultGUID.HasValue)//This fromProgram is not default program
                        {
                            fromProgramInDefaultLanguage = Resolve<IProgramRepository>().GetProgramByGuid((Guid)fromProgramScheduleEntity.Program.DefaultGUID);
                        }
                        else//This fromProgram is default program. Or this is wrong data for old Data Version or some other reason.
                        {
                            fromProgramInDefaultLanguage = Resolve<IProgramRepository>().GetProgramByGuid(fromProgramScheduleEntity.Program.ProgramGUID);
                        }
                        if (fromProgramInDefaultLanguage != null)
                        {
                            Program toProgramInDefaultLanguage = Resolve<IProgramRepository>().GetProgramByGuid(cloneParameterModel.ProgramGuidOfCopiedToProgramsInDefaultLanguage);
                            if (toProgramInDefaultLanguage != null)
                            {
                                if (toProgramInDefaultLanguage.ParentProgramGUID == fromProgramInDefaultLanguage.ProgramGUID)//Match Successful. the toProgram's parentguid == fromDefaultProgram's guid
                                {
                                    isMatchDefaultGuidSuccessful = true;
                                }
                                else
                                {
                                    List<Program> fromProgramMatchedList = Resolve<IProgramRepository>().GetProgramByDefaultGUID(fromProgramInDefaultLanguage.ProgramGUID).Where(p => p.ProgramGUID == toProgramInDefaultLanguage.ParentProgramGUID).ToList();
                                    if (fromProgramMatchedList.Count > 0)//Match Successful. the toProgram's parent guid is fromProgram's guid which program belongs to the fromDefaultProgram but not the default language.
                                    {
                                        isMatchDefaultGuidSuccessful = true;
                                    }
                                }
                            }

                            //Set Default Guid if match successful
                            if (isMatchDefaultGuidSuccessful)
                            {
                                try
                                {
                                    ProgramSchedule toDefaultProgramSchedule = Resolve<IProgramScheduleRepository>().GetProgramScheduleByProgramWeekWeekday(toProgramInDefaultLanguage.ProgramGUID, (int)fromProgramScheduleEntity.Week, (int)fromProgramScheduleEntity.WeekDay);
                                    newEntity.DefaultGUID = toDefaultProgramSchedule.ID;
                                }
                                catch(Exception ex)
                                {
                                    Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                                    isMatchDefaultGuidSuccessful = false;
                                }
                            }
                        }
                    }
                    //else//don't has parent guid ,so can't match,set the default guid =>null
                    //{
                    //    newEntity.DefaultGUID = null;
                    //}

                    //Can't match. Set default guid =>null.
                    if (!isMatchDefaultGuidSuccessful)
                    {
                        newEntity.DefaultGUID = null;
                    }
                    break;
                case DefaultGuidSourceEnum.FromNull:
                    newEntity.DefaultGUID = null;
                    break;
                case DefaultGuidSourceEnum.FromPrimaryKey:
                    newEntity.DefaultGUID = needSetDefaultEntity.ParentProgramScheduleGUID;
                    break;
                default:
                    throw new Exception("Error: Dont't find the DefaultGuidSource for ProgramSchedule Entity.");
                    //break;
            }

            return newEntity;
        }
        #endregion


        public List<ProgramScheduleModel> GetProgramScheduleByProgram(Guid programGuid)
        {
            List<ProgramScheduleModel> scheduleModelList = new List<ProgramScheduleModel>();
            List<ProgramSchedule> schedulelList = Resolve<IProgramScheduleRepository>().GetProgramSchedule(programGuid).ToList();
            foreach (ProgramSchedule schedule in schedulelList)
            {
                ProgramScheduleModel scheduleModel = scheduleModelList.Where(s => s.week == schedule.Week).FirstOrDefault();
                if (scheduleModel != null)
                {
                    scheduleModel = InitialSchedule(schedule, scheduleModel);
                }
                else
                {
                    scheduleModel = new ProgramScheduleModel();
                    scheduleModelList.Add(InitialSchedule(schedule, scheduleModel));
                }
            }

            return scheduleModelList;
        }

        public ProgramScheduleModel GetProgramScheduleByProgramAndWeek(Guid programGuid, int week)
        {
            List<ProgramSchedule> schedulelist = Resolve<IProgramScheduleRepository>().GetProgramScheduleAndWeek(programGuid, week).ToList();
            ProgramScheduleModel model = new ProgramScheduleModel();
            foreach (ProgramSchedule schedule in schedulelist)
            {
                model = InitialSchedule(schedule, model);
            }

            return model;
        }

        public void SaveProgramSchedule(ProgramScheduleModel model, Guid programGuid)
        {
            //Delete program schedule 
            Resolve<IProgramRepository>().DeleteProgramSchedule(programGuid, model.week);

            // add new program schedule
            Program program = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            if (!program.ProgramSchedule.IsLoaded)
            {
                program.ProgramSchedule.Load();
            }
            if (program.ProgramSchedule == null)
            {
                program.ProgramSchedule = new EntityCollection<ProgramSchedule>();
            }
            if (model.monday)
            {
                program.ProgramSchedule.Add(new ProgramSchedule
                {
                    Week = model.week,
                    WeekDay = 1
                });
            }
            if (model.tuesday)
            {
                program.ProgramSchedule.Add(new ProgramSchedule
                {
                    Week = model.week,
                    WeekDay = 2
                });
            }
            if (model.wednesday)
            {
                program.ProgramSchedule.Add(new ProgramSchedule
                {
                    Week = model.week,
                    WeekDay = 3
                });
            }
            if (model.thursday)
            {
                program.ProgramSchedule.Add(new ProgramSchedule
                {
                    Week = model.week,
                    WeekDay = 4
                });
            }
            if (model.friday)
            {
                program.ProgramSchedule.Add(new ProgramSchedule
                {
                    Week = model.week,
                    WeekDay = 5
                });
            }
            if (model.saterday)
            {
                program.ProgramSchedule.Add(new ProgramSchedule
                {
                    Week = model.week,
                    WeekDay = 6
                });
            }
            if (model.sunday)
            {
                program.ProgramSchedule.Add(new ProgramSchedule
                {
                    Week = model.week,
                    WeekDay = 7
                });
            }

            Resolve<IProgramRepository>().Update(program);
        }

        private ProgramScheduleModel InitialSchedule(ProgramSchedule schedule, ProgramScheduleModel scheduleModel)
        {
            switch (schedule.WeekDay)
            {
                case 7:
                    scheduleModel.sunday = true;
                    break;
                case 1:
                    scheduleModel.monday = true;
                    break;
                case 2:
                    scheduleModel.tuesday = true;
                    break;
                case 3:
                    scheduleModel.wednesday = true;
                    break;
                case 4:
                    scheduleModel.thursday = true;
                    break;
                case 5:
                    scheduleModel.friday = true;
                    break;
                case 6:
                    scheduleModel.saterday = true;
                    break;
            }
            scheduleModel.week = schedule.Week.Value;

            return scheduleModel;
        }
    }
}
