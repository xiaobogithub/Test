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
    public class StudyService : ServiceBase, IStudyService
    {
        public List<StudyModel> GetStudyList(int pageNumber, int pageSize)
        {
            List<Study> studyList = Resolve<IStudyRepository>().GetAllStudy().OrderBy(s => s.Name).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            List<StudyModel> modelList = new List<StudyModel>();
            foreach(Study studyEntity in studyList)
            {
                StudyModel model = new StudyModel
                {
                    StudyGUID = studyEntity.StudyGUID,
                    Name = studyEntity.Name,
                    Description = studyEntity.Description
                };
                modelList.Add(model);
            }

            return modelList;
        }

        public int GetNumberOfStudy()
        {
            return Resolve<IStudyRepository>().GetAllStudy().Count();
        }

        public StudyModel GetStudyByGUID(Guid studyGuid)
        {
            Study entity = Resolve<IStudyRepository>().GetStudyByGuid(studyGuid);
            StudyModel model = new StudyModel
            {
                Description = entity.Description,
                Name = entity.Name,
                StudyGUID = entity.StudyGUID,
                ShortName = entity.ShortName
            };

            return model;
        }

        public void AddNewStudy(StudyModel model)
        {
            Study studyEntity = new Study
            {
                Description = model.Description,
                Name = model.Name,
                ShortName = GenerateStuduyCode(),
                StudyGUID = Guid.NewGuid()
            };

            Resolve<IStudyRepository>().Insert(studyEntity);
        }

        public void DeleteStudy(Guid studyGuid)
        {
            Resolve<IStudyRepository>().Delete(studyGuid);
        }

        public void UpdateStudy(StudyModel model)
        {
            Study entity = Resolve<IStudyRepository>().GetStudyByGuid(model.StudyGUID);
            entity.Name = model.Name;
            entity.Description = model.Description;

            Resolve<IStudyRepository>().Update(entity);
        }

        public string GenerateStuduyCode()
        {
            string code = string.Empty;
            do
            {
                code = Ethos.Utility.StringUtility.GenerateCheckCode(6);
            }
            while(IsStudyCodeExisted(code));

            return code;
        }

        public Guid GetStudyGUIDByShortName(string shortName)
        {
            Study entity = Resolve<IStudyRepository>().GetStudyByShortName(shortName);

            return entity.StudyGUID;
        }

        //public StudyContentModel RandomProgram(Guid studyGuid, long studyUserID)
        //{
        //    Study studyEntity = Resolve<IStudyRepository>().GetStudyByGuid(studyGuid);
        //    if(!studyEntity.StudyContent.IsLoaded)
        //    {
        //        studyEntity.StudyContent.Load();
        //    }

        //    int sUserCount = Resolve<IStudyUserRepository>().GetStudyUserBeforeCurrentID(studyGuid, studyUserID).Count();
        //    StudyContent sContent = studyEntity.StudyContent.ToList()[sUserCount % studyEntity.StudyContent.Count];
        //    StudyContentModel scModel = new StudyContentModel
        //    {
        //        RouteURL = sContent.RouteURL,
        //        StudContentGUID = sContent.StudyContentGUID
        //    };

        //    return scModel;
        //}

        public StudyContentModel RandomProgram(string studyShortName)
        {
            Study studyEntity = Resolve<IStudyRepository>().GetStudyByShortName(studyShortName);
            if(!studyEntity.StudyContent.IsLoaded)
            {
                studyEntity.StudyContent.Load();
            }
            Random ran = new Random();
            int randomNumber = ran.Next((studyEntity.StudyContent.Count * 10 - 1));
            StudyContent sContent = studyEntity.StudyContent.ToList()[randomNumber/10];

            StudyContentModel scModel = new StudyContentModel
            {
                RouteURL = sContent.RouteURL,
                StudContentGUID = sContent.StudyContentGUID
            };

            return scModel;
        }

        private bool IsStudyCodeExisted(string code)
        {
            bool flag = false;
            Study studyentity = Resolve<IStudyRepository>().GetStudyByShortName(code);
            if(studyentity != null)
            {
                flag = true;
            }

            return flag;
        }
    }
}
