﻿using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassScheduler
{

    /// <summary>
    /// This is a window in which the user is expected to enter initial data to be used for the rest of the proccess.
    /// </summary>
    /// Author: Kostiantyn Makrasnov (Excel Reader Part & Form User Input)
    /// Author: Yuriy Fedas (Preview of Selected Excel Data File)

    public partial class InitialInputForm : Form
    {

        public UserConfig userData = new UserConfig { };                    // *may need permision adjustment*
        public List<SingleCourse> courses = new List<SingleCourse>();
        public List<SingleCourse> unneededCourses = new List<SingleCourse>();
        List<string> sectionIDs = new List<string>();
        List<string> courseIDs = new List<string>();
        IExcelDataReader excelReader;

        public InitialInputForm()
        {
            InitializeComponent();
        }

        //[FUNCTION - InputWindow_Load]
        //Mainly Turns OFF Visibility of Error Labels at Start
        private void InputWindow_Load(object sender, EventArgs e)
        {
            FirstNameNeedLabel.Visible = false;
            LastNameNeedLabel.Visible = false;
            TermNeedLabel.Visible = false;
            PreviewStatusLabel.Visible = false;
        }

        //[FUNCTION - btnFind_Click]
        //Performs actions designated for "Find" button click
        private void btnFind_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Excel Workbook|*.xlsx", ValidateNames = true })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    FileStream stream = File.Open(ofd.FileName, FileMode.Open, FileAccess.Read);
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }

                if (excelReader != null)
                {
                    PreviewStatusLabel.Text = "Preview Ready!";
                    PreviewStatusLabel.ForeColor = Color.Green;
                    PreviewStatusLabel.Visible = true;
                }
            }
        }

        //[FUNCTION - InputToMainButton_Click]
        //Performs actions designated for "InputToMainButton" button click
        private void InputToMainButton_Click(object sender, EventArgs e)
        {
            if (checkImputCompletion())
            {
                //Removes all imput error labels
                FirstNameNeedLabel.Visible = false;
                LastNameNeedLabel.Visible = false;
                TermNeedLabel.Visible = false;

                //Gathers all input into userData object
                userData.firstName = FirstNameTextBox.Text;
                userData.lastName = LastNameTextBox.Text;
                if (TermComboBox.Text == "Fall")
                    userData.termInterest = "FA";
                else if (TermComboBox.Text == "Spring")
                    userData.termInterest = "SP";
                else
                    userData.termInterest = "JA";

                ReadExcelData();

                //Shows course selection form
                CourseSelectionForm CourseSelection = new CourseSelectionForm(this);
                this.Hide();
                CourseSelection.ShowDialog();
            }
            else
            {
                displayInputErrors();
            }
        }

        //[FUNCTION - ReadExcelData)
        //Summarizes the proccess of excel data filtering 
        void ReadExcelData()
        {
            excelReader.Read(); //skips first row
            ProcessCourseData();
            RemoveIrevSections();
            SortCourses();
            WriteDebugFile();
        }

        //[FUNCTION - checkImputCompletion)
        //Returns True if all imputs on this form are completed correctly
        bool checkImputCompletion()
        {
            if (FirstNameTextBox.Text != "" && LastNameTextBox.Text != "" && TermComboBox.Text != "" && excelReader != null)
                return true;
            else
                return false;
        }

        //[FUNCTION - displayInputErrors)
        //Dispays error labels by those fields that are empty or incorrect
        void displayInputErrors()
        {
            if (FirstNameTextBox.Text == "")
                FirstNameNeedLabel.Visible = true;
            else
                FirstNameNeedLabel.Visible = false;

            if (LastNameTextBox.Text == "")
                LastNameNeedLabel.Visible = true;
            else
                LastNameNeedLabel.Visible = false;

            if (TermComboBox.Text == "")
                TermNeedLabel.Visible = true;
            else
                TermNeedLabel.Visible = false;

            if (excelReader == null)
            {
                PreviewStatusLabel.Text = "File not selected.";
                PreviewStatusLabel.ForeColor = Color.Red;
                PreviewStatusLabel.Visible = true;
            }
        }

        //[FUNCTION - ProcessCourseData]
        //Goes through each row in selected excel spreadsheet dynamically populating the course list
        void ProcessCourseData()
        {
            while (excelReader.Read())
            {
                if (!courses.Exists(s => s.courseName == excelReader.GetString(6) && s.abrvCourseName == TruncatedCourseID()))
                {
                    courses.Add(new SingleCourse
                    {
                        courseName = excelReader.GetString(6),
                        abrvCourseName = TruncatedCourseID(),
                        courseLevel = GetCourseLevel(),
                        termsAvaliable = new List<string> { (excelReader.GetString(2)) },
                        instructAvaliable = new List<string> { (excelReader.GetString(9)) },
                        sections = new List<SingleSection> { (new SingleSection { }) }
                    });

                    int courseIndex = courses.Count() - 1;
                    courses[courseIndex].sections[0].term = excelReader.GetString(2);
                    courses[courseIndex].sections[0].ID = excelReader.GetString(5);
                    courses[courseIndex].sections[0].startTimes = SplitCellIntoList(17, ", ", " NA");
                    courses[courseIndex].sections[0].stopTimes = SplitCellIntoList(18, ", ", " NA");
                    courses[courseIndex].sections[0].meetDays = SplitCellIntoList(19, ", ", "NA");
                    courses[courseIndex].sections[0].instructFirstN = SplitCellIntoList(10, ", ", "");
                    courses[courseIndex].sections[0].instructLastN = SplitCellIntoList(9, ", ", "NA");
                    sectionIDs.Add(excelReader.GetString(5));
                    courseIDs.Add(TruncatedCourseID() + "|" + excelReader.GetString(6)); //length parameter: (7) - some courses are honors (eg. CO-150H vs CO-150-)
                }
                else if (DetermineSectionNeed(courseIDs.IndexOf(TruncatedCourseID() + "|" + excelReader.GetString(6))))
                {
                    int courseIndex = courseIDs.IndexOf(TruncatedCourseID() + "|" + excelReader.GetString(6));
                    int sectionIndex = courses[courseIndex].sections.Count();
                    bool termRecorded = false;
                    bool instructRecorded = false;

                    foreach (var availTerm in courses[courseIndex].termsAvaliable)
                    {
                        if (availTerm == excelReader.GetString(2))
                            termRecorded = true;
                    }
                    if (termRecorded == false)
                        courses[courseIndex].termsAvaliable.Add(excelReader.GetString(2));

                    foreach (var availInstruct in courses[courseIndex].instructAvaliable)
                    {
                        if (availInstruct == excelReader.GetString(9))
                            instructRecorded = true;
                    }
                    if (instructRecorded == false)
                        courses[courseIndex].instructAvaliable.Add(excelReader.GetString(9));

                    courses[courseIndex].sections.Add(new SingleSection { });

                    courses[courseIndex].sections[sectionIndex].term = excelReader.GetString(2);
                    courses[courseIndex].sections[sectionIndex].ID = excelReader.GetString(5);
                    courses[courseIndex].sections[sectionIndex].startTimes = SplitCellIntoList(17, ", ", " NA");
                    courses[courseIndex].sections[sectionIndex].stopTimes = SplitCellIntoList(18, ", ", " NA");
                    courses[courseIndex].sections[sectionIndex].meetDays = SplitCellIntoList(19, ", ", "NA");
                    courses[courseIndex].sections[sectionIndex].instructFirstN = SplitCellIntoList(10, ", ", "");
                    courses[courseIndex].sections[sectionIndex].instructLastN = SplitCellIntoList(9, ", ", "NA");
                    //courses[index].seatingCap.Add(excelReader.GetInt32(16));

                    sectionIDs.Add(excelReader.GetString(5));
                }
            }
        }

        //[FUNCTION - TruncatedCourseID]
        //Cuts course ID before second slash "-"
        string TruncatedCourseID()
        {
            string fullID = excelReader.GetString(5);
            return fullID.Substring(0, fullID.IndexOf("-", 4));
        }
        //[FUNCTION - DetermineSectionNeed]
        //Finds if a given excel line contains a unique section (returns false if already present)
        bool DetermineSectionNeed(int checkIndex)
        {
            bool needSection = true;

            foreach (var section in courses[checkIndex].sections)
            {
                if (section.ID == excelReader.GetString(5) &&
                    section.startTimes.SequenceEqual(SplitCellIntoList(17, ", ", " NA")) &&
                    section.term == excelReader.GetString(2) &&
                    section.instructFirstN.SequenceEqual(SplitCellIntoList(10, ", ", "")) &&
                    section.meetDays.SequenceEqual(SplitCellIntoList(19, ", ", "")))// &&
                    //section.stopTimes.SequenceEqual(SplitCellIntoList(18, ", ", " NA")) &&
                    //section.instructLastN.SequenceEqual(SplitCellIntoList(9, ", ", "")))
                {
                    needSection = false;
                }
                if (needSection == false)
                    break;
            }

            return needSection;
        }

        //[FUNCTION - SplitCellIntoList]
        //Splits data from column index and given delim into a List of strings
        List<string> SplitCellIntoList(int columnIndex, string delim, string nullReplace)
        {
            List<string> result = new List<string>(((excelReader.GetString(columnIndex)) != null ? excelReader.GetString(columnIndex) : nullReplace).Split(new string[] { delim }, StringSplitOptions.None));
            return result;
        }

        //[FUNCTION - RemoveIrevSections]
        //Cleans up course data by removing irrevelant sections with missing times/days
        void RemoveIrevSections()
        {
            List<int> removeCourseIndexes = new List<int>();
            List<int> removeSectionIndexes = new List<int>();

            //DETEMNINE section need
            foreach (SingleCourse course in courses)
                foreach (SingleSection section in course.sections)
                    if (section.meetDays.Exists(s => s.Contains("NA")) || section.startTimes.Exists(s => s.Contains("NA")) || !section.term.Contains(userData.termInterest))
                    {
                        removeCourseIndexes.Add(courses.IndexOf(course));
                        removeSectionIndexes.Add(course.sections.IndexOf(section));
                    }

            //ADD unneeded courses to secondary list (some may have partial valid data)
            foreach (var index in removeCourseIndexes)
                unneededCourses.Add(courses[index]);

            //REMOVE course entry from primary list
            int indexOffset = 0;
            int currentCourseIndex = removeCourseIndexes[0];
            for (int i = 0; i < removeCourseIndexes.Count(); i++)
            {
                if (currentCourseIndex != removeCourseIndexes[i])
                {
                    indexOffset = 0;
                    currentCourseIndex = removeCourseIndexes[i];
                }
                Debug.WriteLine("Removing - Course: " + courses[removeCourseIndexes[i]].courseName + " | Section: " + removeSectionIndexes[i]);
                courses[removeCourseIndexes[i]].sections.RemoveAt(removeSectionIndexes[i] - indexOffset);
                indexOffset++;
            }

            RemoveEmptyCourses();
        }

        //[FUNCTION - RemoveEmptyCourses]
        //Removes courses from the primary list which are without any sections
        void RemoveEmptyCourses()
        {
            int indexOffset = 0;
            List<int> courseRemoveIndexes = new List<int> { };

            foreach (var course in courses)
                if (course.sections.Count() == 0)
                    courseRemoveIndexes.Add(courses.IndexOf(course));

            foreach (var index in courseRemoveIndexes)
            {
                courses.RemoveAt(index - indexOffset);
                indexOffset++;
            }
        }

        //[FUNCTION - SortCourses]
        //Sorts all leftover courses into alphabetical oder (by Course ID - CO-150)
        void SortCourses()
        {
            courses = courses.OrderBy(f => f.abrvCourseName).ToList<SingleCourse>();
        }

        //[FUNCTION - WriteDebugFile]
        //Controls the creation and content of debug .txt file
        void WriteDebugFile()
        {
            FileStream fs = new FileStream(@"AllSectionTimes.txt", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite); //last parameter needed to open file at end
            StreamWriter file = new StreamWriter(fs);

            WriteTermSections(userData.termInterest, TermComboBox.Text, file);
            file.Flush();

            Process.Start(@"AllSectionTimes.txt");
            Debug.WriteLine("FILE OPENED");
        }

        //[FUNCTION - WriteTermSections]
        //Formats course information of a specific term and writes to debug .txt file (one course at a time)
        void WriteTermSections(string abrvTerm, string Term, StreamWriter sw)
        {
            sw.WriteLine("****************************[" + Term.ToUpper() + " Term]****************************");
            int courseNum = 0;
            foreach (var course in courses)
            {
                sw.Flush();
                if (course.termsAvaliable.Any(s => s.EndsWith(abrvTerm)))
                {
                    courseNum++;
                    sw.WriteLine("[" + Term.ToUpper() + " COURSE #" + courseNum + "] - [" + course.abrvCourseName + "] - " + course.courseName + ": ");

                    int indexCount = 0;
                    foreach (var section in course.sections)
                    {
                        if (section.term.Contains(abrvTerm))
                        {
                            int secTimeIndex = 0;

                            sw.Write("  [SECTION #" + (indexCount + 1) + "] -");
                            foreach (var startTime in section.startTimes)
                            {
                                sw.Write(startTime + " -" + section.stopTimes[secTimeIndex]);
                                if (secTimeIndex == 0 && section.startTimes.Count() != 1)
                                    sw.Write(" and");
                                secTimeIndex++;
                            }
                            sw.Write(" | ");

                            int secFacIndex = 0;
                            foreach (var lastName in section.instructLastN)
                            {
                                if (section.instructFirstN[secFacIndex] != "")
                                    sw.Write(section.instructFirstN[secFacIndex] + ", ");
                                else
                                    sw.Write(" ");
                                sw.Write(lastName + " | ");
                                secFacIndex++;
                            }

                            int secDayIndex = 0;
                            foreach (var day in section.meetDays)
                            {
                                if (secDayIndex != 0)
                                    sw.Write("-");
                                sw.Write(day);
                                secDayIndex++;
                            }
                            sw.WriteLine(" |");
                            indexCount++;
                        }
                    }
                    sw.WriteLine("-------------------------------------------------------------------------------------");
                }
            }
        }

        //[FUNCTION - ConvertToInt]
        //Returns an int version of the string if can otherwise null
        int ConvertToInt(double data)
        {
            return (int) data;
        }

        //[FUNCTION - GetCourseLevel]
        //Returns the level of course based on excel reader position
        string GetCourseLevel()
        {
            object value = excelReader.GetValue(8);
            string stringValue = value.ToString();
            return (stringValue != "NA") ? stringValue : "000";
        }
    }
}
