﻿using System;
using System.Linq;
using System.Windows.Forms;

namespace Final_Project
{
    public partial class HealthMetricsForm : Form
    {
        private readonly User loggedInUser;

        public HealthMetricsForm(User user)
        {
            InitializeComponent();
            loggedInUser = user;
            FormStyling.ApplyFormStyling(this);
            FormStyling.ApplyButtonStyling(btnSave);
            FormStyling.ApplyButtonStyling(btnBack);
            LoadPetNames(); // Populate ComboBox with logged-in user's pets
        }

        private void LoadPetNames()
        {
            cmbPetNames.Items.Clear();
            // Add only the pets of the logged-in user
            foreach (var pet in loggedInUser.Pets)
            {
                cmbPetNames.Items.Add(pet.Name);
            }
        }

        private void cmbPetNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedPetName = cmbPetNames.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedPetName))
            {
                ClearHealthMetricsFields();
                return;
            }

            // Get the selected pet
            var pet = loggedInUser.Pets.FirstOrDefault(p => p.Name == selectedPetName);

            if (pet != null)
            {
                // Populate the health metrics fields
                txtWeight.Text = pet.Weight.ToString();
                txtHeight.Text = pet.Height.ToString();
                txtAge.Text = pet.Age.ToString();
            }
        }

        private void ClearHealthMetricsFields()
        {
            // Clear all textboxes when no pet is selected
            txtWeight.Clear();
            txtHeight.Clear();
            txtAge.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var selectedPetName = cmbPetNames.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedPetName))
            {
                MessageBox.Show("Please select a pet to update the health metrics.");
                return;
            }

            // Get the selected pet
            var pet = loggedInUser.Pets.FirstOrDefault(p => p.Name == selectedPetName);

            if (pet != null)
            {
                // Validate and update the health metrics fields
                if (double.TryParse(txtWeight.Text, out double weight))
                    pet.Weight = weight;

                if (double.TryParse(txtHeight.Text, out double height))
                    pet.Height = height;

                if (int.TryParse(txtAge.Text, out int age))
                    pet.Age = age;

                MessageBox.Show($"Health metrics updated for {pet.Name}.");
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
