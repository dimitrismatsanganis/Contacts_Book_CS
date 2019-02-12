using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Contact_Book
{
    public partial class Contacts : Form
    {
        Person person;
        List<Person> people = new List<Person>();
        public Contacts()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {   
            string line;
            StreamReader sr = new StreamReader("Contacts.txt");
            {
                while ((line = sr.ReadLine()) != null)
                {
                    string[] information = line.Split(',');               
                    person = new Person()
                         .addName(information[0])
                         .addSurname(information[1])
                         .addPhone(information[2])
                         .addEmail(information[3])
                         .addAddress(information[4])
                         .addBirthdate(DateTime.Parse(information[5]));
                    people.Add(person);
                    ListViewItem item = new ListViewItem(person.Name);
                    item.SubItems.Add(person.Surname);
                    item.SubItems.Add(person.Phone);
                    item.SubItems.Add(person.Email);
                    item.SubItems.Add(person.Address);
                    item.SubItems.Add(person.Birthdate.ToShortDateString());
                    listView1.Items.Add(item);
                }
            }
            sr.Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                nametxt.Text = people[listView1.SelectedItems[0].Index].Name;
                surnametxt.Text = people[listView1.SelectedItems[0].Index].Surname;
                phonetxt.Text = people[listView1.SelectedItems[0].Index].Phone;
                emailtxt.Text = people[listView1.SelectedItems[0].Index].Email;
                addresstxt.Text = people[listView1.SelectedItems[0].Index].Address;
                birth_date.Value = people[listView1.SelectedItems[0].Index].Birthdate;
            }
            catch
            {
                //Do nothing.
            }

        }
        private void addbtn_Click(object sender, EventArgs e)  //Add Button.
        {
            if (nametxt.Text == "" || surnametxt.Text == "" || phonetxt.Text == "" || emailtxt.Text == "" || addresstxt.Text == "")
            {
                MessageBox.Show("All fields are required.");
            }
            else if (phonetxt.Text.Length < 10 && System.Text.RegularExpressions.Regex.IsMatch(phonetxt.Text, "[^0-9]")) //Phone Number Validation.
            {
                MessageBox.Show("Please enter a valid phone number.", "Invalid Phone Number");
            }
            else if (!this.emailtxt.Text.Contains('@') || !this.emailtxt.Text.Contains('.'))  //Email Validation.
            {
                MessageBox.Show("Please enter a valid email address.", "Invalid Email");
                emailtxt.Clear();
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(nametxt.Text, "^[a-zA-Z ]"))  //Name is valid only if contains alphabetical characters.
            {
                MessageBox.Show("Name field accepts only alphabetical characters.");
                nametxt.Clear();
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(surnametxt.Text, "^[a-zA-Z ]"))  //Surname is valid only if contains alphabetical characters.
            {
                MessageBox.Show("Surname field accepts only alphabetical characters.");
                surnametxt.Clear();
            }
            else
            {
                person = new Person()
               .addName(nametxt.Text)
               .addSurname(surnametxt.Text)
               .addPhone(phonetxt.Text)
               .addEmail(emailtxt.Text)
               .addAddress(addresstxt.Text)
               .addBirthdate(birth_date.Value);
                people.Add(person);
                ListViewItem item = new ListViewItem(person.Name);
                item.SubItems.Add(person.Surname);
                item.SubItems.Add(person.Phone);
                item.SubItems.Add(person.Email);
                item.SubItems.Add(person.Address);
                item.SubItems.Add(person.Birthdate.ToShortDateString());
                listView1.Items.Add(item);
                nametxt.Clear();
                surnametxt.Clear();
                phonetxt.Clear();
                emailtxt.Clear();
                addresstxt.Clear();
                StreamWriter sw = new StreamWriter("Contacts.txt", true);
                {
                    sw.WriteLine(person.Name + "," + person.Surname + "," + person.Phone + "," + person.Email + "," + person.Address + "," + person.Birthdate);
                }
                sw.Close();
                MessageBox.Show("Contact has been added successfully.");

            }

        }

        private void savebtn_Click(object sender, EventArgs e) //Save Button.
        {
            try
            {
                people[listView1.SelectedItems[0].Index].Name = nametxt.Text;
                people[listView1.SelectedItems[0].Index].Surname = surnametxt.Text;
                people[listView1.SelectedItems[0].Index].Phone = phonetxt.Text;
                people[listView1.SelectedItems[0].Index].Email = emailtxt.Text;
                people[listView1.SelectedItems[0].Index].Address = addresstxt.Text;
                people[listView1.SelectedItems[0].Index].Birthdate = birth_date.Value;

                if (nametxt.Text == "" || surnametxt.Text == "" || phonetxt.Text == "" || emailtxt.Text == "" || addresstxt.Text == "")
                {
                    MessageBox.Show("All fields are required.");
                }
                else if (phonetxt.Text.Length < 10 && phonetxt.Text.Length != 0 && System.Text.RegularExpressions.Regex.IsMatch(phonetxt.Text, "[^0-9]")) //Phone Number Validation.
                {
                    MessageBox.Show("Please enter a valid phone number.", "Invalid Phone Number");
                }
                else if (!this.emailtxt.Text.Contains('@') || !this.emailtxt.Text.Contains('.'))  //Email Validation.
                {
                    MessageBox.Show("Please enter a valid email address.", "Invalid Email");
                    emailtxt.Clear();
                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(nametxt.Text, "^[a-zA-Z ]"))  //Name is valid only if contains alphabetical characters.
                {
                    MessageBox.Show("Name field accepts only alphabetical characters.");
                    nametxt.Clear();
                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(surnametxt.Text, "^[a-zA-Z ]"))  //Surname is valid only if contains alphabetical characters.
                {
                    MessageBox.Show("Surname field accepts only alphabetical characters.");
                    surnametxt.Clear();
                }
                else
                {
                    listView1.SelectedItems[0].SubItems[0].Text = nametxt.Text;
                    listView1.SelectedItems[0].SubItems[1].Text = surnametxt.Text;
                    listView1.SelectedItems[0].SubItems[2].Text = phonetxt.Text;
                    listView1.SelectedItems[0].SubItems[3].Text = emailtxt.Text;
                    listView1.SelectedItems[0].SubItems[4].Text = addresstxt.Text;
                    listView1.SelectedItems[0].SubItems[5].Text = birth_date.Value.ToShortDateString();

                    File.WriteAllText("Contacts.txt", String.Empty);
                    using (StreamWriter sw = new StreamWriter("Contacts.txt", true))
                    {
                        foreach (Person p in people)
                        {
                            sw.WriteLine(p.Name + "," + p.Surname + "," + p.Phone + "," + p.Email + "," + p.Address + "," + p.Birthdate);
                        }
                    }
                    MessageBox.Show("Changes has been saved successfully.");
                }
            }     
            catch 
            {
                MessageBox.Show("Please select the contact you want to edit first.");
            }
        
        }

        private void removebtn_Click(object sender, EventArgs e) //Remove Button.
        {
            try
            { 
                listView1.SelectedItems[0].Remove();
                people.RemoveAt(listView1.SelectedItems[0].Index);
                nametxt.Clear();
                surnametxt.Clear();
                phonetxt.Clear();
                emailtxt.Clear();
                addresstxt.Clear();
            }
            catch
            { 
                //Do nothing.
            }

            File.WriteAllText("Contacts.txt", String.Empty);
            StreamWriter sw = new StreamWriter("Contacts.txt", true);

            foreach (ListViewItem lvi in listView1.Items)
            {
                sw.WriteLine(lvi.SubItems[0].Text + "," + lvi.SubItems[1].Text + "," + lvi.SubItems[2].Text + "," + lvi.SubItems[3].Text + "," + lvi.SubItems[4].Text + "," + lvi.SubItems[5].Text);

            }
            sw.Close();
        }   

        private void searchbtn_Click(object sender, EventArgs e) //Search Button.
        {
            foreach (ListViewItem lvi in listView1.Items)
            {
                if (lvi.SubItems[0].Text == searchtxt.Text || lvi.SubItems[1].Text == searchtxt.Text || lvi.SubItems[2].Text == searchtxt.Text)
                {
                    MessageBox.Show("Name : " + lvi.SubItems[0].Text + Environment.NewLine + "Surname : " + lvi.SubItems[1].Text + Environment.NewLine + "Phone Number : " + lvi.SubItems[2].Text + Environment.NewLine + "E-mail : " + lvi.SubItems[3].Text + Environment.NewLine + "Address : " + lvi.SubItems[4].Text + Environment.NewLine + "Birth Date : " + lvi.SubItems[5].Text);
                    lvi.Selected = true;
                    lvi.Focused = true;                    
                }
            }
        }
    }
}
