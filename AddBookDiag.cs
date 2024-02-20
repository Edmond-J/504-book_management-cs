using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace BooksManagementApp {

    public partial class AddBookDiag : Form {
        private DataGridView booksDataGridView;

        public AddBookDiag(DataGridView booksDataGridView) {
            InitializeComponent();
            this.booksDataGridView = booksDataGridView;
        }

        private void AddBookDiag_Load(object sender, EventArgs e) {
        }

        private void Btn_apply_Click(object sender, EventArgs e) {
            string sqlQuery = "INSERT INTO books (`id`, `isbn`, `title`, `author`, `quantity`) VALUES(@id, @isbn, @title, @author, @quantity)";
            MySqlConnection conn = new MySqlConnection(MainUI.connectionString);
            MySqlCommand command = new MySqlCommand(sqlQuery, conn);
            conn.Open();
            command.Parameters.AddWithValue("@id", txt_id.Text);
            command.Parameters.AddWithValue("@isbn", txt_isbn.Text);
            command.Parameters.AddWithValue("@title", txt_title.Text);
            command.Parameters.AddWithValue("@author", txt_author.Text);
            command.Parameters.AddWithValue("@quantity", txt_quantity.Text);
            command.ExecuteNonQuery();
            MySqlDataAdapter sda = new MySqlDataAdapter("SELECT * FROM books", conn);
            DataTable dt = new DataTable();
            booksDataGridView.DataSource = dt;
            sda.Fill(dt);
            conn.Close();
            this.Close();
        }

        private void CancelButton1_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}