using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TelegramBot.Data.Account;
using TelegramBot.IO;


namespace TelegramBot.Data.SQL
{
    public class MySqlDatabase
    {
        readonly object _start = new object();

        public MySqlDatabase(string connectionString)
        {
            if (MySqlConnection == null)
            {
                MySqlConnection = new SqlConnection(connectionString);
            }
        }

        public SqlConnection MySqlConnection { get; set; }

      
        public  bool GetCountTayga(out int count)
        {
            var args = new[]
            {
                new SqlParameter("Count", SqlDbType.Int) {Direction = ParameterDirection.Output}
            };
            var result = CallFunction("GetCountTayga", args);
            if ((args[0].Value == DBNull.Value) || (args[0].Value == null))
            {
                count = 0;
                return false;
            }
            count = (int) args[0].Value;
            return true;
        }
        public  bool GetPay(long id, out int count)
        {
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value=id},
                new SqlParameter("Count", SqlDbType.Int) {Direction = ParameterDirection.Output}
            };
            var result = CallFunction("GetPay", args);
            if ((args[1].Value == DBNull.Value) || (args[1].Value == null))
            {
                count = 0;
                return false;
            }
            count = (int)args[1].Value;
            return true;
        }

        public  bool GetIdByTelegramId(long telegramId, out List<Representative> list)
        {
            list = new List<Representative>();
            var args = new[]
            {
                new SqlParameter("TelegramID", SqlDbType.BigInt) {Value = telegramId}
            };
            var dataReader = CallFunctionReader("GetIdByTelegramId", args);
            if (dataReader == null) return false;
            lock (dataReader)
            {
                while (dataReader.Read())
                {
                    list.Add(new Representative(
                            (long) dataReader["Id"], 
                        $"{(string)dataReader["Name"]}",
                        $"{(string)dataReader["Surename"]}", 
                        $"{(string)dataReader["Patronymic"]}",
                        $"{(string)dataReader["Number"]}"));
                }
                dataReader.Close();
            }
            return true;
        }
        public  bool GetListIdActive( out List<long> list)
        {
            list = new List<long>();
            SqlParameter[] args ={};
            var dataReader = CallFunctionReader("GetListIdActive", args);
            if (dataReader == null) return false;
            lock (dataReader)
            {
                while (dataReader.Read())
                {
                    list.Add((long)dataReader["Id"]);
                }
                dataReader.Close();
            }
            return true;
        }

        public bool GetListPay(out Dictionary<long, int> list)
        {
            list = new Dictionary<long, int>();
            SqlParameter[] args = { };
            var dataReader = CallFunctionReader("GetListPay", args);
            if (dataReader == null) return false;
            lock (dataReader)
            {
                while (dataReader.Read())
                {
                    list.Add((long)dataReader["Id"], (int)dataReader["Pay"]);
                }
                dataReader.Close();
            }
            return true;
        }
        
        public  bool GetTelegramId(long id, out long telegramId)
        {
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = id},
                new SqlParameter("TelegramID", SqlDbType.BigInt) {Direction = ParameterDirection.Output}
            };
            var result = CallFunction("GetTelegramId", args);
            if ((args[1].Value == DBNull.Value) || (args[1].Value == null))
            {
                telegramId = 0;
                return false;
            }
            telegramId = (long) args[1].Value;
            return result;
        }
        public  bool GetTaygaIdByTelegram(long telegramId, out long id)
        {
            var args = new[]
            {
                new SqlParameter("TelegramId", SqlDbType.BigInt) {Value = telegramId},
                new SqlParameter("Id", SqlDbType.BigInt) {Direction = ParameterDirection.Output}
            };
            var result = CallFunction("GetTaygaIdByTelegram", args);
            if ((args[1].Value == DBNull.Value) || (args[1].Value == null))
            {
                id = 0;
                return false;
            }
            id = (long)args[1].Value;
            return result;
        }

        public bool GetTempTaygaMobile(long telegramId, out string mobile)
        {
            var args = new[]
            {
                new SqlParameter("TelegramId", SqlDbType.BigInt) {Value = telegramId},
                new SqlParameter("Mobile", SqlDbType.VarChar, 50) {Direction = ParameterDirection.Output}
            };
            var result = CallFunction("GetTempTaygaMobile", args);
            if ((args[1].Value == DBNull.Value) || (args[1].Value == null))
            {
                mobile = "";
                return false;
            }
            mobile = (string)args[1].Value;
            return result;
        }

        public bool GetTempTaygaStep(long telegramId, out int step)
        {
            var args = new[]
            {
                new SqlParameter("TelegramId", SqlDbType.BigInt) {Value = telegramId},
                new SqlParameter("Step", SqlDbType.Int) {Direction = ParameterDirection.Output}
            };
            var result = CallFunction("GetTempTaygaStep", args);
            if ((args[1].Value == DBNull.Value) || (args[1].Value == null))
            {
                step = 0;
                return false;
            }
            step = (int)args[1].Value;
            return result;
        }
        public bool GetFromReferal(long telegramId, out long taygaid, out bool rocket)
        {
            var args = new[]
            {
                new SqlParameter("TelegramId", SqlDbType.BigInt) {Value = telegramId},
                new SqlParameter("Id", SqlDbType.BigInt) {Direction = ParameterDirection.Output},
                new SqlParameter("Rocket", SqlDbType.Bit) {Direction = ParameterDirection.Output}
            };
            var result = CallFunction("GetFromReferal", args);
            if ((args[1].Value == DBNull.Value) || (args[1].Value == null))
            {
                taygaid = 0;
                rocket = false;
                return false;
            }
            taygaid = (long)args[1].Value;
            rocket =  (bool) args[2].Value;
            return result;
        }
        public  bool InsertTayga(long id, string name, string surename, string patronymic, string number)
        {
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = id},
                new SqlParameter("Name", SqlDbType.VarChar, 50) {Value = name},
                new SqlParameter("Surename", SqlDbType.VarChar, 50) {Value = surename},
                new SqlParameter("Patronymic", SqlDbType.VarChar, 50) {Value = patronymic},
                new SqlParameter("Number", SqlDbType.VarChar, 50) {Value = number},
            };
            bool result = CallFunction("InsertTayga", args);
            return result;
        }
        public  bool InsertTelegram(long telegramId, long taygaId)
        {
            var args = new[]
            {
                new SqlParameter("TelegramId", SqlDbType.BigInt) {Value = telegramId},
                new SqlParameter("TaygaId", SqlDbType.BigInt) {Value = taygaId}
            };
            bool result = CallFunction("InsertTelegram", args);
            return result;
        }

        public bool InsertTempTayga(long telegramId)
        {
            var args = new[]
            {
                new SqlParameter("TelegramId", SqlDbType.BigInt) {Value = telegramId}
            };
            bool result = CallFunction("InsertTempTayga", args);
            return result;
        }
        public  bool InsertFromReferal(long telegramId, long taygaId, bool rocket)
        {
            var args = new[]
            {
                new SqlParameter("TelegramId", SqlDbType.BigInt) {Value = telegramId},
                new SqlParameter("TaygaId", SqlDbType.BigInt) {Value = taygaId},
                new SqlParameter("Rocket", SqlDbType.Bit) {Value = rocket}
            };
            bool result = CallFunction("InsertFromReferal", args);
            return result;
        }

        public bool UpdateFromReferal(long telegramId, long taygaId)
        {
            var args = new[]
            {
                new SqlParameter("TelegramId", SqlDbType.BigInt) {Value = telegramId},
                new SqlParameter("TaygaId", SqlDbType.BigInt) {Value = taygaId}
            };
            bool result = CallFunction("UpdateFromReferal", args);
            return result;
        }
        public bool UpdateFromReferalRocket(long telegramId, bool rocket)
        {
            var args = new[]
            {
                new SqlParameter("TelegramId", SqlDbType.BigInt) {Value = telegramId},
                new SqlParameter("Rocket", SqlDbType.Bit) {Value = rocket}
            };
            bool result = CallFunction("UpdateFromReferalRocket", args);
            return result;
        }

        public  bool IsBlocked(long id)
        {
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = id},
                new SqlParameter("Block", SqlDbType.Bit) {Direction = ParameterDirection.Output}
            };
            var result = CallFunction("IsBlocked", args);
            return (args[1].Value != DBNull.Value) && (bool) args[1].Value;
        }

        public  bool IsPresentTayga(long id)
        {
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = id},
                new SqlParameter("Present", SqlDbType.Bit) {Direction = ParameterDirection.Output}
            };
            var result = CallFunction("IsPresentTayga", args);
            return (args[1].Value != DBNull.Value) && (bool) args[1].Value;
        }

        public bool IsPresentTelegram(long id)
        {
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = id},
                new SqlParameter("Present", SqlDbType.Bit) {Direction = ParameterDirection.Output}
            };
            var result = CallFunction("IsPresentTelegram", args);
            return (args[1].Value != DBNull.Value) && (bool) args[1].Value;
        }

        public bool IsPresentTempTelegram(long id)
        {
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = id},
                new SqlParameter("Present", SqlDbType.Bit) {Direction = ParameterDirection.Output}
            };
            var result = CallFunction("IsPresentTempTelegram", args);
            return (args[1].Value != DBNull.Value) && (bool) args[1].Value;
        }

        public  bool IsStart(long id)
        {
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = id},
                new SqlParameter("Start", SqlDbType.Bit) {Direction = ParameterDirection.Output}
            };
            var result = CallFunction("IsStart", args);
            return (args[1].Value != DBNull.Value) && (bool) args[1].Value;
        }

        public bool IsActivity(long id)
        {
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = id},
                new SqlParameter("Activity", SqlDbType.Bit) {Direction = ParameterDirection.Output}
            };
            var result = CallFunction("IsActivity", args);
            return (args[1].Value != DBNull.Value) && (bool)args[1].Value;
        }

        public bool UpdateActivity(long id, bool block)
        {
            var args = new[]
            {
                new SqlParameter("Id", SqlDbType.BigInt) {Value = id},
                new SqlParameter("Activity", SqlDbType.Int) {Value = block}
            };
            bool result = CallFunction("UpdateActivity", args);
            return result;
        }
   
        public  bool UpdateStatus(long uid, bool status)
        {
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = uid},
                new SqlParameter("Status", SqlDbType.Bit) {Value = status}
            };
            return CallFunction("UpdateStatus", args);
        }
        public  bool UpdatePay(long uid, int count)
        {
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = uid},
                new SqlParameter("Pay", SqlDbType.Int) {Value = count}
            };
            return CallFunction("UpdatePay", args);
        }
        public bool UpdateTempTaygaMobile(long telegramId, string mobile)
        {
            var args = new[]
            {
                new SqlParameter("TelegramId", SqlDbType.BigInt) {Value = telegramId},
                new SqlParameter("Mobile", SqlDbType.VarChar, 50) {Value = mobile}
            };
            return CallFunction("UpdateTempTaygaMobile", args);
        }
        public  bool UpdateTempTaygaStep(long telegramId, int step)
        {
            var args = new[]
            {
                new SqlParameter("TelegramId", SqlDbType.BigInt) {Value = telegramId},
                new SqlParameter("Step", SqlDbType.Int) {Value = step}
            };
            return CallFunction("UpdateTempTaygaStep", args);
        }

        public bool CallFunction(string functionName, params SqlParameter[] parameters)
        {
            if (MySqlConnection.State != ConnectionState.Open)
            {
                Connect();
            }
            try
            {
                lock (_start)
                {
                    using (var command = new SqlCommand(functionName, MySqlConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddRange(parameters);
                        return command.ExecuteNonQuery() != 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.OutputEncoding = Encoding.UTF8;
                Log.AddError("Sql " + functionName, ex);
                MySqlConnection.Close();
                MySqlConnection = new SqlConnection(MySqlConnection.ConnectionString);
                Connect();
                return false;
            }
        }

        public SqlDataReader CallFunctionReader(string functionName, params SqlParameter[] parameters)
        {
            if (MySqlConnection.State != ConnectionState.Open)
            {
                Connect();
            }
            try
            {
                lock (_start)
                {
                    using (var command = new SqlCommand(functionName, MySqlConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddRange(parameters);
                        return command.ExecuteReader();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.OutputEncoding = Encoding.UTF8;
                Log.AddError("Sql " + functionName, ex);
                MySqlConnection.Close();
                MySqlConnection = new SqlConnection(MySqlConnection.ConnectionString);
                Connect();
                return null;
            }
        }

        public void Connect()
        {
            if (MySqlConnection.State == ConnectionState.Open)
            {
                return;
            }
            MySqlConnection.Open();
        }
    }
}