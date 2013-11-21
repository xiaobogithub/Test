/*
*   json2.js
*/

(function ($) {

    $.widget("ct.programuserreport", {

        options: {
            programUserReport: null,
            tableName: 'reportContents',
            tableHead: 'reportContentsHead',
            afterRender: null
        },

        _create: function () {
            var widget = this;
            if (widget.options.programUserReport == null) {
                CT.Program.GetProgramUserReport(widget, function (widget, data) {
                    widget.options.programUserReport = data;
                    widget.loadProgramUserReport(widget.options.programUserReport);
                });
            }
            else {
                widget.loadProgramUserReport(widget.options.programUserReport);
            }
        },

        loadProgramUserReport: function (programUserReport) {
            var widget = this;
            var widgetId = widget.element[0].id;
            var tableId = widget.options.tableName;
            var tableHeadId = widget.options.tableHead;
            $('tr').remove('.' + tableId + 'data');
            for (var r = 0; r < programUserReport.length; r++) {
                var row = programUserReport[r];
                var rowId = tableId + '-' + r;
                var rowTr = '<tr id="' + rowId + '" class="' + tableId + 'data" ></tr>';
                $('#' + tableId).append(rowTr);

                //ProgramLogoUrl
                var programLogoUrlId = rowId + '-programLogoUrl';
                var programLogoId = rowId + '-programLogo';
//                if (programUserReport[r].ProgramLogoUrl != null)
//                    colTd = '<td id="' + programLogoUrlId + '"><input id="' + programLogoId + '" type="image" src="' + programUserReport[r].ProgramLogoUrl + '" class="ct-image" /></td>';
//                else
                    colTd = '<td id="' + programLogoUrlId + '" class="name">' + programUserReport[r].ProgramName + '</td>';
                $('#' + rowId).append(colTd);

                //AllUser
                var allUserId = rowId + '-allUser';
                colTd = '<td id="' + allUserId + '">' + programUserReport[r].AllUser + '</td>';
                $('#' + rowId).append(colTd);

                //NotCompleteScreening
                var notCompleteScreeningId = rowId + '-notCompleteScreening';
                colTd = '<td id="' + notCompleteScreeningId + '">' + programUserReport[r].NotCompleteScreening + '</td>';
                $('#' + rowId).append(colTd);

                //CompleteScreening
                var completeScreeningId = rowId + '-completeScreening';
                colTd = '<td id="' + completeScreeningId + '">' + programUserReport[r].CompleteScreening + '</td>';
                $('#' + rowId).append(colTd);

                //RegisteredUser
                var registeredUserId = rowId + '-registeredUser';
                colTd = '<td id="' + registeredUserId + '">' + programUserReport[r].RegisteredUser + '</td>';
                $('#' + rowId).append(colTd);

                //UsersInProgramme
                var usersInProgrammeId = rowId + '-usersInProgramme';
                colTd = '<td id="' + usersInProgrammeId + '">' + programUserReport[r].UsersInProgramme + '</td>';
                $('#' + rowId).append(colTd);

                //CompleteUser
                var completeUserId = rowId + '-completeUser';
                colTd = '<td id="' + completeUserId + '">' + programUserReport[r].CompleteUser + '</td>';
                $('#' + rowId).append(colTd);

                //TerminateUser
                var terminateUserId = rowId + '-terminateUser';
                colTd = '<td id="' + terminateUserId + '">' + programUserReport[r].TerminateUser + '</td>';
                $('#' + rowId).append(colTd);

                //RegisteredLast24Hours
                var registeredLast24HoursId = rowId + '-registeredLast24Hours';
                colTd = '<td id="' + registeredLast24HoursId + '">' + programUserReport[r].RegisteredLast24Hours + '</td>';
                $('#' + rowId).append(colTd);

                //RegisteredLastWeek
                var registeredLastWeekId = rowId + '-registeredLastWeek';
                colTd = '<td id="' + registeredLastWeekId + '">' + programUserReport[r].RegisteredLastWeek + '</td>';
                $('#' + rowId).append(colTd);

                //RegisteredLastMonth
                var registeredLastMonthId = rowId + '-registeredLastMonth';
                colTd = '<td id="' + registeredLastMonthId + '">' + programUserReport[r].RegisteredLastMonth + '</td>';
                $('#' + rowId).append(colTd);
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

    $.extend($.ct.programuserreport, {
        version: "@VERSION"
    });

})(jQuery);