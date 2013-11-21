using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using ChangeTech.Silverlight.Common;
using ChangeTech.Silverlight.DesignPage.ChangeTechWCFService;

namespace ChangeTech.Silverlight.DesignPage.PageTemplate
{
    public partial class SMSTemplate : UserControl
    {
        public SMSTemplatePageContentModel PageContentModel { get; set; }
        public SMSTemplate()
        {
            InitializeComponent();
            PageContentModel = new SMSTemplatePageContentModel();
            PageContentModel.PageSequenceGUID = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PAGE_SEQUENCE_GUID));
        }

        public string Validate()
        {
            string validString = string.Empty;
            if(string.IsNullOrEmpty(textTextBox.Text))
            {
                validString += "Please input text.\n";
            }
            if(string.IsNullOrEmpty(hhtimeTextBox.Text))
            {
                validString += "Please input time HH.\n";
            }
            if(string.IsNullOrEmpty(mmtimeTextBox.Text))
            {
                validString += "Please input time MM. \n";
            }
            if(string.IsNullOrEmpty(daysToSendTextBox.Text))
            {
                validString += "Please input days to send.\n";
            }

            return validString;
        }

        public void FillContent()
        {
            PageContentModel.DaysToSend = Convert.ToInt32(daysToSendTextBox.Text.Trim());
            PageContentModel.Text = textTextBox.Text.Trim();
            PageContentModel.Time = (Convert.ToInt32(hhtimeTextBox.Text) * 60 + Convert.ToInt32(mmtimeTextBox.Text)).ToString();
        }

        public void BindPageContent(EditSMSTemplatePageContentModel editPagecontentModel)
        {
            daysToSendTextBox.Text = editPagecontentModel.DaysToSend;
            textTextBox.Text = editPagecontentModel.Text;
            if(!string.IsNullOrEmpty(editPagecontentModel.Time))
            {
                int minutes = Convert.ToInt32(editPagecontentModel.Time);
                hhtimeTextBox.Text = (minutes / 60).ToString();
                mmtimeTextBox.Text = (minutes % 60).ToString();
            }
            // for page variable
            if(editPagecontentModel.PageVariable != null)
            {
                SetPageVariable(editPagecontentModel.PageVariable);
            }
        }

        public void DisableControl(string reasonStr)
        {
            daysToSendTextBox.IsEnabled = false;
            textTextBox.IsEnabled = false;
            mmtimeTextBox.IsEnabled = false;
            hhtimeTextBox.IsEnabled = false;
            pagevariableHyperlinkButtion.IsEnabled = false;
            variableHyperlinkButtion.IsEnabled = false;
        }

        public void EnableControl()
        {
            daysToSendTextBox.IsEnabled = true;
            textTextBox.IsEnabled = true;
            mmtimeTextBox.IsEnabled = true;
            hhtimeTextBox.IsEnabled = true;
            pagevariableHyperlinkButtion.IsEnabled = true;
            variableHyperlinkButtion.IsEnabled = true;
        }

        private void SelectVaribleHandler(object sender, PageVariableEventArgs e)
        {
            if(e.pageVariable != null)
            {
                textTextBox.Text += " {V:" + e.pageVariable.Name + "}";
            }
        }

        private void SelectPageVaribleHandler(object sender, PageVariableEventArgs e)
        {
            if(e.pageVariable != null)
            {
                SimplePageVariableModel variableModel = new SimplePageVariableModel
                {
                    Name = e.pageVariable.Name,
                    PageVariableGuid = e.pageVariable.PageVariableGUID
                };
                SetPageVariable(variableModel);
            }
        }

        private void variableHyperlinkButtion_Click(object sender, RoutedEventArgs e)
        {
            VariableList.Show();
        }

        private void SetPageVariable(SimplePageVariableModel variable)
        {
            pagevariableHyperlinkButtion.Content = "Edit variable";
            PageContentModel.PageVariableGUID = variable.PageVariableGuid;
            variableNameTextBlock.Text = "{V:" + variable.Name + "}";
        }

        private void pagevariableHyperlinkButtion_Click(object sender, RoutedEventArgs e)
        {
            pageVariableList.Show();
        }
    }
}
