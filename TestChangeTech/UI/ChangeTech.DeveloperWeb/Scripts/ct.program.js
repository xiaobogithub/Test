CT.Program = {

    GetPagesOfSession: function (context, sessionguid, action) {
        $.ajax({
            url: 'PageReviewService.svc/GetPagesOfSession',
            cache: false,
            type: 'POST',
            async: true,
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify({ sessionId: sessionguid }),
            context: { context: context, action: action },
            success: function (data) {
                this.action(this.context, data.GetPagesOfSessionResult);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status + ", " + thrownError);
            }
        });
    },

    GetPageTemplates: function (context, action) {
        $.ajax({
            url: 'PageReviewService.svc/GetPageTemplates',
            cache: false,
            type: 'POST',
            async: true,
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify({}),
            context: { context: context, action: action },
            success: function (data) {
                this.action(this.context, data.GetPageTemplatesResult);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status + ", " + thrownError);
            }
        });
    },

    SavePage: function (context, simplepagecontentmodel, action) {
        $.ajax({
            url: 'PageReviewService.svc/SavePage',
            cache: false,
            type: 'POST',
            async: true,
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify({ simplePageContentModel: simplepagecontentmodel }),
            context: { context: context, action: action },
            success: function (data) {
                this.action(this.context, data.SavePageResult);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status + ", " + thrownError);
            }
        });
    },

    UpdatePageContentForPageReview: function (context, pagecontent, action) {
        $.ajax({
            url: 'PageReviewService.svc/UpdatePageContentForPageReview',
            cache: false,
            type: 'POST',
            async: true,
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify({ pageContent: pagecontent }),
            context: { context: context, action: action },
            success: function (data) {
                this.action(this.context, data.UpdatePageContentForPageReviewResult);
                $("#successMsgDiv").removeClass().addClass("confirmationmessage");
                window.setTimeout(function () { $("#successMsgDiv").removeClass().addClass("confirmationmessage hidden"); }, 1000);

            },
            error: function (xhr, ajaxOptions, thrownError) {
                $("#errorMsgDiv").removeClass().addClass("alertmessage");
                //window.setTimeout(function () { $("#errorMsgDiv").removeClass().addClass("alertmessage hidden"); }, 5000);
            }
        });
    },

    IsPageHasMoreReference: function (context, sessionguid, pagesequenceguid, action) {
        $.ajax({
            url: 'PageReviewService.svc/IsPageHasMoreReference',
            cache: false,
            type: 'POST',
            async: true,
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify({ sessionId: sessionguid, pageSequenceId: pagesequenceguid }),
            context: { context: context, action: action },
            success: function (data) {
                this.action(this.context, data.IsPageHasMoreReferenceResult);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                //alert(xhr.status + ", " + thrownError);
            }
        });
    },

    DeletePageForPageReview: function (context, deletemodel, action) {
        $.ajax({
            url: 'PageReviewService.svc/DeletePageForPageReview',
            cache: false,
            type: 'POST',
            async: true,
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify({ deleteModel: deletemodel }),
            context: { context: context, action: action },
            success: function (data) {
                this.action(this.context, data.DeletePageForPageReviewResult);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status + ", " + thrownError);
            }
        });
    },

    AdjustPageOrderForPageReview: function (context, originalmodel, swaptomodel, action) {
        $.ajax({
            url: 'PageReviewService.svc/AdjustPageOrderForPageReview',
            cache: false,
            type: 'POST',
            async: true,
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify({ originalModel: originalmodel, swapToModel: swaptomodel }),
            context: { context: context, action: action },
            success: function (data) {
                this.action(this.context, data.AdjustPageOrderForPageReviewResult);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status + ", " + thrownError);
            }
        });
    },

    GetTranslationJobContent: function (context, translationjobguid, action) {
        $.ajax({
            url: window.location.protocol + '//' + window.location.host + '/Services/TranslationJobService.svc/GetTranslationJobContent', //'TranslationJobService.svc/GetTranslationJobContent',
            cache: false,
            type: 'POST',
            async: true,
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify({ translationJobId: translationjobguid }),
            context: { context: context, action: action },
            success: function (data) {
                this.action(this.context, data.GetTranslationJobContentResult);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status + ", " + thrownError);
            }
        });
    },

    UpdateTranslationJobContent: function (context, jobcontent, action) {
        $.ajax({
            url: window.location.protocol + '//' + window.location.host + '/Services/TranslationJobService.svc/UpdateTranslationJobContent',
            cache: false,
            type: 'POST',
            async: true,
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify({ jobContent: jobcontent }),
            context: { context: context, action: action },
            success: function (data) {
                this.action(this.context, data.UpdateTranslationJobContentResult);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status + ", " + thrownError);
            }
        });
    },

    GetTranslationJobElement: function (context, translationjobcontentguid, action) {
        $.ajax({
            url: window.location.protocol + '//' + window.location.host + '/Services/TranslationJobService.svc/GetTranslationJobElement',
            cache: false,
            type: 'POST',
            async: true,
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify({ translationJobContentId: translationjobcontentguid }),
            context: { context: context, action: action },
            success: function (data) {
                this.action(this.context, data.GetTranslationJobElementResult);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status + ", " + thrownError);
            }
        });
    },

    UpdateTranslationJobTranslated: function (context, jobtranslated, action) {
        $.ajax({
            url: window.location.protocol + '//' + window.location.host + '/Services/TranslationJobService.svc/UpdateTranslationJobTranslated',
            cache: false,
            type: 'POST',
            async: true,
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify({ jobTranslated: jobtranslated }),
            context: { context: context, action: action },
            success: function (data) {
                this.action(this.context, data.UpdateTranslationJobTranslatedResult);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status + ", " + thrownError);
            }
        });
    },

    GoogleTranslateForElement: function (context, googletranslateid, rowId, jobelement, action) {
        $.ajax({
            url: window.location.protocol + '//' + window.location.host + '/Services/TranslationJobService.svc/GoogleTranslateForElement',
            cache: false,
            type: 'POST',
            async: true,
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify({ jobElement: jobelement }),
            context: { context: context, action: action },
            success: function (data) {
                this.action(this.context, googletranslateid, rowId, jobelement, data.GoogleTranslateForElementResult);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                //DTD-1435
                if (xhr.status != 0) {
                    alert(xhr.status + ", " + thrownError);
                }
            }
        });
    },

    GetTranslationJobPagePreview: function (context, rowIdOfPreview, pageguid, translationjobcontentguid, action) {
        $.ajax({
            url: window.location.protocol + '//' + window.location.host + '/Services/TranslationJobService.svc/GetTranslationJobPagePreview',
            cache: false,
            type: 'POST',
            async: true,
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify({ pageId: pageguid, translationJobContentId: translationjobcontentguid }),
            context: { context: context, action: action },
            success: function (data) {
                this.action(this.context, rowIdOfPreview, data.GetTranslationJobPagePreviewResult);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                //DTD-1435
                if (xhr.status != 0) {
                    alert(xhr.status + ", " + thrownError);
                }
            }
        });
    },

    GetChoiceOfFlashOrHtml5: function (context, programCode, action) {
        $.ajax({
            url: 'ChangeTechService.svc/GetChoiceOfFlashOrHtml5',
            cache: false,
            type: 'POST',
            async: true,
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify({ ProgramCode: programCode }),
            context: { context: context, action: action },
            success: function (data) {
                this.action(this.context, data.GetChoiceOfFlashOrHtml5Result);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status + ", " + thrownError);
            }
        });
    },

    GetRedirectInfo: function (context, redirectParameterModel, action) {
        $.ajax({
            url: 'ChangeTechService.svc/GetRedirectInfo',
            cache: false,
            type: 'POST',
            async: true,
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify({ RedirectParameterModel: redirectParameterModel }),
            context: { context: context, action: action },
            success: function (data) {
                this.action(this.context, data.GetRedirectInfoResult);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status + ", " + thrownError);
            }
        });
    },

    GetSpecialStringValueForHtml5Incompatible: function (context, programCode, action) {
        $.ajax({
            url: 'ChangeTechService.svc/GetSpecialStringValueForHtml5Incompatible',
            cache: false,
            type: 'POST',
            async: true,
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify({ ProgramCode: programCode }),
            context: { context: context, action: action },
            success: function (data) {
                this.action(this.context, data.GetSpecialStringValueForHtml5IncompatibleResult);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status + ", " + thrownError);
            }
        });
    },

    GetProgramUserReport: function (context, action) {
        $.ajax({
            url: window.location.protocol + '//' + window.location.host + '/Services/MonitorService.svc/GetProgramUserReport',
            cache: false,
            type: 'POST',
            async: true,
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify({}),
            context: { context: context, action: action },
            success: function (data) {
                this.action(this.context, data.GetProgramUserReportResult);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status + ", " + thrownError);
            }
        });
    },

    GetProgramDailySMSTime: function (context, programGuid, action) {
        $.ajax({
            url: 'ManageDailyReportSMSService.svc/GetProgramDailySMSTime',
            cache: false,
            type: 'POST',
            async: true,
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify({ ProgramGuid: programGuid }),
            context: { context: context, action: action },
            success: function (data) {
                this.action(this.context, data.GetProgramDailySMSTimeResult);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status + ", " + thrownError);
            }
        });
    },

    UpdateProgramDailySMSTime: function (context, sourceContent, sourceId, programGuid, dailySMSTime, action) {
        $.ajax({
            url: 'ManageDailyReportSMSService.svc/UpdateProgramDailySMSTime',
            cache: false,
            type: 'POST',
            async: true,
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify({ ProgramGuid: programGuid, DailySMSTime: dailySMSTime }),
            context: { context: context, action: action },
            success: function (data) {
                this.action(this.context, sourceContent, sourceId, data.UpdateProgramDailySMSTimeResult);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status + ", " + thrownError);
            }
        });
    },

    GetDailySMSContentList: function (context, programGuid, action) {
        $.ajax({
            //url: 'ManageDailyReportSMSService.svc/GetDailySMSContentList',
            url: window.location.protocol + '//' + window.location.host + '/ManageDailyReportSMSService.svc/GetDailySMSContentList',
            cache: false,
            type: 'POST',
            async: true,
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify({ ProgramGuid: programGuid }),
            context: { context: context, action: action },
            success: function (data) {
                this.action(this.context, data.GetDailySMSContentListResult);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status + ", " + thrownError);
            }
        });
    },

    UpdateProgramDailySMSContentBySessionGuid: function (context, sessionGuid, newContent, action) {
        $.ajax({
            url: 'ManageDailyReportSMSService.svc/UpdateProgramDailySMSContentBySessionGuid',
            cache: false,
            type: 'POST',
            async: true,
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify({ SessionGuid: sessionGuid, NewContent: newContent }),
            context: { context: context, action: action },
            success: function (data) {
                this.action(this.context, data.UpdateProgramDailySMSContentBySessionGuidResult);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status + ", " + thrownError);
            }
        });
    },

    //Get TimeZoneOpts 
    GetTimeZoneOpts: function (context, programGuid, action) {
        $.ajax({
            url: 'ChangeTechService.svc/GetTimeZoneOpts',
            cache: false,
            type: 'POST',
            async: true,
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify({ ProgramGuid: programGuid }),
            context: { context: context, action: action },
            success: function (data) {
                this.action(this.context, data.GetTimeZoneOptsResult);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status + ", " + thrownError);
            }
        });
    },


    GetOrderPrograms: function (context, languageGuid, action) {
        $.ajax({
            url: window.location.protocol + '//' + window.location.host + '/Services/OrderSystemService.svc/GetOrderPrograms',
            cache: false,
            type: 'POST',
            async: true,
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify({ LanguageGuid: languageGuid }),
            context: { context: context, action: action },
            success: function (data) {
                this.action(this.context, data.GetOrderProgramsResult);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status + ',' + thrownError);
            }
        });
    }
};
