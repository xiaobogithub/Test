using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using ChangeTech.Models;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;
using Ethos.Utility;

namespace ChangeTech.Services
{
    public class HPOrderLicenceService : ServiceBase, IHPOrderLicenceService
    {

        public void Insert(HPOrderLicenceModel hpOrderLicenceModel)
        {
            HPOrderLicence orderLicenceEntity = new HPOrderLicence
            {
                HPOrderLicenceGUID = Guid.NewGuid(),
                HPOrderGUID = hpOrderLicenceModel.HPOrderGUID,
                Created = DateTime.UtcNow,
                ProgramUserGUID = hpOrderLicenceModel.ProgramUserGUID
            };
            //if (!orderLicenceEntity.ProgramUserReference.IsLoaded) orderLicenceEntity.ProgramUserReference.Load();
            if (hpOrderLicenceModel.ProgramUserGUID != null && hpOrderLicenceModel.ProgramUserGUID != Guid.Empty)
            {
                orderLicenceEntity.ProgramUser = Resolve<IProgramUserRepository>().GetProgramUserByProgramUserGuid(hpOrderLicenceModel.ProgramUserGUID);
            }

            Resolve<IHPOrderLicenceRepository>().Insert(orderLicenceEntity);
        }

        public HPOrderLicenceModel GetOrderLicenceByOrderLicenceGuid(Guid hpOrderLicenceGuid)
        {
            HPOrderLicence hpOrderLicenceEntity = Resolve<IHPOrderLicenceRepository>().GetOrderLicenceByOrderLicenceGuid(hpOrderLicenceGuid);
            HPOrderLicenceModel hpOrderLicenceModel = null;
            if (hpOrderLicenceEntity!=null)
            {
                hpOrderLicenceModel = new HPOrderLicenceModel
                {
                    HPOrderGUID = hpOrderLicenceEntity.HPOrderGUID,
                    HPOrderLicenceGUID = hpOrderLicenceEntity.HPOrderLicenceGUID,
                    Created = hpOrderLicenceEntity.Created.Value,
                    ProgramUserGUID = hpOrderLicenceEntity.ProgramUserGUID
                };
            }

            return hpOrderLicenceModel;
        }

        public HPOrderLicenceModel GetOrderLicenceByProgramUserGuid(Guid programUserGuid)
        {
            HPOrderLicence hpOrderLicenceEntity = Resolve<IHPOrderLicenceRepository>().GetOrderLicenceByProgramUserGuid(programUserGuid);
            HPOrderLicenceModel hpOrderLicenceModel = null;
            if (hpOrderLicenceEntity != null)
            {
                hpOrderLicenceModel = new HPOrderLicenceModel
                {
                    HPOrderGUID = hpOrderLicenceEntity.HPOrderGUID,
                    HPOrderLicenceGUID = hpOrderLicenceEntity.HPOrderLicenceGUID,
                    Created = hpOrderLicenceEntity.Created.Value,
                    ProgramUserGUID = hpOrderLicenceEntity.ProgramUserGUID
                };
            }

            return hpOrderLicenceModel;
        }

        public int GetUsedOrderLicencesByResellerGuidAndHPOrderGuid(Guid hpOrderGuid)
        {
            return Resolve<IHPOrderLicenceRepository>().GetUsedOrderLicencesByHPOrderGuid(hpOrderGuid).Count();
        }

        public ValidateOrderLicenceResponseModel ValidateHPOrderLicence(Guid orderGuid, Guid programGuid)
        {
            ValidateOrderLicenceResponseModel responseModel = new ValidateOrderLicenceResponseModel();
            try
            {
                HPOrder orderEntity = Resolve<IHPOrderRepository>().GetHPOrderByOrderGuid(orderGuid);
                int usedProgramLicences = Resolve<IHPOrderLicenceRepository>().GetUsedOrderLicencesByHPOrderGuid(orderGuid).ToList().Count();
                if (programGuid == orderEntity.HPProgramGUID)
                {
                    if (orderEntity.StartDate.Value <= DateTime.UtcNow.Date && orderEntity.StopDate.Value >= DateTime.UtcNow.Date )
                    {
                        if (orderEntity.HPOrderStatusID == (int)OrderStatusEnum.Active)
                        {
                            if (usedProgramLicences + 1 <= orderEntity.NumberOfEmployees.Value)
                            {
                                responseModel.Result = ResultEnum.Success;
                            }
                            else
                            {
                                // has't ProgramLicence 
                                responseModel.Result = ResultEnum.Error;
                                responseModel.LoginFailedType = LoginFailedTypeEnum.NoOrderLicence;
                            }
                        }
                        else
                        {
                            // Order Cancelled 
                            responseModel.Result = ResultEnum.Error;
                            responseModel.LoginFailedType = LoginFailedTypeEnum.OrderHasCancelled;
                        }
                    }
                    else
                    {
                        //Order Expired
                        responseModel.Result = ResultEnum.Error;
                        responseModel.LoginFailedType = LoginFailedTypeEnum.OrderHasExpired;
                    }
                }
                else
                {
                    //Program don't in current Order .
                    responseModel.Result = ResultEnum.Error;
                    responseModel.LoginFailedType = LoginFailedTypeEnum.NoOrderLicence;
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method Name {0}", System.Reflection.MethodBase.GetCurrentMethod().Name));
                throw ex;
            }

            return responseModel;
        }

        public void Update(HPOrderLicenceModel hpOrderLicenceModel)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid hpOrderLicenceGuid)
        {
            throw new NotImplementedException();
        }

        public void DeleteEntityByProgramUserGuid(Guid programUserGuid)
        {
            throw new NotImplementedException();
        }
    }
}
