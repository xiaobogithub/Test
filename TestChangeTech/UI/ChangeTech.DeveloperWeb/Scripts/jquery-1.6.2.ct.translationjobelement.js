/*
*   json2.js
*/

(function ($) {

    $.widget("ct.translationjobelement", {

        options: {
            translationJobContentGUID: null,
            translationJobElement: null,
            validationSelector: '#validationsummary',
            displaySelector: '#translationJobElements',
            tableName: 'translationJobElements',
            currentRowId: null,
            currentElementId: null,
            currentType: null,
            afterRender: null
        },

        _create: function () {
            //debugger;
            var widget = this;
            if (widget.options.translationJobElement == null) {
                CT.Program.GetTranslationJobElement(widget, widget.options.translationJobContentGUID, function (widget, data) {
                    widget.options.translationJobElement = data;
                    widget.loadTranslationJobElement(widget.options.translationJobElement);
                });
            }
            else {
                widget.loadTranslationJobElement(widget.options.translationJobElement);
            }
        },

        loadTranslationJobElement: function (translationJobElement) {
            //debugger;
            var widget = this;
            var widgetId = widget.element[0].id;
            var tableId = widget.options.tableName;
            var row = null;
            if (translationJobElement.length > 0) {
                for (var i = 0; i < translationJobElement.length; i++) {
                    if (translationJobElement[i].Order.toString().substr(translationJobElement[i].Order.toString().length - 4, 4) != '0000') {
                        row = translationJobElement[i];
                        break;
                    }
                }
                row = translationJobElement[0];
            }
            var order = "";
            for (var r = 0; r < translationJobElement.length; r++) {
                if (r == 0) {
                    order = translationJobElement[r].Order.toString();
                }
                else {
                    if (order != translationJobElement[r].Order.toString() && translationJobElement.length != r + 1) {
                        var pageOrder = order.substr(order.length - 4, 4).toString();

                        //append empty line or pagePreview
                        if (pageOrder == '0000' || order.indexOf('9999') > -1) {//it is not page, but is session ,ps, or misc
                            var rowIdEmpty = tableId + '-' + r;
                            var rowTrEmpty = '<tr  style="height:5px"><td  colspan="6"></td></tr>';
                            $('#' + tableId).append(rowTrEmpty);
                        }
                        else {
                            if (row != null) {
                                var pageId = row.ToObjectGUID;
                                var rowId = tableId + '-' + r;
                                var rowIdPreview = rowId + '-' + 'Preview';
                                var rowTrPreview = '<tr  style="height:5px"><td  colspan="6" align="center"><input style="width:auto;" class="button-update-translater" type="button" id="' + rowIdPreview + '" value="Preview For Page" /></td></tr>';
                                $('#' + tableId).append(rowTrPreview);
                                CT.Program.GetTranslationJobPagePreview(widget, rowIdPreview, pageId, widget.options.translationJobContentGUID, function (widget, rowIdOfPreview, data) {
                                    if (data != null) {
                                        var previewModel = data;
                                        $('#' + rowIdOfPreview).click(widget._preview).data('widget', widget).data('preview', previewModel);
                                    }
                                });
                            }
                        }

                        //the follows assign the next order and row.
                        order = translationJobElement[r].Order.toString();
                        row = null;
                        for (var j = r; j < translationJobElement.length; j++) {
                            if (translationJobElement[j].Order.toString().substr(translationJobElement[j].Order.toString().length - 4, 4) != '0000') {
                                row = translationJobElement[j];
                                break;
                            }
                        }
                    }
                }
                var rowId = tableId + '-' + r;
                var rowTr = '<tr id="' + rowId + '" ></tr>';
                $('#' + tableId).append(rowTr);
//                if (r % 2 == 1)
//                    $('#' + rowId).removeClass('ct-bg-LightGray').addClass('ct-bg-DarkGray');
//                else

//                    $('#' + rowId).removeClass('ct-bg-DarkGray').addClass('ct-bg-LightGray');

                var colTd = '';

                //Element No
                var elementnoId = rowId + '-elementno';
                colTd = '<td id="' + elementnoId + '">' + (r + 1) + '</td>';
                $('#' + rowId).append(colTd);
                $('#' + elementnoId).click(widget._click).data('widget', widget).data('id', elementnoId).data('translationJobElement', translationJobElement[r]).data('type', null).data('content', null);

                //Position
                var positionId = rowId + '-position';
                colTd = '<td id="' + positionId + '"><p class="name">' + translationJobElement[r].Position + '</p></td>';
                $('#' + rowId).append(colTd);
                $('#' + positionId).click(widget._click).data('widget', widget).data('id', positionId).data('translationJobElement', translationJobElement[r]).data('type', null).data('content', null);

                //Original
                var originalStr = translationJobElement[r].Original;
                var reg = new RegExp("\r\n", "g");
                originalStr = originalStr.replace(reg, "<br/>");
                reg = new RegExp("\r", "g");
                originalStr = originalStr.replace(reg, "<br/>");
                reg = new RegExp("\n", "g");
                originalStr = originalStr.replace(reg, "<br/>");
                reg = new RegExp("<br/><br/>", "g");
                originalStr = originalStr.replace(reg, "<span class=\"emptyline\" style=\"display:block;\">&nbsp;</span>");
                var originalId = rowId + '-original';
                colTd = '<td id="' + originalId + '"><p class="description">' + originalStr + '</p></td>';
                $('#' + rowId).append(colTd);
                $('#' + originalId).click(widget._click).data('widget', widget).data('id', originalId).data('translationJobElement', translationJobElement[r]).data('type', null).data('content', null);
                //alert(typeof (translationJobElement[r].Original));
                //Max
                var maxId = rowId + '-max';
                colTd = '<td id="' + maxId + '"><p class="useramount">' + translationJobElement[r].MaxLength + '</p></td>';
                $('#' + rowId).append(colTd);
                $('#' + maxId).click(widget._click).data('widget', widget).data('id', maxId).data('translationJobElement', translationJobElement[r]).data('type', null).data('content', null);

                //Google Translate
                var googletranslateId = rowId + '-googletranslate';
                colTd = '<td id="' + googletranslateId + '"><p class="description">' + translationJobElement[r].GoogleTranslate + '</p></td>';
                $('#' + rowId).append(colTd);

                CT.Program.GoogleTranslateForElement(widget, googletranslateId, rowId, translationJobElement[r], function (widget, googletranslateId, rowId, jobelement, data) {
                    if (data != null) {
                        var activeData = data;
                        var reg = new RegExp("\r\n", "g");
                        activeData = activeData.replace(reg, "<br/>");
                        reg = new RegExp("\r", "g");
                        activeData = activeData.replace(reg, "<br/>");
                        reg = new RegExp("\n", "g");
                        activeData = activeData.replace(reg, "<br/>");
                        reg = new RegExp("<br/><br/>", "g");
                        activeData = activeData.replace(reg, "<span class=\"emptyline\" style=\"display:block;\">&nbsp;</span>");
                        $('#' + googletranslateId).empty().append(activeData);
                        if (jobelement.StatusID.toString() == '0') {
                            switch (jobelement.DefaultTranslatedContent.toString()) {
                                case '2':
                                    $('#' + rowId + '-translated').empty().append(data.replace("\r\n", "<br/>").replace("\r", "<br/>").replace("\n", "<br/>"));

                                    $('#' + rowId + '-translated').click(widget._click).data('widget', widget).data('id', rowId + '-translated').data('translationJobElement', jobelement).data('type', 'Translated').data('content', data);
                                    break;
                            }
                        }
                    }
                });
                $('#' + googletranslateId).click(widget._click).data('widget', widget).data('id', googletranslateId).data('translationJobElement', translationJobElement[r]).data('type', null).data('content', null);

                //Translated
                var translatedId = rowId + '-translated';
                if (translationJobElement[r].StatusID.toString() == '0') {
                    switch (translationJobElement[r].DefaultTranslatedContent.toString()) {
                        case '1':
                            var transOriginalStr = translationJobElement[r].Original;
                            var reg = new RegExp("\r\n", "g");
                            transOriginalStr = transOriginalStr.replace(reg, "<br/>");
                            reg = new RegExp("\r", "g");
                            transOriginalStr = transOriginalStr.replace(reg, "<br/>");
                            reg = new RegExp("\n", "g");
                            transOriginalStr = transOriginalStr.replace(reg, "<br/>");
                            reg = new RegExp("<br/><br/>", "g");
                            transOriginalStr = transOriginalStr.replace(reg, "<span class=\"emptyline\" style=\"display:block;\">&nbsp;</span>");
                            colTd = '<td id="' + translatedId + '"><p class="description">' + transOriginalStr + '</p></td>';
                            $('#' + rowId).append(colTd);
                            $('#' + translatedId).click(widget._click).data('widget', widget).data('id', translatedId).data('translationJobElement', translationJobElement[r]).data('type', 'Translated').data('content', translationJobElement[r].Original);
                            break;
                        case '2':
                            colTd = '<td id="' + translatedId + '"></td>';
                            $('#' + rowId).append(colTd);
                            $('#' + translatedId).click(widget._click).data('widget', widget).data('id', translatedId).data('translationJobElement', translationJobElement[r]).data('type', 'Translated').data('content', '');
                            break;
                        case '3':
                            colTd = '<td id="' + translatedId + '"></td>';
                            $('#' + rowId).append(colTd);
                            $('#' + translatedId).click(widget._click).data('widget', widget).data('id', translatedId).data('translationJobElement', translationJobElement[r]).data('type', 'Translated').data('content', '');
                            break;
                    }
                }
                else {
                    var transStr = translationJobElement[r].Translated;
                    var reg = new RegExp("\r\n", "g");
                    transStr = transStr.replace(reg, "<br/>");
                    reg = new RegExp("\r", "g");
                    transStr = transStr.replace(reg, "<br/>");
                    reg = new RegExp("\n", "g");
                    transStr = transStr.replace(reg, "<br/>");
                    reg = new RegExp("<br/><br/>", "g");
                    transStr = transStr.replace(reg, "<span class=\"emptyline\" style=\"display:block;\">&nbsp;</span>");
                    colTd = '<td id="' + translatedId + '">' + transStr + '</td>';
                    $('#' + rowId).append(colTd);
                    $('#' + translatedId).click(widget._click).data('widget', widget).data('id', translatedId).data('translationJobElement', translationJobElement[r]).data('type', 'Translated').data('content', translationJobElement[r].Translated);
                }
                //$('#' + translatedId).click(widget._click).data('widget', widget).data('id', translatedId).data('translationJobElement', translationJobElement[r]).data('type', 'Translated').data('content', translationJobElement[r].Translated);

                //add a preview in the last row
                if (row != null && translationJobElement.length == r + 1 && order.substr(order.length - 4, 4).toString() != '0000' && order.indexOf('9999') < 0) {
                    var pageId = row.ToObjectGUID;
                    var rowIdPreview = rowId + '-Preview';
                    var rowTrPreview = '<tr  style="height:5px"><td  colspan="6" align="center"><input style="width:auto;"  type="button" class="button-update-translater" id="' + rowIdPreview + '" value="Preview For Page" /></td></tr>';
                    $('#' + tableId).append(rowTrPreview);
                    CT.Program.GetTranslationJobPagePreview(widget, rowIdPreview, pageId, widget.options.translationJobContentGUID, function (widget, rowIdOfPreview, data) {
                        if (data != null) {
                            var previewModel = data;
                            $('#' + rowIdOfPreview).click(widget._preview).data('widget', widget).data('preview', previewModel);
                        }
                    });
                }
            }
            $('#' + tableId + 'Head').click(widget._click).data('widget', widget).data('id', tableId).data('translationJobElement', null).data('type', null).data('content', null);

        },

        _click: function () {
            //debugger;
            var widget = $(this).data('widget');
            var translationJobElement = $(this).data('translationJobElement');
            var id = $(this).data('id');
            var type = $(this).data('type');
            var content = $(this).data('content');

            if (widget.options.currentRowId != null) {
                var activeId = widget.options.currentRowId;
                var activeElement = widget.options.currentElement;
                var activeType = widget.options.currentType;
                var activeValue = $('#' + activeId + ' textarea')[0].value; //.text(); //.value;
                //alert(typeof (activeValue));

                //                var reg = new RegExp("&#xD;", "g");
                //                activeValue = activeValue.replace(reg, "\r");
                //                reg = new RegExp("&#xA;", "g");
                //                activeValue = activeValue.replace(reg, "\n");
                switch (activeType) {
                    case "Translated":
                        if (activeElement.MaxLength != null) {
                            //                            if (activeValue.length == 0) {
                            //                                $(widget.options.validationSelector).empty().append('Translated is required.');
                            //                                return;
                            //                            }
                            if (activeValue.length > activeElement.MaxLength) {
                                $(widget.options.validationSelector).empty().append('Please enter no more than ' + activeElement.MaxLength + ' characters.');
                                return;
                            }
                        }
                        var jobTranslated = {
                            TranslationJobContentGUID: activeElement.TranslationJobContentGUID,
                            TranslationJobElementGUID: activeElement.TranslationJobElementGUID,
                            ToObjectGUID: activeElement.ToObjectGUID,
                            Object: activeElement.Object,
                            Position: activeElement.Position,
                            Translated: activeValue
                        };
                        CT.Program.UpdateTranslationJobTranslated(widget, jobTranslated, function (widget, data) {
                        });
                        break;
                }
                var activeStr = activeValue;
                var reg = new RegExp("\r\n", "g");
                activeStr = activeStr.replace(reg, "<br/>");
                reg = new RegExp("\r", "g");
                activeStr = activeStr.replace(reg, "<br/>");
                reg = new RegExp("\n", "g");
                activeStr = activeStr.replace(reg, "<br/>");
                reg = new RegExp("<br/><br/>", "g");
                activeStr = activeStr.replace(reg, "<span class=\"emptyline\" style=\"display:block;\">&nbsp;</span>");

                //alert(activeValue)
                $('#' + activeId).empty().append(activeStr);
                $('#' + activeId).click(widget._click).data('widget', widget).data('id', activeId).data('content', activeValue);
            }
            if (type != null) {
                widget.options.currentRowId = id;
                widget.options.currentElement = translationJobElement;
                widget.options.currentType = type;
                //                var inputText = '<textarea class="ct-edit-textarea {validate: {required: true, maxlength: 10, messages: {required: \'Translated is required.\', maxlength: \'Please enter no more than ' + translationJobElement.MaxLength + ' characters.\'}}}" >' + content + '</textarea>'

                var inputText = '<textarea class="ct-edit-textarea" >' + content + '</textarea>';
                //var inputText = '<textarea class="ct-edit-textarea" >' + $('#' + id).text() + '</textarea>';
                $('#' + id).empty().append(inputText);
                $('#' + id).unbind('click');
            }
            else {
                widget.options.currentRowId = null;
                widget.options.currentElement = null;
                widget.options.currentType = null;
            }

        },

        _preview: function () {
            var widget = $(this).data('widget');
            var previewModel = $(this).data('preview');
            var queryString = '';

            var previewFlashHref = 'http://program.changetech.no/ChangeTechF.html';
            var previewHTML5Href = 'http://program.changetech.no/ChangeTech5.html';
            if (previewModel != null) {
                queryString += '?Mode=Preview&Object=Page';
                queryString += '&SessionGUID=' + previewModel.SessionGuid;
                queryString += '&PgSequenceGUID=' + previewModel.PageSequenceGuid;
                queryString += '&PgGUID=' + previewModel.PageGuid;
                queryString += '&LanguageGUID=' + previewModel.LanguageGuid;
                queryString += '&UserGUID=' + previewModel.UserGuid;

                previewFlashHref += queryString;
                previewHTML5Href += queryString;
                window.open(previewFlashHref);
                window.open(previewHTML5Href);
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

    $.extend($.ct.translationjobelement, {
        version: "@VERSION"
    });

})(jQuery);