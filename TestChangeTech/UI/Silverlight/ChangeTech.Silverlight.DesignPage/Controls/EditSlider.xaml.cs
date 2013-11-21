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
using System.Collections.ObjectModel;
using ChangeTech.Silverlight.DesignPage.ChangeTechWCFService;
using System.Windows.Browser;

namespace ChangeTech.Silverlight.DesignPage.Controls
{
    public class SliderInfo
    {
        public int Start { get; set; }
        public int End { get; set; }
        public int Step { get; set; }
    }
    public partial class EditSlider : UserControl
    {
        public PageQuestionModel question;
        public Dictionary<Guid, ModelStatus> ObjectStatus;

        public EditSlider()
        {
            InitializeComponent();
        }

        public void Show()
        {
            if (question.SubItems != null && question.SubItems.Count > 1)
            {
                //for (int i = 0; i < question.SubItems.Count; i++)
                //{
                //    for (int j = 0; j < question.SubItems.Count - i - 1; j++)
                //    {
                //        int itm1;
                //        int itm2;
                //        if (!Int32.TryParse(question.SubItems[j].Item, out itm1)) itm1 = 0;
                //        if (!Int32.TryParse(question.SubItems[j + 1].Item, out itm2)) itm2 = 0;

                //        if (itm1 > itm2)
                //        {
                //            PageQuestionItemModel temp = question.SubItems[j];
                //            question.SubItems[j] = question.SubItems[j + 1];
                //            question.SubItems[j + 1] = temp;
                //        }
                //    }
                //}
                SliderInfo sliderinfo = GetSliderInfo(question);
                txtBeginContent.Text = string.IsNullOrEmpty(question.BeginContent)? string.Empty: question.BeginContent;
                txtEndContent.Text = string.IsNullOrEmpty(question.EndContent)? string.Empty : question.EndContent;
                txtBegin.Text = sliderinfo.Start.ToString();
                txtEnd.Text = sliderinfo.End.ToString();
                txtStep.Text = sliderinfo.Step.ToString();
            }
            else
            {
                txtBeginContent.Text = "";
                txtEndContent.Text = "";
                txtBegin.Text = "";
                txtEnd.Text = "";
                txtStep.Text = "1";
            }
            Visibility = Visibility.Visible;
        }

