using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;

namespace ChangeTech.Contracts
{
    public interface IUserAnswerService
    {
        void SaveUserAnswer(List<UserAnswerModel> answerList, string programMode);
    }
}
