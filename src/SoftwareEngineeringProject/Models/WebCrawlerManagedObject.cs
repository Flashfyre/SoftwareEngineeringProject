using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareEngineeringProject.Models
{
    public abstract class WebCrawlerManagedObject
    {
        public abstract DateTime? LastUpdatedDate { get; set; }
        [NotMapped]
        protected string tableName { get
            {
                return GetType().Name + "s";
            }
        }
        protected virtual string[] ColumnNames { get; }
        protected virtual object[] Values { get; }
        protected virtual int KeyCount {
            get
            {
                return 1;
            }
        }
        protected virtual string IdWhereClause {
            get
            {
                string[] columnNames = ColumnNames;
                object[] values = Values;

                string whereClause = " WHERE ";
                for (int v = 0; v < KeyCount; v++)
                {
                    object value = values[v];
                    string columnName = columnNames[v];
                    whereClause += (v > 0 ? " AND " : "") + columnName + " = " + ((value is int || value is byte) ? value.ToString() : value is DateTime? ? "CAST('" + value.ToString() + "' AS DATETIME2)" : "'" + value.ToString() + "'");
                }

                return whereClause;
            }
        }
        public virtual string IdString {
            get
            {
                string retVal = "";
                object[] values = this.Values;

                for (int k = 0; k < KeyCount; k++)
                {
                    retVal += (k > 0 ? "/" : "") + values[k].ToString();
                }

                return retVal;
            }
        }

        public WebCrawlerManagedObject AddItem(SqlConnection conn)
        {
            LastUpdatedDate = DateTime.Now;

            conn.Open();

            SqlCommand command;
            string commandText = "INSERT INTO " + tableName + "(" + string.Join(", ", ColumnNames) + ") VALUES (";

            for (int v = 0; v < Values.Length; v++)
            {
                object value = Values[v];
                commandText += (v > 0 ? ", " : "") + (value != null ? ((value is int || value is byte) ? value.ToString() : value is DateTime? ? "CAST('" + value.ToString() + "' AS DATETIME2)"
                    : "'" + value.ToString() + "'") : "NULL");
            }

            commandText += ")";

            command = new SqlCommand(commandText, conn);
            command.ExecuteNonQuery();

            conn.Close();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("{0} was successfully added to the database.", ToString());
            Console.ForegroundColor = ConsoleColor.Gray;

            return this;
        }

        public WebCrawlerManagedObject UpdateItem(SqlConnection conn)
        {
            LastUpdatedDate = DateTime.Now;

            conn.Open();

            SqlCommand command;
            string commandText = "UPDATE " + tableName + " SET";

            for (int v = KeyCount; v < Values.Length; v++)
            {
                object value = Values[v];
                commandText += (v > KeyCount ? ", " : "") + " " + ColumnNames[v] + " = " + (value != null ? ((value is int || value is byte) ? value.ToString() : value is DateTime? ? "CAST('" + value.ToString()
                    + "' AS DATETIME2)" : "'" + value.ToString() + "'") : "NULL");
            }

            commandText += IdWhereClause;

            command = new SqlCommand(commandText, conn);
            command.ExecuteNonQuery();

            conn.Close();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("{0} was successfully updated.", ToString());
            Console.ForegroundColor = ConsoleColor.Gray;

            return this;
        }

        public WebCrawlerManagedObject DeleteItem(SqlConnection conn)
        {
            conn.Open();

            SqlCommand command = new SqlCommand("DELETE FROM " + tableName + IdWhereClause, conn);
            command.ExecuteNonQuery();

            conn.Close();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("{0} was successfully deleted from the database.", ToString());
            Console.ForegroundColor = ConsoleColor.Gray;

            return this;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", GetType().Name, IdString);
        }
    }
}