        public void Hide()
        {
            Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Check whether user has fill in all necessary information
        /// </summary>
        /// <returns></returns>
        public string Validate()
        {
            string errorMessage = string.Empty;
            int int32Val;
            if (!int.TryParse(txtBegin.Text, out int32Val))
            {
                errorMessage += "Begin filed should be integer\n";
            }
            if (!int.TryParse(txtEnd.Text, out int32Val))
            {
                errorMessage += "End filed should be integer\n";
            }
            else
            {
                if (int.Parse(txtEnd.Text) <= int.Parse(txtBegin.Text))
                {
                    errorMessage += "Begin should be smaller than End\n";
                }
                if (int.Parse(txtEnd.Text) >= 100)
                {
                    errorMessage += "End should be smaller than 100\n";
                }
            }
            if (!int.TryParse(txtStep.Text, out int32Val))
            {
                errorMessage += "Step filed should be integer\n";
            }
            else
            {
                if (int.Parse(txtStep.Text) >= int.Parse(txtEnd.Text))
                {
                    errorMessage += "Step should smaller than End\n";
                }
            }
            return errorMessage;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string validateMessage = Validate();
            if (validateMessage == string.Empty)
            {
                question.BeginContent = txtBeginContent.Text;
                question.EndContent = txtEndContent.Text;
                int End = Convert.ToInt32(txtEnd.Text);
                int Step = Convert.ToInt32(txtStep.Text);
                int Now = Convert.ToInt32(txtBegin.Text);
                if (question.SubItems == null)
                {
                    question.SubItems = new ObservableCollection<PageQuestionItemModel>();
                    int intOrder = question.SubItems.Count;
                    do
                    {
                        if (Now > End)
                        {
                            Now = End;
                        }
                        PageQuestionItemModel item = new PageQuestionItemModel
                        {
                            Guid = Guid.NewGuid(),
                            Item = Now.ToString(),
                            Order = ++intOrder,// question.SubItems.Count+1
                        };
                        ObjectStatus.Add(item.Guid, ModelStatus.QuestionItemAdded);
                        question.SubItems.Add(item);
                        Now += Step;
                    } while (Now < End + Step);
                }
                else
                {
                    bool hasItem = true;
                    for (int i = 0; i < question.SubItems.Count; i++)
                    {
                        if (hasItem)
                        {
                            if (Now >= End)
                            {
                                Now = End;
                                hasItem = false;
                            }
                            question.SubItems[i].Item = Now.ToString();
                            if (ObjectStatus.ContainsKey(question.SubItems[i].Guid))
                            {
                                if (ObjectStatus[question.SubItems[i].Guid] != ModelStatus.QuestionItemAdded)
                                {
                                    ObjectStatus[question.SubItems[i].Guid] = ModelStatus.QuestionItemUpdated;
                                }
                            }
                            else
                            {
                                ObjectStatus.Add(question.SubItems[i].Guid, ModelStatus.QuestionItemUpdated);
                            }
                            Now += Step;
                        }
                        else
                        {
                            if (ObjectStatus.ContainsKey(question.SubItems[i].Guid))
                            {
                                ObjectStatus[question.SubItems[i].Guid] = ModelStatus.QuestionItemDeleted;
                            }
                            else
                            {
                                ObjectStatus.Add(question.SubItems[i].Guid, ModelStatus.QuestionItemDeleted);
                            }
                            question.SubItems.Remove(question.SubItems[i]);
                            i--;
                        }
                    }
                    if (Now <= End)
                    {
                        int intOrder = question.SubItems.Count;
                        do
                        {
                            if (Now > End)
                            {
                                Now = End;
                            }
                            PageQuestionItemModel item = new PageQuestionItemModel
                            {
                                Guid = Guid.NewGuid(),
                                Item = Now.ToString(),
                                Order = ++intOrder,// question.SubItems.Count+1
                            };
                            ObjectStatus.Add(item.Guid, ModelStatus.QuestionItemAdded);
                            question.SubItems.Add(item);
                            Now += Step;
                        } while (Now < End + Step);
                    }
                }
                Hide();
            }
            else
            {
                HtmlPage.Window.Alert(validateMessage);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private SliderInfo GetSliderInfo(PageQuestionModel questionModel)
        {
            SliderInfo sliderinfo = new SliderInfo();
            List<int> list = new List<int>();
            foreach (PageQuestionItemModel pqim in questionModel.SubItems)
            {
                int result;
                if (int.TryParse(pqim.Item, out result))
                {
                    list.Add(Convert.ToInt32(pqim.Item));
                }
                else
                {
                    ErrorMessage.Text = "Some data is error, Please reset it again!";
                }
            }
            if (list.Count > 1)
            {
                list.Sort();
                sliderinfo.Start = list[0];
                sliderinfo.Step = list[1] - list[0];
                sliderinfo.End = list[list.Count - 1];
            }
            return sliderinfo;
        }

        #region Drag & Drop
        bool isMouseCaptured;
        Point mousePosition;
        bool clickOnDataGridColumn;

        private void RenameCategoryPopupPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement item = sender as FrameworkElement;

            if (!clickOnDataGridColumn)
            {
                mousePosition = e.GetPosition(null);
                isMouseCaptured = true;
                item.CaptureMouse();
                item.Cursor = Cursors.Hand;
            }
        }

        private void RenameCategoryPopupPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement item = sender as FrameworkElement;
            isMouseCaptured = false;
            clickOnDataGridColumn = false;
            item.ReleaseMouseCapture();
            mousePosition.X = mousePosition.Y = 0;
            item.Cursor = null;
        }

        private void RenameCategoryPopupPanel_MouseMove(object sender, MouseEventArgs e)
        {
            FrameworkElement item = sender as FrameworkElement;
            if (isMouseCaptured)
            {
                // Calculate the current position of the object.
                double deltaV = e.GetPosition(null).Y - mousePosition.Y;
                double deltaH = e.GetPosition(null).X - mousePosition.X;
                double newTop = deltaV + (double)item.GetValue(Canvas.TopProperty);
                double newLeft = deltaH + (double)item.GetValue(Canvas.LeftProperty);

                // Set new position of object.
                item.SetValue(Canvas.TopProperty, newTop);
                item.SetValue(Canvas.LeftProperty, newLeft);

                // Update position global variables.
                mousePosition = e.GetPosition(null);
            }
        }

        private void PageListDataGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            clickOnDataGridColumn = true;
        }

        private void PageListDataGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            clickOnDataGridColumn = false;
        }
        #endregion
    }
}
