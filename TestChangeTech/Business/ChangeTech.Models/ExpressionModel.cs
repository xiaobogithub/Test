using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ChangeTech.Models
{
    [DataContract]
    public class ExpressionGroupModel
    {
        [DataMember]
        public Guid ExpressionGroupGUID { get; set; }
        [DataMember]
        public Guid ProgramGUID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
    }

    [DataContract]
    public class ExpressionModel
    {
        [DataMember]
        public Guid ExpressionGUID { get; set; }
        [DataMember]
        public Guid ExpressionGroupGUID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string ExpressionText { get; set; }
    }

    [DataContract]
    public class EditExpressionGroupModel
    {
        [DataMember]
        public Guid SessionGUID { get; set; }
        [DataMember]
        public Guid ProgramGUID { get; set; }
        [DataMember]
        public string ProgramName { get; set; }
        [DataMember]
        public List<ExpressionGroupModel> ExpressionGroups { get; set; }
        [DataMember]
        public SortedList<Guid, ModelStatus> ObjectStatus { get; set; }
    }

    [DataContract]
    public class EditExpressionModel
    {
        [DataMember]
        public List<ExpressionModel> Expressions {get; set;}
        [DataMember]
        public SortedList<Guid, ModelStatus> ObjectStatus {get; set;}
    }

    public class PagesequenceAndPageCountModel
    {
        public int PagesequenceOrder { get; set; }
        public int PageCount { get; set; }
    }

    public class PageExpressionNodeModel
    {
        public bool IsUsed { get; set; }
        public int Day { get; set; }
        public int PagesequenceOrder { get; set; }
        public int PageOrder { get; set; }
        public string BeforeExpression { get; set; }
        public string AfterExpression { get; set; }

        public PageExpressionNodeModel ParentPage { get; set; }
        public List<PageExpressionNodeModel> ChildPages { get; set; }

    }

    public class RelapsePageExpressionNodeModel
    {
        public bool IsUsed { get; set; }
        public string PagesequenceName { get; set; }
        public string PagesequnceGUID { get; set; }
        public int PageOrder { get; set; }
        public string BeforeExpression { get; set; }
        public string AfterExpression { get; set; }

        public RelapsePageExpressionNodeModel ParentPage { get; set; }
        public List<RelapsePageExpressionNodeModel> ChildPages { get; set; }

    }
   
}