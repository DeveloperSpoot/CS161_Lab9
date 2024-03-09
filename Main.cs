using System.Security.AccessControl;

namespace CS161_Lab9
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private double[][] SECTIONS = new double[3][];         //Initalizing our jagged array.

        private void Main_Load(object sender, EventArgs e) //Our onLoad event method.
        {
            // Initializing our sub arrays in our jagged array.
            SECTIONS[0] = new double[12];
            SECTIONS[1] = new double[8];
            SECTIONS[2] = new double[10];


            //Vairables to track the lowest and highest scores.
            double highestScore = 0.00;
            int highestSection = 0;

            double lowestScore = 100000.00;
            int lowestSection = 0;

            try
            {
                // Creating an array of our section files and section ListBoxs.
                StreamReader[] sectionFiles = { File.OpenText("Section1.txt"), File.OpenText("Section2.txt"), File.OpenText("Section3.txt") };
                ListBox[] sectionListBoxs = { section1ListBox, section2ListBox, section3ListBox };


                int fileIndex = 0; // Tracking which file we are currently reading.
                foreach (var file in sectionFiles) // Looping though all three files.
                {
                    int index = 0; // Tracking each line we are.
                    while (!file.EndOfStream) // Looping through each file.
                    {
                        try
                        {
                            string currentLine = file.ReadLine(); // Getting the current score on the current line.

                            double currentScore = double.Parse(currentLine); // Parsing said score into a double.


                            SECTIONS[fileIndex][index] = currentScore; // Updating our jagged array with the score.

                            sectionListBoxs[fileIndex].Items.Add(currentLine); // Displaying the score in the appropriate section list box.


                            if (highestScore < currentScore) // Checking for the highest score.
                            {
                                // Upadting the highest score.
                                highestScore = currentScore;
                                highestSection = fileIndex;
                            }

                            if (lowestScore > currentScore) // Checking for the lowest score.
                            {
                                //Updating the lowest score.
                                lowestScore = currentScore;
                                lowestSection = fileIndex;
                            }

                            index++; // Updating the line index.
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message); // Displayin
                        }
                    }

                    sectionFiles[fileIndex].Close(); // Closing each file once we are done reading it,

                    calculateSectionAverage(fileIndex); // Calculating the section average.
                    fileIndex++; // Updating our file index.
                }

                calculateAverage(); // Calculating the overall average.

                highestScoreLabel.Text = highestScore.ToString("n2"); //Displaying highest score.
                highestSectionLabel.Text = (highestSection + 1).ToString();

                lowestScoreLabel.Text = lowestScore.ToString("n2"); // Displaying lowest score,
                lowestSectionLabel.Text = (lowestSection + 1).ToString();


            }
            catch (Exception ex) // Catching any incompetence.
            {
                MessageBox.Show(ex.Message); // Displaying any errors.
            }
        }

        private void calculateSectionAverage(int section) // Method to calculate the section average, that has an argument for the section to calculate.
        {
            Label[] sectionAverageLabels = { section1AverageLabel, section2AverageLabel, section3AverageLabel }; // List of average labels for easy access.

            double totalScore = 0.00;
            foreach (var score in SECTIONS[section]) // Looping through each score
            {
                totalScore += score; // Adding each score to the total for the section.
            }

            double averageScore = totalScore / SECTIONS[section].Length; // Calculating the score.

            sectionAverageLabels[section].Text = averageScore.ToString("n2"); // Displaying the score.
        }

        private void calculateAverage() // Method to calculate the overall vaerage.
        {
            double averageScoreSec1 = double.Parse(section1AverageLabel.Text); // Getting each score average from each section.
            double averageScoreSec2 = double.Parse(section2AverageLabel.Text);
            double averageScoreSec3 = double.Parse(section3AverageLabel.Text);

            double average = (averageScoreSec1 + averageScoreSec2 + averageScoreSec3) / 3; // Calculating the overall average.

            averageLabel.Text = average.ToString("n2"); // Diplaying the average.
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}