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
        protected virtual string[] columnNames { get; }
        protected virtual object[] values { get; }
        protected virtual int keyCount {
            get
            {
                return 1;
            }
        }
        protected virtual string IdWhereClause {
            get
            {
                string[] columnNames = this.columnNames;
                object[] values = this.values;

                string whereClause = " WHERE ";
                for (int v = 0; v < keyCount; v++)
                {
                    object value = values[v];
                    string columnName = columnNames[v];
                    whereClause += (v > 0 ? " AND " : "") + columnName + " = " + ((value is int || value is byte) ? value.ToString() : value is DateTime? ? "CAST('" + value.ToString() + "' AS DATETIME2)" : "'" + value.ToString() + "'");
                }

                return whereClause;
            }
        }
        protected virtual string IdString {
            get
            {
                string retVal = "";
                object[] values = this.values;

                for (int k = 0; k < keyCount; k++)
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
            string commandText = "INSERT INTO " + tableName + "(" + string.Join(", ", columnNames) + ") VALUES (";

            for (int v = 0; v < values.Length; v++)
            {
                object value = values[v];
                commandText += (v > 0 ? ", " : "") + ((value is int || value is byte) ? value.ToString() : value is DateTime? ? "CAST('" + value.ToString() + "' AS DATETIME2)" : "'" + value.ToString() + "'");
            }

            commandText += ")";

            command = new SqlCommand(commandText, conn);
            command.ExecuteNonQuery();

            conn.Close();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("{0} was successfully added to the database.", ToString());
            Console.ForegroundColor = ConsoleColor.White;

            return this;
        }

        public WebCrawlerManagedObject UpdateItem(SqlConnection conn)
        {
            LastUpdatedDate = DateTime.Now;

            conn.Open();

            SqlCommand command = new SqlCommand("UPDATE " + tableName + " SET LastUpdatedDate = CAST('" + LastUpdatedDate.Value.ToString() + "' AS DATETIME2)" + IdWhereClause, conn);
            command.ExecuteNonQuery();

            conn.Close();

            return this;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", GetType().Name, IdString);
        }
    }
}
