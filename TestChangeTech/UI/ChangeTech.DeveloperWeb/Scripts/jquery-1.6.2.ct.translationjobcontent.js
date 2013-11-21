/*
*   json2.js
*/

(function ($) {

    $.widget("ct.translationjobcontent", {

        options: {
            translationJobGUID: null,
            translationJobContent: null,
            querystr_translationJobGUID: 'TranslationJobGUID',
            querystr_translationJobContentGUID: 'TranslationJobContentGUID',
            displaySelector: '#translationJobContents',
            tableName: 'translationJobContents',
            translateHref: 'ManageTranslationJobElement.aspx',
            currentRowId: null,
            currentContentId: null,
            //            currentJobId: null,
            currentType: null,
            afterRender: null
        },

        _create: function () {
            var widget = this;
            if (widget.options.translationJobContent == null) {
                CT.Program.GetTranslationJobContent(widget, widget.options.translationJobGUID, function (widget, data) {
                    widget.options.translationJobContent = data;
                    widget.loadTranslationJobContent(widget.options.translationJobContent);
                });
            }
            else {
                widget.loadTranslationJobContent(widget.options.translationJobContent);
            }
        },

        loadTranslationJobContent: function (translationJobContent) {
            var widget = this;
            var widgetId = widget.element[0].id;
            var tableId = widget.options.tableName;
            for (var r = 0; r < translationJobContent.length; r++) {
                var row = translationJobContent[r];
                var rowId = tableId + '-' + r;
                var rowTr = '<tr id="' + rowId + '" ></tr>';
                $('#' + tableId).append(rowTr);
//                if (r % 2 == 1)
//                    $('#' + rowId).removeClass('ct-bg-LightGray').addClass('ct-bg-DarkGray');
//                else

//                    $('#' + rowId).removeClass('ct-bg-DarkGray').addClass('ct-bg-LightGray');

                var colTd = '';

                //Session
                var sessionId = rowId + '-session';
                colTd = '<td id="' + sessionId + '"><p class="name">' + translationJobContent[r].ContentName + '</p></td>';
                $('#' + rowId).append(colTd);
                $('#' + sessionId).click(widget._click).data('widget', widget).data('id', sessionId).data('translationJobContentGuid', translationJobContent[r].TranslationJobContentGUID).data('type', null).data('content', null);

                //Elements
                var elementsId = rowId + '-elements';
                colTd = '<td id="' + elementsId + '"><p class="useramount">' + translationJobContent[r].Elements + '</p></td>';
                $('#' + rowId).append(colTd);
                $('#' + elementsId).click(widget._click).data('widget', widget).data('id', elementsId).data('translationJobContentGuid', translationJobContent[r].TranslationJobContentGUID).data('type', null).data('content', null);

                //Words
                var wordsId = rowId + '-words';
                colTd = '<td id="' + wordsId + '"><p class="useramount">' + translationJobContent[r].Words + '</p></td>';
                $('#' + rowId).append(colTd);
                $('#' + wordsId).click(widget._click).data('widget', widget).data('id', wordsId).data('translationJobContentGuid', translationJobContent[r].TranslationJobContentGUID).data('type', null).data('content', null);

                //Completed
                var completedId = rowId + '-completed';
                colTd = '<td id="' + completedId + '"><p class="useramount">' + translationJobContent[r].Completed + '</p></td>';
                $('#' + rowId).append(colTd);
                $('#' + completedId).click(widget._click).data('widget', widget).data('id', completedId).data('translationJobContentGuid', translationJobContent[r].TranslationJobContentGUID).data('type', null).data('content', null);

                //Notes
                var notesId = rowId + '-notes';
                colTd = '<td id="' + notesId + '">' + translationJobContent[r].Note + '</td>';
                $('#' + rowId).append(colTd);
                $('#' + notesId).click(widget._click).data('widget', widget).data('id', notesId).data('translationJobContentGuid', translationJobContent[r].TranslationJobContentGUID).data('type', 'Notes').data('content', translationJobContent[r].Note);

                //Button
                var translateId = rowId + '-translate';
                var buttonId = rowId + '-button';
                colTd = '<td id="' + buttonId + '"><input type="button" class="button-update" id="' + translateId + '" value="Translate" /></td>';
                $('#' + rowId).append(colTd);
                $('#' + translateId).click(widget._translate).data('widget', widget).data('translationJobContentGuid', translationJobContent[r].TranslationJobContentGUID);
                $('#' + buttonId).click(widget._click).data('widget', widget).data('id', buttonId).data('translationJobContentGuid', translationJobContent[r].TranslationJobContentGUID).data('type', null).data('content', null);

            }
            $('#' + tableId + 'Head').click(widget._click).data('widget', widget).data('id', tableId).data('translationJobContentGuid', null).data('type', null).data('content', null);

        },

        _click: function () {
            var widget = $(this).data('widget');
            var translationJobContentGuid = $(this).data('translationJobContentGuid');
            var id = $(this).data('id');
            var type = $(this).data('type');
            var content = $(this).data('content');

            if (widget.options.currentRowId != null) {
                var activeId = widget.options.currentRowId;
                var activeContentId = widget.options.currentContentId;
                var activeType = widget.options.currentType;
                var activeValue = $('#' + activeId + ' textarea')[0].value;
                switch (activeType) {
                    case "Notes":
                        var jobContent = {
                            TranslationJobContentGUID: activeContentId,
                            Note: activeValue
                        };
                        CT.Program.UpdateTranslationJobContent(widget, jobContent, function (widget, data) {
                        });
                        break;
                }
                $('#' + activeId).empty().append(activeValue);
                $('#' + activeId).click(widget._click).data('widget', widget).data('id', activeId).data('content', activeValue);
            }
            if (type != null) {
                widget.options.currentRowId = id;
                widget.options.currentContentId = translationJobContentGuid;
                widget.options.currentType = type;
                var inputText = '<textarea class="ct-edit-textarea" >' + content + '</textarea>'
                $('#' + id).empty().append(inputText);
                $('#' + id).unbind('click');
            }
            else {
                widget.options.currentRowId = null;
                widget.options.currentContentId = null;
                widget.options.currentType = null;
            }

        },

        _translate: function () {
            var widget = $(this).data('widget');
            var translationJobContentGuid = $(this).data('translationJobContentGuid');

            var translateHref = '';
            if (widget.options.translateHref != null) {
                translateHref += widget.options.translateHref;
                if (widget.options.translationJobGUID != null) {
                    translateHref += '?' + widget.options.querystr_translationJobGUID + '=' + widget.options.translationJobGUID;
                    translateHref += '&' + widget.options.querystr_translationJobContentGUID + '=' + translationJobContentGuid;
                    window.location = translateHref;
                }
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

    $.extend($.ct.translationjobcontent, {
        version: "@VERSION"
    });

})(jQuery);