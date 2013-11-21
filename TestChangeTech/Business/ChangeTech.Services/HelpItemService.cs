using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using ChangeTech.Models;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.Services
{
    public class HelpItemService : ServiceBase, IHelpItemService
    {
        public void Update(HelpItemModel itemModel)
        {
            HelpItem item = Resolve<IHelpItemRepository>().GetItem(itemModel.HelpItemGUID);
            item.Question = itemModel.Question;
            item.Answer = itemModel.Answer;
            if (item.Order != itemModel.Order)
            {
                List<HelpItem> itemlist = Resolve<IHelpItemRepository>().GetItemByProgram(itemModel.ProgramGUID).ToList<HelpItem>();
                foreach (HelpItem hitem in itemlist)
                {
                    if (hitem.Order == itemModel.Order)
                    {
                        hitem.Order = item.Order;
                        Resolve<IHelpItemRepository>().Update(hitem);
                        break;
                    }
                }
                item.Order = itemModel.Order;
            }
            Resolve<IHelpItemRepository>().Update(item);
            Resolve<ITranslationJobService>().UpdateElementWhenFromUpdated("HelpItem", item.HelpItemGUID.ToString(), Guid.Empty);
        }

        public void Insert(HelpItemModel itemModel)
        {
            AdustItemListOrder(itemModel);          
            HelpItem item = new HelpItem
            {
                HelpItemGUID = Guid.NewGuid(),
                //Language = Resolve<ILanguageRepository>().GetLanguage(itemModel.LanguageGUID),
                Program = Resolve<IProgramRepository>().GetProgramByGuid(itemModel.ProgramGUID),
                Order = itemModel.Order,
                Question = itemModel.Question,
                Answer = itemModel.Answer
            };

            Resolve<IHelpItemRepository>().Insert(item);
        }     

        public void Delete(Guid itemGuid)
        {
            HelpItem item = Resolve<IHelpItemRepository>().GetItem(itemGuid);
            //if (!item.LanguageReference.IsLoaded)
            //{
            //    item.LanguageReference.Load();
            //}
            if (!item.ProgramReference.IsLoaded)
            {
                item.ProgramReference.Load();
            }

            List<HelpItem> itemList = Resolve<IHelpItemRepository>().GetItemByProgram(item.Program.ProgramGUID).ToList<HelpItem>();
            foreach (HelpItem hitem in itemList)
            {
                if (hitem.Order > item.Order)
                {
                    hitem.Order--;
                    Resolve<IHelpItemRepository>().Update(hitem);
                }
            }
            Resolve<IHelpItemRepository>().Delete(itemGuid);
        }

        public HelpItemModel GetHelpItemModel(Guid itemGuid)
        {
            HelpItemModel itemModel = new HelpItemModel();
            HelpItem item = Resolve<IHelpItemRepository>().GetItem(itemGuid);
            if (item != null)
            {
                //if (!item.LanguageReference.IsLoaded)
                //{
                //    item.LanguageReference.Load();
                //}
                if (!item.ProgramReference.IsLoaded)
                {
                    item.ProgramReference.Load();
                }
                itemModel.Answer = item.Answer;
                itemModel.Question = item.Question;
                itemModel.HelpItemGUID = item.HelpItemGUID;
                //itemModel.LanguageName = item.Language.Name;
                //itemModel.LanguageGUID = item.Language.LanguageGUID;
                itemModel.ProgramGUID = item.Program.ProgramGUID;
                itemModel.ProgramName = item.Program.Name;
                itemModel.Order = Convert.ToInt32(item.Order);
            }
            return itemModel;
        }

        public List<HelpItemModel> GetHelpItemModelList(Guid programGuid)
        {
            List<HelpItemModel> helpItemModelList = new List<HelpItemModel>();
            List<HelpItem> itemlist = Resolve<IHelpItemRepository>().GetItemByProgram(programGuid).ToList<HelpItem>();
            foreach (HelpItem item in itemlist)
            {
                HelpItemModel itemModel = new HelpItemModel
                {
                    Answer = item.Answer,
                    Question = item.Question,
                    Order = Convert.ToInt32(item.Order),
                    HelpItemGUID = item.HelpItemGUID
                    //LanguageGUID = languageGuid
                };
                helpItemModelList.Add(itemModel);
            }
            return helpItemModelList;
        }

        public void MakeHelpItemOrderUp(Guid programGuid, Guid helpItemGuid)
        {
            HelpItem adjustItem = Resolve<IHelpItemRepository>().GetItem(helpItemGuid);
            if (adjustItem.Order > 1)
            {
                List<HelpItem> itemList = Resolve<IHelpItemRepository>().GetItemByProgram(programGuid).ToList<HelpItem>();
                foreach (HelpItem item in itemList)
                {
                    if (item.Order == adjustItem.Order - 1)
                    {
                        item.Order++;
                        Resolve<IHelpItemRepository>().Update(item);
                        adjustItem.Order--;
                        Resolve<IHelpItemRepository>().Update(adjustItem);
                        break;
                    }
                }               
            }
        }

        public void MakeHelpItemOrderDown(Guid programGuid, Guid helpItemGuid)
        {
            HelpItem adjustItem = Resolve<IHelpItemRepository>().GetItem(helpItemGuid);
            
            if (!IsTheLastItem(programGuid,helpItemGuid))
            {
                List<HelpItem> itemList = Resolve<IHelpItemRepository>().GetItemByProgram(programGuid).ToList<HelpItem>();
                foreach (HelpItem item in itemList)
                {
                    if (item.Order == adjustItem.Order + 1)
                    {
                        item.Order--;
                        Resolve<IHelpItemRepository>().Update(item);
                        adjustItem.Order++;
                        Resolve<IHelpItemRepository>().Update(adjustItem);
                        break;
                    }
                }               
            }
        }

        public int GetHelpItemCount(Guid programGuid)
        {
            IQueryable<HelpItem> itemList = Resolve<IHelpItemRepository>().GetItemByProgram(programGuid);
            return itemList.Count();
        }

        private bool IsTheLastItem(Guid programGuid, Guid helpItemGuid)
        {
            bool flag = true;
            IQueryable<HelpItem> itemList = Resolve<IHelpItemRepository>().GetItemByProgram(programGuid);
            HelpItem adjustItem = Resolve<IHelpItemRepository>().GetItem(helpItemGuid);
            foreach (HelpItem item in itemList)
            {
                if (item.Order > adjustItem.Order)
                {
                    flag = false;
                    break;
                }
            }

            return flag;
        }

        private void AdustItemListOrder(HelpItemModel itemModel)
        {
            List<HelpItem> itemList = Resolve<IHelpItemRepository>().GetItemByProgram(itemModel.ProgramGUID).ToList<HelpItem>();
            foreach (HelpItem item in itemList)
            {
                if (item.Order >= itemModel.Order)
                {
                    item.Order++;
                    Resolve<IHelpItemRepository>().Update(item);
                }
            }
        }

        public HelpItem CloneHelpItem(HelpItem helpItem)
        {
            try
            {
                HelpItem cloneHelpItem = new HelpItem
                {
                    Answer = helpItem.Answer,
                    HelpItemGUID = Guid.NewGuid(),
                    Order = helpItem.Order,
                    Question = helpItem.Question,
                    ParentHelpItemGUID = helpItem.HelpItemGUID,
                    DefaultGUID = helpItem.DefaultGUID
                };
                return cloneHelpItem;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public HelpItem SetDefaultGuidForHelpItem(HelpItem needSetDefaultEntity, CloneProgramParameterModel cloneParameterModel)
        {
            HelpItem newEntity = new HelpItem();

            newEntity = needSetDefaultEntity;
            switch (cloneParameterModel.source)
            {
                case DefaultGuidSourceEnum.FromDefaultGuid:
                    //newEntity.DefaultGUID = needSetDefaultEntity.DefaultGUID;//This is default value, don't need assign again.
                    break;
                case DefaultGuidSourceEnum.FromMatchDefaultGuidFunction:
                    bool isMatchDefaultGuidSuccessful = false;//whether default guid can be matched, if not ,set null.
                    Guid fromHelpItemGuid = newEntity.ParentHelpItemGUID == null ? Guid.Empty : (Guid)newEntity.ParentHelpItemGUID;
                    HelpItem fromHelpItemEntity = Resolve<IHelpItemRepository>().GetItem(fromHelpItemGuid);
                    if (fromHelpItemEntity != null)
                    {
                        if (!fromHelpItemEntity.ProgramReference.IsLoaded) fromHelpItemEntity.ProgramReference.Load();
                        Program fromProgramInDefaultLanguage = new Program();
                        if (fromHelpItemEntity.Program.DefaultGUID.HasValue)//This fromProgram is not default program
                        {
                            fromProgramInDefaultLanguage = Resolve<IProgramRepository>().GetProgramByGuid((Guid)fromHelpItemEntity.Program.DefaultGUID);
                        }
                        else//This fromProgram is default program. Or this is wrong data for old Data Version or some other reason.
                        {
                            fromProgramInDefaultLanguage = Resolve<IProgramRepository>().GetProgramByGuid(fromHelpItemEntity.Program.ProgramGUID);
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
                                    HelpItem toHelpItem = Resolve<IHelpItemRepository>().GetItemByProgram(toProgramInDefaultLanguage.ProgramGUID).Where(h => h.Order == fromHelpItemEntity.Order).FirstOrDefault();
                                    newEntity.DefaultGUID = toHelpItem.HelpItemGUID;
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
                    newEntity.DefaultGUID = needSetDefaultEntity.ParentHelpItemGUID;
                    break;
                default:
                    throw new Exception("Error: Dont't find the DefaultGuidSource for HelpItem Entity.");
                    //break;
            }

            return newEntity;
        }
    }
}
