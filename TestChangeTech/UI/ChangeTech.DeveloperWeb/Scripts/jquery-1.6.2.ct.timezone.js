/*
*   json2.js
*/

(function ($) {

    $.widget("ct.timezone", {

        options: {
            programGuid: null,
            timeZoneOpts: null,
            pId: "pTimeZone"
            //timeZoneDropDown: "DropDownTimezone"
        },

        _create: function () {
            var widget = this;
            if (widget.options.timeZoneOpts == null) {
                CT.Program.GetTimeZoneOpts(widget, widget.options.programGuid, function (widget, data) {
                    widget.options.timeZoneOpts = data;
                    widget.loadTimeZoneOpts(widget.options.timeZoneOpts);
                });
            }
            else {
                widget.loadloadTimeZoneOpts(widget.options.timeZoneOpts);
            }
        },

        loadTimeZoneOpts: function (timeZoneOpts) {
            var widget = this;
            var widgetId = widget.element[0].id;
            //var timeZoneDDLId = widget.options.timeZoneDropDown;
            $("#" + widget.options.pId).append(timeZoneOpts);
            //$("#" + timeZoneDDLId).change(function () {
            //      alert($("#" + timeZoneDDLId).val());
            //});
        },
        getElementById: function (elementId) {
            return document.getElementById(elementId);
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

    $.extend($.ct.timezone, {
        version: "@VERSION"
    });

})(jQuery);