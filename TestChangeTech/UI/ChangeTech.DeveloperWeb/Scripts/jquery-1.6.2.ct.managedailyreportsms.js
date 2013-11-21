/*
*   json2.js
*/

(function ($) {

    $.widget("ct.managedailyreportsms", {

        options: {
            programGuid: null,
            programPage: null,
            sessionPage: null,

            dailySMSContentList: null,
            dailySMSTimeHHmm: null,

            displaySelector: '#DailySMSTable',
            tableName: 'DailySMSTable',
            dailySMSTimeTD: 'dailySMSTimeTD',
            //dailySMSTimeUpdateTD: 'dailySMSTimeUpdate',
            dailySMSTimeNameTD: 'dailySMSTimeNameTD',
            dailySMSTimeUpdateTD: 'dailySMSTimeUpdateTD',

            currentRowId: null,
            currentDailySMSId: null,
            currentSessionId: null,
            currentType: null,
            currentDailySMSTimeType: null,
            currentDailySMSTimeRowId: null,
            currentDailySMSTimeContent: null
        },

        _create: function () {
            var widget = this;
            //For sms content list
            if (widget.options.dailySMSContentList == null) {
                CT.Program.GetDailySMSContentList(widget, widget.options.programGuid, function (widget, data) {
                    widget.options.dailySMSContentList = data;
                    widget.loadDailySMSContentList(widget.options.dailySMSContentList);
                });
            }
            else {
                widget.loadDailySMSContentList(widget.options.dailySMSContentList);
            }

            //For daily sms time
            if (widget.options.dailySMSTimeHHmm == null) {
                CT.Program.GetProgramDailySMSTime(widget, widget.options.programGuid, function (widget, data) {
                    widget.options.dailySMSTimeHHmm = data;
                    widget.loadDailySMSTimeHHmm(widget.options.dailySMSTimeHHmm);
                });
            }
            else {
                widget.loadDailySMSTimeHHmm(widget.options.dailySMSTimeHHmm);
            }
        },

        loadDailySMSTimeHHmm: function (dailySMSTime) {
            var widget = this;
            var vardailySMSTimeTD = widget.options.dailySMSTimeTD;
            var vardailySMSTimeNameTD = widget.options.dailySMSTimeNameTD;
            var vardailySMSTimeUpdateTD = widget.options.dailySMSTimeUpdateTD;
            //if (dailySMSTime != null && dailySMSTime != '') {
            //    if (dailySMSTime == null || dailySMSTime == "") {
            //        dailySMSTime = "00:00";
            //    }
            $('#' + vardailySMSTimeTD).append(dailySMSTime);
            $('#' + vardailySMSTimeTD).click(widget._updateDailySMSTime).data('widget', widget).data('programguid', widget.options.programGuid).data('id', vardailySMSTimeTD).data('type', 'dailySMSTime').data('content', dailySMSTime);
            //}
            $('#' + vardailySMSTimeNameTD).click(widget._updateDailySMSTime).data('widget', widget).data('programguid', widget.options.programGuid).data('id', vardailySMSTimeNameTD).data('type', null).data('content', null);
            $('#' + vardailySMSTimeUpdateTD).click(widget._updateDailySMSTime).data('widget', widget).data('programguid', widget.options.programGuid).data('id', vardailySMSTimeUpdateTD).data('type', null).data('content', null);
        },

        _updateDailySMSTime: function () {
            var widget = $(this).data('widget');
            var programGuid = $(this).data('programguid');
            var id = $(this).data('id');
            var type = $(this).data('type');
            var content = $(this).data('content');

            if (widget.options.currentDailySMSTimeRowId != null) {
                var activeId = widget.options.currentDailySMSTimeRowId;
                var activeType = widget.options.currentDailySMSTimeType;
                var activeValue = $('#' + widget.options.dailySMSTimeTD + ' input')[0].value;
                switch (activeType) {
                    case "dailySMSTime":
                        var newContent = activeValue;
                        //var activeValue = $('#' + activeId + ' textarea')[0].value;
                        CT.Program.UpdateProgramDailySMSTime(widget, widget.options.currentDailySMSTimeContent, activeId, programGuid, newContent, function (widget, sourceContent, sourceId, data) {
                            var validationResult = data;
                            if (data == false) {
                                $('#' + sourceId).empty().append(sourceContent);
                                $('#' + sourceId).unbind('click');
                                $('#' + sourceId).click(widget._updateDailySMSTime).data('widget', widget).data('id', sourceId).data('content', sourceContent);
                            }
                        });
                        break;
                }

                $('#' + activeId).empty().append(activeValue);
                $('#' + activeId).click(widget._updateDailySMSTime).data('widget', widget).data('id', activeId).data('content', activeValue);
            }
            if (type != null) {
                widget.options.currentDailySMSTimeRowId = id;
                widget.options.programGuid = programGuid;
                widget.options.currentDailySMSTimeType = type;
                widget.options.currentDailySMSTimeContent= content;
                //var inputText = '<textarea class="ct-edit-textarea" rows="1" >' + content + '</textarea>';
                var inputText = '<input type="text" maxlength="5" value="' + content + '"  class="textfield-extention" />';
                $('#' + id).empty().append(inputText);
                $('#' + id).unbind('click');
            }
            else {
                widget.options.currentDailySMSTimeRowId = null;
                widget.options.currentDailySMSTimeType = null;
                widget.options.currentDailySMSTimeContent= null;
            }
        },

        loadDailySMSContentList: function (dailySMSContentList) {
            var widget = this;
            var tableId = widget.options.tableName;
            for (var r = 0; r < dailySMSContentList.length; r++) {
                var row = dailySMSContentList[r];
                var rowId = tableId + '-' + r;
                var rowTr = '<tr id="' + rowId + '" ></tr>';
                $('#' + tableId).append(rowTr);

                var colTd = '';

                //SessionNumber
                var SessionNumberId = rowId + '-SessionNumber';
                colTd = '<td id="' + SessionNumberId + '"><p class="name">' + dailySMSContentList[r].SessionNum + '</p></td>';
                $('#' + rowId).append(colTd);
                $('#' + SessionNumberId).click(widget._click).data('widget', widget).data('sessionGuid', dailySMSContentList[r].SessionGuid).data('dailySMSGuid', dailySMSContentList[r].ProgramDailySMSGuid).data('id', SessionNumberId).data('type', null).data('content', null);

                //SessionDescription
                var SessionDescriptionId = rowId + '-SessionDescription';
                colTd = '<td id="' + SessionDescriptionId + '">' + dailySMSContentList[r].SessionDescription + '</td>';
                $('#' + rowId).append(colTd);
                $('#' + SessionDescriptionId).click(widget._click).data('widget', widget).data('sessionGuid', dailySMSContentList[r].SessionGuid).data('dailySMSGuid', dailySMSContentList[r].ProgramDailySMSGuid).data('id', SessionDescriptionId).data('type', null).data('content', null);

                //DailySMSContent
                var DailySMSContentId = rowId + '-DailySMSContent';
                colTd = '<td id="' + DailySMSContentId + '">' + dailySMSContentList[r].DailySMSContent + '</td>';
                $('#' + rowId).append(colTd);
                $('#' + DailySMSContentId).click(widget._click).data('widget', widget).data('sessionGuid', dailySMSContentList[r].SessionGuid).data('dailySMSGuid', dailySMSContentList[r].ProgramDailySMSGuid).data('id', DailySMSContentId).data('type', 'DailySMSContent').data('content', dailySMSContentList[r].DailySMSContent);

            }
            $('#' + tableId + 'Head').click(widget._click).data('widget', widget).data('sessionGuid', null).data('dailySMSGuid', null).data('id', tableId).data('type', null).data('content', null);
        },

        _click: function () {
            var widget = $(this).data('widget');
            var sessionGuid = $(this).data('sessionGuid');
            var dailySMSGuid = $(this).data('dailySMSGuid');
            var id = $(this).data('id');
            var type = $(this).data('type');
            var content = $(this).data('content');

            if (widget.options.currentRowId != null) {
                var activeId = widget.options.currentRowId;
                var activeDailySMSId = widget.options.currentDailySMSId;
                var activeSessionId = widget.options.currentSessionId;

                var activeType = widget.options.currentType;
                var activeValue = $('#' + activeId + ' textarea')[0].value;
                switch (activeType) {
                    case "DailySMSContent":
                        var newContent = activeValue;
                        CT.Program.UpdateProgramDailySMSContentBySessionGuid(widget, activeSessionId, newContent, function (widget, data) {
                        });
                        break;
                }
                $('#' + activeId).empty().append(activeValue);
                $('#' + activeId).click(widget._click).data('widget', widget).data('id', activeId).data('content', activeValue);
            }
            if (type != null) {
                widget.options.currentRowId = id;
                widget.options.currentDailySMSId = dailySMSGuid;
                widget.options.currentSessionId = sessionGuid;

                widget.options.currentType = type;
                var inputText = '<textarea class="ct-edit-textarea" >' + content + '</textarea>';
                $('#' + id).empty().append(inputText);
                $('#' + id).unbind('click');
            }
            else {
                widget.options.currentRowId = null;
                widget.options.currentDailySMSId = null;
                widget.options.currentType = null;
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

    $.extend($.ct.managedailyreportsms, {
        version: "@VERSION"
    });

})(jQuery);