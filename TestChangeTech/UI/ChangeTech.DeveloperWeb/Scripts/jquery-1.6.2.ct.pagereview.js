/*
*   json2.js
*/

(function ($) {

    $.widget("ct.pagereview", {

        options: {
            sessionGuid: null,
            programPage: null,
            sessionPage: null,
            sessionPages: null,
            userGuid: null,
            previousPage: 'PageReview',
            querystr_sessionGuid: 'SessionGUID',
            querystr_pageSequenceGuid: 'PgSequenceGUID',
            querystr_pageGuid: 'PgGUID',
            querystr_pageOrder: 'PgOrder',
            querystr_userGuid: 'UserGUID',
            querystr_readonly: 'ReadOnly',
            querystr_programPage: 'ProgramPg',
            querystr_sessionPage: 'SessionPg',
            querystr_previousPage: 'PrePg',
            querystr_mode: 'PresenterMode',
            mode: 'PagePresenterImage',
            displaySelector: '#pageContents',
            tableName: 'pageContents',
            editPageHref: 'EditPage.aspx',
            imageManagerHref: 'ImageManager.aspx',
            currentRowId: null,
            currentPageId: null,
            currentPageSequenceId: null,
            currentPageOrder: null,
            currentType: null,
            afterRender: null
        },

        _create: function () {
            var widget = this;
            if (widget.options.sessionPages == null) {
                CT.Program.GetPagesOfSession(widget, widget.options.sessionGuid, function (widget, data) {
                    widget.options.sessionPages = data;
                    widget.loadPageReview(widget.options.sessionPages);
                });
            }
            else {
                widget.loadPageReview(widget.options.sessionPages);
            }
        },

        loadPageReview: function (sessionPages) {
            var widget = this;
            var widgetId = widget.element[0].id;
            var tableId = widget.options.tableName;

            for (var r = 0; r < sessionPages.length; r++) {
                var row = sessionPages[r];
                var rowId = tableId + '-' + r;
                var rowTr = '<tr id="' + rowId + '" ></tr>';
                $('#' + tableId).append(rowTr);
                ////                if (sessionPages[r].TemplateName == 'Standard')
                ////                    $('#' + rowId).removeClass('ct-bg-DarkGray').addClass('ct-bg-LightGray');
                ////                else
                ////                    $('#' + rowId).removeClass('ct-bg-LightGray').addClass('ct-bg-DarkGray');

                var colTd = '';

                //SequenceOrder
                var sequenceOrderId = rowId + '-sequenceOrder';
                var arrowUpId = rowId + '-arrowUp';
                var arrowUpImageId = rowId + '-arrowUpImage';
                var arrowDownId = rowId + '-arrowDown';
                var arrowDownImageId = rowId + '-arrowDownImage';

                if (r == 0)
                    colTd = '<td id="' + sequenceOrderId + '" ><p class="counter">' + sessionPages[r].SequenceOrder + '</p><input type="submit" id="' + arrowDownImageId + '"  class="button-down" value="" /></td>';
                else if (r > 0 && r < sessionPages.length - 1)
                    colTd = '<td id="' + sequenceOrderId + '" ><input type="submit" id="' + arrowUpImageId + '"  class="button-up" value="" /><p class="counter">' + sessionPages[r].SequenceOrder + '</p><input type="submit" id="' + arrowDownImageId + '"  class="button-down" value="" /></td>';
                else
                    colTd = '<td id="' + sequenceOrderId + '" ><input type="submit" id="' + arrowUpImageId + '"  class="button-up" value="" /><p class="counter">' + sessionPages[r].SequenceOrder + '</p></td>';

                $('#' + rowId).append(colTd);
                $('#' + sequenceOrderId).click(widget._click).data('widget', widget).data('pageGuid', sessionPages[r].ID).data('pageSequenceGUID', sessionPages[r].PageSequenceGUID).data('order', sessionPages[r].Order).data('id', sequenceOrderId).data('type', null).data('content', null);
                //up
                $('#' + arrowUpId).click(widget._click).data('widget', widget).data('pageGuid', sessionPages[r].ID).data('pageSequenceGUID', sessionPages[r].PageSequenceGUID).data('order', sessionPages[r].Order).data('id', arrowUpId).data('type', null).data('content', null);
                if (r > 0)
                    $('#' + arrowUpImageId).click(widget._sortOrder).data('widget', widget).data('pageGuid', sessionPages[r].ID).data('pageSequenceGUID', sessionPages[r].PageSequenceGUID).data('order', sessionPages[r].Order).data('targetPageGuid', sessionPages[r - 1].ID).data('targetPageSequenceGUID', sessionPages[r - 1].PageSequenceGUID).data('targetOrder', sessionPages[r - 1].Order).data('type', 'up');
                //down
                if (arrowDownId != undefined)
                    $('#' + arrowDownId).click(widget._click).data('widget', widget).data('pageGuid', sessionPages[r].ID).data('pageSequenceGUID', sessionPages[r].PageSequenceGUID).data('order', sessionPages[r].Order).data('id', arrowDownId).data('type', null).data('content', null);
                if (r < sessionPages.length - 1)
                    $('#' + arrowDownImageId).click(widget._sortOrder).data('widget', widget).data('pageGuid', sessionPages[r].ID).data('pageSequenceGUID', sessionPages[r].PageSequenceGUID).data('order', sessionPages[r].Order).data('targetPageGuid', sessionPages[r + 1].ID).data('targetPageSequenceGUID', sessionPages[r + 1].PageSequenceGUID).data('targetOrder', sessionPages[r + 1].Order).data('type', 'down');

                //Order
                var orderId = rowId + '-order';
                colTd = '<td id="' + orderId + '"><p class="counter-seq">' + sessionPages[r].Order + '</p></td>';
                $('#' + rowId).append(colTd);
                $('#' + orderId).click(widget._click).data('widget', widget).data('pageGuid', sessionPages[r].ID).data('pageSequenceGUID', sessionPages[r].PageSequenceGUID).data('order', sessionPages[r].Order).data('id', orderId).data('type', null).data('content', null);

                //PresenterImageUrl
                var presenterImageUrlId = rowId + '-presenterImageUrl';
                var presenterImageId = rowId + '-presenterImage';
                if (sessionPages[r].PresenterImageUrl != null)
                    colTd = '<td id="' + presenterImageUrlId + '"><input id="' + presenterImageId + '" type="image" src="' + sessionPages[r].PresenterImageUrl + '" class="ct-image" /></td>';
                else
                    colTd = '<td id="' + presenterImageUrlId + '"></td>';
                $('#' + rowId).append(colTd);
                $('#' + presenterImageUrlId).click(widget._click).data('widget', widget).data('pageGuid', sessionPages[r].ID).data('pageSequenceGUID', sessionPages[r].PageSequenceGUID).data('order', sessionPages[r].Order).data('id', presenterImageUrlId).data('type', null).data('content', null);
                $('#' + presenterImageId).click(widget._presenterImage).data('widget', widget).data('pageGuid', sessionPages[r].ID).data('pageSequenceGUID', sessionPages[r].PageSequenceGUID).data('order', sessionPages[r].Order);

                //Heading & Body & Button
                var headingId = rowId + '-heading';
                var bodyId = rowId + '-body';
                var primaryButtonCaptionId = rowId + '-pbc';
                colTd = '<td><textarea name="textfield2" rows="1" class="textfield-heading" id="' + headingId + '">' + sessionPages[r].Heading + '</textarea><textarea name="textfield2" rows="7" class="textfield-body" id="' + bodyId + '">' + sessionPages[r].Body + '</textarea><input name="textfield2" type="text" class="textfield-button" id="' + primaryButtonCaptionId + '" value="' + sessionPages[r].PrimaryButtonCaption + '" /></td>';
                $('#' + rowId).append(colTd);
                $('#' + headingId).click(widget._click).data('widget', widget).data('pageGuid', sessionPages[r].ID).data('pageSequenceGUID', sessionPages[r].PageSequenceGUID).data('order', sessionPages[r].Order).data('id', headingId).data('content', sessionPages[r].Heading).data('type', 'Heading');
                $('#' + bodyId).click(widget._click).data('widget', widget).data('pageGuid', sessionPages[r].ID).data('pageSequenceGUID', sessionPages[r].PageSequenceGUID).data('order', sessionPages[r].Order).data('id', bodyId).data('content', sessionPages[r].Body).data('type', 'Body');
                $('#' + primaryButtonCaptionId).click(widget._click).data('widget', widget).data('pageGuid', sessionPages[r].ID).data('pageSequenceGUID', sessionPages[r].PageSequenceGUID).data('order', sessionPages[r].Order).data('id', primaryButtonCaptionId).data('content', sessionPages[r].PrimaryButtonCaption).data('type', 'PrimaryButtonCaption');

                //                //Body
                //                var bodyId = rowId + '-body';
                //                colTd = '<td id="' + bodyId + '">' + sessionPages[r].Body + '</td>';
                //                $('#' + rowId).append(colTd);
                //                $('#' + bodyId).click(widget._click).data('widget', widget).data('pageGuid', sessionPages[r].ID).data('pageSequenceGUID', sessionPages[r].PageSequenceGUID).data('order', sessionPages[r].Order).data('id', bodyId).data('content', sessionPages[r].Body).data('type', 'Body');

                //                //PrimaryButtonCaption
                //                var primaryButtonCaptionId = rowId + '-pbc';
                //                colTd = '<td id="' + primaryButtonCaptionId + '">' + sessionPages[r].PrimaryButtonCaption + '</td>';
                //                $('#' + rowId).append(colTd);
                //                $('#' + primaryButtonCaptionId).click(widget._click).data('widget', widget).data('pageGuid', sessionPages[r].ID).data('pageSequenceGUID', sessionPages[r].PageSequenceGUID).data('order', sessionPages[r].Order).data('id', primaryButtonCaptionId).data('content', sessionPages[r].PrimaryButtonCaption).data('type', 'PrimaryButtonCaption');

                //BeforeShowExpression
                var beforeShowExpressionId = rowId + '-beforeShowExpression';
                if (sessionPages[r].BeforeShowExpression != null)
                    colTd = '<td id="' + beforeShowExpressionId + '">' + sessionPages[r].BeforeShowExpression + '</td>';
                else
                    colTd = '<td id="' + beforeShowExpressionId + '"></td>';
                $('#' + rowId).append(colTd);
                $('#' + beforeShowExpressionId).click(widget._click).data('widget', widget).data('pageGuid', sessionPages[r].ID).data('pageSequenceGUID', sessionPages[r].PageSequenceGUID).data('order', sessionPages[r].Order).data('id', beforeShowExpressionId).data('type', null).data('content', null);

                //AfterShowExpression
                var afterShowExpressionId = rowId + '-afterShowExpression';
                if (sessionPages[r].AfterShowExpression != null)
                    colTd = '<td id="' + afterShowExpressionId + '">' + sessionPages[r].AfterShowExpression + '</td>';
                else
                    colTd = '<td id="' + afterShowExpressionId + '"></td>';
                $('#' + rowId).append(colTd);
                $('#' + afterShowExpressionId).click(widget._click).data('widget', widget).data('pageGuid', sessionPages[r].ID).data('pageSequenceGUID', sessionPages[r].PageSequenceGUID).data('order', sessionPages[r].Order).data('id', afterShowExpressionId).data('type', null).data('content', null);

                //TemplateName
                var templateNameId = rowId + '-templateName';
                if (sessionPages[r].TemplateName != null)
                    colTd = '<td id="' + templateNameId + '">' + sessionPages[r].TemplateName + '</td>';
                else
                    colTd = '<td id="' + templateNameId + '"></td>';
                $('#' + rowId).append(colTd);
                $('#' + templateNameId).click(widget._click).data('widget', widget).data('pageGuid', sessionPages[r].ID).data('pageSequenceGUID', sessionPages[r].PageSequenceGUID).data('order', sessionPages[r].Order).data('id', templateNameId).data('type', null).data('content', null);

                //Buttons
                var editId = rowId + '-Edit';
                var newId = rowId + '-New';
                var deleteId = rowId + '-Delete';
                var buttonId = rowId + '-button';
                var selectOperateId = rowId + '-selectOperate';

                //<input type="button" id="' + newId + '" class="button-open" value="New Page" /><input type="submit" id="' + deleteId + '" class="button-delete" value="Delete" />
                colTd = '<td id="' + buttonId + '"><div class="buttons"><input type="button" style="width:150px;font-size: 14px;background: url(../gfx/icon-arrow-right.png) 132px 6px no-repeat #0084bc;" id="' + editId + '" class="button-open" value="Edit" /><select name="select" id="' + selectOperateId + '" class="listmenu-small"><option  selected="selected">More options...</option><option>New page</option><option>Delete page</option></select></div></td>';
                $('#' + rowId).append(colTd);
                $('#' + editId).click(widget._editPage).data('widget', widget).data('pageGuid', sessionPages[r].ID).data('pageSequenceGUID', sessionPages[r].PageSequenceGUID).data('userGuid', widget.options.userGuid);
                //$('#' + newId).click(widget._newPage).data('widget', widget).data('pageGuid', sessionPages[r].ID).data('pageSequenceGUID', sessionPages[r].PageSequenceGUID).data('order', sessionPages[r].Order);
                //$('#' + deleteId).click(widget._deletePage).data('widget', widget).data('pageGuid', sessionPages[r].ID).data('pageSequenceGUID', sessionPages[r].PageSequenceGUID).data('order', sessionPages[r].Order);
                $('#' + selectOperateId).change(widget._selectOperate).data('widget', widget).data('pageGuid', sessionPages[r].ID).data('pageSequenceGUID', sessionPages[r].PageSequenceGUID).data('order', sessionPages[r].Order);
                $('#' + buttonId).click(widget._click).data('widget', widget).data('pageGuid', sessionPages[r].ID).data('pageSequenceGUID', sessionPages[r].PageSequenceGUID).data('order', sessionPages[r].Order).data('id', buttonId).data('type', null).data('content', null);

                //                //ArrowUp
                //                var arrowUpId = rowId + '-arrowUp';
                //                var arrowUpImageId = rowId + '-arrowUpImage';
                //                if (r > 0)
                //                    colTd = '<td id="' + arrowUpId + '"><input id="' + arrowUpImageId + '" type="image" src="Images/arrow_up_blue.gif" /></td>';
                //                else
                //                    colTd = '<td id="' + arrowUpId + '"></td>';
                //                $('#' + rowId).append(colTd);
                //                $('#' + arrowUpId).click(widget._click).data('widget', widget).data('pageGuid', sessionPages[r].ID).data('pageSequenceGUID', sessionPages[r].PageSequenceGUID).data('order', sessionPages[r].Order).data('id', arrowUpId).data('type', null).data('content', null);
                //                if (r > 0)
                //                    $('#' + arrowUpImageId).click(widget._sortOrder).data('widget', widget).data('pageGuid', sessionPages[r].ID).data('pageSequenceGUID', sessionPages[r].PageSequenceGUID).data('order', sessionPages[r].Order).data('targetPageGuid', sessionPages[r - 1].ID).data('targetPageSequenceGUID', sessionPages[r - 1].PageSequenceGUID).data('targetOrder', sessionPages[r - 1].Order).data('type', 'up');

                //                //ArrowDown
                //                var arrowDownId = rowId + '-arrowDown';
                //                var arrowDownImageId = rowId + '-arrowDownImage';
                //                if (r < sessionPages.length - 1)
                //                    colTd = '<td id="' + arrowDownId + '"><input id="' + arrowDownImageId + '" type="image" src="Images/arrow_down_blue.gif" /></td>';
                //                else
                //                    colTd = '<td id="' + arrowDownId + '"></td>';
                //                $('#' + rowId).append(colTd);
                //                $('#' + arrowDownId).click(widget._click).data('widget', widget).data('pageGuid', sessionPages[r].ID).data('pageSequenceGUID', sessionPages[r].PageSequenceGUID).data('order', sessionPages[r].Order).data('id', arrowDownId).data('type', null).data('content', null);
                //                if (r < sessionPages.length - 1)
                //                    $('#' + arrowDownImageId).click(widget._sortOrder).data('widget', widget).data('pageGuid', sessionPages[r].ID).data('pageSequenceGUID', sessionPages[r].PageSequenceGUID).data('order', sessionPages[r].Order).data('targetPageGuid', sessionPages[r + 1].ID).data('targetPageSequenceGUID', sessionPages[r + 1].PageSequenceGUID).data('targetOrder', sessionPages[r + 1].Order).data('type', 'down');

            }
            $('#' + tableId + 'Head').click(widget._click).data('widget', widget).data('pageGuid', null).data('pageSequenceGUID', null).data('order', null).data('id', tableId).data('type', null).data('content', null);

        },

        _click: function () {
            var widget = $(this).data('widget');
            var pageGuid = $(this).data('pageGuid');
            var pageSequenceGUID = $(this).data('pageSequenceGUID');
            var userGuid = $(this).data('userGuid');
            var order = $(this).data('order');
            var id = $(this).data('id');
            var type = $(this).data('type');
            var content = $(this).data('content');

            if (widget.options.currentRowId != null) {
                var activeId = widget.options.currentRowId;
                var activePageId = widget.options.currentPageId;
                var activePageSequenceId = widget.options.currentPageSequenceId;
                var activePageOrder = widget.options.currentPageOrder;
                var activeType = widget.options.currentType;
                var activeValue = $('#' + activeId)[0].value;
                switch (activeType) {
                    case "Heading":
                        CT.Program.IsPageHasMoreReference(widget, widget.options.sessionGuid, activePageSequenceId, function (widget, data) {
                            var isPageHasMoreReference = data;
                            if (isPageHasMoreReference) {
                                if (confirm("Do you want to imfact other session which used this pageSquence?\n if 'yes' press 'ok'\n if 'no' press 'cancel'")) {
                                    var pageContent = {
                                        IsUpdatePageSequence: true,
                                        SessionGUID: widget.options.sessionGuid,
                                        PageSequenceGUID: activePageSequenceId,
                                        PageGUID: activePageId,
                                        PageOrder: activePageOrder,
                                        Heading: activeValue,
                                        Body: null,
                                        PrimaryButtonCaption: null
                                    };
                                    CT.Program.UpdatePageContentForPageReview(widget, pageContent, function (widget, data) {
                                    });
                                }
                                else {
                                    var pageContent = {
                                        IsUpdatePageSequence: false,
                                        SessionGUID: widget.options.sessionGuid,
                                        PageSequenceGUID: activePageSequenceId,
                                        PageGUID: activePageId,
                                        PageOrder: activePageOrder,
                                        Heading: activeValue,
                                        Body: null,
                                        PrimaryButtonCaption: null
                                    };
                                    CT.Program.UpdatePageContentForPageReview(widget, pageContent, function (widget, data) {
                                    });
                                }
                            }
                            else {
                                var pageContent = {
                                    IsUpdatePageSequence: false,
                                    SessionGUID: widget.options.sessionGuid,
                                    PageSequenceGUID: activePageSequenceId,
                                    PageGUID: activePageId,
                                    PageOrder: activePageOrder,
                                    Heading: activeValue,
                                    Body: null,
                                    PrimaryButtonCaption: null
                                };
                                CT.Program.UpdatePageContentForPageReview(widget, pageContent, function (widget, data) {
                                });
                            }
                        });
                        break;
                    case "Body":
                        CT.Program.IsPageHasMoreReference(widget, widget.options.sessionGuid, activePageSequenceId, function (widget, data) {
                            var isPageHasMoreReference = data;
                            if (isPageHasMoreReference) {
                                if (confirm("Do you want to imfact other session which used this pageSquence?\n if 'yes' press 'ok'\n if 'no' press 'cancel'")) {
                                    var pageContent = {
                                        IsUpdatePageSequence: true,
                                        SessionGUID: widget.options.sessionGuid,
                                        PageSequenceGUID: activePageSequenceId,
                                        PageGUID: activePageId,
                                        PageOrder: activePageOrder,
                                        Heading: null,
                                        Body: activeValue,
                                        PrimaryButtonCaption: null
                                    };
                                    CT.Program.UpdatePageContentForPageReview(widget, pageContent, function (widget, data) {
                                    });
                                }
                                else {
                                    var pageContent = {
                                        IsUpdatePageSequence: false,
                                        SessionGUID: widget.options.sessionGuid,
                                        PageSequenceGUID: activePageSequenceId,
                                        PageGUID: activePageId,
                                        PageOrder: activePageOrder,
                                        Heading: null,
                                        Body: activeValue,
                                        PrimaryButtonCaption: null
                                    };
                                    CT.Program.UpdatePageContentForPageReview(widget, pageContent, function (widget, data) {
                                    });
                                }
                            }
                            else {
                                var pageContent = {
                                    IsUpdatePageSequence: false,
                                    SessionGUID: widget.options.sessionGuid,
                                    PageSequenceGUID: activePageSequenceId,
                                    PageGUID: activePageId,
                                    PageOrder: activePageOrder,
                                    Heading: null,
                                    Body: activeValue,
                                    PrimaryButtonCaption: null
                                };
                                CT.Program.UpdatePageContentForPageReview(widget, pageContent, function (widget, data) {
                                });
                            }
                        });
                        break;
                    case "PrimaryButtonCaption":
                        CT.Program.IsPageHasMoreReference(widget, widget.options.sessionGuid, activePageSequenceId, function (widget, data) {
                            var isPageHasMoreReference = data;
                            if (isPageHasMoreReference) {
                                if (confirm("Do you want to imfact other session which used this pageSquence?\n if 'yes' press 'ok'\n if 'no' press 'cancel'")) {
                                    var pageContent = {
                                        IsUpdatePageSequence: true,
                                        SessionGUID: widget.options.sessionGuid,
                                        PageSequenceGUID: activePageSequenceId,
                                        PageGUID: activePageId,
                                        PageOrder: activePageOrder,
                                        Heading: null,
                                        Body: null,
                                        PrimaryButtonCaption: activeValue
                                    };
                                    CT.Program.UpdatePageContentForPageReview(widget, pageContent, function (widget, data) {
                                    });
                                }
                                else {
                                    var pageContent = {
                                        IsUpdatePageSequence: false,
                                        SessionGUID: widget.options.sessionGuid,
                                        PageSequenceGUID: activePageSequenceId,
                                        PageGUID: activePageId,
                                        PageOrder: activePageOrder,
                                        Heading: null,
                                        Body: null,
                                        PrimaryButtonCaption: activeValue
                                    };
                                    CT.Program.UpdatePageContentForPageReview(widget, pageContent, function (widget, data) {
                                    });
                                }
                            }
                            else {
                                var pageContent = {
                                    IsUpdatePageSequence: false,
                                    SessionGUID: widget.options.sessionGuid,
                                    PageSequenceGUID: activePageSequenceId,
                                    PageGUID: activePageId,
                                    PageOrder: activePageOrder,
                                    Heading: null,
                                    Body: null,
                                    PrimaryButtonCaption: activeValue
                                };
                                CT.Program.UpdatePageContentForPageReview(widget, pageContent, function (widget, data) {
                                });
                            }
                        });
                        break;
                }               
                $('#' + activeId).val(activeValue);
                $('#' + activeId).click(widget._click).data('widget', widget).data('id', activeId).data('content', activeValue);
            }
            if (type != null) {
                widget.options.currentRowId = id;
                widget.options.currentPageId = pageGuid;
                widget.options.currentPageSequenceId = pageSequenceGUID;
                widget.options.currentPageOrder = order;

                widget.options.currentType = type;
                //var inputText = '<textarea class="ct-edit-textarea" >' + content + '</textarea>'
                //$('#' + id).empty().append(inputText);
                $('#' + id).unbind('click');
            }
            else {
                widget.options.currentRowId = null;
                widget.options.currentPageId = null;
                widget.options.currentType = null;
            }

        },

        _editPage: function () {
            var widget = $(this).data('widget');
            var pageGuid = $(this).data('pageGuid');
            var pageSequenceGUID = $(this).data('pageSequenceGUID');
            var userGUID = $(this).data('userGuid');

            var editPageHref = '';
            if (widget.options.editPageHref != null) {
                editPageHref += widget.options.editPageHref;
                if (widget.options.sessionGuid != null) {
                    editPageHref += '?' + widget.options.querystr_sessionGuid + '=' + widget.options.sessionGuid;
                    editPageHref += '&' + widget.options.querystr_pageSequenceGuid + '=' + pageSequenceGUID;
                    editPageHref += '&' + widget.options.querystr_pageGuid + '=' + pageGuid;
                    editPageHref += '&' + widget.options.querystr_userGuid + '=' + userGUID;
                    editPageHref += '&' + widget.options.querystr_readonly + '=False';

                    if (widget.options.programPage != null) {
                        editPageHref += '&' + widget.options.querystr_programPage + '=' + widget.options.programPage;
                        if (widget.options.sessionPage != null) {
                            editPageHref += '&' + widget.options.querystr_programPage + '=' + widget.options.sessionPage;
                            if (widget.options.previousPage != null) {
                                editPageHref += '&' + widget.options.querystr_previousPage + '=' + widget.options.previousPage;
                                window.location = editPageHref;
                            }
                        }
                    }
                }
            }
        },

        _newPage: function () {
            var widget = $(this).data('widget');
            var pageGuid = $(this).data('pageGuid');
            var pageSequenceGUID = $(this).data('pageSequenceGUID');
            var order = $(this).data('order');
            var templateCount = $('#template')[0].length;
            if (templateCount == 0) {
                CT.Program.GetPageTemplates(widget, function (widget, data) {
                    for (var i = 0; i < data.length; i++) {
                        $('#template').append('<option value="' + data[i].Guid + '">' + data[i].Name + '</option>');
                    }
                });
            }
            else {
                $('#template').val($('#template')[0][0].value);
                $('#heading').val("");
                $('#body').val("");
                $('#primarybuttonname').val("");
            }
            $('#result').dialog({
                modal: true,
                buttons: {
                    Save: function () {
                        var simplePageContentModel = {
                            Order: order + 1,
                            ID: pageGuid,
                            PageSequenceGUID: pageSequenceGUID,
                            TemplateGUID: $('#template').val(),
                            Heading: $('#heading').val(),
                            Body: $('#body').val(),
                            PrimaryButtonCaption: $('#primarybuttonname').val()
                        };
                        CT.Program.SavePage(widget, simplePageContentModel, function (widget, data) {
                            $('#result').dialog('close');
                            window.location = window.location;
                        });
                    },
                    Cancel: function () {
                        $(this).dialog('close');
                    }
                },
                close: function (event, ui) {
                    if (window.location.hash.length > 1) {
                        document.location = window.location.hash.substring(1);
                    }
                }
            });
        },

        _selectOperate: function () {
            var widget = $(this).data('widget');
            var pageGuid = $(this).data('pageGuid');
            var pageSequenceGUID = $(this).data('pageSequenceGUID');
            var order = $(this).data('order');
            if ($(this).val() == 'New page') {
                var templateCount = $('#template')[0].length;
                if (templateCount == 0) {
                    CT.Program.GetPageTemplates(widget, function (widget, data) {
                        for (var i = 0; i < data.length; i++) {
                            $('#template').append('<option value="' + data[i].Guid + '">' + data[i].Name + '</option>');
                        }
                    });
                }
                else {
                    $('#template').val($('#template')[0][0].value);
                    $('#heading').val("");
                    $('#body').val("");
                    $('#primarybuttonname').val("");
                }
                $('#result').dialog({
                    modal: true,
                    buttons: {
                        Save: function () {
                            var simplePageContentModel = {
                                Order: order + 1,
                                ID: pageGuid,
                                PageSequenceGUID: pageSequenceGUID,
                                TemplateGUID: $('#template').val(),
                                Heading: $('#heading').val(),
                                Body: $('#body').val(),
                                PrimaryButtonCaption: $('#primarybuttonname').val()
                            };
                            CT.Program.SavePage(widget, simplePageContentModel, function (widget, data) {
                                $('#result').dialog('close');
                                window.location = window.location;
                            });
                        },
                        Cancel: function () {
                            $(this).dialog('close');
                        }
                    },
                    close: function (event, ui) {
                        if (window.location.hash.length > 1) {
                            document.location = window.location.hash.substring(1);
                        }
                    }
                });
            }
            else if ($(this).val() == 'Delete page') {
                if (confirm("You are deleting a page from current page sequence, are you sure you want to do this action?")) {
                    CT.Program.IsPageHasMoreReference(widget, widget.options.sessionGuid, pageSequenceGUID, function (widget, data) {
                        var isPageHasMoreReference = data;
                        if (isPageHasMoreReference) {
                            if (confirm("Do you want to imfact other session which used this pageSquence?\n if 'yes' press 'ok'\n if 'no' press 'cancel'")) {
                                var deleteModel = {
                                    IsUpdatePageSequence: true,
                                    SessionGUID: widget.options.sessionGuid,
                                    PageSequenceGUID: pageSequenceGUID,
                                    PageGUID: pageGuid,
                                    PageOrder: order
                                };
                                CT.Program.DeletePageForPageReview(widget, deleteModel, function (widget, data) {
                                    window.location = window.location;
                                });
                            }
                            else {
                                var deleteModel = {
                                    IsUpdatePageSequence: false,
                                    SessionGUID: widget.options.sessionGuid,
                                    PageSequenceGUID: pageSequenceGUID,
                                    PageGUID: pageGuid,
                                    PageOrder: order
                                };
                                CT.Program.DeletePageForPageReview(widget, deleteModel, function (widget, data) {
                                    window.location = window.location;
                                });
                            }
                        }
                        else {
                            var deleteModel = {
                                IsUpdatePageSequence: false,
                                SessionGUID: widget.options.sessionGuid,
                                PageSequenceGUID: pageSequenceGUID,
                                PageGUID: pageGuid,
                                PageOrder: order
                            };
                            CT.Program.DeletePageForPageReview(widget, deleteModel, function (widget, data) {
                                window.location = window.location;
                            });
                        }
                    });
                }
                else {
                    return false;
                }
            }
        },


        _presenterImage: function () {
            var widget = $(this).data('widget');
            var pageGuid = $(this).data('pageGuid');
            var pageSequenceGUID = $(this).data('pageSequenceGUID');
            var order = $(this).data('order');
            var mode = widget.options.mode;
            var imageManagerURL = widget.options.imageManagerHref;
            imageManagerURL += '?' + widget.options.querystr_sessionGuid + '=' + widget.options.sessionGuid;
            imageManagerURL += '&' + widget.options.querystr_pageSequenceGuid + '=' + pageSequenceGUID;
            imageManagerURL += '&' + widget.options.querystr_pageGuid + '=' + pageGuid;
            imageManagerURL += '&' + widget.options.querystr_pageOrder + '=' + order;
            imageManagerURL += '&' + widget.options.querystr_mode + '=' + mode;
            $('#imagemanager').attr('src', imageManagerURL);
            $('#presenterimage').dialog({
                width: 900,
                height: 700,
                modal: true,
                close: function (event, ui) {
                    if (window.location.hash.length > 1) {
                        document.location = window.location.hash.substring(1);
                    }
                }
            });
            return false;
        },

        _sortOrder: function () {
            var widget = $(this).data('widget');
            var pageGuid = $(this).data('pageGuid');
            var pageSequenceGUID = $(this).data('pageSequenceGUID');
            var order = $(this).data('order');
            var targetPageGuid = $(this).data('targetPageGuid');
            var targetPageSequenceGUID = $(this).data('targetPageSequenceGUID');
            var targetOrder = $(this).data('targetOrder');
            CT.Program.IsPageHasMoreReference(widget, widget.options.sessionGuid, pageSequenceGUID, function (widget, data) {
                var isPageHasMoreReference = data;
                if (isPageHasMoreReference) {
                    if (confirm("Do you want to imfact other session which used this pageSquence?\n if 'yes' press 'ok'\n if 'no' press 'cancel'")) {
                        var originalModel = {
                            IsUpdatePageSequence: true,
                            SessionGUID: widget.options.sessionGuid,
                            PageSequenceGUID: pageSequenceGUID,
                            PageGUID: pageGuid,
                            PageOrder: order
                        };
                        var swapToModel = {
                            IsUpdatePageSequence: true,
                            SessionGUID: widget.options.sessionGuid,
                            PageSequenceGUID: targetPageSequenceGUID,
                            PageGUID: targetPageGuid,
                            PageOrder: targetOrder
                        };
                        CT.Program.AdjustPageOrderForPageReview(widget, originalModel, swapToModel, function (widget, data) {
                            window.location = window.location;
                        });
                    }
                    else {
                        var originalModel = {
                            IsUpdatePageSequence: false,
                            SessionGUID: widget.options.sessionGuid,
                            PageSequenceGUID: pageSequenceGUID,
                            PageGUID: pageGuid,
                            PageOrder: order
                        };
                        var swapToModel = {
                            IsUpdatePageSequence: false,
                            SessionGUID: widget.options.sessionGuid,
                            PageSequenceGUID: targetPageSequenceGUID,
                            PageGUID: targetPageGuid,
                            PageOrder: targetOrder
                        };
                        CT.Program.AdjustPageOrderForPageReview(widget, originalModel, swapToModel, function (widget, data) {
                            window.location = window.location;
                        });
                    }
                }
                else {
                    var originalModel = {
                        IsUpdatePageSequence: false,
                        SessionGUID: widget.options.sessionGuid,
                        PageSequenceGUID: pageSequenceGUID,
                        PageGUID: pageGuid,
                        PageOrder: order
                    };
                    var swapToModel = {
                        IsUpdatePageSequence: false,
                        SessionGUID: widget.options.sessionGuid,
                        PageSequenceGUID: targetPageSequenceGUID,
                        PageGUID: targetPageGuid,
                        PageOrder: targetOrder
                    };
                    CT.Program.AdjustPageOrderForPageReview(widget, originalModel, swapToModel, function (widget, data) {
                        window.location = window.location;
                    });
                }
            });
            return false;
        },

        _deletePage: function () {
            var widget = $(this).data('widget');
            var pageGuid = $(this).data('pageGuid');
            var pageSequenceGUID = $(this).data('pageSequenceGUID');
            var order = $(this).data('order');
            if (confirm("You are deleting a page from current page sequence, are you sure you want to do this action?")) {
                CT.Program.IsPageHasMoreReference(widget, widget.options.sessionGuid, pageSequenceGUID, function (widget, data) {
                    var isPageHasMoreReference = data;
                    if (isPageHasMoreReference) {
                        if (confirm("Do you want to imfact other session which used this pageSquence?\n if 'yes' press 'ok'\n if 'no' press 'cancel'")) {
                            var deleteModel = {
                                IsUpdatePageSequence: true,
                                SessionGUID: widget.options.sessionGuid,
                                PageSequenceGUID: pageSequenceGUID,
                                PageGUID: pageGuid,
                                PageOrder: order
                            };
                            CT.Program.DeletePageForPageReview(widget, deleteModel, function (widget, data) {
                                window.location = window.location;
                            });
                        }
                        else {
                            var deleteModel = {
                                IsUpdatePageSequence: false,
                                SessionGUID: widget.options.sessionGuid,
                                PageSequenceGUID: pageSequenceGUID,
                                PageGUID: pageGuid,
                                PageOrder: order
                            };
                            CT.Program.DeletePageForPageReview(widget, deleteModel, function (widget, data) {
                                window.location = window.location;
                            });
                        }
                    }
                    else {
                        var deleteModel = {
                            IsUpdatePageSequence: false,
                            SessionGUID: widget.options.sessionGuid,
                            PageSequenceGUID: pageSequenceGUID,
                            PageGUID: pageGuid,
                            PageOrder: order
                        };
                        CT.Program.DeletePageForPageReview(widget, deleteModel, function (widget, data) {
                            window.location = window.location;
                        });
                    }
                });
            }
            else {
                return false;
            }
        },
        destroy: function () {
            // Should detach click event handlers.
            $.Widget.prototype.destroy.apply(this, arguments);
        },

        _setOption: function (key, value) {
            // Should allow properties to be changed?
            $.Widget.prototype._setOption.apply(this, arguments);
        }
    });

    $.extend($.ct.pagereview, {
        version: "@VERSION"
    });

})(jQuery);