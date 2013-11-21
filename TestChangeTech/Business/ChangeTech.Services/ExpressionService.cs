using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Contracts;
using Ethos.DependencyInjection;
using ChangeTech.Models;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;
using Ethos.Utility;
using System.Text.RegularExpressions;

namespace ChangeTech.Services
{
    public class ExpressionService : ServiceBase, IExpressionService
    {
        public void AddExpression(ExpressionModel expressionModel)
        {
            try
            {
                Expression expressionEntity = new Expression();
                expressionEntity.ExpressionGroup = Resolve<IExpressionGroupRepository>().GetExpressionGroup(expressionModel.ExpressionGroupGUID);
                expressionEntity.ExpressionGUID = expressionModel.ExpressionGUID;
                expressionEntity.ExpressionText = expressionModel.ExpressionText;
                expressionEntity.Name = expressionModel.Name;
                expressionEntity.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
                Resolve<IExpressionRepository>().AddExpression(expressionEntity);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method: AddExpression; ExpressionGroup: {0}; ExpressionGUID: {1}; ExpressionText: {2}; Name: {3}",
                    expressionModel.ExpressionGroupGUID, expressionModel.ExpressionGUID, expressionModel.ExpressionText, expressionModel.Name));
                throw ex;
            }
        }


        public List<ExpressionModel> GetExpressionsOfGroup(Guid expressionGroupGuid)
        {
            List<ExpressionModel> expressions = new List<ExpressionModel>();
            try
            {
                List<Expression> expressionsEntity = Resolve<IExpressionRepository>().GetExpressionOfGroup(expressionGroupGuid).ToList<Expression>();
                foreach (Expression expressionEntity in expressionsEntity)
                {
                    ExpressionModel expressionModel = new ExpressionModel();
                    if (!expressionEntity.ExpressionGroupReference.IsLoaded)
                    {
                        expressionEntity.ExpressionGroupReference.Load();
                    }
                    expressionModel.ExpressionGroupGUID = expressionEntity.ExpressionGroup.ExpressionGroupGUID;
                    expressionModel.ExpressionGUID = expressionEntity.ExpressionGUID;
                    expressionModel.ExpressionText = expressionEntity.ExpressionText;
                    expressionModel.Name = expressionEntity.Name;
                    expressions.Add(expressionModel);
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method: GetExpressionsOfGroup; ExpressionGroup: {0}; ",
                    expressionGroupGuid));
                throw ex;
            }
            return expressions;
        }

        public List<ExpressionModel> GetExpressionOfProgram(Guid sessionGuid)
        {
            List<ExpressionModel> expressions = new List<ExpressionModel>();
            try
            {
                Session sessionEntity = Resolve<ISessionRepository>().GetSessionBySessionGuid(sessionGuid);
                if (!sessionEntity.ProgramReference.IsLoaded)
                {
                    sessionEntity.ProgramReference.Load();
                }

                List<Expression> expressionsEntity = Resolve<IExpressionRepository>().GetExpressionOfProgram(sessionEntity.Program.ProgramGUID).ToList<Expression>();
                foreach (Expression expressionEntity in expressionsEntity)
                {
                    ExpressionModel expressionModel = new ExpressionModel();
                    if (!expressionEntity.ExpressionGroupReference.IsLoaded)
                    {
                        expressionEntity.ExpressionGroupReference.Load();
                    }
                    expressionModel.ExpressionGroupGUID = expressionEntity.ExpressionGroup.ExpressionGroupGUID;
                    expressionModel.ExpressionGUID = expressionEntity.ExpressionGUID;
                    expressionModel.ExpressionText = expressionEntity.ExpressionText;
                    expressionModel.Name = expressionEntity.Name;
                    expressions.Add(expressionModel);
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method: GetExpressionsOfGroup; SessionGuid: {0}; ",
                    sessionGuid));
                throw ex;
            }
            return expressions;
        }

        public List<string> CheckExpressionForSession(Session sessinEntity, List<RelapsePageExpressionNodeModel> allRelapsePageExpressionNodeList)
        {

            List<string> resultList = new List<string>();
            //checking the existed page from the expression for the session
            List<string> pageList = CheckExistedPageFromExpressionForSession(sessinEntity);
            if (pageList != null && pageList.Count > 0)
            {
                resultList.AddRange(pageList);
            }

            //checking loop for session
            string checkexpression = System.Configuration.ConfigurationManager.AppSettings["CheckExpression"];
            if (checkexpression != null && checkexpression.ToUpper().Contains("LOOP"))
            {
                List<string> loopList = CheckExpressionForSessionInLoopWithoutRelapse(sessinEntity);
                if (loopList != null && loopList.Count > 0)
                {
                    resultList.AddRange(loopList);
                }
            }

            //checking relapse for session
            if (checkexpression != null && checkexpression.ToUpper().Contains("LOOP,RELAPSE"))
            {
                List<string> relapseList = CheckRelapseExpressionForSession(sessinEntity, allRelapsePageExpressionNodeList);
                if (relapseList != null && relapseList.Count > 0)
                {
                    resultList.AddRange(relapseList);
                }
            }

            return resultList;
        }

        #region Check exception for existed page
        private List<string> CheckExistedPageFromExpressionForSession(Session sessionEntity)
        {
            List<string> resultList = new List<string>();
            string returnresult = string.Empty;
            List<string> expressionList = GetExpressionForSession(sessionEntity.SessionGUID);
            //Session sessinEntity = Resolve<ISessionRepository>().GetSessionBySessionGuid(sessionGuid);
            List<PagesequenceAndPageCountModel> pagesInSession = new List<PagesequenceAndPageCountModel>();

            if (!sessionEntity.SessionContent.IsLoaded)
            {
                sessionEntity.SessionContent.Load();
            }
            List<SessionContent> sessionContentList = sessionEntity.SessionContent.Where(sc => sc.IsDeleted.HasValue && sc.IsDeleted == false || !sc.IsDeleted.HasValue).OrderBy(sc => sc.PageSequenceOrderNo).ToList();
            foreach (SessionContent scontent in sessionContentList)
            {
                if (!scontent.PageSequenceReference.IsLoaded)
                {
                    scontent.PageSequenceReference.Load();
                }
                if (!scontent.PageSequence.Page.IsLoaded)
                {
                    scontent.PageSequence.Page.Load();
                }
                int pagecount = scontent.PageSequence.Page.Where(p => !p.IsDeleted.HasValue || p.IsDeleted.HasValue && p.IsDeleted == false).Count();
                pagesInSession.Add(new PagesequenceAndPageCountModel { PagesequenceOrder = scontent.PageSequenceOrderNo, PageCount = pagecount });
            }

            foreach (string expression in expressionList)
            {
                List<string> daynumberlist = new List<string>();

                string[] expressionarray = expression.Split(';');
                string getDayNumber = expressionarray[2];
                while (getDayNumber.Contains("GOTO"))
                {
                    string subDayNumber = string.Empty;
                    getDayNumber = getDayNumber.Substring(getDayNumber.IndexOf("GOTO") + 4);

                    int elseNumber = getDayNumber.IndexOf("ELSE");
                    int ifNumber = getDayNumber.IndexOf("IF");

                    if (elseNumber == -1 && ifNumber == -1)
                    {
                        subDayNumber = getDayNumber;
                    }
                    else if (elseNumber == -1 && ifNumber != -1)
                    {
                        subDayNumber = getDayNumber.Substring(0, ifNumber);
                    }
                    else if (elseNumber != -1 && ifNumber == -1)
                    {
                        subDayNumber = getDayNumber.Substring(0, elseNumber);
                    }
                    else if (elseNumber != -1 && ifNumber != -1)
                    {
                        subDayNumber = getDayNumber.Substring(0, elseNumber > ifNumber ? ifNumber : elseNumber);
                    }

                    daynumberlist.Add(subDayNumber.Trim());
                }

                foreach (string subresult in daynumberlist)
                {
                    bool flug = false;

                    if (subresult.Contains("END") || subresult.Contains("NextPageSequence") || subresult.Contains("GetIndex"))
                    {
                        continue;
                    }

                    string[] subDayNumber = subresult.Split('.');
                    int pagesequenceNumber = Convert.ToInt32(subDayNumber[0].Trim());
                    int pageNumber = Convert.ToInt32(subDayNumber[1].Trim());
                    PagesequenceAndPageCountModel pagesequencePair = pagesInSession.Where(p => p.PagesequenceOrder == pagesequenceNumber).FirstOrDefault();
                    if (pagesequencePair != null)
                    {
                        if (pagesequencePair.PageCount >= pageNumber)
                        {
                            flug = true;
                        }
                    }

                    if (flug == false)
                    {
                        resultList.Add("day " + sessionEntity.Name + ", pagesequence " + expressionarray[0] + ", page " + expressionarray[1] + ", Expression " + subresult + " not exist.");
                        //returnresult += "day " + sessinEntity.Name + ", pagesequence " + expressionarray[0] + ", page " + expressionarray[1] + ", Expression " + subresult + " not exist.";
                    }
                }
            }
            return resultList;
        }

        public List<string> GetExpressionForSession(Guid sessionGuid)
        {
            return Resolve<IExpressionRepository>().GetExpressionForSession(sessionGuid);
        }

        #endregion Check exception for existed page

        #region Check expresion for loop
        //check expressions in a session are in a loop 
        private List<string> CheckExpressionForSessionInLoopWithoutRelapse(Session sessinEntity)
        {
            List<string> resultList = new List<string>();
            List<string> pageList = GetPagesequenceForSessionWithExpression(sessinEntity.SessionGUID);
            //load all pages and add them into a list of pages 
            List<PageExpressionNodeModel> pageExpressionNodeList = new List<PageExpressionNodeModel>();



            PageExpressionNodeModel pageExpressionNode = null;//current page node
            int nIndex = 0;
            int nCount = pageList.Count;
            foreach (string expression in pageList)
            {
                string[] expressionarray = expression.Split(';');
                int currentPagesequenceOrder = Convert.ToInt32(expressionarray[0].Trim()); //stord current pagesequence order
                int currentPageOrder = Convert.ToInt32(expressionarray[1].Trim());//stored current page order

                pageExpressionNode = new PageExpressionNodeModel
                {
                    IsUsed = false,
                    ParentPage = null,
                    Day = sessinEntity.Day.Value,
                    PagesequenceOrder = currentPagesequenceOrder,
                    PageOrder = currentPageOrder
                };

                int length = expressionarray.Length;

                switch (length)
                {
                    case 3: //has a expression
                        if (expressionarray[2].IndexOf("Before") >= 0)
                        {
                            pageExpressionNode.BeforeExpression = expressionarray[2].Substring(6).Trim();
                        }
                        if (expressionarray[2].IndexOf("After") >= 0)
                        {
                            pageExpressionNode.AfterExpression = expressionarray[2].Substring(5).Trim();
                        }
                        break;

                    case 4: //has two expression
                        pageExpressionNode.AfterExpression = expressionarray[3].Substring(5).Trim();
                        break;

                    default:
                        break;
                }

                string nextPageExpression = string.Empty;
                if (nIndex + 1 < nCount)
                {
                    nextPageExpression = pageList[nIndex + 1];
                }

                CreatePageExpression(ref pageExpressionNode, ref resultList, pageList, nextPageExpression);

                pageExpressionNodeList.Add(pageExpressionNode);

                nIndex++;
            }//end foreach

            //check if expressions are in a loop

            ValidatePageExpressionForSessionInLoopWithoutRelapse(pageExpressionNodeList, ref resultList);
            return resultList;
        }
        //get all page with expression
        public List<string> GetPagesequenceForSessionWithExpression(Guid sessionGuid)
        {
            return Resolve<IExpressionRepository>().GetPagesequenceForSessionWithExpression(sessionGuid);
        }
        //create the page with a expression
        private void CreatePageExpression(ref PageExpressionNodeModel pageExpressionNode, ref List<string> resultList, List<string> pageList, string nextPageExpression)
        {
            if (!string.IsNullOrEmpty(pageExpressionNode.BeforeExpression))
            {
                CreateChildForExpression(ref pageExpressionNode, 1, pageList, ref resultList);
            }
            if (!string.IsNullOrEmpty(pageExpressionNode.AfterExpression))
            {
                CreateChildForExpression(ref pageExpressionNode, 0, pageList, ref resultList);
            }
            if (!string.IsNullOrEmpty(nextPageExpression))
            {
                CreateChildForNextPage(ref pageExpressionNode, nextPageExpression);
            }


        }

        //check if expression like "GOTO Pagesequence.Page" or "GOTO END" or "GOTO NextPagesequence"
        private bool IsExpressionGoToPage(string expression)
        {
            bool flag = false;
            if (string.IsNullOrEmpty(expression))
            {
                return flag;
            }
            if ("ENDPAGE" == expression.Trim().ToUpper())
            {
                flag = true;
                return flag;
            }
            if (!expression.Contains("IF"))
            {
                int pos = expression.IndexOf("GOTO");
                if (pos > -1)
                {
                    string subContent = expression.Substring(pos + 4).Trim();
                    if ("END" == subContent.ToUpper())
                    {
                        flag = true;
                        return flag;
                    }

                    if ("NEXTPAGESEQUENCE" == subContent.ToUpper())
                    {
                        flag = true;
                        return flag;
                    }
                    double seq;
                    flag = double.TryParse(subContent, out seq);//check if the part of string is number 
                    return flag;
                }
            }
            return flag;
        }

        // create child page from expression
        private void CreateChildForExpression(ref PageExpressionNodeModel pageExpressionNode, int isBefore, List<string> pageList, ref List<string> resultList)
        {
            int pagesequenceOrder = pageExpressionNode.PagesequenceOrder;
            int pageOrder = pageExpressionNode.PageOrder;
            string expression = null;
            if (1 == isBefore)//before expression
            {
                expression = pageExpressionNode.BeforeExpression;
            }
            else //after expression
            {
                if (IsExpressionGoToPage(pageExpressionNode.BeforeExpression))//if before expression like "GOTO 1.2",Don't add any children from after expression
                {
                    return;
                }
                expression = pageExpressionNode.AfterExpression;

            }
            if (string.IsNullOrEmpty(expression))
            {
                return;
            }

            if (!expression.Contains("GOTO"))
            {
                return;
            }

            if (pageExpressionNode.ChildPages == null)
            {
                pageExpressionNode.ChildPages = new List<PageExpressionNodeModel>();
            }

            string[] expressionArray = expression.Split(new string[2] { "IF", "ELSE" }, StringSplitOptions.RemoveEmptyEntries);
            string gotoResult = null;
            int pos = -1;
            bool isNextPageSequence = false;
            string[] subDayNumbers = null;
            foreach (string content in expressionArray)
            {
                pos = content.IndexOf("GOTO");
                if (pos > -1)
                {
                    gotoResult = content.Substring(pos + 4).Trim();
                    if (gotoResult.Contains("END") || gotoResult.Contains("GetIndex"))
                    {
                        continue;
                    }
                    if (gotoResult.Contains("NextPageSequence"))
                    {
                        isNextPageSequence = true;
                    }
                    int childPagesequenceOrder = 0;
                    int childPageNumber = 0;
                    if (isNextPageSequence)
                    {

                        childPagesequenceOrder = pageExpressionNode.PagesequenceOrder + 1;
                        childPageNumber = 1;
                        string findExpression = pageList.Where(p => p.Contains(childPagesequenceOrder.ToString() + ";1")).FirstOrDefault();
                        if (string.IsNullOrEmpty(findExpression))//next pagerequnce doesn't exist
                        {
                            continue;
                        }
                    }
                    else
                    {
                        subDayNumbers = gotoResult.Split('.');
                        childPagesequenceOrder = Convert.ToInt32(subDayNumbers[0].Trim());
                        childPageNumber = Convert.ToInt32(subDayNumbers[1].Trim());
                    }


                    PageExpressionNodeModel childPageExpression = new PageExpressionNodeModel
                    {
                        ParentPage = pageExpressionNode,
                        Day = pageExpressionNode.Day,

                        PagesequenceOrder = childPagesequenceOrder,
                        PageOrder = childPageNumber,

                        BeforeExpression = null,
                        AfterExpression = null,
                        ChildPages = null
                    };

                    pageExpressionNode.ChildPages.Add(childPageExpression);
                }
            }

        }

        // add next page into childpages. eg2.1->2.2
        private void CreateChildForNextPage(ref PageExpressionNodeModel pageExpressionNode, string nextPageExpression)
        {
            if (string.IsNullOrEmpty(nextPageExpression))
            {
                return;
            }
            if (IsExpressionGoToPage(pageExpressionNode.BeforeExpression))//if before expression like "GOTO 1.2" or "GOTO END" or "GOTO NextPagesequence",Don't add any children from next page
            {
                return;
            }
            if (IsExpressionGoToPage(pageExpressionNode.AfterExpression))//if after expression like "GOTO 1.2" or "GOTO END" or "GOTO NextPagesequence",Don't add any children from next page
            {
                return;
            }
            if (null == pageExpressionNode.ChildPages)
            {
                pageExpressionNode.ChildPages = new List<PageExpressionNodeModel>();
            }
            //the next page is the child page 
            string[] expressionarray = nextPageExpression.Split(';');
            int childPagesequenceOrder = Convert.ToInt32(expressionarray[0].Trim()); //stord next pagesequence order
            int childPageNumber = Convert.ToInt32(expressionarray[1].Trim());//stored next page order

            PageExpressionNodeModel childPageExpression = new PageExpressionNodeModel();

            childPageExpression.ParentPage = pageExpressionNode;
            childPageExpression.Day = pageExpressionNode.Day;

            childPageExpression.PagesequenceOrder = childPagesequenceOrder;
            childPageExpression.PageOrder = childPageNumber;
            childPageExpression.BeforeExpression = null;
            childPageExpression.AfterExpression = null;
            childPageExpression.ChildPages = null;

            pageExpressionNode.ChildPages.Add(childPageExpression);

        }

        //check if expressions are in a loop
        private void ValidatePageExpressionForSessionInLoopWithoutRelapse(List<PageExpressionNodeModel> pageExpressionNodeList, ref List<string> resultList)
        {

            List<PageExpressionNodeModel> pageExpressionTopList = new List<PageExpressionNodeModel>();//prepare node list for search
            foreach (PageExpressionNodeModel copyPageExpressionNode in pageExpressionNodeList)
            {
                pageExpressionTopList.Add(copyPageExpressionNode);
            }

            //the item in the pageExpressionNodeList will be created into page tree as a page node,and will be removed from topPageExpressionNodeList
            foreach (PageExpressionNodeModel pageExpressionNode in pageExpressionNodeList)
            {
                if (pageExpressionNode.IsUsed)
                {
                    continue;
                }

                if (!pageExpressionNode.IsUsed)
                {
                    pageExpressionNode.IsUsed = true;
                }

                if (null == pageExpressionNode.ChildPages || 0 == pageExpressionNode.ChildPages.Count)//the current page expression has not child page expression. 
                {
                    continue;
                }
                if (pageExpressionNode.ChildPages != null && pageExpressionNode.ChildPages.Count > 0)
                {
                    FillChild(pageExpressionNode, ref pageExpressionTopList, ref resultList);
                }

            }

        }

        //fill child for pageExpressionNode from pageExpressionTopList
        private void FillChild(PageExpressionNodeModel pageExpressionNode, ref List<PageExpressionNodeModel> pageExpressionTopList, ref List<string> resultList)
        {

            foreach (PageExpressionNodeModel childPageExpressionNode in pageExpressionNode.ChildPages)
            {
                childPageExpressionNode.ParentPage = pageExpressionNode;//reset parent page. if not, the child page will lost parent node, GetPathLog validate it.
                //check if childPageExpressionNode is in pageExpressionNodeList                    
                PageExpressionNodeModel resultPageExpressionNode =
                    pageExpressionTopList.Where(p => p.PagesequenceOrder == childPageExpressionNode.PagesequenceOrder
                        && p.PageOrder == childPageExpressionNode.PageOrder).FirstOrDefault();
                if (null == resultPageExpressionNode)//if childPageExpressioNode doesn't exists in top list
                {
                    PathLog(childPageExpressionNode, ref resultList, false);
                }
                else // add a child node
                {
                    if (!ValidateRepeat(childPageExpressionNode))
                    {
                        childPageExpressionNode.BeforeExpression = resultPageExpressionNode.BeforeExpression;
                        childPageExpressionNode.AfterExpression = resultPageExpressionNode.AfterExpression;

                        childPageExpressionNode.ChildPages = resultPageExpressionNode.ChildPages;

                        if (!resultPageExpressionNode.IsUsed)
                        {
                            resultPageExpressionNode.IsUsed = true;
                        }

                        if (childPageExpressionNode.ChildPages != null && childPageExpressionNode.ChildPages.Count > 0)
                        {
                            FillChild(childPageExpressionNode, ref pageExpressionTopList, ref resultList);
                        }
                    }
                    else
                    {
                        PathLog(childPageExpressionNode, ref resultList, true);
                    }

                }

            }
            //avoid memory is not enough
            if (pageExpressionNode.ParentPage != null && pageExpressionNode.ParentPage.ParentPage != null)
            {
                pageExpressionNode.ChildPages.Clear();
            }
        }

        //record the passed node into the log
        private void PathLog(PageExpressionNodeModel bottomPageExpressionNode, ref List<string> resultList, bool isLoop)
        {
            if (bottomPageExpressionNode == null)
            {
                return;
            }

            List<string> logList = new List<string>();
            logList.Add(string.Format(" Day {0} Pagesequence {1} Page {2}", bottomPageExpressionNode.Day, bottomPageExpressionNode.PagesequenceOrder, bottomPageExpressionNode.PageOrder));

            PageExpressionNodeModel parentPageExpressionNode = bottomPageExpressionNode.ParentPage;
            int nI = 0;
            while (parentPageExpressionNode != null)
            {
                string content = string.Format(" Day {0} Pagesequence {1} Page {2} ", parentPageExpressionNode.Day, parentPageExpressionNode.PagesequenceOrder, parentPageExpressionNode.PageOrder);
                logList.Insert(0, content);
                parentPageExpressionNode = parentPageExpressionNode.ParentPage;
                nI++;
            }

            if (isLoop)
            {
                logList.Insert(0, "There may be a loop in the below path:");
            }
            else
            {
                logList.Insert(0, "The below path is broken:");
            }
            resultList.AddRange(logList);

        }

        //check if the node is repeat in the path
        public bool ValidateRepeat(PageExpressionNodeModel pageExpressionNode)
        {
            if (null == pageExpressionNode)
            {
                return false;
            }

            PageExpressionNodeModel parentPageExpressionNode = pageExpressionNode.ParentPage;
            while (parentPageExpressionNode != null)
            {
                if (parentPageExpressionNode.PagesequenceOrder == pageExpressionNode.PagesequenceOrder &&
                    parentPageExpressionNode.PageOrder == pageExpressionNode.PageOrder)
                {
                    return true;
                }
                parentPageExpressionNode = parentPageExpressionNode.ParentPage;
            }
            return false;
        }

        #endregion Check expresion for loop

        #region Check expression from relapse in a session is a loop
        #region called from programService
        public List<RelapsePageExpressionNodeModel> GetRelapsePages(List<string> relapsePageList)
        {
            List<RelapsePageExpressionNodeModel> relapsePageNodeList = new List<RelapsePageExpressionNodeModel>();
            int nIndex = 0;
            int nCount = relapsePageList.Count;
            string[] expressionarray = null;
            RelapsePageExpressionNodeModel pageExpressionNode = null;
            string currentPagesequenceGuid = string.Empty;
            int currentPageOrder = 0;
            string currentPagesequenceName = string.Empty;
            foreach (string expression in relapsePageList)
            {
                expressionarray = expression.Split(';');
                currentPagesequenceGuid = expressionarray[0].Trim(); //stord current pagesequenceguid
                currentPageOrder = Convert.ToInt32(expressionarray[1].Trim());//stored current page order
                currentPagesequenceName = expressionarray[2].Trim(); //stord current pagesequence name
                pageExpressionNode = new RelapsePageExpressionNodeModel
                {
                    IsUsed = false,
                    ParentPage = null,
                    PagesequenceName = currentPagesequenceName,
                    PagesequnceGUID = currentPagesequenceGuid,
                    PageOrder = currentPageOrder
                };

                int length = expressionarray.Length;
                switch (length)
                {
                    case 4: //has a expression
                        if (expressionarray[3].IndexOf("Before") >= 0)
                        {
                            pageExpressionNode.BeforeExpression = expressionarray[3].Substring(6).Trim();
                        }
                        if (expressionarray[3].IndexOf("After") >= 0)
                        {
                            pageExpressionNode.AfterExpression = expressionarray[3].Substring(5).Trim();
                        }
                        break;

                    case 5: //has two expression
                        pageExpressionNode.AfterExpression = expressionarray[4].Substring(5).Trim();
                        break;

                    default:
                        break;
                }

                string nextPageExpression = string.Empty;
                if (nIndex + 1 < nCount)
                {
                    nextPageExpression = relapsePageList[nIndex + 1];
                }

                CreateRelapsePageExpression(ref pageExpressionNode, nextPageExpression);

                relapsePageNodeList.Add(pageExpressionNode);

                nIndex++;
            }//end foreach
            return relapsePageNodeList;
        }

        //create the page with a expression
        private void CreateRelapsePageExpression(ref RelapsePageExpressionNodeModel pageExpressionNode, string nextPageExpression)
        {
            if (!string.IsNullOrEmpty(pageExpressionNode.BeforeExpression))
            {
                CreateRelapseChildForExpression(ref pageExpressionNode, 1);
            }
            if (!string.IsNullOrEmpty(pageExpressionNode.AfterExpression))
            {
                CreateRelapseChildForExpression(ref pageExpressionNode, 0);
            }
            if (!string.IsNullOrEmpty(nextPageExpression))
            {
                CreateRelapseChildForNextPage(ref pageExpressionNode, nextPageExpression);
            }


        }

        //check if before expression like "GOTO Pagesequence.Page" or "GOTO END" or "GOTO NextPagesequence" or "GOSUB"
        private bool IsExpressionGoToPageOrGoSub(string expression)
        {
            bool flag = false;
            flag = IsExpressionGoToPage(expression);
            if (flag)
            {
                return flag;
            }
            if (string.IsNullOrEmpty(expression))
            {
                return flag;
            }
            if (!expression.Contains("IF"))
            {
                int pos = expression.IndexOf("GOSUB");
                if (pos > -1)
                {

                    flag = true;
                    return flag;
                }
            }
            return flag;
        }


        // create child page from expression
        private void CreateRelapseChildForExpression(ref RelapsePageExpressionNodeModel pageExpressionNode, int isBefore)
        {
            string pagesequenceGuid = pageExpressionNode.PagesequnceGUID;
            int pageOrder = pageExpressionNode.PageOrder;
            string pagesequenceName = pageExpressionNode.PagesequenceName;
            string expression = null;

            if (1 == isBefore)//before expression
            {
                expression = pageExpressionNode.BeforeExpression;
            }
            else //after expression
            {
                if (IsExpressionGoToPageOrGoSub(pageExpressionNode.BeforeExpression))//if before expression like "GOTO 1.2" or "GOTO END" or "GOTO NextPagesequence",Don't add any children from after expression
                {
                    return;
                }
                expression = pageExpressionNode.AfterExpression;

            }
            if (string.IsNullOrEmpty(expression))
            {
                return;
            }

            if (!expression.Contains("GOTO") && !expression.Contains("GOSUB"))
            {
                return;
            }

            if (pageExpressionNode.ChildPages == null)
            {
                pageExpressionNode.ChildPages = new List<RelapsePageExpressionNodeModel>();
            }

            string[] expressionArray = expression.Split(new string[2] { "IF", "ELSE" }, StringSplitOptions.RemoveEmptyEntries);
            string gotoResult = null;
            int pos = -1;
            string[] subDayNumbers = null;
            string childPagesequenceguid = string.Empty;
            int childPageorder = 0;

            foreach (string content in expressionArray)
            {
                pos = content.IndexOf("GOTO");
                if (pos > -1)
                {
                    gotoResult = content.Substring(pos + 4).Trim();
                    if (gotoResult.Contains("END") || gotoResult.Contains("GetIndex") || gotoResult.Contains("NextPageSequence"))
                    {
                        continue;
                    }
                    subDayNumbers = gotoResult.Split('.');
                    childPagesequenceguid = pagesequenceGuid;///?
                    childPageorder = Convert.ToInt32(subDayNumbers[1].Trim());
                    RelapsePageExpressionNodeModel childPageExpression = new RelapsePageExpressionNodeModel
                    {
                        ParentPage = pageExpressionNode,
                        PagesequnceGUID = childPagesequenceguid,
                        PageOrder = childPageorder,
                        PagesequenceName = pagesequenceName,
                        IsUsed = false,
                        BeforeExpression = null,
                        AfterExpression = null,
                        ChildPages = null
                    };

                    pageExpressionNode.ChildPages.Add(childPageExpression);
                    continue;
                }
                pos = content.IndexOf("GOSUB");
                if (pos > -1)
                {
                    pos = content.IndexOf("Relapse:");
                    if (-1 == pos)
                    {
                        continue;
                    }
                    gotoResult = content.Substring(pos + 8);
                    int endpos = gotoResult.IndexOf("}");
                    if (endpos > -1)
                    {
                        gotoResult = gotoResult.Substring(0, endpos);
                        RelapsePageExpressionNodeModel childPageExpression = new RelapsePageExpressionNodeModel
                        {
                            ParentPage = pageExpressionNode,
                            PagesequenceName = null,
                            PagesequnceGUID = gotoResult,
                            PageOrder = 1,
                            IsUsed = false,
                            BeforeExpression = null,
                            AfterExpression = null,
                            ChildPages = null
                        };

                        pageExpressionNode.ChildPages.Add(childPageExpression);
                    }
                }

            }

        }

        // add next page into childpages. eg2.1->2.2
        private void CreateRelapseChildForNextPage(ref RelapsePageExpressionNodeModel pageExpressionNode, string nextPageExpression)
        {
            if (string.IsNullOrEmpty(nextPageExpression))
            {
                return;
            }
            if (IsExpressionGoToPageOrGoSub(pageExpressionNode.BeforeExpression))//if before expression like "GOTO 1.2" or "GOTO END" or "GOTO NextPagesequence" or "GOSUB",Don't add any children from next page
            {
                return;
            }
            if (IsExpressionGoToPageOrGoSub(pageExpressionNode.AfterExpression))//if after expression like "GOTO 1.2" or "GOTO END" or "GOTO NextPagesequence" or "GOSUB",Don't add any children from next page
            {
                return;
            }
            if (null == pageExpressionNode.ChildPages)
            {
                pageExpressionNode.ChildPages = new List<RelapsePageExpressionNodeModel>();
            }
            //the next page is the child page 
            string[] expressionarray = nextPageExpression.Split(';');
            string childPagesequenceguid = expressionarray[0].Trim(); //stord next pagesequence guid
            int childPageNumber = Convert.ToInt32(expressionarray[1].Trim());//stored next page order
            string childPagesequenceName = expressionarray[2].Trim(); //stord next pagesequence name
            RelapsePageExpressionNodeModel childPageExpression = new RelapsePageExpressionNodeModel
            {
                IsUsed = false,
                ParentPage = pageExpressionNode,
                PagesequnceGUID = childPagesequenceguid,
                PageOrder = childPageNumber,
                PagesequenceName = childPagesequenceName,
                BeforeExpression = null,
                AfterExpression = null,
                ChildPages = null
            };

            pageExpressionNode.ChildPages.Add(childPageExpression);

        }
        #endregion
        //check expression from relapse in a session is a loop
        private List<string> CheckRelapseExpressionForSession(Session sessionEntity, List<RelapsePageExpressionNodeModel> allRelapsePageExpressionNodeList)
        {
            List<string> resultList = new List<string>();

            List<string> enterPageList = GetExpressionForRelapseFromSession(sessionEntity.SessionGUID);//get enter for relapse
            List<string> pagesequenceguidList = new List<string>(); //collect all pagesequenceguid from relapse expression         

            string[] expressionarray = null;
            foreach (string expression in enterPageList)
            {
                expressionarray = expression.Split(';');
                if (expressionarray[2].Contains("GOSUB"))
                {
                    GetPagesequenceIdList(expressionarray[2], ref pagesequenceguidList);
                }

            }//end foreach

            List<RelapsePageExpressionNodeModel> relapsePageExpressionNodeList = null;
            foreach (string pagesequenceGuid in pagesequenceguidList)
            {
                if (relapsePageExpressionNodeList != null)
                {
                    relapsePageExpressionNodeList.Clear();
                }
                relapsePageExpressionNodeList = allRelapsePageExpressionNodeList.Where(p => p.PagesequnceGUID.ToUpper() == pagesequenceGuid.ToUpper()).ToList();
                if (relapsePageExpressionNodeList != null && relapsePageExpressionNodeList.Count > 0)
                {
                    ValidateRelapsePageExpressionInLoop(relapsePageExpressionNodeList, allRelapsePageExpressionNodeList, ref resultList);
                }

            }
            return resultList;
        }


        //get all expression for relapse from a session
        public List<string> GetExpressionForRelapseFromSession(Guid sessionGuid)
        {
            return Resolve<IExpressionRepository>().GetExpressionForRelapseFromSession(sessionGuid);
        }

        //create the page with a expression
        private void GetPagesequenceIdList(string expression, ref List<string> guidList)
        {
            string[] expressionArray = expression.Split(new string[2] { "IF", "ELSE" }, StringSplitOptions.RemoveEmptyEntries);
            int pos = -1;
            int endpos = -1;
            string guid = string.Empty;
            foreach (string content in expressionArray)
            {
                pos = content.IndexOf("GOSUB");
                if (pos > -1)
                {
                    pos = content.IndexOf("Relapse:");
                    if (pos > -1)
                    {
                        guid = content.Substring(pos + 8);
                        endpos = guid.IndexOf("}");
                        if (endpos > -1)
                        {
                            guid = guid.Substring(0, endpos);
                            guidList.Add(guid);
                        }
                    }
                }
            }
        }

        private void ValidateRelapsePageExpressionInLoop(List<RelapsePageExpressionNodeModel> pageExpressionNodeList, List<RelapsePageExpressionNodeModel> allRelapsePageExpressionNodeList, ref List<string> resultList)
        {

            //the item in the pageExpressionNodeList will be created into page tree as a page node,and will be removed from topPageExpressionNodeList
            foreach (RelapsePageExpressionNodeModel pageExpressionNode in pageExpressionNodeList)
            {
                if (pageExpressionNode.IsUsed)
                {
                    continue;
                }

                if (!pageExpressionNode.IsUsed)
                {
                    pageExpressionNode.IsUsed = true;
                }

                if (null == pageExpressionNode.ChildPages || 0 == pageExpressionNode.ChildPages.Count)//the current page expression has not child page expression. 
                {
                    continue;
                }
                if (pageExpressionNode.ChildPages != null && pageExpressionNode.ChildPages.Count > 0)
                {
                    FillChild(pageExpressionNode, allRelapsePageExpressionNodeList, ref resultList);
                }

            }

        }

        //fill child for pageExpressionNode from allRelapsePageExpressionNodeList
        private void FillChild(RelapsePageExpressionNodeModel pageExpressionNode, List<RelapsePageExpressionNodeModel> allRelapsePageExpressionNodeList, ref List<string> resultList)
        {

            foreach (RelapsePageExpressionNodeModel childPageExpressionNode in pageExpressionNode.ChildPages)
            {
                childPageExpressionNode.ParentPage = pageExpressionNode;//reset parent page. if not, the child page will lost parent node, GetPathLog validate it.

                //check if childPageExpressionNode is in pageExpressionNodeList                    
                RelapsePageExpressionNodeModel resultPageExpressionNode =
                    allRelapsePageExpressionNodeList.Where(p => p.PagesequnceGUID.ToUpper() == childPageExpressionNode.PagesequnceGUID.ToUpper()
                        && p.PageOrder == childPageExpressionNode.PageOrder).FirstOrDefault();
                if (null == resultPageExpressionNode)//if childPageExpressioNode doesn't exists in the all list
                {
                    PathLog(childPageExpressionNode, ref resultList, false);
                }
                else // add a child node
                {
                    childPageExpressionNode.PagesequenceName = resultPageExpressionNode.PagesequenceName;// some pageseqence name are empty
                    if (!ValidateRepeat(childPageExpressionNode))
                    {
                        childPageExpressionNode.BeforeExpression = resultPageExpressionNode.BeforeExpression;
                        childPageExpressionNode.AfterExpression = resultPageExpressionNode.AfterExpression;

                        childPageExpressionNode.ChildPages = resultPageExpressionNode.ChildPages;

                        if (!resultPageExpressionNode.IsUsed)
                        {
                            resultPageExpressionNode.IsUsed = true;
                        }

                        if (childPageExpressionNode.ChildPages != null && childPageExpressionNode.ChildPages.Count > 0)
                        {
                            FillChild(childPageExpressionNode, allRelapsePageExpressionNodeList, ref resultList);
                        }
                    }
                    else
                    {
                        PathLog(childPageExpressionNode, ref resultList, true);
                    }

                }

            }
            //avoid memory is not enough
            if (pageExpressionNode.ParentPage != null && pageExpressionNode.ParentPage.ParentPage != null)
            {
                pageExpressionNode.ChildPages.Clear();
            }
        }

        //record the passed node into the log
        private void PathLog(RelapsePageExpressionNodeModel bottomPageExpressionNode, ref List<string> resultList, bool isLoop)
        {
            if (bottomPageExpressionNode == null)
            {
                return;
            }

            List<string> logList = new List<string>();
            logList.Add(string.Format(" Pagesequence {0} Page {1}", bottomPageExpressionNode.PagesequenceName, bottomPageExpressionNode.PageOrder));

            RelapsePageExpressionNodeModel parentPageExpressionNode = bottomPageExpressionNode.ParentPage;
            int nI = 0;
            while (parentPageExpressionNode != null)
            {
                string content = string.Format(" Pagesequence {0} Page {1} ", parentPageExpressionNode.PagesequenceName, parentPageExpressionNode.PageOrder);
                logList.Insert(0, content);
                parentPageExpressionNode = parentPageExpressionNode.ParentPage;
                nI++;
            }

            if (isLoop)
            {
                logList.Insert(0, "There may be a loop in the below subroutine path:");
            }
            else
            {
                logList.Insert(0, "The below subroutine path is broken:");
            }
            resultList.AddRange(logList);

        }

        //check if the node is repeat in the path
        public bool ValidateRepeat(RelapsePageExpressionNodeModel pageExpressionNode)
        {
            if (null == pageExpressionNode)
            {
                return false;
            }

            RelapsePageExpressionNodeModel parentPageExpressionNode = pageExpressionNode.ParentPage;
            while (parentPageExpressionNode != null)
            {
                if (parentPageExpressionNode.PagesequnceGUID.ToUpper() == pageExpressionNode.PagesequnceGUID.ToUpper() &&
                    parentPageExpressionNode.PageOrder == pageExpressionNode.PageOrder)
                {
                    return true;
                }
                parentPageExpressionNode = parentPageExpressionNode.ParentPage;
            }
            return false;
        }

        #endregion Check expression from relapse in a session is a loop

        public void SaveExpressions(EditExpressionModel editExpressionModel)
        {
            foreach (ExpressionModel expression in editExpressionModel.Expressions)
            {
                switch (editExpressionModel.ObjectStatus[expression.ExpressionGUID])
                {
                    case ModelStatus.ModelAdd:
                        AddExpression(expression);
                        break;
                    case ModelStatus.ModelEdit:
                        UpdateExpression(expression);
                        break;
                }
            }

            try
            {
                foreach (Guid expressionGuid in editExpressionModel.ObjectStatus.Keys)
                {
                    if (editExpressionModel.ObjectStatus[expressionGuid] == ModelStatus.ModelDelete)
                    {
                        Resolve<IExpressionRepository>().DeleteExpression(expressionGuid);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method: SaveExpressions - Delete;"));
                throw ex;
            }
        }

        private void UpdateExpression(ExpressionModel expressionModel)
        {
            try
            {
                Expression expressionEntity = Resolve<IExpressionRepository>().GetExpression(expressionModel.ExpressionGUID);
                expressionEntity.ExpressionGroup = Resolve<IExpressionGroupRepository>().GetExpressionGroup(expressionModel.ExpressionGroupGUID);
                expressionEntity.ExpressionText = expressionModel.ExpressionText;
                expressionEntity.Name = expressionModel.Name;
                expressionEntity.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
                Resolve<IExpressionRepository>().UpdateExpression(expressionEntity);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method: UpdateExpression; ExpressionGroup: {0}; ExpressionText {1}; ExpressionGUID {2}; Name {3} ",
                    expressionModel.ExpressionGroupGUID, expressionModel.ExpressionText, expressionModel.ExpressionGUID, expressionModel.Name));
                throw ex;
            }
        }
    }
}
