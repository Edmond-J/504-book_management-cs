using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace BooksManagementApp {

    public partial class EditBookDiag : Form {
        private DataGridView booksDataGridView;

        public EditBookDiag() {
            InitializeComponent();
        }

        public EditBookDiag(int id, int isbn, string title, string author, int quantity, DataGridView booksDataGridView) {
            InitializeComponent();
            this.booksDataGridView = booksDataGridView;
            txt_id.Text = "" + id;
            txt_isbn.Text = "" + isbn;
            txt_title.Text = title;
            txt_author.Text = author;
            txt_quantity.Text = "" + quantity;
        }

        private void Btn_apply_Click(object sender, EventArgs e) {
            string sqlQuery = "UPDATE books SET isbn=@isbn,title=@title,author=@author,quantity=@quantity WHERE id=@id";
            MySqlConnection conn = new MySqlConnection(MainUI.connectionString);
            MySqlCommand command = new MySqlCommand(sqlQuery, conn);
            conn.Open();
            command.Parameters.AddWithValue("@isbn", txt_isbn.Text);
            command.Parameters.AddWithValue("@title", txt_title.Text);
            command.Parameters.AddWithValue("@author", txt_author.Text);
            command.Parameters.AddWithValue("@quantity", txt_quantity.Text);
            command.Parameters.AddWithValue("@id", txt_id.Text);
            command.ExecuteNonQuery();
            MySqlDataAdapter sda = new MySqlDataAdapter("SELECT * FROM books", conn);
            DataTable dt = new DataTable();
            booksDataGridView.DataSource = dt;
            sda.Fill(dt);
            conn.Close();
            this.Close();
        }

        private void Btn_cancel_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void CancelButton1_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void test() {
            int a = 4;
            Console.WriteLine(a);
        }
    }
}