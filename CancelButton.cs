using System.Drawing;
using System.Windows.Forms;

namespace BooksManagementApp {

    internal class CancelButton : Button {

        public CancelButton() {
            Text = "Cancel";
            BackColor = Color.Orange;
            ForeColor = Color.WhiteSmoke;
            Font = new Font("Segoe UI Variable", 9, FontStyle.Bold);
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
        }
    }
}