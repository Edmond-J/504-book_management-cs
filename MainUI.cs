using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace BooksManagementApp {

    public partial class MainUI : Form {
        public static string connectionString = "datasource=localhost;port=3306;username=root;password=;database=edmond_book_management;";

        public MainUI() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            FillDataGridView("SELECT * FROM books");
        }

        private void FillDataGridView(String sqlQuery) {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlDataAdapter sda = new MySqlDataAdapter(sqlQuery, conn);
            conn.Open();
            DataTable dt = new DataTable();
            gridView_books.DataSource = dt;
            sda.Fill(dt);
            conn.Close();
        }

        private void Btn_add_Click(object sender, EventArgs e) {
            AddBookDiag addDialog = new AddBookDiag(gridView_books);
            addDialog.ShowDialog();
        }

        private void Btn_edit_Click(object sender, EventArgs e) {
            if (gridView_books.SelectedRows.Count > 0) {
                DataGridViewRow selectedRow = gridView_books.SelectedRows[0];
                int id = int.Parse(selectedRow.Cells["id"].Value.ToString());
                int isbn = int.Parse(selectedRow.Cells["isbn"].Value.ToString());
                string title = selectedRow.Cells["title"].Value.ToString();
                string author = selectedRow.Cells["author"].Value.ToString();
                int quantity = int.Parse(selectedRow.Cells["quantity"].Value.ToString());
                EditBookDiag editDialog = new EditBookDiag(id, isbn, title, author, quantity, gridView_books);
                editDialog.ShowDialog();
            }
        }

        private void Btn_delete_Click(object sender, EventArgs e) {
            if (gridView_books.SelectedRows.Count > 0) {
                DataGridViewRow selectedRow = gridView_books.SelectedRows[0];
                int id = int.Parse(selectedRow.Cells["id"].Value.ToString());
                string title = selectedRow.Cells["title"].Value.ToString();
                DialogResult result = MessageBox.Show($"Are you sure to DELETE the book: {title}？", "Delete Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK) {
                    string sqlQuery = "DELETE FROM books WHERE id = " + id;
                    MySqlConnection conn = new MySqlConnection(connectionString);
                    conn.Open();
                    MySqlCommand command = new MySqlCommand(sqlQuery, conn);
                    command.ExecuteNonQuery();
                    conn.Close();
                    FillDataGridView("SELECT * FROM books");
                }
            }
        }

        private void Btn_search_Click(object sender, EventArgs e) {
            string sqlQuery = "SELECT * FROM books WHERE " + comboBox_searchBy.Text + " LIKE '%" + txt_searchKey.Text + "%'";
            //string sqlQuery = $"SELECT * FROM books WHERE @comboBox_searchBy.Text LIKE '%{txt_searchKey.Text}%'";
            //SELECT * FROM `books` WHERE title = "Pygmalion";
            FillDataGridView(sqlQuery);
        }

        private void Btn_clear_Click(object sender, EventArgs e) {
            txt_searchKey.Text = string.Empty;
            comboBox_searchBy.SelectedItem = null;
            FillDataGridView("SELECT * FROM books");
        }
    }
}