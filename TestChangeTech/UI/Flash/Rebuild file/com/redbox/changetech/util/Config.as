﻿//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.util {
    import assets.*;
    import com.redbox.changetech.vo.*;
    import flash.utils.*;
    import mx.collections.*;
    import com.redbox.changetech.util.Enumerations.*;

    public class Config {

        public static const MODULE_SWFS:Dictionary = new Dictionary();
        public static const NUMBER_OF_ROOMS:Number = 4;
        public static const ROOM_MAPPINGS:Dictionary = new Dictionary();
        public static const ROOM_ORDER:Dictionary = new Dictionary();
        public static const WEEKDAYS:Array = [DayOfWeek.Monday.Text, DayOfWeek.Tuesday.Text, DayOfWeek.Wednesday.Text, DayOfWeek.Thursday.Text, DayOfWeek.Friday.Text, DayOfWeek.Saturday.Text, DayOfWeek.Sunday.Text];
        public static const ROOM_CONFIGS:ArrayCollection = new ArrayCollection();
        public static const POP_UP_MODULE_SWFS:Dictionary = new Dictionary();
        public static const ROOM_CONFIG:Dictionary = new Dictionary();
        public static const MODULE_PATH:String = "com/redbox/changetech/view/modules/";

        ROOM_ORDER[RoomName.Emotion] = 0;
        ROOM_ORDER[RoomName.Motivation] = 1;
        ROOM_ORDER[RoomName.Willpower] = 2;
        ROOM_ORDER[RoomName.Blank] = 3;
        ROOM_CONFIG[RoomName.Emotion] = new RoomVO();
        RoomVO(ROOM_CONFIG[RoomName.Emotion]).roomName = RoomName.Emotion.Text;
        RoomVO(ROOM_CONFIG[RoomName.Emotion]).buttonGradColour1 = 14849328;
        RoomVO(ROOM_CONFIG[RoomName.Emotion]).buttonGradColour2 = 8540180;
        RoomVO(ROOM_CONFIG[RoomName.Emotion]).textColour1 = 14057232;
        RoomVO(ROOM_CONFIG[RoomName.Emotion]).textColour2 = 0x2F2F2F;
        RoomVO(ROOM_CONFIG[RoomName.Emotion]).boxColour1 = 0xFFFFFF;
        RoomVO(ROOM_CONFIG[RoomName.Emotion]).boxColour2 = 15198441;
        RoomVO(ROOM_CONFIG[RoomName.Emotion]).badgeAsset = Assets.getInstance().balance_badge_emotion;
        RoomVO(ROOM_CONFIG[RoomName.Emotion]).demoPopUpCopy = "emotion_room_message";
        ROOM_CONFIG[RoomName.Motivation] = new RoomVO();
        RoomVO(ROOM_CONFIG[RoomName.Motivation]).roomName = RoomName.Motivation.Text;
        RoomVO(ROOM_CONFIG[RoomName.Motivation]).buttonGradColour1 = 2729699;
        RoomVO(ROOM_CONFIG[RoomName.Motivation]).buttonGradColour2 = 1664908;
        RoomVO(ROOM_CONFIG[RoomName.Motivation]).textColour1 = 2263998;
        RoomVO(ROOM_CONFIG[RoomName.Motivation]).textColour2 = 0x2F2F2F;
        RoomVO(ROOM_CONFIG[RoomName.Motivation]).boxColour1 = 0xFFFFFF;
        RoomVO(ROOM_CONFIG[RoomName.Motivation]).boxColour2 = 15198441;
        RoomVO(ROOM_CONFIG[RoomName.Motivation]).badgeAsset = Assets.getInstance().balance_badge_motivation;
        RoomVO(ROOM_CONFIG[RoomName.Motivation]).demoPopUpCopy = "motivation_room_message";
        ROOM_CONFIG[RoomName.Willpower] = new RoomVO();
        RoomVO(ROOM_CONFIG[RoomName.Willpower]).roomName = RoomName.Willpower.Text;
        RoomVO(ROOM_CONFIG[RoomName.Willpower]).buttonGradColour1 = 10668606;
        RoomVO(ROOM_CONFIG[RoomName.Willpower]).buttonGradColour2 = 6326814;
        RoomVO(ROOM_CONFIG[RoomName.Willpower]).textColour1 = 8892465;
        RoomVO(ROOM_CONFIG[RoomName.Willpower]).textColour2 = 0x2F2F2F;
        RoomVO(ROOM_CONFIG[RoomName.Willpower]).boxColour1 = 0xFFFFFF;
        RoomVO(ROOM_CONFIG[RoomName.Willpower]).boxColour2 = 15198441;
        RoomVO(ROOM_CONFIG[RoomName.Willpower]).badgeAsset = Assets.getInstance().balance_badge_willpower;
        RoomVO(ROOM_CONFIG[RoomName.Willpower]).demoPopUpCopy = "willpower_room_message";
        ROOM_CONFIG[RoomName.Blank] = new RoomVO();
        RoomVO(ROOM_CONFIG[RoomName.Blank]).roomName = RoomName.Blank.Text;
        RoomVO(ROOM_CONFIG[RoomName.Blank]).buttonGradColour1 = 2729699;
        RoomVO(ROOM_CONFIG[RoomName.Blank]).buttonGradColour2 = 1664908;
        RoomVO(ROOM_CONFIG[RoomName.Blank]).textColour1 = 2263998;
        RoomVO(ROOM_CONFIG[RoomName.Blank]).boxColour1 = 0xFFFFFF;
        RoomVO(ROOM_CONFIG[RoomName.Blank]).boxColour2 = 15198441;
        RoomVO(ROOM_CONFIG[RoomName.Blank]).badgeAsset = null;
        RoomVO(ROOM_CONFIG[RoomName.Blank]).demoPopUpCopy = "......this is a blank room.....</p>";
        ROOM_CONFIGS.addItem(ROOM_CONFIG[RoomName.Emotion]);
        ROOM_CONFIGS.addItem(ROOM_CONFIG[RoomName.Motivation]);
        ROOM_CONFIGS.addItem(ROOM_CONFIG[RoomName.Willpower]);
        ROOM_CONFIGS.addItem(ROOM_CONFIG[RoomName.Blank]);
        ROOM_MAPPINGS[ContentType.Intro.Text] = ROOM_ORDER[RoomName.Blank];
        ROOM_MAPPINGS[ContentType.ScreeningTest.Text] = ROOM_ORDER[RoomName.Blank];
        ROOM_MAPPINGS[ContentType.Registration.Text] = ROOM_ORDER[RoomName.Willpower];
        ROOM_MAPPINGS[ContentType.PersonalPlan.Text] = ROOM_ORDER[RoomName.Motivation];
        ROOM_MAPPINGS[ContentType.PositivePsychology.Text] = ROOM_ORDER[RoomName.Emotion];
        ROOM_MAPPINGS[ContentType.PositivePsychologyPAFD.Text] = ROOM_ORDER[RoomName.Emotion];
        ROOM_MAPPINGS[ContentType.PositivePsychologySHS.Text] = ROOM_ORDER[RoomName.Emotion];
        ROOM_MAPPINGS[ContentType.DailyConsumption.Text] = ROOM_ORDER[RoomName.Motivation];
        ROOM_MAPPINGS[ContentType.ProgressMonitor.Text] = ROOM_ORDER[RoomName.Willpower];
        ROOM_MAPPINGS[ContentType.BasicMotivation.Text] = ROOM_ORDER[RoomName.Motivation];
        ROOM_MAPPINGS[ContentType.BasicWillpower.Text] = ROOM_ORDER[RoomName.Willpower];
        ROOM_MAPPINGS[ContentType.BasicEmotion.Text] = ROOM_ORDER[RoomName.Emotion];
        ROOM_MAPPINGS[ContentType.Welcome.Text] = ROOM_ORDER[RoomName.Blank];
        ROOM_MAPPINGS[ContentType.CueReactivity.Text] = ROOM_ORDER[RoomName.Willpower];
        ROOM_MAPPINGS[ContentType.LapseDetection.Text] = ROOM_ORDER[RoomName.Motivation];
        ROOM_MAPPINGS[ContentType.RelapseTherapy.Text] = ROOM_ORDER[RoomName.Willpower];
        ROOM_MAPPINGS[ContentType.LapseTherapy.Text] = ROOM_ORDER[RoomName.Willpower];
        ROOM_MAPPINGS[ContentType.ConsumptionReport.Text] = ROOM_ORDER[RoomName.Motivation];
        ROOM_MAPPINGS[ContentType.ScreeningPart2a.Text] = ROOM_ORDER[RoomName.Blank];
        ROOM_MAPPINGS[ContentType.ScreeningPart2b.Text] = ROOM_ORDER[RoomName.Blank];
        MODULE_SWFS[ContentType.ScreeningTest.Text] = (MODULE_PATH + "ScreeningTest.swf");
        MODULE_SWFS[ContentType.DailyConsumption.Text] = (MODULE_PATH + "DailyConsumption.swf");
        MODULE_SWFS[ContentType.PersonalPlan.Text] = (MODULE_PATH + "PersonalPlan.swf");
        MODULE_SWFS[ContentType.ProgressMonitor.Text] = (MODULE_PATH + "ProgressMonitor.swf");
        MODULE_SWFS[ContentType.Registration.Text] = (MODULE_PATH + "Registration.swf");
        MODULE_SWFS[ContentType.Intro.Text] = (MODULE_PATH + "Intro.swf");
        MODULE_SWFS[ContentType.PositivePsychology.Text] = (MODULE_PATH + "PositivePsychology.swf");
        MODULE_SWFS[ContentType.PositivePsychologySHS.Text] = (MODULE_PATH + "PositivePsychologySHS.swf");
        MODULE_SWFS[ContentType.PositivePsychologyPAFD.Text] = (MODULE_PATH + "PositivePsychologyPAFD.swf");
        MODULE_SWFS[ContentType.BasicMotivation.Text] = (MODULE_PATH + "BasicMotivation.swf");
        MODULE_SWFS[ContentType.BasicWillpower.Text] = (MODULE_PATH + "BasicWillpower.swf");
        MODULE_SWFS[ContentType.BasicEmotion.Text] = (MODULE_PATH + "BasicEmotion.swf");
        MODULE_SWFS[ContentType.Welcome.Text] = (MODULE_PATH + "Welcome.swf");
        MODULE_SWFS[ContentType.CueReactivity.Text] = (MODULE_PATH + "CueReactivity.swf");
        MODULE_SWFS[ContentType.LapseDetection.Text] = (MODULE_PATH + "LapseDetection.swf");
        MODULE_SWFS[ContentType.RelapseTherapy.Text] = (MODULE_PATH + "RelapseTherapy.swf");
        MODULE_SWFS[ContentType.LapseTherapy.Text] = (MODULE_PATH + "LapseTherapy.swf");
        MODULE_SWFS[ContentType.ConsumptionReport.Text] = (MODULE_PATH + "ConsumptionReport.swf");
        MODULE_SWFS[ContentType.ScreeningPart2a.Text] = (MODULE_PATH + "ScreeningPart2a.swf");
        MODULE_SWFS[ContentType.ScreeningPart2b.Text] = (MODULE_PATH + "ScreeningPart2b.swf");
    }
}//package com.redbox.changetech.util 
