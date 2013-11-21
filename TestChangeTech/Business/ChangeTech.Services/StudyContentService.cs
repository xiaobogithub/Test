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
    public class StudyContentService : ServiceBase, IStudyContentService
    {
        public void AddNewStudyContent(InsertStudyContentModel model)
        {
            StudyContent content = new StudyContent
            {
                Study = Resolve<IStudyRepository>().GetStudyByGuid(model.StudyGUID),
                StudyContentGUID = Guid.NewGuid(),
                RouteURL = model.RouteURL
            };

            Resolve<IStudyContentRepository>().Insert(content);
        }

        public List<StudyContentModel> GetStudyContentList(Guid studyGuid)
        {
            List<StudyContentModel> modelList = new List<StudyContentModel>();
            List<StudyContent> contentEntityList = Resolve<IStudyContentRepository>().GetStudyContents(studyGuid).ToList();

            foreach(StudyContent content in contentEntityList)
            {
                StudyContentModel model = new StudyContentModel
                {
                    RouteURL = content.RouteURL,
                    StudContentGUID = content.StudyContentGUID
                };

                modelList.Add(model);
            }

            return modelList;
        }

        public void DeleteStudyContentByGUID(Guid studyContentGuid)
        {
            Resolve<IStudyContentRepository>().Delete(studyContentGuid);
        }

        public void UpdateStudyContent(StudyContentModel model)
        {
            StudyContent content = Resolve<IStudyContentRepository>().Get(model.StudContentGUID);
            content.RouteURL = model.RouteURL;
            Resolve<IStudyContentRepository>().Update(content);
        }

        public StudyContentModel GetStudyContentByGuid(Guid studyContentGuid)
        {
            StudyContent content = Resolve<IStudyContentRepository>().Get(studyContentGuid);
            StudyContentModel scModel = new StudyContentModel
            {
                RouteURL = content.RouteURL,
                StudContentGUID = studyContentGuid
            };

            return scModel;
        }
    }
}
