using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dgv_with_date_time
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if(!(DesignMode || _isHandleInitialized))
            {
                _isHandleInitialized = true;
                InitializeDataGridView();
            }
        }

        BindingList<Record> Records = new BindingList<Record>();
        private void InitializeDataGridView()
        {
            dataGridView1.AllowUserToAddRows = false;

            // Bind the list of records to the DataGridView
            dataGridView1.DataSource = Records;

            // Add one or more records to autogenerate the Columns
            for (int i = 0; i < 3; i++) Records.Add(new Record());

            dataGridView1.Columns.Remove(dataGridView1.Columns[nameof(Record.Date)]);
            dataGridView1.Columns.Add(
                // This class is copied from Microsoft example:
                // https://docs.microsoft.com/en-us/dotnet/desktop/winforms/controls/how-to-host-controls-in-windows-forms-datagridview-cells?view=netframeworkdesktop-4.8&redirectedfrom=MSDN
                new CalendarColumn
                {
                    Name = nameof(Record.Date),
                    DataPropertyName = nameof(Record.Date),
                });

            dataGridView1.Columns.Remove(dataGridView1.Columns[nameof(Record.Value)]);
            dataGridView1.Columns.Add(
                new DataGridViewComboBoxColumn
                {
                    Name = nameof(Record.Value),
                    DataPropertyName = nameof(Record.Value),
                    DataSource = Enum.GetValues(typeof(CBValues)),
                });

            // Format columns
            dataGridView1.Columns[nameof(Record.Description)].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[nameof(Record.Date)].Width = 150;
        }

        private bool _isHandleInitialized = false;
    }
    enum CBValues
    {
        Apple,
        Orange,
        Banana,
    }
    
    class Record
    {
        public string Description { get; set; } = $"Item {autoName++}";
        public DateTime Date { get; set; } = DateTime.Now;
        public CBValues Value { get; set; } = CBValues.Apple;

        static char autoName = 'A';
    }
}
