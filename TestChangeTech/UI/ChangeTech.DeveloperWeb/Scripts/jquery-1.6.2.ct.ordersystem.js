/*
*   json2.js
*/

(function ($) {

    $.widget("ct.ordersystem", {

        options: {
            languageGuid: null,
            orderContentsByLanguageGuid: null,
            tableName: 'orderContents',
            tableHead: 'orderContentsHead'
        },

        _create: function () {
            var widget = this;
            if (widget.options.orderContentsByLanguageGuid == null) {
                CT.Program.GetOrderContents(widget, widget.options.languageGuid, function (widget, data) {
                    widget.options.orderContentsByLanguageGuid = data;
                    widget.loadOrderContentsByLanguageGuid(widget.options.orderContentsByLanguageGuid);
                });
            }
            else {
                widget.loadOrderContentsByLanguageGuid(widget.options.orderContentsByLanguageGuid);
            }
        },

        loadOrderContentsByLanguageGuid: function (orderContents) {
            var widget = this;
            var widgetId = widget.element[0].id;
            var tableId = widget.options.tableName;
            var tableHeadId = widget.options.tableHead;
            $('tr').remove('.' + tableId + 'data');
            for (var r = 0; r < orderContents.length; r++) {
                var row = orderContents[r];
                var rowId = tableId + '-' + r;
                var rowTr = '<tr id="' + rowId + '" class="' + tableId + 'data" align="center" ></tr>';
                $('#' + tableId).append(rowTr);


                //programLicences
                var programLicencesId = rowId + "programLicences";
                colTd = '<td id="' + programLicencesId + '" style="width:80px" >' + '<input type="text" id="' + row.ProgramGuid + '" name="' + row.ProgramGuid + '" class="licence" style="width:80px"/>' // '<asp:TextBox ID="' + row.ProgramGuid + '" runat="server"></asp:TextBox>' + '</td>';
                $('#' + rowId).append(colTd);

                //programName
                var programNameId = rowId + "programName";
                colTd = '<td id="' + programNameId + '">' + row.ProgramName + '</td>';
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

    $.extend($.ct.ordersystem, {
        version: "@VERSION"
    });

})(jQuery);